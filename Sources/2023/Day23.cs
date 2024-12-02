using AOC;
using AOC.Model;
using AOC.Utilities.Math;
using Microsoft.VisualBasic;
 
 
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Configuration;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using static AOC2023.Day22;
using static AOC2023.Day23;

namespace AOC2023
{
    public class Day23 : Solver, IDay
    {
        public static string[,] Map;
        public static int n;
        public static int[] EndPoint = new int[2];
        public static int[] StartPoint = new int[2];
        public void Part1(object input, bool test, ref object solution)
        {
            List<string> inputList = new List<string>();
            inputList = (List<string>)input;

            n = inputList.Count();
            Map = new string[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Map[i, j] = inputList[i].Substring(j, 1);
                }
            }
            int[] YouAre = new int[2];
            YouAre[0] = 0; YouAre[1] = 1;
            Path path = new Path(YouAre);
            goal = false;
            Dictionary<int, Path> Paths = new Dictionary<int, Path>();
            Paths.Add(path.n_path, path);
            while (!goal)
            {
                ExploringPosition(ref Paths);
                Console.WriteLine($"{Paths[1].YouAre[0]},{Paths[1].YouAre[1]}");
            }

            int maxStep = 0;
            foreach (Path p in Paths.Values)
            {
                if (p.ExploredPosition.Count() - 1 > maxStep) maxStep = p.ExploredPosition.Count() - 1;

            }
            solution = maxStep;
        }
        public bool goal = false;

