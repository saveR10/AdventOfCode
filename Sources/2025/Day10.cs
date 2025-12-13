using AOC;
using AOC.DataStructures.Clustering;
using AOC.Model;
using AOC.SearchAlghoritmhs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using static AOC.SearchAlghoritmhs.ResearchAlgorithmsAttribute;
using System.Linq;   // <- importante
using Microsoft.Z3;


namespace AOC2025
{
    [ResearchAlgorithmsAttribute(ResolutionEnum.SystemLinearEquations)]
    [ResearchAlgorithmsAttribute(ResolutionEnum.SMTSolver)]
    [ResearchAlgorithmsAttribute(TypologyEnum.Overflow)]
    [ResearchAlgorithmsAttribute(TypologyEnum.BinaryOperation)]
    [ResearchAlgorithmsAttribute(TypologyEnum.MachineInstructions)]
    [ResearchAlgorithmsAttribute(TypologyEnum.Combinatorial)]
    [ResearchAlgorithmsAttribute(DifficultEnum.VeryHard)]
    public class Day10 : AOC.Solver, IDay
    {
        public void Part1(object input, bool test, ref object solution)
        {
            string inputText = (string)input;
            var lines = inputText.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            long totalPresses = 0;

            foreach (var line in lines)
            {
                ParseMachine(line, out string pattern, out List<int[]> buttons);

                int L = pattern.Length;       // numero luci
                int M = buttons.Count;        // numero bottoni

                // Costruisco A come array di bitmask (righe)
                // Ogni riga è un array di ulong per rappresentare M bit
                int words = (M + 63) / 64;
                ulong[][] A = new ulong[L][];
                for (int i = 0; i < L; i++)
                    A[i] = new ulong[words];

                // Riempi matrice
                for (int j = 0; j < M; j++)
                {
                    foreach (int idx in buttons[j])
                        A[idx][j / 64] |= 1UL << (j % 64);
                }

                // vettore target b
                bool[] b = new bool[L];
                for (int i = 0; i < L; i++)
                    b[i] = pattern[i] == '#';

                // Risolvo sistema A x = b (mod 2)
                var sol = SolveGF2(A, b, M);

                if (!sol.HasSolution)
                {
                    // Nel puzzle non capita, ma gestisco lo stesso
                    continue;
                }

                // Prendo la soluzione minima
                ulong[] best = FindMinWeightSolution(sol);

                // Peso
                int presses = Popcount(best);
                totalPresses += presses;
            }

            solution = totalPresses;
        }

        private void ParseMachine(string line, out string pattern, out List<int[]> buttons)
        {
            // Esempio:
            // [.##.] (3) (1,3) (2) (2,3) (0,2) (0,1) {3,5,4,7}

            int i1 = line.IndexOf('[');
            int i2 = line.IndexOf(']');
            pattern = line.Substring(i1 + 1, i2 - i1 - 1);

            string rest = line.Substring(i2 + 1);

            buttons = new List<int[]>();

            int p = 0;
            while (true)
            {
                int o = rest.IndexOf('(', p);
                if (o < 0) break;
                int c = rest.IndexOf(')', o);
                string inner = rest.Substring(o + 1, c - o - 1).Trim();
                if (inner.Length == 0)
                    buttons.Add(Array.Empty<int>());
                else
                    buttons.Add(inner.Split(',').Select(int.Parse).ToArray());
                p = c + 1;
            }
        }

