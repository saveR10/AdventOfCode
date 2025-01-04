using AOC;
using AOC.DataStructures.Clustering;
using AOC.Model;
using AOC.SearchAlghoritmhs;
using System;
using System.Collections.Generic;
using System.Drawing.Design;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AOC2024
{
    [ResearchAlgorithms(ResearchAlgorithmsAttribute.TypologyEnum.BinaryOperation)]
    [ResearchAlgorithms(ResearchAlgorithmsAttribute.TypologyEnum.Gate)]
    public class Day24 : Solver, IDay
    {
        public static Dictionary<string, bool> wire = new Dictionary<string, bool>();
        public static int nPF = 1;

        public void Part1(object input, bool test, ref object solution)
        {
            MinPQGate<Gate> gatePQ = new MinPQGate<Gate>();
            bool connection = false;
            string inputText = (string)input;
            int nTest = 0;
            if (test)
            {
                switch (nTest)
                {
                    case 0:
                        break;
                    case 1:
                        inputText = "x00: 1\r\nx01: 1\r\nx02: 1\r\ny00: 0\r\ny01: 1\r\ny02: 0\r\n\r\nx00 AND y00 -> z00\r\nx01 XOR y01 -> z01\r\nx02 OR y02 -> z02";
                        break;
                }
            }

            foreach (string line in inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None))
            {

                if (!string.IsNullOrEmpty(line))
                {
                    if (connection)
                    {
                        var c = line.Split(Delimiter.delimiter_space, StringSplitOptions.None);
                        var i1 = c[0];
                        var i2 = c[2];
                        var o3 = c[4];
                        var operand = c[1];
                        Gate gate1 = new Gate(i1, i2, o3, operand);
                        gatePQ.Insert(gate1);
                    }
                    else
                    {
                        var w = line.Split(':');
                        wire.Add(w[0], Convert.ToBoolean(int.Parse(w[1])));
                    }
                }
                else
                {
                    connection = true;
                }
            }
            Gate toEvaluate;
            while (!gatePQ.IsEmpty())
            {
                toEvaluate = gatePQ.Min();
                if (toEvaluate.ready)
                {

                    bool a = Convert.ToBoolean(wire[toEvaluate.firstPort]);
                    bool b = Convert.ToBoolean(wire[toEvaluate.secondPort]);
                    bool result;
                    switch (toEvaluate.operand)
                    {
                        case "OR":
                            result = a || b; // true
                            if (wire.ContainsKey(toEvaluate.outputPort)) throw new Exception("output già presente, modificare la logica");
                            wire.Add(toEvaluate.outputPort, result);
                            break;
                        case "XOR":
                            result = a ^ b; // true (XOR: true se solo uno dei due è true)
                            if (wire.ContainsKey(toEvaluate.outputPort)) throw new Exception("output già presente, modificare la logica");
                            wire.Add(toEvaluate.outputPort, result);
                            break;
                        case "AND":
                            result = a && b; // false
                            if (wire.ContainsKey(toEvaluate.outputPort)) throw new Exception("output già presente, modificare la logica");
                            wire.Add(toEvaluate.outputPort, result);
                            break;
                    }
                    gatePQ.DelMin();
                }
                //Se il gate non era ancora pronto, valutiamo se lo è adesso
                else if (wire.ContainsKey(toEvaluate.firstPort) && wire.ContainsKey(toEvaluate.secondPort))
                {
                    toEvaluate.ready = true;
                    gatePQ.DelMin();
                    gatePQ.Insert(toEvaluate);
                }
                //Se il gate non è ancora pronto, lo mettiamo in coda e lasciamo elaborarne altri
                else
                {
                    gatePQ.DelMin();
                    toEvaluate.priorityFunction++;
                    gatePQ.Insert(toEvaluate);
                }

            }
            List<int> output = wire
    .Where(w => w.Key.StartsWith("z"))                // Filtra solo le chiavi che iniziano con "z"
    .OrderByDescending(w => int.Parse(w.Key.Substring(1)))      // Ordina per valore numerico della chiave
    .Select(w => w.Value ? 1 : 0)                     // Mappa i valori: true -> 1, false -> 0
    .ToList();
            solution = Convert.ToInt64(string.Join("", output), 2);

        }
        public class Gate
        {
            public Gate(string firstPort, string secondPort, string outputPort, string operand)
            {
                this.firstPort = firstPort;
                this.secondPort = secondPort;
                this.operand = operand;
                this.outputPort = outputPort;
                if (wire.ContainsKey(this.firstPort) && wire.ContainsKey(this.secondPort)) this.ready = true;
                if (this.ready) this.priorityFunction = 0;
                else
                {
                    this.priorityFunction = nPF;
                    nPF++;
                }
                //if (this.firstPort.StartsWith("x") || this.firstPort.StartsWith("y")) this.safetyOperation = true;
                //else if (!this.firstPort.StartsWith("x") && !this.firstPort.StartsWith("y") && !this.outputPort.StartsWith("z")) this.safetyOperation = true;
                //else this.safetyOperation = false;
            }
            public bool safetyOperation { get; set; }
            public string firstPort { get; set; }
            public string secondPort { get; set; }
            public string outputPort { get; set; }
            public bool ready { get; set; }
            public int priorityFunction { get; set; }
            public string operand { get; set; }
        }
        public class MinPQGate<Key>
        {
            private Key[] pq;                    // store items at indices 1 to n
            private int n;                       // number of items on priority queue
            private readonly Comparer<Key> comparator;  // optional comparator

            public MinPQGate(int initCapacity)
            {
                pq = new Key[initCapacity + 1];
                n = 0;
            }

            public MinPQGate() : this(1) { }

            public MinPQGate(int initCapacity, Comparer<Key> comparator)
            {
                this.comparator = comparator;
                pq = new Key[initCapacity + 1];
                n = 0;
            }

            public MinPQGate(Comparer<Key> comparator) : this(1, comparator) { }

            public MinPQGate(Key[] keys)
            {
                n = keys.Length;
                pq = new Key[keys.Length + 1];
                for (int i = 0; i < n; i++)
                    pq[i + 1] = keys[i];
                for (int k = n / 2; k >= 1; k--)
                    Sink(k);
                //Assert isMinHeap();
            }

            public bool IsEmpty() => n == 0;

            public int Size() => n;

            public Key Min()
            {
                if (IsEmpty()) throw new InvalidOperationException("Priority queue underflow");
                return pq[1];
            }

            private void Resize(int capacity)
            {
                if (capacity <= n) return;
                Key[] temp = new Key[capacity];
                for (int i = 1; i <= n; i++)
                    temp[i] = pq[i];
                pq = temp;
            }

            public void Insert(Key x)
            {
                if (n == pq.Length - 1) Resize(2 * pq.Length);
                pq[++n] = x;
                Swim(n);
                //Assert isMinHeap();
            }

            public Key DelMin()
            {
                if (IsEmpty()) throw new InvalidOperationException("Priority queue underflow");
                Key min = pq[1];
                Exchange(1, n--);
                Sink(1);
                pq[n + 1] = default; // to avoid loitering and help with garbage collection
                if ((n > 0) && (n == (pq.Length - 1) / 4)) Resize(pq.Length / 2);
                //assert isMinHeap();
                return min;
            }

            private void Swim(int k)
            {
                while (k > 1 && Greater(k / 2, k))
                {
                    Exchange(k / 2, k);
                    k = k / 2;
                }
            }

            private void Sink(int k)
            {
                while (2 * k <= n)
                {
                    int j = 2 * k;
                    if (j < n && Greater(j, j + 1)) j++;
                    if (!Greater(k, j)) break;
                    Exchange(k, j);
                    k = j;
                }
            }

            private bool Greater(int i, int j)
            {
                dynamic tpq = pq;
                if (comparator == null)
                {
                    return (((Gate)tpq[i]).priorityFunction).CompareTo(((Gate)tpq[j]).priorityFunction) > 0;
                }
                else
                {
                    return comparator.Compare(pq[i], pq[j]) > 0;
                }

            }

            private void Exchange(int i, int j)
            {
                Key swap = pq[i];
                pq[i] = pq[j];
                pq[j] = swap;
            }
            public bool isMinHeap()
            {
                for (int i = 1; i <= n; i++)
                {
                    if (pq[i] == null) return false;
                }
                for (int i = n + 1; i < pq.Length; i++)
                {
                    if (pq[i] != null) return false;
                }
                if (pq[0] != null) return false;
                return isMinHeapOrdered(1);
            }
            public bool isMinHeapOrdered(int k)
            {
                if (k > n) return true;
                int left = 2 * k;
                int right = 2 * k + 1;
                if (left <= n && Greater(k, left)) return false;
                if (right <= n && Greater(k, right)) return false;
                return isMinHeapOrdered(left) && isMinHeapOrdered(right);
            }


        }

        public static Dictionary<string, bool> wireNew = new Dictionary<string, bool>();

        public void Part2(object input, bool test, ref object solution)
        {
            var triples = new List<Triple>();
            MinPQGate<Gate> gatePQ = new MinPQGate<Gate>();
            bool connection = false;
            string inputText = (string)input;
            int nTest = 0;
            if (test)
            {
                switch (nTest)
                {
                    case 0:
                        break;
                    case 1:
                        inputText = "x00: 1\r\nx01: 1\r\nx02: 1\r\ny00: 0\r\ny01: 1\r\ny02: 0\r\n\r\nx00 AND y00 -> z00\r\nx01 XOR y01 -> z01\r\nx02 OR y02 -> z02";
                        break;
                }
            }

            foreach (string line in inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None))
            {
                if (!string.IsNullOrEmpty(line))
                {
                    if (connection)
                    {
                        var c = line.Split(Delimiter.delimiter_space, StringSplitOptions.None);
                        var i1 = c[0];
                        var i2 = c[2];
                        var o3 = c[4];
                        var operand = c[1];
                        Gate gate1 = new Gate(i1, i2, o3, operand);
                        gatePQ.Insert(gate1);
                        triples.Add(new Triple(
                            line,
                            String.Compare(i1, i2, StringComparison.Ordinal) < 0 ? i1 : i2,
                            String.Compare(i1, i2, StringComparison.Ordinal) > 0 ? i1 : i2,
                            operand,
                            o3
                        ));
                    }
                    else
                    {
                        var w = line.Split(':');
                        wire.Add(w[0], Convert.ToBoolean(int.Parse(w[1])));
                    }
                }
                else
                {
                    connection = true;
                }
            }


            var answer = new HashSet<string>();

            // Esaminare le regole specifiche
            foreach (var triple in triples)
            {
                // Se il target è 'z' e l'operatore non è XOR, aggiungilo alla risposta
                if (triple.Target.StartsWith("z") && triple.Op != "XOR" && triple.Target != "z45")
                {
                    answer.Add(triple.Target);
                }

                // Gestire i casi degli XOR che devono sempre mirare a Z
                if (!triple.Target.StartsWith("z") && triple.Op == "XOR" && !triple.Lhs.StartsWith("x"))
                {
                    answer.Add(triple.Target);
                }

                // Gli AND devono alimentare gli OR
                if (triple.Op == "AND" && triple.Lhs != "x00")
                {
                    var feeds = triples.Where(t => t.Lhs == triple.Target || t.Rhs == triple.Target).ToList();
                    foreach (var fed in feeds)
                    {
                        if (fed.Op != "OR")
                        {
                            answer.Add(triple.Target);
                            break;
                        }
                    }
                }

                // Gli OR devono essere targetizzati da AND
                if (triple.Op == "OR")
                {
                    var LHSFeeds = triples.Single(t => t.Target == triple.Lhs);
                    if (LHSFeeds.Op != "AND")
                    {
                        answer.Add(LHSFeeds.Target);
                    }

                    var RHSFeeds = triples.Single(t => t.Target == triple.Rhs);
                    if (RHSFeeds.Op != "AND")
                    {
                        answer.Add(RHSFeeds.Target);
                    }
                }
            }

            var ans = answer.ToList();
            ans.Sort();
            solution = string.Join(",", ans);


            /*Gate toEvaluate;
            while (!gatePQ.IsEmpty())
            {
                toEvaluate = gatePQ.Min();
                if (toEvaluate.ready)
                {
                    bool a = Convert.ToBoolean(wire[toEvaluate.firstPort]);
                    bool b = Convert.ToBoolean(wire[toEvaluate.secondPort]);
                    bool result;
                    switch (toEvaluate.operand)
                    {
                        case "OR":
                            result = a || b; // true
                            if (wire.ContainsKey(toEvaluate.outputPort)) throw new Exception("output già presente, modificare la logica");
                            wire.Add(toEvaluate.outputPort, result);
                            break;
                        case "XOR":
                            result = a ^ b; // true (XOR: true se solo uno dei due è true)
                            if (wire.ContainsKey(toEvaluate.outputPort)) throw new Exception("output già presente, modificare la logica");
                            wire.Add(toEvaluate.outputPort, result);
                            break;
                        case "AND":
                            result = a && b; // false
                            if (wire.ContainsKey(toEvaluate.outputPort)) throw new Exception("output già presente, modificare la logica");
                            wire.Add(toEvaluate.outputPort, result);
                            break;
                    }
                    gatePQ.DelMin();
                }
                //Se il gate non era ancora pronto, valutiamo se lo è adesso
                else if (wire.ContainsKey(toEvaluate.firstPort) && wire.ContainsKey(toEvaluate.secondPort))
                {
                    toEvaluate.ready = true;
                    gatePQ.DelMin();
                    gatePQ.Insert(toEvaluate);
                }
                //Se il gate continua a non essere pronto, lo mettiamo in coda e lasciamo elaborarne altri
                else
                {
                    gatePQ.DelMin();
                    toEvaluate.priorityFunction++;
                    gatePQ.Insert(toEvaluate);
                }
            }

            //somma X e Y e ottieni Z
            List<int> X_wire = wire
                                .Where(w => w.Key.StartsWith("x"))                      // Filtra solo le chiavi che iniziano con "x"
                                .OrderByDescending(w => int.Parse(w.Key.Substring(1)))  // Ordina per valore numerico della chiave
                                .Select(w => w.Value ? 1 : 0)                           // Mappa i valori: true -> 1, false -> 0
                                .ToList();
            var X_decimal = Convert.ToInt64(string.Join("", X_wire), 2);

            List<int> Y_wire = wire
                                .Where(w => w.Key.StartsWith("y"))                      // Filtra solo le chiavi che iniziano con "y"
                                .OrderByDescending(w => int.Parse(w.Key.Substring(1)))  // Ordina per valore numerico della chiave
                                .Select(w => w.Value ? 1 : 0)                           // Mappa i valori: true -> 1, false -> 0
                                .ToList();
            var Y_decimal = Convert.ToInt64(string.Join("", Y_wire), 2);
            var Z_decimal_XplusY = X_decimal + Y_decimal;
            var Z_binary_XplusY = Convert.ToString(Z_decimal_XplusY, 2);


            //parti da Z
            List<int> Z_wire = wire
                                .Where(w => w.Key.StartsWith("z"))                      // Filtra solo le chiavi che iniziano con "z"
                                .OrderByDescending(w => int.Parse(w.Key.Substring(1)))  // Ordina per valore numerico della chiave
                                .Select(w => w.Value ? 1 : 0)                           // Mappa i valori: true -> 1, false -> 0
                                .ToList();
            string Z_binary = string.Join("", Z_wire);
            var Z_decimal = Convert.ToInt64(string.Join("", Z_binary), 2);

            //65738163119216
            //65740327379952


            string Z_binary_XplusY_padded = Convert.ToString(Z_decimal_XplusY, 2).PadLeft(64, '0'); // Usa un padding per allineare i numeri
            string Z_binary_padded = Convert.ToString(Z_decimal, 2).PadLeft(64, '0');

            Console.WriteLine($"Binary X+Y: {Z_binary_XplusY_padded}");
            Console.WriteLine($"Binary Z:   {Z_binary_padded}");

            int differences = 0;
            for (int i = 0; i < Z_binary_XplusY_padded.Length; i++)
            {
                if (Z_binary_XplusY_padded[i] != Z_binary_padded[i])
                {
                    Console.WriteLine($"Differenza alla posizione {i}: X+Y = {Z_binary_XplusY_padded[i]}, Z = {Z_binary_padded[i]}");
                    differences++;
                }
            }

            Console.WriteLine($"Numero totale di differenze: {differences}");*/
        }
        public class Triple
        {
            public string Original { get; set; }
            public string Lhs { get; set; }
            public string Rhs { get; set; }
            public string Op { get; set; }
            public string Target { get; set; }

            public Triple(string original, string lhs, string rhs, string op, string target)
            {
                Original = original;
                Lhs = lhs;
                Rhs = rhs;
                Op = op;
                Target = target;
            }
        }
    }
}