        public void ExploringPosition(ref Dictionary<int, Path> Paths)
        {
            List<Path> NewPath = new List<Path>();
            int totalPossibleDirections = 0;
            foreach (Path path in Paths.Values)
            {
                int possibleDirections = 0;
                List<int[]> TempExploredPosition = new List<int[]>();
                foreach (var ep in path.ExploredPosition)
                {
                    TempExploredPosition.Add(new int[2] { ep[0], ep[1] });
                }
                int x = path.YouAre[0];
                int y = path.YouAre[1];


                if (x > 0) if (!Map[x - 1, y].Equals("#")) if ((Map[x - 1, y].Equals(".")) || (Map[x - 1, y].Equals("^"))) if (path.ContainsPoint(x - 1, y))
                            {
                                possibleDirections++;
                                if (possibleDirections > 1)
                                {
                                }
                                else
                                {
                                    path.YouAre[0] -= 1;
                                    path.ExploredPosition.Add(new int[2] { x - 1, y });
                                }
                            }

                if (y < n - 1) if (!Map[x, y + 1].Equals("#")) if ((Map[x, y + 1].Equals(".")) || (Map[x, y + 1].Equals(">"))) if (path.ContainsPoint(x, y + 1))
                            {
                                possibleDirections++;
                                if (possibleDirections > 1)
                                {

                                    Path npright = new Path(TempExploredPosition, x, y);
                                    npright.YouAre[1] += 1;
                                    npright.ExploredPosition.Add(new int[2] { x, y + 1 });
                                    NewPath.Add(npright);
                                }
                                else
                                {
                                    path.YouAre[1] += 1;
                                    path.ExploredPosition.Add(new int[2] { x, y + 1 });
                                }
                            }


                if (x < n - 1) if (!Map[x + 1, y].Equals("#")) if ((Map[x + 1, y].Equals(".")) || (Map[x + 1, y].Equals("v"))) if (path.ContainsPoint(x + 1, y))
                            {
                                possibleDirections++;
                                if (possibleDirections > 1)
                                {
                                    Path npdown = new Path(TempExploredPosition, x, y);
                                    npdown.YouAre[0] += 1;
                                    npdown.ExploredPosition.Add(new int[2] { x + 1, y });
                                    NewPath.Add(npdown);
                                }
                                else
                                {
                                    path.YouAre[0] += 1;
                                    path.ExploredPosition.Add(new int[2] { x + 1, y });
                                }
                            }


                if (y > 0) if (!Map[x, y - 1].Equals("#")) if ((Map[x, y - 1].Equals(".")) || (Map[x, y - 1].Equals("<"))) if (path.ContainsPoint(x, y - 1))
                            {
                                possibleDirections++;
                                if (possibleDirections > 1)
                                {
                                    Path npleft = new Path(TempExploredPosition, x, y);
                                    npleft.YouAre[1] -= 1;
                                    npleft.ExploredPosition.Add(new int[2] { x, y - 1 });
                                    NewPath.Add(npleft);
                                }
                                else
                                {
                                    path.YouAre[1] -= 1;
                                    path.ExploredPosition.Add(new int[2] { x, y - 1 });
                                }
                            }
                totalPossibleDirections += possibleDirections;

            }

            if (NewPath.Count != 0)
            {
                foreach (var np in NewPath)
                {
                    Paths.Add(np.n_path, np);
                }
            }

            if (totalPossibleDirections == 0)
            {
                goal = true;
            }



        }
        public static int counter_paths = 0;
        /*public class Path
        {
            public Path()
            {

            }
            public Path(int[] YouAre)
            {
                //this.YouAre = new int[2];
                this.YouAre[0] = YouAre[0];
                this.YouAre[1] = YouAre[1];
                this.n_path = counter_paths + 1;
                counter_paths++;
                this.ExploredPosition = new List<int[]>();
                this.ExploredPosition.Add(new int[2] { YouAre[0], YouAre[1] });
            }
            public Path(List<int[]> TempExploredPosition, int x, int y)
            {
                this.YouAre = new int[2];
                this.YouAre[0] = x;
                this.YouAre[1] = y;
                this.n_path = counter_paths + 1;
                counter_paths++;
                this.ExploredPosition = new List<int[]>();
                foreach (var item in TempExploredPosition)
                {
                    this.ExploredPosition.Add(new int[2] { item[0], item[1] });
                }
            }
            public int n_path { get; set; }
            private int[] _youAre = new int[2];
            public int[] YouAre
            {
                get
                { return _youAre; }
                set
                {
                    _youAre = value;

                }
            }
            public List<int[]> ExploredPosition { get; set; }
            public bool f_goal = false;

            public bool ContainsPoint(int x, int y)
            {
                bool ret = true;
                foreach (var p in this.ExploredPosition)
                {
                    if (p[0] == x && p[1] == y)
                    {
                        ret = false;
                        break;
                    }
                }
                return ret;
            }
            public int priorityFunction { get; set; }
            public int SetPriorityFunction(int step, int heuristic) { return step + heuristic; }
            public int ManhattanMeasure { get; set; }
            public int heuristic { get; set; }
            public int step { get; set; }

        }*/
        public class Path
        {
            public Path()
            {

            }
            public Path(int[] YouAre)
            {
                this.YouAre[0] = YouAre[0];
                this.YouAre[1] = YouAre[1];
                this.n_path = counter_paths + 1;
                counter_paths++;
                this.ExploredPosition = new List<int[]>();
                this.ExploredPosition.Add(new int[2] { YouAre[0], YouAre[1] });
                this.step = this.ExploredPosition.Count() - 1;
                this.ManhattanMeasure = manhattan();
                this.heuristic = ManhattanMeasure;
                this.priorityFunction = SetPriorityFunction(this.step,this.heuristic);
                this.ParallelPaths = FindParallelPaths();
            }
            public Path(List<int[]> TempExploredPosition, int x, int y)
            {
                this.YouAre = new int[2];
                this.YouAre[0] = x;
                this.YouAre[1] = y;
                this.n_path = counter_paths + 1;
                counter_paths++;
                this.ExploredPosition = new List<int[]>();
                foreach (var item in TempExploredPosition)
                {
                    this.ExploredPosition.Add(new int[2] { item[0], item[1] });
                }
            }
            public int n_path { get; set; }
            private int[] _youAre = new int[2];
            public int[] YouAre
            {
                get { return _youAre; }
                set { _youAre = value;
                    this.ManhattanMeasure = manhattan();
                    this.heuristic = ManhattanMeasure;
                    this.priorityFunction = SetPriorityFunction(this.step,this.heuristic);
                }
            }
            public List<int[]> ExploredPosition { get; set; }
            public bool f_goal = false;

            public bool ContainsPoint(int x, int y)
            {
                bool ret = true;
                foreach (var p in this.ExploredPosition)
                {
                    if (p[0] == x && p[1] == y)
                    {
                        ret = false;
                        break;
                    }
                }
                return ret;
            }