        private GF2Solution SolveGF2(ulong[][] A, bool[] b, int M)
        {
            int L = A.Length;
            int words = (M + 63) / 64;

            // Copia locale
            ulong[][] mat = A.Select(r => (ulong[])r.Clone()).ToArray();
            bool[] rhs = (bool[])b.Clone();

            int rank = 0;
            int[] pivotCol = new int[L];  // pivotCol[r] = colonna pivot di riga r (o -1)
            Fill(pivotCol, -1);

            // Eliminazione
            for (int col = 0; col < M && rank < L; col++)
            {
                int w = col / 64;
                ulong mask = 1UL << (col % 64);

                int sel = -1;
                for (int r = rank; r < L; r++)
                {
                    if ((mat[r][w] & mask) != 0)
                    {
                        sel = r;
                        break;
                    }
                }
                if (sel < 0) continue;

                // swap
                (mat[rank], mat[sel]) = (mat[sel], mat[rank]);
                (rhs[rank], rhs[sel]) = (rhs[sel], rhs[rank]);

                pivotCol[rank] = col;

                // elimina in tutte le altre righe
                for (int r = 0; r < L; r++)
                {
                    if (r != rank && ((mat[r][w] & mask) != 0))
                    {
                        XORRow(mat[r], mat[rank]);
                        rhs[r] ^= rhs[rank];
                    }
                }

                rank++;
            }

            // Controllo inconsistenza
            for (int r = rank; r < L; r++)
            {
                bool empty = true;
                for (int w = 0; w < words; w++)
                    if (mat[r][w] != 0) { empty = false; break; }

                if (empty && rhs[r])
                    return new GF2Solution { HasSolution = false };
            }

            // Soluzione particolare x0
            ulong[] x0 = new ulong[words];

            // x0[pivotCol[r]] = rhs[r]
            for (int r = 0; r < rank; r++)
            {
                if (rhs[r])
                {
                    int c = pivotCol[r];
                    x0[c / 64] |= 1UL << (c % 64);
                }
            }

            // Kernel: una base vector per ogni colonna libera
            var kernel = new List<ulong[]>();
            for (int col = 0; col < M; col++)
            {
                // se col non è pivot → libera
                if (!pivotCol.Contains(col))
                {
                    ulong[] vec = new ulong[words];
                    vec[col / 64] |= 1UL << (col % 64);

                    // Per ogni pivot riga, calcola bit pivot così che A*vec = 0
                    for (int r = 0; r < rank; r++)
                    {
                        int pc = pivotCol[r];
                        int w = pc / 64;
                        int bit = pc % 64;

                        // calcola dot product row_r · vec
                        bool dp = false;
                        for (int ww = 0; ww < words; ww++)
                        {
                            dp ^= Parity(mat[r][ww] & vec[ww]);
                        }

                        if (dp) // pivot deve essere 1 per annullare
                        {
                            vec[w] ^= 1UL << bit;
                        }
                    }

                    kernel.Add(vec);
                }
            }

            return new GF2Solution
            {
                HasSolution = true,
                Particular = x0,
                Kernel = kernel
            };
        }
        private static void Fill<T>(T[] arr, T value)
        {
            for (int i = 0; i < arr.Length; i++)
                arr[i] = value;
        }
        private class GF2Solution
        {
            public bool HasSolution;
            public ulong[] Particular;       // soluzione x0
            public List<ulong[]> Kernel;     // base del kernel
        }

        private ulong[] FindMinWeightSolution(GF2Solution sol)
        {
            var x0 = sol.Particular;
            var K = sol.Kernel;
            int k = K.Count;
            int words = x0.Length;

            // Caso semplice: nessun vettore libero → x0 è unica soluzione
            if (k == 0)
                return x0;

            int bestWeight = int.MaxValue;
            ulong[] best = null;

            int combos = 1 << k;
            for (int mask = 0; mask < combos; mask++)
            {
                ulong[] x = (ulong[])x0.Clone();

                for (int i = 0; i < k; i++)
                {
                    if (((mask >> i) & 1) != 0)
                    {
                        for (int w = 0; w < words; w++)
                            x[w] ^= K[i][w];
                    }
                }

                int wgt = Popcount(x);
                if (wgt < bestWeight)
                {
                    bestWeight = wgt;
                    best = x;
                }
            }

            return best;
        }

        private int Popcount(ulong[] arr)
        {
            int s = 0;
            foreach (ulong x in arr)
                s += PopcountUlong(x);
            return s;
        }
        private int PopcountUlong(ulong x)
        {
            // SWAR popcount (Hacker's Delight)
            x = x - ((x >> 1) & 0x5555555555555555UL);
            x = (x & 0x3333333333333333UL) + ((x >> 2) & 0x3333333333333333UL);
            x = (x + (x >> 4)) & 0x0F0F0F0F0F0F0F0FUL;
            x = x + (x >> 8);
            x = x + (x >> 16);
            x = x + (x >> 32);
            return (int)(x & 0x7F);
        }
        private void XORRow(ulong[] a, ulong[] b)
        {
            for (int i = 0; i < a.Length; i++)
                a[i] ^= b[i];
        }

