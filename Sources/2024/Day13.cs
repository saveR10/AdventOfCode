using AOC;
using AOC.DataStructures.Clustering;
using AOC.Model;
using AOC.SearchAlghoritmhs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AOC2024
{
    [ResearchAlghoritms(ResearchAlghoritmsAttribute.TypologyEnum.SystemLinearEquations)] //Equazioni Diofantee 𝑎⋅𝑥+𝑏⋅𝑦=𝑐, Metodo di Cramer
    public class Day13 : Solver, IDay
    {
        List<Machine> Machines = new List<Machine>();
        List<int> machine;
        public void Part1(object input, bool test, ref object solution)
        {
            string inputText = (string)input;
            int m = 0;
            foreach (string instruction in inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None))
            {
                if (!string.IsNullOrEmpty(instruction))
                {
                    if (machine == null) machine = new List<int>();
                    if(instruction.StartsWith("Button A"))
                    {
                        var x = Regex.Split(instruction, @"X\+")[1].Split(',')[0];
                        var y = Regex.Split(instruction, @"X\+\d+")[1].Split(',')[1].Split('+')[1];
                        machine.Add(int.Parse(x));
                        machine.Add(int.Parse(y));
                    }
                    else if(instruction.StartsWith("Button B"))
                    {
                        var x = Regex.Split(instruction, @"X\+")[1].Split(',')[0];
                        var y = Regex.Split(instruction, @"X\+\d+")[1].Split(',')[1].Split('+')[1];
                        machine.Add(int.Parse(x));
                        machine.Add(int.Parse(y));
                    }
                    else if (instruction.StartsWith("Prize"))
                    {
                        var x = Regex.Split(instruction, @"X\=")[1].Split(',')[0];
                        var y = Regex.Split(instruction, @"X\=")[1].Split(',')[1].Split('=')[1];
                        machine.Add(int.Parse(x));
                        machine.Add(int.Parse(y));
                    }
                }
                else
                {
                    if (machine != null)
                    {
                        Machines.Add(new Machine(machine[0], machine[1], machine[2], machine[3], machine[4], machine[5]));
                    }
                    machine = null;
                    m++;
                }
            }
            if (machine != null)
            {
                Machines.Add(new Machine(machine[0], machine[1], machine[2], machine[3], machine[4], machine[5]));
            }
            machine = null;

            var result = SolveClawMachines(Machines);
            Console.WriteLine($"Prizes Won: {result.prizesWon}, Total Cost: {result.totalCost}");
            solution = result.totalCost;
        }

        public static (int prizesWon, int totalCost) SolveClawMachines(List<Machine> machines)
        {
            int totalCost = 0;
            int prizesWon = 0;

            foreach (var machine in machines)
            {
                long A_x = machine.A_x;
                long A_y = machine.A_y;
                long B_x = machine.B_x;
                long B_y = machine.B_y;
                long P_x = machine.P_x;
                long P_y = machine.P_y;

                int minCost = int.MaxValue;
                bool prizeWon = false;

                // Iterazione per trovare la combinazione di n_A e n_B
                for (int n_A = 0; n_A <= 100; n_A++)
                {
                    for (int n_B = 0; n_B <= 100; n_B++)
                    {
                        if (n_A * A_x + n_B * B_x == P_x && n_A * A_y + n_B * B_y == P_y)
                        {
                            int cost = 3 * n_A + n_B;
                            if (cost < minCost)
                            {
                                minCost = cost;
                                prizeWon = true;
                            }
                        }
                    }
                }

                if (prizeWon)
                {
                    prizesWon++;
                    totalCost += minCost;
                }
            }

            return (prizesWon, totalCost);
        }
        public class Machine
        {
            public long A_x, A_y, B_x, B_y, P_x, P_y;

            public Machine(long a_x, long a_y, long b_x, long b_y, long p_x, long p_y)
            {
                A_x = a_x;
                A_y = a_y;
                B_x = b_x;
                B_y = b_y;
                P_x = p_x;
                P_y = p_y;
            }
        }


        public void Part2(object input, bool test, ref object solution)
        {
            string inputText = (string)input;
            int m = 0;
            foreach (string instruction in inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None))
            {
                if (!string.IsNullOrEmpty(instruction))
                {
                    if (machine == null) machine = new List<int>();
                    if (instruction.StartsWith("Button A"))
                    {
                        var x = Regex.Split(instruction, @"X\+")[1].Split(',')[0];
                        var y = Regex.Split(instruction, @"X\+\d+")[1].Split(',')[1].Split('+')[1];
                        machine.Add(int.Parse(x));
                        machine.Add(int.Parse(y));
                    }
                    else if (instruction.StartsWith("Button B"))
                    {
                        var x = Regex.Split(instruction, @"X\+")[1].Split(',')[0];
                        var y = Regex.Split(instruction, @"X\+\d+")[1].Split(',')[1].Split('+')[1];
                        machine.Add(int.Parse(x));
                        machine.Add(int.Parse(y));
                    }
                    else if (instruction.StartsWith("Prize"))
                    {
                        var x = Regex.Split(instruction, @"X\=")[1].Split(',')[0];
                        var y = Regex.Split(instruction, @"X\=")[1].Split(',')[1].Split('=')[1];
                        machine.Add(int.Parse(x));
                        machine.Add(int.Parse(y));
                    }
                }
                else
                {
                    if (machine != null)
                    {
                        Machines.Add(new Machine(machine[0], machine[1], machine[2], machine[3], machine[4] + (long)10000000000000, machine[5] + (long)10000000000000));
                    }
                    machine = null;
                    m++;
                }
            }
            if (machine != null)
            {
                Machines.Add(new Machine(machine[0], machine[1], machine[2], machine[3], machine[4]+ (long)10000000000000, machine[5]+(long)10000000000000));
            }
            machine = null;

            var result = SolveClawMachinesHigher(Machines);
            solution = result;
        }

        public static long SolveClawMachinesHigher(List<Machine> machines)
        {
            long totalCost = 0;

            foreach (var machine in machines)
            {
                double determinant = machine.A_x * machine.B_y - machine.A_y * machine.B_x;

                // Verifichiamo che il determinante sia diverso da zero (sistema ha una sola soluzione)
                if (determinant == 0)
                    continue;

                double x = (machine.P_x * machine.B_y - machine.P_y * machine.B_x) / determinant;
                double y = (machine.A_x * machine.P_y - machine.P_x * machine.A_y) / determinant;

                // Verifichiamo che le soluzioni siano intere e non negative
                if (x % 1 == 0 && y % 1 == 0 && x >= 0 && y >= 0)
                {
                    totalCost += (long)(3 * x + y);
                }
            }

            return totalCost;
        }
        // Metodo per risolvere un'equazione diofantea a * x + b * y = c
        public static bool SolveDiophantine(long a, long b, long c, out long x, out long y)
        {
            // Passaggio 1: Calcola il GCD di a e b e ottieni i coefficienti di Bézout (x0, y0)
            long gcd = GCD(a, b, out long x0, out long y0);
            
            // Passaggio 2: Verifica se c è divisibile per il GCD; se no, nessuna soluzione esiste
            if (c % gcd != 0)
            {
                x = y = 0;
                return false;
            }

            // Passaggio 3: Scala la soluzione particolare trovata per adattarla a c
            // Dato che GCD(a, b) = a * x0 + b * y0, moltiplicando per (c / gcd), otteniamo:
            // a * (x0 * scale) + b * (y0 * scale) = c
            long scale = c / gcd;
            x = x0 * scale;
            y = y0 * scale;
            //Soluzione particolare iniziale: x=20000000016800, y=-85000000071400
            //Verifica che x e y della soluzione particolare soddisfino l'equazione originale
            if (a * x + b * y != c)
                return false;

            //Soluzione generale dell'equazione: x = x0 + k*(b/GCD) = 20000000016800 + k(22/2) = 20000000016800 + 11*k 
            //                                   y = y0 - k*(a/GCD) =-85000000071400 - k(94/2) =-85000000071400 - 47*k 


            // Passaggio 4: Aggiusta la soluzione per avere termini non negativi (se necessario)
            // Le soluzioni generali sono x = x_p + k * (b / gcd), y = y_p - k * (a / gcd)
            long bDivG = b / gcd; // Passo per x
            long aDivG = a / gcd; // Passo per y

            // Calcola il minimo k per rendere x >= 0 e y >= 0 contemporaneamente
            long k_min_x = x < 0 ? (long)Math.Ceiling((double)-x / bDivG) : 0;
            long k_min_y = y < 0 ? (long)Math.Ceiling((double)-y / aDivG) : 0;

            // Determina il massimo tra i due valori. 
            long k_min = Math.Max(k_min_x, k_min_y);
            //Applichiamo k_min per trovare la soluzione generale: : x = x + k_min*(b/GCD) = 20000000016800 + 1808510639818*11
            //                                                       y = y - k_min*(a/GCD) =-85000000071400 - 1808510639818*47

            // Aggiorna x e y usando k_min
            x += k_min * bDivG;
            y -= k_min * aDivG;

            if (a * x + b * y == c)
            {
                // La soluzione è valida
                return true;
            }
            else
            {
                // La soluzione non è valida
                return false;
            }
        }
        // Extended Euclidean Algorithm to compute GCD and coefficients
        public static long GCD(long a, long b, out long x, out long y)
        {
            if (b == 0)
            {
                x = 1;
                y = 0;
                return a;
            }

            long gcd = GCD(b, a % b, out long x1, out long y1);
            x = y1;
            y = x1 - (a / b) * y1;
            return gcd;
        }

        // Calculate the Least Common Multiple
        public static long LCM(long a, long b)
        {
            return Math.Abs(a * b) / GCD(a, b, out _, out _);
        }

    }
}