            public int priorityFunction { get; set; }
            public int SetPriorityFunction(int step, int heuristic) { return heuristic-step; }
            public int ManhattanMeasure { get; set; }
            public int heuristic { get; set; }
            public int step { get; set; }
            public List<Path> ParallelPaths { get; set; }
            public List<Path> FindParallelPaths()
            {
                List<Path> ParallelPaths = new List<Path>();
                int possibleDirections = 0;

                int x = this.YouAre[0];
                int y = this.YouAre[1];

                if (x > 0) if (!Map[x - 1, y].Equals("#")) if (this.ContainsPoint(x - 1, y))
                        {
                            possibleDirections++;
                            this.step++;
                            this.YouAre = new int[2] { this.YouAre[0] - 1, this.YouAre[1] };
                            this.ExploredPosition.Add(YouAre);
                        }

                if (y < n - 1) if (!Map[x, y + 1].Equals("#")) if (this.ContainsPoint(x, y + 1))
                        {
                            if (possibleDirections > 1)
                            {
                                possibleDirections++;
                                Path pest = new Path();
                                this.step++;
                                pest.YouAre = new int[2] { this.YouAre[0], this.YouAre[1] + 1 };
                                pest.ExploredPosition = this.ExploredPosition;
                                pest.ExploredPosition.Add(new int[2] { x, y + 1 });
                                ParallelPaths.Add(pest);
                            }
                            else
                            {
                                possibleDirections++;
                                this.step++;
                                this.YouAre = new int[2] { this.YouAre[0], this.YouAre[1] + 1 };
                                this.ExploredPosition.Add(YouAre);
                            }
                        }
                if (x < n - 1) if (!Map[x + 1, y].Equals("#")) if (this.ContainsPoint(x + 1, y))
                        {
                            if (possibleDirections > 1)
                            {
                                possibleDirections++;
                                Path psud = new Path();
                                this.step++;
                                psud.YouAre = new int[2] { this.YouAre[0] + 1, this.YouAre[1] };
                                psud.ExploredPosition = this.ExploredPosition;
                                psud.ExploredPosition.Add(new int[2] { x + 1, y });
                                ParallelPaths.Add(psud);
                            }
                            else
                            {
                                possibleDirections++;
                                this.YouAre = new int[2] { this.YouAre[0] + 1, this.YouAre[1] };
                                this.ExploredPosition.Add(YouAre);
                            }
                        }
                if (y > 0) if (!Map[x, y - 1].Equals("#")) if (this.ContainsPoint(x, y - 1))
                        {
                            if (possibleDirections>1)
                            {
                                Path pwest = new Path();
                                this.step++;
                                pwest.YouAre = new int[2] { this.YouAre[0], this.YouAre[1] - 1 };
                                pwest.ExploredPosition = this.ExploredPosition;
                                pwest.ExploredPosition.Add(new int[2] { x, y - 1 });
                                ParallelPaths.Add(pwest);
                            }
                            else
                            {
                                possibleDirections++;
                                this.step++;
                                this.YouAre = new int[2] { this.YouAre[0], this.YouAre[1] -1};
                                this.ExploredPosition.Add(YouAre);
                            }
                        }
                return ParallelPaths;
            }

            public int manhattan()
            {
                int manhattanmeasure = 0;
                manhattanmeasure = Math.Abs((this.YouAre[0]) - (StartPoint[0])) + Math.Abs((this.YouAre[1]) - (StartPoint[1]));
                return manhattanmeasure;
            }

        }

        public int end_X { get; set; }
        public int end_Y { get; set; }
        public void Part2(object input, bool test, ref object solution)
        {
            List<string> inputList = new List<string>();
            inputList = (List<string>)input;

            n = inputList.Count();
            end_X = n - 1;
            end_Y = n - 2;
            EndPoint = new int[2];
            EndPoint[0] = end_X;
            EndPoint[1] = end_Y;

            Map = new string[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Map[i, j] = inputList[i].Substring(j, 1);
                }
            }

            StartPoint= new int[2];
            StartPoint[0] = 0; StartPoint[1] = 1;
            Path initial = new Path(StartPoint);
            goal = false;
            MaxPQPath<Path> SearchPath = new MaxPQPath<Path>();
            MaxPQPath<Path> PreviousSearchPath = new MaxPQPath<Path>();
            SearchPath.Insert(initial);
            Path ToEvaluate = new Path();

            while (!SearchPath.IsEmpty() || !goal)
            {
                ToEvaluate = SearchPath.Max();
                if (ToEvaluate.YouAre == EndPoint) goal = true;
                ShowPosition(ToEvaluate.YouAre);


            }


            int maxStep = 0;

