using AOC;
using AOC.DataStructures.Clustering;
using AOC.DataStructures.PriorityQueue;
using AOC.Model;
using AOC.SearchAlghoritmhs;
using AOC.Utilities.Map;
using AOC.Utilities.Math;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization; 
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;
using System.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using static AOC.SearchAlghoritmhs.ResearchAlghoritmsAttribute;
using static AOC2015.Day19;
using static AOC2015.Day9;
using Solver = AOC.Solver;

namespace AOC2015
{
    [ResearchAlghoritmsAttribute(TypologyEnum.Reduction)]
    public class Day19 : Solver, IDay
    {
        public void Part1(object input, bool test, ref object solution)
        {
            string inputText = (string)input;
            var inputlist = inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None);
            bool Transformation = true;
            string Molecules = "";
            Dictionary<string, List<string>> TransformationRules = new Dictionary<string, List<string>>();
            for (int i = 0; i < inputlist.Length; i++)
            {
                if (string.IsNullOrEmpty(inputlist[i]))
                {
                    Transformation = false;
                }
                if (Transformation)
                {
                    var r = inputlist[i].Split(Delimiter.delimiter_space, StringSplitOptions.None);
                    if (!TransformationRules.ContainsKey(r[0]))
                    {
                        TransformationRules.Add(r[0], new List<string> { });
                        TransformationRules[r[0]].Add(r[r.Length - 1]);
                    }
                    else
                    {
                        TransformationRules[r[0]].Add(r[r.Length - 1]);
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(inputlist[i]))
                    {
                        Molecules = inputlist[i];
                        List<string> NewMolecules = new List<string>();
                        string Molecula = "";
                        List<string> MoleculePartList = new List<string>();
                        for (int m = 0; m < Molecules.Length; m++)
                        {

                            if (TransformationRules.ContainsKey(Molecules[m].ToString()))
                            {
                                Molecula = Molecules[m].ToString();
                            }
                            else
                            {

                                MoleculePartList = AddMoleculesPart(Molecules[m].ToString(), MoleculePartList);
                                foreach (var mol in MoleculePartList)
                                {
                                    if (TransformationRules.ContainsKey(mol))
                                    {
                                        Molecula = mol;
                                        //break;
                                    }
                                }
                            }

                            if (!string.IsNullOrEmpty(Molecula))
                            {
                                foreach (var rules in TransformationRules[Molecula])
                                {
                                    NewMolecules.Add(
                              Molecules.Substring(0, m - (Molecula.Length - 1))
                            + rules
                            + Molecules.Substring(m + 1, Molecules.Length - m - 1).Trim());
                                }
                                Molecula = "";
                                MoleculePartList = new List<string>();
                            }
                        }
                        solution = NewMolecules.Distinct().Count();
                    }
                }
            }
        }