        private bool Parity(ulong x)
        {
            // bit parity
            x ^= x >> 32;
            x ^= x >> 16;
            x ^= x >> 8;
            x ^= x >> 4;
            x ^= x >> 2;
            x ^= x >> 1;
            return (x & 1) != 0;
        }




        public void Part2(object input, bool test, ref object solution)
        {
            TestZ3();

            string inputText = (string)input;
            string[] lines = inputText.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            long total = 0;

            foreach (string line in lines)
            {
                ParseMachine(line, out _, out List<int[]> buttons, out int[] target);
                total += MinPressesZ3(buttons, target);
            }

            solution = total;
        }


        private int MinPressesZ3(List<int[]> buttons, int[] target)
        {
            int B = buttons.Count;
            int C = target.Length;

            Context ctx = new Context();
            Optimize opt = ctx.MkOptimize();

            // Variabili x_j >= 0
            IntExpr[] x = new IntExpr[B];
            for (int j = 0; j < B; j++)
            {
                x[j] = ctx.MkIntConst("x" + j);
                opt.Add(ctx.MkGe(x[j], ctx.MkInt(0)));
            }

            // Vincoli sui contatori
            for (int i = 0; i < C; i++)
            {
                List<ArithExpr> terms = new List<ArithExpr>();

                for (int j = 0; j < B; j++)
                {
                    int[] btn = buttons[j];
                    for (int k = 0; k < btn.Length; k++)
                    {
                        if (btn[k] == i)
                        {
                            terms.Add(x[j]);
                            break;
                        }
                    }
                }

                opt.Add(ctx.MkEq(
                    ctx.MkAdd(terms.ToArray()),
                    ctx.MkInt(target[i])
                ));
            }

            // Minimizza somma delle pressioni
            ArithExpr sum = ctx.MkAdd(x);
            opt.MkMinimize(sum);

            if (opt.Check() != Status.SATISFIABLE)
                throw new Exception("Nessuna soluzione");

            Model model = opt.Model;

            int result = 0;
            for (int j = 0; j < B; j++)
            {
                IntNum val = (IntNum)model.Evaluate(x[j], true);
                result += val.Int;
            }

            model.Dispose();
            opt.Dispose();
            ctx.Dispose();

            return result;
        }
        public void TestZ3()
        {
            Context ctx = new Context();
            ctx.Dispose();
        }
        // Parsing della riga della macchina
        private void ParseMachine(string line, out string pattern, out List<int[]> buttons, out int[] target)
        {
            int i1 = line.IndexOf('[');
            int i2 = line.IndexOf(']');
            pattern = line.Substring(i1 + 1, i2 - i1 - 1);

            string rest = line.Substring(i2 + 1);

            buttons = new List<int[]>();
            int p = 0;
            while (true)
            {
                int o = rest.IndexOf('(', p);
                if (o < 0) break;
                int c = rest.IndexOf(')', o);
                string inner = rest.Substring(o + 1, c - o - 1).Trim();
                if (inner.Length == 0)
                    buttons.Add(Array.Empty<int>());
                else
                    buttons.Add(inner.Split(',').Select(int.Parse).ToArray());
                p = c + 1;
            }

            int brace1 = rest.IndexOf('{');
            int brace2 = rest.IndexOf('}');
            string targetText = rest.Substring(brace1 + 1, brace2 - brace1 - 1);
            target = targetText.Split(',').Select(int.Parse).ToArray();
        }

        // DP ricorsiva con memoization
        private int MinPressesDP(List<int[]> buttons, int[] target)
        {
            var memo = new Dictionary<string, int>();
            return Solve(target, buttons, memo);
        }

        private int Solve(int[] remaining, List<int[]> buttons, Dictionary<string, int> memo)
        {
            string key = string.Join(",", remaining);
            if (memo.TryGetValue(key, out int cached))
                return cached;

            if (remaining.All(v => v == 0))
                return 0;

            int minPresses = int.MaxValue;

            foreach (var btn in buttons)
            {
                bool valid = true;
                int[] next = new int[remaining.Length];

                for (int i = 0; i < remaining.Length; i++)
                {
                    next[i] = remaining[i] - (btn.Contains(i) ? 1 : 0);
                    if (next[i] < 0) valid = false;
                }

                if (valid)
                {
                    int presses = 1 + Solve(next, buttons, memo);
                    if (presses < minPresses)
                        minPresses = presses;
                }
            }

            memo[key] = minPresses;
            return minPresses;
        }
    }
}