            /*

        foreach (Path p in Paths.Values)
        {
            if (p.ExploredPosition.Count() - 1 > maxStep && p.f_goal)
            {
                maxStep = p.ExploredPosition.Count() - 1;
            }
            if (maxStep == 166)
            {
                var dd = "";
            }

        }*/
            solution = maxStep; //sarà ExploringPosition-1 del path n4. Ma il 16 ha 167 ExploringPosition, perché?=
        }

        public void ShowPosition(int[] You)
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (You[0] == i && You[1] == j)
                    {
                        Console.Write("O");
                    }
                    else
                    {
                        Console.Write(Map[i, j]);
                    }
                }
                Console.WriteLine();
            }
            Thread.Sleep(50);
            Console.WriteLine();
        }



        public class MaxPQPath<Path>
        {
            private Path[] pq;                    // store items at indices 1 to n
            private int n;                       // number of items on priority queue
            private Comparer<Path> comparator;    // optional comparator

            public MaxPQPath(int initCapacity)
            {
                pq = new Path[initCapacity + 1];
                n = 0;
            }

            public MaxPQPath() : this(1)
            {
            }

            public MaxPQPath(int initCapacity, Comparer<Path> comparator)
            {
                this.comparator = comparator;
                pq = new Path[initCapacity + 1];
                n = 0;
            }

            public MaxPQPath(Comparer<Path> comparator) : this(1, comparator)
            {
            }

            public MaxPQPath(Path[] keys)
            {
                n = keys.Length;
                pq = new Path[keys.Length + 1];
                Array.Copy(keys, 0, pq, 1, keys.Length);
                for (int k = n / 2; k >= 1; k--)
                    Sink(k);
                if (!IsMaxHeap())
                    throw new InvalidOperationException("Heap construction failed");
            }

            public bool IsEmpty()
            {
                return n == 0;
            }

            public int Size()
            {
                return n;
            }

            public Path Max()
            {
                if (IsEmpty()) throw new InvalidOperationException("Priority queue underflow");
                return pq[1];
            }

            private void Resize(int capacity)
            {
                if (capacity <= n)
                    throw new ArgumentException("New capacity is smaller than current size");

                Path[] temp = new Path[capacity];
                Array.Copy(pq, 1, temp, 1, n);
                pq = temp;
            }

            public void Insert(Path x)
            {
                if (n == pq.Length - 1)
                    Resize(2 * pq.Length);

                pq[++n] = x;
                Swim(n);
                if (!IsMaxHeap())
                    throw new InvalidOperationException("Heap invariant violated");
            }

            public Path DelMax()
            {
                if (IsEmpty())
                    throw new InvalidOperationException("Priority queue underflow");

                Path max = pq[1];
                Exchange(1, n--);
                Sink(1);
                pq[n + 1] = default(Path); // To avoid loitering and help with garbage collection

                if (n > 0 && n == (pq.Length - 1) / 4)
                    Resize(pq.Length / 2);

                if (!IsMaxHeap())
                    throw new InvalidOperationException("Heap invariant violated");

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
                    if (j < n && Less(j, j + 1))
                        j++;
                    if (!Less(k, j))
                        break;
                    Exchange(k, j);
                    k = j;
                }
            }

            private bool Less(int i, int j)
            {
                if (comparator == null)
                    return Comparer<Path>.Default.Compare(pq[i], pq[j]) < 0;
                else
                    return comparator.Compare(pq[i], pq[j]) < 0;
            }

            private void Exchange(int i, int j)
            {
                Path temp = pq[i];
                pq[i] = pq[j];
                pq[j] = temp;
            }

            private bool IsMaxHeap()
            {
                for (int i = 1; i <= n; i++)
                {
                    if (pq[i] == null)
                        return false;
                }
                for (int i = n + 1; i < pq.Length; i++)
                {
                    if (pq[i] != null)
                        return false;
                }
                if (pq[0] != null)
                    return false;

                return IsMaxHeapOrdered(1);
            }

            private bool IsMaxHeapOrdered(int k)
            {
                if (k > n)
                    return true;

                int left = 2 * k;
                int right = 2 * k + 1;
                if (left <= n && Less(k, left))
                    return false;
                if (right <= n && Less(k, right))
                    return false;

                return IsMaxHeapOrdered(left) && IsMaxHeapOrdered(right);
            }
        }

        public void ExploringPositionP2(ref Dictionary<int, Path> Paths)
        {

        }





    }
}