        public static List<string> AddMoleculesPart(string Molecule, List<string> MoleculePartList)
        {
            List<string> NewMolecules = new List<string> { };
            NewMolecules.Add(Molecule);

            foreach (var molecula in MoleculePartList)
            {
                NewMolecules.Add(molecula + Molecule);
            }
            return NewMolecules;
        }
        public static Dictionary<string, List<string>> TransformationRules = new Dictionary<string, List<string>>();
        public static string Medicine;
        //Il metodo a riduzione inversa con sostituzioni greedy è il più efficiente e adatto per questo problema. Funziona bene con il tipo di regole e struttura molecolare forniti nel puzzle.
        public void Part2(object input, bool test, ref object solution)
        {
            string inputText = (string)input;
            if (test) inputText = "e => H\r\ne => O\r\n" + inputText;
            var inputlist = inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None);
            bool Transformation = true;
            TransformationRules = new Dictionary<string, List<string>>();
            for (int i = 0; i < inputlist.Length; i++)
            {
                if (string.IsNullOrEmpty(inputlist[i]))
                {
                    Transformation = false;
                }
                if (Transformation)
                {
                    var r = inputlist[i].Split(Delimiter.delimiter_space, StringSplitOptions.None);
                    if (!TransformationRules.ContainsKey(r[r.Length - 1]))
                    {
                        TransformationRules.Add(r[r.Length - 1], new List<string> { });
                        TransformationRules[r[r.Length - 1]].Add(r[0]);
                    }
                    else
                    {
                        TransformationRules[r[r.Length - 1]].Add(r[0]);
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(inputlist[i]))
                    {
                        Medicine = inputlist[i];
                        Molecula initial = new Molecula("e", true);
                        //Molecula initial = new Molecula("ORnPBPMgArCaCaCaSiThCaCaSiThCa", true);
                        MaxPQMolecule<Molecula> previousSearchNodesPQ = new MaxPQMolecule<Molecula>();
                        MaxPQMolecule<Molecula> searchNodesPQ = new MaxPQMolecule<Molecula>();
                        searchNodesPQ.Insert(initial);
                        //TestQueue(searchNodesPQ);
                        Molecula toEvaluate = new Molecula();
                        int cont = 0;
                        while (!searchNodesPQ.IsEmpty())
                        {
                            cont++;
                            toEvaluate = searchNodesPQ.Max();
                            Console.WriteLine($"{cont} - {toEvaluate._composite}");
                            foreach (Molecula mol in toEvaluate._possibleTransformations)
                            {
                                Molecula NewMolecula = new Molecula(mol._composite, true, toEvaluate._step + 1);

                                if ((!searchNodesPQ.Contain(NewMolecula._composite)) && (!previousSearchNodesPQ.Contain(NewMolecula._composite)))
                                {
                                    NewMolecula.parent = toEvaluate;
                                    searchNodesPQ.Insert(NewMolecula);
                                }
                                else
                                {
                                    Molecula OldMolecula = searchNodesPQ.Select(NewMolecula._composite);
                                    if (OldMolecula != null)
                                    {
                                        if (NewMolecula._step < OldMolecula._step)
                                        {
                                            NewMolecula.parent = toEvaluate;
                                            searchNodesPQ.Remove(OldMolecula._composite);
                                            searchNodesPQ.Insert(NewMolecula);
                                        }
                                    }
                                    else
                                    {
                                        OldMolecula = previousSearchNodesPQ.Select(NewMolecula._composite);
                                        if (NewMolecula._step < OldMolecula._step)
                                        {
                                            NewMolecula.parent = toEvaluate;
                                            previousSearchNodesPQ.Remove(OldMolecula._composite);
                                            searchNodesPQ.Insert(NewMolecula);
                                        }
                                    }
                                }
                            }
                            searchNodesPQ.Remove(toEvaluate._composite);
                            if (!previousSearchNodesPQ.Contain(toEvaluate._composite)) previousSearchNodesPQ.Insert(toEvaluate);

                            if (searchNodesPQ.HasGoal())
                            {
                                Console.WriteLine("Soluzione trovata");
                                solution = searchNodesPQ.Select(Medicine)._step;
                                while (!searchNodesPQ.IsEmpty()) searchNodesPQ.DelMax();
                            }
                        }
                    }
                }
            }
        }
        public void TestQueue(MaxPQMolecule<Molecula> searchNodesPQ)
        {
            Molecula prova3 = new Molecula("e", true);
            prova3._priority = 3;
            Molecula prova1 = new Molecula("e", true);
            prova1._priority = 1;
            Molecula prova10 = new Molecula("e", true);
            prova10._priority = 10;
            Molecula prova1000 = new Molecula("e", true);
            prova1000._priority = 1000;
            Molecula prova0 = new Molecula("e", true);
            prova0._priority = 0;
            searchNodesPQ.Insert(prova3);
            searchNodesPQ.Insert(prova1);
            searchNodesPQ.Insert(prova10);
            searchNodesPQ.Insert(prova1000);
            searchNodesPQ.Insert(prova0);
            var a = searchNodesPQ.Max();
            searchNodesPQ.DelMax();
            var b = searchNodesPQ.Max();
            searchNodesPQ.DelMax();
            var c = searchNodesPQ.Max();
            searchNodesPQ.DelMax();

        }
        public static int SetMatchMeasure(string composite)
        {
            int measure = 0;
            for (int i = 0; i < Math.Min(composite.Length, Medicine.Length); i++)
            {
                if (composite[i] == Medicine[i]) measure++;
                else break;
            }
            if (composite.Length > Medicine.Length) measure -= (composite.Length - Medicine.Length);
            return measure;
        }
        public class Molecula
        {
            public string _composite;
            public int _length;
            public int _indexToTransform;
            public int _step;
            public bool Goal;
            public List<Molecula> _possibleTransformations;
            public int _matchMeasure;
            public int _priority;
            public Molecula parent;

            #region ctr
            public Molecula()
            { }

            public Molecula(string Composite, bool FindTransformations = false, int Step = 0)
            {
                this._composite = Composite;
                this._length = Composite.Length;
                this._step = Step;
                this.Goal = isMedicine();
                this._matchMeasure = SetMatchMeasure(this._composite);
                this._indexToTransform = this._matchMeasure;
                this._priority = this._matchMeasure - this._step;
                if (FindTransformations && !this.Goal) GenerateMolecule();
            }

            #endregion
            #region Methods 
            public bool isMedicine() { return (this._composite == Medicine); }

            public void GenerateMolecule()
            {
                //bool exit = false;
                //while (!exit)
                //{
                    this._possibleTransformations = new List<Molecula>();
                    for (int c = Math.Max(this._indexToTransform-4,0); c < this._composite.Length; c++)
                    {
                        if (this._composite[c].ToString() == "B" ||
                            this._composite[c].ToString() == "F" ||
                            this._composite[c].ToString() == "H" ||
                            this._composite[c].ToString() == "N" ||
                            this._composite[c].ToString() == "O" ||
                            this._composite[c].ToString() == "P" ||
                            this._composite[c].ToString() == "e")
                        {
                            foreach (var t in TransformationRules[this._composite[c].ToString()])
                            {
                                Molecula toAdd = new Molecula(
                                    this._composite.Substring(0, c) +
                                    t +
                                    this._composite.Substring(c + 1, this._composite.Length - (c + 1))
                                    , false, this._step + 1);
                                this._possibleTransformations.Add(toAdd);
                            }
                        }
                        else if (this._composite.Length > c && ((this._composite[c].ToString() == "A" && this._composite[c + 1].ToString() == "l") ||
                                                              (this._composite[c].ToString() == "C" && this._composite[c + 1].ToString() == "a") ||
                                                              (this._composite[c].ToString() == "M" && this._composite[c + 1].ToString() == "g") ||
                                                              (this._composite[c].ToString() == "S" && this._composite[c + 1].ToString() == "i") ||
                                                              (this._composite[c].ToString() == "T" && this._composite[c + 1].ToString() == "h") ||
                                                              (this._composite[c].ToString() == "T" && this._composite[c + 1].ToString() == "i"))
                                )
                        {
                            foreach (var t in TransformationRules[this._composite.Substring(c, 2)])
                            {
                                Molecula toAdd = new Molecula(
                                    this._composite.Substring(0, c) +
                                    t +
                                    this._composite.Substring(c + 2, this._composite.Length - (c + 2))
                                    , false, this._step + 1);
                                this._possibleTransformations.Add(toAdd);
                            }
                        }
                        //            if (NewComposite == null) break;

                        //            foreach (var t in TransformationRules[NewComposite._composite])
                        //            {
                        //                Molecula toAdd = new Molecula(
                        //                    this._composite.Substring(0, NewComposite._indexMedicine) +
                        //                    t +
                        //                    this._composite.Substring(NewComposite._indexMedicine + 1, this._composite.Length - NewComposite._indexMedicine)
                        //                    , false, this._step + 1);
                        //                this._possibleTransformations.Add(toAdd);

                        //            }
                    }
                    
                    /*if (this._possibleTransformations.Any(m => m._matchMeasure > this._matchMeasure)) exit = true;
                    else
                    {
                        this._possibleTransformations.RemoveAll(x => x._composite != "");
                        this._indexToTransform--;
                    }*/
                //}
            }
            #endregion

        }
        public class MaxPQMolecule<Key>
        {
            private Key[] pq;                    // store items at indices 1 to n
            private int n;                       // number of items on priority queue
            private IComparer<Key> comparator;  // optional comparator

            public bool HasGoal()
            {
                dynamic tpq = pq;
                bool isGoal = false;
                for (int i = 1; i < n; i++)
                {
                    if (((Molecula)tpq[i])._composite == Medicine) isGoal = true;
                }
                return isGoal;
            }
            public MaxPQMolecule(int initCapacity)
            {
                pq = new Key[initCapacity + 1];
                n = 0;
            }

            public MaxPQMolecule() : this(1) { }

            public MaxPQMolecule(int initCapacity, Comparer<Key> comparator)
            {
                this.comparator = comparator;
                pq = new Key[initCapacity + 1];
                n = 0;
            }

            public MaxPQMolecule(Comparer<Key> comparator) : this(1, comparator) { }

            public MaxPQMolecule(Key[] keys)
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

            public Key Max()
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


            public bool Contain(string comp)
            {
                dynamic tpq = pq;
                bool isContain = false;
                for (int i = 1; i < n; i++)
                {
                    if (((Molecula)tpq[i])._composite == comp) isContain = true;
                }
                return isContain;
            }

            public Molecula Select(string comp)
            {
                Molecula result = new Molecula();

                dynamic tpq = pq;
                for (int i = 1; i <= n; i++)
                {
                    if (((Molecula)tpq[i])._composite == comp)
                    {
                        result = tpq[i];
                        break;
                    }
                }

                return result;
            }

            public bool Remove(string comp)
            {
                dynamic tpq = pq;
                if (IsEmpty()) throw new InvalidOperationException("Priority queue underflow");

                for (int i = 1; i <= n; i++)
                {
                    if (((Molecula)tpq[i])._composite == comp)
                    {
                        Key del = pq[i];
                        for (int j = i + 1; j <= n; j++)
                        {
                            Exchange(j - 1, j);
                        }
                        n--;
                        Sink(1);
                        tpq[n + 1] = null;
                        if ((n > 0) && (n == (pq.Length - 1) / 4)) Resize(pq.Length / 2);
                        return true;
                    }

                }
                return false;
            }

            public Key DelMax()
            {
                if (IsEmpty()) throw new InvalidOperationException("Priority queue underflow");
                Key max = pq[1];
                Exchange(1, n--);
                Sink(1);
                pq[n + 1] = default; // to avoid loitering and help with garbage collection
                if ((n > 0) && (n == (pq.Length - 1) / 4)) Resize(pq.Length / 2);
                //assert isMinHeap();
                return max;
            }

            private void Swim(int k)
            {
                while (k > 1 && Less(k / 2, k))
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
                    if (j < n && Less(j, j + 1)) j++;
                    if (!Less(k, j)) break;
                    Exchange(k, j);
                    k = j;
                }
            }

            private bool Less(int i, int j)
            {
                dynamic tpq = pq;
                if (comparator == null)
                {
                    return (((Molecula)tpq[j])._priority).CompareTo(((Molecula)tpq[i])._priority) > 0;
                }
                else
                {
                    return comparator.Compare(pq[j], pq[i]) > 0;
                }
            }

            private void Exchange(int i, int j)
            {
                Key swap = pq[i];
                pq[i] = pq[j];
                pq[j] = swap;
            }

            public bool isMaxHeap()
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
                return isMaxHeapOrdered(1);
            }
            public bool isMaxHeapOrdered(int k)

            {
                if (k > n) return true;
                int left = 2 * k;
                int right = 2 * k + 1;
                if (left <= n && Less(k, left)) return false;
                if (right <= n && Less(k, right)) return false;
                return isMaxHeapOrdered(left) && isMaxHeapOrdered(right);
            }

            public static void Example()
            {
                long startTime = DateTime.Now.Ticks;

                string inputName = "tinyPQ.txt";
                string exercise = "MinPQ";

                StreamWriter fw = new StreamWriter($"../../DataStructures/IO/Output/{exercise}_{inputName}");
                fw.Write($"Questo è il risultato dell'esercizio {exercise} con input {inputName} :");
                fw.WriteLine();

                using (StreamReader reader = new StreamReader($"../../DataStructures/IO/Input/{inputName}"))
                {
                    string line = reader.ReadLine()?.Trim();
                    List<string> lista = new List<string>();
                    while (line != null)
                    {
                        int n = line.Trim().Split(' ').Length;
                        for (int i = 0; i < n; i++)
                        {
                            lista.Add(line.Split(' ')[i]);
                        }
                        line = reader.ReadLine();
                    }

                    MinPQ<string> pq = new MinPQ<string>();

                    for (int i = 0; i < lista.Count; i++)
                    {
                        string a = lista[i];
                        if (a != "-")
                            pq.Insert(a);
                        else if (!pq.IsEmpty())
                            fw.Write(pq.DelMin() + " ");
                    }

                    fw.WriteLine();
                    fw.Write($"({pq.Size()} left on pq)");

                    fw.WriteLine();
                    long endTime = DateTime.Now.Ticks;
                    long timeElapsed = endTime - startTime;
                    fw.Write($"Execution time in ticks: {timeElapsed}");
                    fw.WriteLine();
                    fw.Write($"Execution time in milliseconds: {timeElapsed / TimeSpan.TicksPerMillisecond}");
                    fw.WriteLine();
                    fw.Write($"Execution time in seconds: {timeElapsed / TimeSpan.TicksPerSecond}");
                    fw.WriteLine();
                }
                fw.Close();
            }
        }



        ///*  e 
        //        O
        //        HH
        //        HOH

        //        e
        //        H
        //        HO
        //        HHH
        //        HOHH
        //        HOHOH
        //        HOHOHO
        //    */
    }
}