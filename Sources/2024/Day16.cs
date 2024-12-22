using AOC;
using AOC.DataStructures.Clustering;
using AOC.Model;
using AOC.SearchAlghoritmhs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;
using static AOC.SearchAlghoritmhs.ResearchAlghoritmsAttribute;
using static AOC2015.Day9;

namespace AOC2024
{
    [ResearchAlghoritmsAttribute(TypologyEnum.Dijkstra)]
    public class Day16 : Solver, IDay
    {
        public static string[,] Map;
        public static string[,] StoryMap;
        public static int n;
        public static int[] StartPosition = new int[2];
        public static int[] EndPoint = new int[2];
        public static MinPQNode<Node> SearchNodes;
        public static MinPQNode<Node> PreviousSearchNodes;
        public void Part1(object input, bool test, ref object solution)
        {
            string inputText = (string)input;
            int nTest = 0;
            if (test)
            {
                switch (nTest)
                {
                    case 0:
                        break;
                    case 1:
                        inputText = "#################\r\n#...#...#...#..E#\r\n#.#.#.#.#.#.#.#.#\r\n#.#.#.#...#...#.#\r\n#.#.#.#.###.#.#.#\r\n#...#.#.#.....#.#\r\n#.#.#.#.#.#####.#\r\n#.#...#.#.#.....#\r\n#.#.#####.#.###.#\r\n#.#.#.......#...#\r\n#.#.###.#####.###\r\n#.#.#...#.....#.#\r\n#.#.#.#####.###.#\r\n#.#.#.........#.#\r\n#.#.#.#########.#\r\n#S#.............#\r\n#################";
                        break;
                }
            }
            n = inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None)[0].Length;
            Map = new string[n, n];
            StoryMap = new string[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Map[i, j] = inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None)[i].Substring(j, 1).ToString();
                    StoryMap[i, j] = inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None)[i].Substring(j, 1).ToString();
                    if (Map[i, j] == "S")
                    {
                        StartPosition[0] = i;
                        StartPosition[1] = j;
                        Map[i, j] = "X";
                        StoryMap[i, j] = "X";
                    }
                    if (Map[i, j] == "E")
                    {
                        EndPoint[0] = i;
                        EndPoint[1] = j;
                    }
                }
            }
            AOC.Utilities.Map.Map.ShowStringMap(Map);
            SearchNodes = new MinPQNode<Node>();
            PreviousSearchNodes = new MinPQNode<Node>();
            SearchNodes.Insert(new Node($"{StartPosition[0].ToString()},{StartPosition[1].ToString()}",
                                           0,
                                           0,
                                           Direction.Right,
                                           true));

            Node toEvaluate;
            while (!SearchNodes.IsEmpty())
            {
                toEvaluate = SearchNodes.Min();
                AOC.Utilities.Map.Map.ShowBooleanMap(toEvaluate.PathMap, n);
                if (toEvaluate.IsTarget)
                {
                    solution = toEvaluate.TotalScore;
                    break;
                }
                else
                {
                    foreach (Node node in toEvaluate.Neighbors)
                    {
                        Node newNode = new Node(node.Point,
                                                node.TotalStep,
                                                node.TurnedDirection,
                                                node.direction,
                                                true,
                                                node.PathMap);
                        AOC.Utilities.Map.Map.ShowBooleanMap(newNode.PathMap, n);

                        if ((!SearchNodes.Contain(newNode)) && (!PreviousSearchNodes.Contain(newNode)))
                        {
                            SearchNodes.Insert(newNode);
                        }
                        else if ((!SearchNodes.Contain(newNode)) && (PreviousSearchNodes.Contain(newNode)))
                        {
                            if (newNode.TotalScore < PreviousSearchNodes.Select(newNode).TotalScore && newNode.Point != toEvaluate.Point)
                            {
                                PreviousSearchNodes.Remove(newNode);
                                SearchNodes.Insert(newNode);
                            }
                        }
                        else if ((SearchNodes.Contain(newNode)) && (!PreviousSearchNodes.Contain(newNode)))
                        {
                            if (newNode.TotalScore < SearchNodes.Select(newNode).TotalScore && newNode.Point != toEvaluate.Point)
                            {
                                SearchNodes.Remove(newNode);
                                SearchNodes.Insert(newNode);
                            }
                        }
                    }
                    SearchNodes.Remove(toEvaluate);
                    if (!PreviousSearchNodes.Contain(toEvaluate))
                    {
                        StoryMap[int.Parse(toEvaluate.Point.Split(',')[0]), int.Parse(toEvaluate.Point.Split(',')[1])] = "X";
                        PreviousSearchNodes.Insert(toEvaluate);
                    }
                }
            }
        }
        public static bool[,] AddPointPathMap(string point, bool[,] BoolMap)
        {
            bool[,] NewPathMap = new bool[n, n];
            NewPathMap = (bool[,])BoolMap.Clone();
            NewPathMap[int.Parse(point.Split(',')[0]), int.Parse(point.Split(',')[1])] = true;

            return NewPathMap;
        }
        public static MinPQPosition<Position> Positions = new MinPQPosition<Position>();
        public static MinPQPosition<Position> OldPositions = new MinPQPosition<Position>();
        public static bool[,] SitPathMap;
        public static int[,] HistoryMap;
        List<Node> OptimalPaths = new List<Node>();
        public void Part2(object input, bool test, ref object solution)
        {
            string inputText = (string)input;
            int nTest = 1;
            if (test)
            {
                switch (nTest)
                {
                    case 0:
                        break;
                    case 1:
                        inputText = "#################\r\n#...#...#...#..E#\r\n#.#.#.#.#.#.#.#.#\r\n#.#.#.#...#...#.#\r\n#.#.#.#.###.#.#.#\r\n#...#.#.#.....#.#\r\n#.#.#.#.#.#####.#\r\n#.#...#.#.#.....#\r\n#.#.#####.#.###.#\r\n#.#.#.......#...#\r\n#.#.###.#####.###\r\n#.#.#...#.....#.#\r\n#.#.#.#####.###.#\r\n#.#.#.........#.#\r\n#.#.#.#########.#\r\n#S#.............#\r\n#################";
                        break;
                }
            }
            n = inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None)[0].Length;
            Map = new string[n, n];
            StoryMap = new string[n, n];
            SitPathMap = new bool[n, n];
            HistoryMap = new int[n, n];//contiene di default -1, altrimenti lo step della prima volta arrivato lì
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    HistoryMap[i, j] = -1;
                }
            }
            int[] Position = new int[2];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Map[i, j] = inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None)[i].Substring(j, 1).ToString();
                    StoryMap[i, j] = inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None)[i].Substring(j, 1).ToString();
                    if (Map[i, j] == "S")
                    {
                        StartPosition[0] = i;
                        StartPosition[1] = j;
                        Map[i, j] = "X";
                        StoryMap[i, j] = "X";
                    }
                    if (Map[i, j] == "E")
                    {
                        EndPoint[0] = i;
                        EndPoint[1] = j;
                    }
                }
            }
            AOC.Utilities.Map.Map.ShowStringMap(Map);
            Direction direction = new Direction();
            direction = Direction.Right;

            Position position = new Position(StartPosition[0], StartPosition[1], Direction.Right, 0, 0, new bool[n, n]);
            //List<Position> Positions = new List<Position>();
            Positions = new MinPQPosition<Position>();
            //List<Position> OldPositions = new List<Position>();
            OldPositions = new MinPQPosition<Position>();
            Positions.Insert(position);
            List<Position> OptimalPaths = new List<Position>();
            bool target = false;
            int minPath = -1;
            Position toEvaluate;
            //while (!target)
            //while (!Positions.All(pos => pos.position[0] == EndPoint[0] && pos.position[1] == EndPoint[1]))
            while (!Positions.IsEmpty())
            {
                toEvaluate = Positions.Min();
                //AOC.Utilities.Map.Map.ShowBooleanMap(toEvaluate.story, n);

                if (toEvaluate.target)
                {
                    if (minPath == -1)
                    {
                        minPath = toEvaluate.score;
                    }
                    if (minPath == toEvaluate.score)
                    {
                        OptimalPaths.Add(toEvaluate);
                    }
                    Positions.Remove(toEvaluate);
                    //break;
                }
                else
                {
                    List<Position> NewPositions = new List<Position>();
                    NewPositions.AddRange(NextStep(toEvaluate));

                    foreach (var p in NewPositions)
                    {
                        if (!Positions.Contain(p))
                            if (!OldPositions.Contain(p))
                                Positions.Insert(p);
                    }

                    if (toEvaluate.target == false && toEvaluate.stopped == true)
                    {
                        OldPositions.Insert(toEvaluate);
                        Positions.Remove(toEvaluate);
                    }
                    //if (Positions.Any(p => p.stopped == true && p.target==false))
                    //{

                    //    OldPositions.AddRange(Positions.Where(p => p.stopped == true && p.target == false).Where(p=>!OldPositions.Any(op=>op.story.GetHashCode().Equals(p.story.GetHashCode()))));
                    //    Positions.RemoveAll(p => p.stopped == true && p.target == false);
                    //}
                }
            }
            //int minScore = Positions.Where(p=>p.target).OrderByDescending(p=>p.score).Last().score;
            //int minScore = Positions.Min().score;
            int minScore = OptimalPaths.FirstOrDefault().score;

            foreach (var p in OptimalPaths)
            {
                SitPathMap = AddSitPathMap(SitPathMap, p.story);
            }

            int count = 0;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (SitPathMap[i, j]) count++;
                }

            }
            solution = count;

            //Gestione Dikjstra
            /*SearchNodes = new MinPQNode<Node>();
            PreviousSearchNodes = new MinPQNode<Node>();
            SearchNodes.Insert(new Node($"{StartPosition[0].ToString()},{StartPosition[1].ToString()}",
                                           0,
                                           0,
                                           Direction.Right,
                                           true));
            int minPath = -1;
            Node toEvaluate;
            while (!SearchNodes.IsEmpty())
            {
                toEvaluate = SearchNodes.Min();
                ShowStringMap(StoryMap, toEvaluate.Point);
                if (toEvaluate.IsTarget)
                {
                    if (minPath == -1 || toEvaluate.TotalStep == minPath)
                    {
                        if (minPath == -1)
                        {
                            minPath = toEvaluate.TotalStep;
                        }
                        Node copiedNode = new Node(
                                    toEvaluate.Point,
                                    toEvaluate.TotalStep,
                                    toEvaluate.TurnedDirection,
                                    toEvaluate.direction,
                                    true,
                                    DeepCopyMap(toEvaluate.PathMap)
                                );

                        OptimalPaths.Add(copiedNode);
                    }
                    SearchNodes.DelMin();
                    //continue; // Evita ulteriori elaborazioni del nodo target
                }
                else
                {
                    foreach (Node node in toEvaluate.Neighbors)
                    {
                        Node newNode = new Node(node.Point,
                                                node.TotalStep,
                                                node.TurnedDirection,
                                                node.direction,
                                                true,
                                                node.PathMap);
                        AOC.Utilities.Map.Map.ShowBooleanMap(newNode.PathMap,n);

                        if (!SearchNodes.Contain(newNode) &&
                            !PreviousSearchNodes.Contain(newNode))
                        {
                            SearchNodes.Insert(newNode);
                        }
                        else if (PreviousSearchNodes.Contain(newNode))
                        {
                            // Aggiorna solo se troviamo un percorso migliore
                            if (newNode.TotalScore < PreviousSearchNodes.Select(newNode).TotalScore)
                            {
                                //PreviousSearchNodes.Remove(newNode);
                                SearchNodes.Insert(newNode);
                            }
                        }
                        else if (SearchNodes.Contain(newNode))
                        {
                            // Aggiorna solo se il nuovo punteggio è migliore
                            if (newNode.TotalScore < SearchNodes.Select(newNode).TotalScore)
                            {
                                //SearchNodes.Remove(newNode);
                                SearchNodes.Insert(newNode);
                            }
                        }
                    }
                    SearchNodes.Remove(toEvaluate);
                    if (!PreviousSearchNodes.Contain(toEvaluate))
                    {
                        StoryMap[int.Parse(toEvaluate.Point.Split(',')[0]), int.Parse(toEvaluate.Point.Split(',')[1])] = "X";
                        PreviousSearchNodes.Insert(toEvaluate);
                    }
                }
            }
            foreach (var path in OptimalPaths)
            {
                SitPathMap = AddSitPathMap(SitPathMap, path.PathMap);
            }
            foreach (var path in OptimalPaths)
            {
                Console.WriteLine($"Percorso con {path.TotalStep} passi:");
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        Console.Write(path.PathMap[i, j] ? "X" : ".");
                    }
                    Console.WriteLine();
                }
            }
            int c = 0;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (SitPathMap[i, j]) c++;
                }

            }
            solution = c;*/
        }
        public class MinPQPosition<Key> where Key : class
        {
            private Position[] pq;  // store items at indices 1 to n
            private int n;  // number of items on priority queue
            private IComparer<int> comparator;// optional comparator
            public MinPQPosition(int initCapacity)
            {
                pq = new Position[initCapacity + 1];
                n = 0;
            }

            public MinPQPosition() : this(1)
            {
            }

            public MinPQPosition(Position[] keys)
            {
                n = keys.Length;
                pq = new Position[keys.Length + 1];
                for (int i = 0; i < n; i++)
                    pq[i + 1] = keys[i];
                for (int k = n / 2; k >= 1; k--)
                    Sink(k);
                // Assert isMinHeap();
            }

            public bool IsEmpty() { return n == 0; }

            public int Size() { return n; }

            public Position Min()
            {
                if (IsEmpty()) throw new InvalidOperationException("Priority queue underflow");
                return pq[1];
            }

            // resize the underlying array to have the given capacity
            private void Resize(int capacity)
            {
                //Assert capacity > n
                if (capacity <= n) return;
                Position[] temp = new Position[capacity];
                for (int i = 1; i <= n; i++)
                {
                    temp[i] = pq[i];
                }
                pq = temp;
            }

            public void Insert(Position x)
            {
                if (n == pq.Length - 1) Resize(2 * pq.Length);

                pq[++n] = x;
                Swim(n);
                //Assert isMinHeap();
                if(HistoryMap[x.position[0], x.position[1]]!=-1) HistoryMap[x.position[0], x.position[1]] = x.step;
            }

            public Position DelMin()
            {
                if (IsEmpty()) throw new InvalidOperationException("Priority queue underflow");
                Position min = pq[1];
                Exchange(1, n--);
                Sink(1);
                pq[n + 1] = null;
                if ((n > 0) && (n == (pq.Length - 1) / 4)) Resize(pq.Length / 2);
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
                    return (((Position)tpq[i]).score).CompareTo(((Position)tpq[j]).score) > 0;
                }
                else
                {
                    return comparator.Compare(pq[i].score, pq[j].score) > 0;
                }
            }

            private void Exchange(int i, int j)
            {
                Position swap = pq[i];
                pq[i] = pq[j];
                pq[j] = swap;
            }

            // is pq[1..n] a min heap?
            private bool isMinHeap()
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
            // is subtree of pq[1..n] rooted at k a min heap?
            private bool isMinHeapOrdered(int k)
            {
                if (k > n) return true;
                int left = 2 * k;
                int right = 2 * k + 1;
                if (left <= n && Greater(k, left)) return false;
                if (right <= n && Greater(k, right)) return false;
                return isMinHeapOrdered(left) && isMinHeapOrdered(right);
            }
            public bool EqualMap(bool[,] Map1, bool[,] Map2)
            {
                int nl = Map1.GetLength(0);
                bool eq = true;
                for (int i = 0; i < nl; i++)
                {
                    for (int j = 0; j < nl; j++)
                    {
                        if (Map1[i, j] != Map2[i, j]) return false;
                    }
                }
                return eq;
            }
            public bool Contain(Position x)
            {
                dynamic t = x;
                dynamic tpq = pq;
                bool isContain = false;
                for (int i = 1; i <= n; i++)
                {
                    if (((Position)tpq[i]).position == ((Position)t).position &&
                        ((Position)tpq[i]).direction == ((Position)t).direction &&
                        ((Position)tpq[i]).story.GetHashCode().Equals(((Position)t[i]).story.GetHashCode()))
                    //((Position)tpq[i]).step != ((Position)t).step)
                    //EqualMap(((Node)tpq[i]).PathMap, ((Node)t).PathMap))
                    {
                        isContain = true;
                        break;
                    }
                }
                return isContain;
            }
            public bool Contain(int x, int y, Direction direction, int step)
            {
                dynamic t = x;
                dynamic tpq = pq;
                bool isContain = false;
                for (int i = 1; i <= n; i++)
                {
                    if (HistoryMap[x,y]!=-1 &&
                        HistoryMap[x,y]<step)
                    {
                        isContain = true;
                        break;
                    }
                }
                return isContain;
            }
            public void Remove(Position x)
            {
                dynamic t = x;
                dynamic tpq = pq;
                if (IsEmpty()) throw new InvalidOperationException("Priority queue underflow");
                for (int i = 1; i <= n; i++)
                {
                    if (((Position)tpq[i]).position == ((Position)t).position &&
                        ((Position)tpq[i]).direction == ((Position)t).direction &&
                        ((Position)tpq[i]).step == ((Position)t).step &&
                        ((Position)tpq[i]).score == ((Position)t).score &&
                        ((Position)tpq[i]).story.GetHashCode() == ((Position)t).story.GetHashCode())
                    {
                        for (int j = i + 1; j <= n; j++)
                        {
                            Exchange(j - 1, j);
                        }
                        n--;
                        Sink(1);
                        pq[n + 1] = null;
                        if ((n > 0) && (n == (pq.Length - 1) / 4)) Resize(pq.Length / 2);
                    }
                }
            }
            public Position Select(Key x)
            {
                Position result = new Position();
                dynamic t = x;
                dynamic tpq = pq;
                for (int i = 1; i <= n; i++)
                {
                    if (((Position)tpq[i]).position == ((Position)t).position &&
                        ((Position)tpq[i]).direction == ((Position)t).direction)
                    {
                        {
                            result = tpq[i];
                            break;
                        }
                    }
                }
                return result;
            }
        }

        public static int idExt = 0;
        public List<Position> NextStep(Position position)
        {
            List<Position> NewPositions = new List<Position>();
            int posX = position.position[0];
            int posY = position.position[1];
            Direction TryDirection = position.direction;
            int TurnedDirection = 0;
            int stepOld = position.step;
            bool[,] storyOld = new bool[n, n]; storyOld = (bool[,])position.story.Clone();
            bool Turned = false;
            for (int i = 0; i < 4; i++)
            {
                switch (TryDirection)
                {
                    case Direction.Right:
                        if (posY + 1 < n)
                            if (Map[posX, posY + 1] != "#" && position.story[posX, posY + 1] == false &&
                                (!Positions.Contain(posX, posY + 1, TryDirection, stepOld + 1)))
                                if (TurnedDirection == 0)
                                {
                                    position.position[1] = posY + 1;
                                    position.step += 1;
                                    position.score += 1;
                                    position.story[posX, posY + 1] = true;
                                    HistoryMap[posX, posY+1] = position.step;
                                }
                                else
                                {
                                    bool[,] newstory = new bool[n, n];
                                    newstory = (bool[,])storyOld.Clone();
                                    NewPositions.Add(new Position(posX, posY + 1, TryDirection, stepOld + 1, position.turnedDirection + TurnedDirection, newstory));
                                }

                        break;
                    case Direction.Down:
                        if (posX + 1 < n)
                            if (Map[posX + 1, posY] != "#" && position.story[posX + 1, posY] == false &&
                                (!Positions.Contain(posX + 1, posY, TryDirection, stepOld + 1)))
                                if (TurnedDirection == 0)
                                {
                                    position.position[0] = posX + 1;
                                    position.step += 1;
                                    position.score += 1;
                                    position.story[posX + 1, posY] = true;
                                    HistoryMap[posX+1, posY] = position.step;
                                }
                                else
                                {
                                    bool[,] newstory = new bool[n, n];
                                    newstory = (bool[,])storyOld.Clone();
                                    NewPositions.Add(new Position(posX + 1, posY, TryDirection, stepOld + 1, position.turnedDirection + TurnedDirection, newstory));
                                }

                        break;
                    case Direction.Left:
                        if (posY - 1 >= 0)
                            if (Map[posX, posY - 1] != "#" && position.story[posX, posY - 1] == false &&
                                (!Positions.Contain(posX, posY - 1, TryDirection, stepOld + 1)))
                                if (TurnedDirection == 0)
                                {
                                    position.position[1] = posY - 1;
                                    position.step += 1;
                                    position.score += 1;
                                    position.story[posX, posY - 1] = true;
                                    HistoryMap[posX, posY - 1] = position.step;
                                }
                                else
                                {
                                    bool[,] newstory = new bool[n, n];
                                    newstory = (bool[,])storyOld.Clone();
                                    NewPositions.Add(new Position(posX, posY - 1, TryDirection, stepOld + 1, position.turnedDirection + TurnedDirection, newstory));
                                }

                        break;
                    case Direction.Up:
                        if (posX - 1 >= 0)
                            if (Map[posX - 1, posY] != "#" && position.story[posX - 1, posY] == false &&
                                (!Positions.Contain(posX - 1, posY, TryDirection, stepOld + 1)))
                                if (TurnedDirection == 0)
                                {
                                    position.position[0] = posX - 1;
                                    position.step += 1;
                                    position.score += 1;
                                    position.story[posX - 1, posY] = true;
                                    HistoryMap[posX-1, posY] = position.step;
                                }
                                else
                                {
                                    bool[,] newstory = new bool[n, n];
                                    newstory = (bool[,])storyOld.Clone();
                                    NewPositions.Add(new Position(posX - 1, posY, TryDirection, stepOld + 1, position.turnedDirection + TurnedDirection, newstory));
                                }

                        break;
                }
                TryDirection = (Direction)(((int)TryDirection + 1) % 4);

                if ((int)TryDirection == ((int)position.direction + 2) % 4)
                {
                    TurnedDirection = 2;
                }
                else if (TryDirection == position.direction)
                {
                    TurnedDirection = 0;
                }
                else
                {
                    TurnedDirection = 1;
                }
            }
            if (position.position[0] == posX && position.position[1] == posY) position.stopped = true;
            if (position.position[0] == EndPoint[0] && position.position[1] == EndPoint[1]) position.target = true;
            //foreach (var pos in NewPositions)
            //{
            //    var sco = pos.score.ToString();
            //    var stepStr = pos.step.ToString();

            //    // Prendi le ultime cifre di score con la stessa lunghezza di step
            //    var lastDigitsOfScore = sco.Substring(Math.Max(0, sco.Length - stepStr.Length));

            //    if (!lastDigitsOfScore.Equals(stepStr))
            //    {
            //        // Aggiungi il tuo codice se la condizione non è soddisfatta
            //        Console.WriteLine($"Score: {pos.score}, Step: {pos.step} -> Non corrispondono!");
            //    }
            //}
            if (NewPositions.Any(p => p.id == 193))
            {

            }

            return NewPositions;
        }

        public static int GetNextId()
        {
            idExt++;
            return idExt;
        }
        public class Position
        {
            public Position()
            {

            }
            public Position(int x, int y, Direction direction, int step, int turnedDirection, bool[,] story)
            {
                this.position[0] = x; this.position[1] = y;
                this.direction = direction;
                this.step = step;
                this.turnedDirection = turnedDirection;
                this.score = this.step + turnedDirection * 1000;
                this.story = story; this.story[x, y] = true;
                this.target = false;
                this.id = GetNextId();
            }

            public int[] position = new int[2];
            public bool[,] story = new bool[n, n];
            public Direction direction = new Direction();
            public int step = new int();
            public int turnedDirection = new int();
            public int score = new int();
            public bool stopped = false;
            public bool target = false;
            public int id;
        }

        private bool[,] DeepCopyMap(bool[,] original)
        {
            int rows = original.GetLength(0);
            int cols = original.GetLength(1);
            bool[,] copy = new bool[rows, cols];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    copy[i, j] = original[i, j];
                }
            }
            return copy;
        }

        public bool[,] AddSitPathMap(bool[,] OldPathMap, bool[,] PathMap)
        {
            bool[,] NewPathMap = new bool[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (PathMap[i, j]) NewPathMap[i, j] = true;
                    else NewPathMap[i, j] = OldPathMap[i, j];
                }
            }

            return NewPathMap;
        }
        public static void ShowStringMap(string[,] Map, string point)
        {
            int dimX = Map.GetLength(0); // Numero di righe
            int dimY = Map.GetLength(1); // Numero di colonne

            // Costruire l'intestazione delle colonne
            string tensRow = "   "; // Riga delle decine
            string unitsRow = "   "; // Riga delle unità

            for (int c = 0; c < dimY; c++)
            {
                tensRow += (c / 10) > 0 ? (c / 10).ToString() : " "; // Decine o spazio vuoto
                unitsRow += (c % 10).ToString();                     // Unità
            }

            // Stampare intestazione
            Console.WriteLine(tensRow); // Riga decine
            Console.WriteLine(unitsRow); // Riga unità

            // Stampare la mappa
            for (int r = 0; r < dimX; r++)
            {
                // Stampa l'indice di riga (con spaziatura per allineamento)
                Console.Write(r.ToString().PadLeft(2) + " ");
                for (int c = 0; c < dimY; c++)
                {
                    if (r == int.Parse(point.Split(',')[0]) && c == int.Parse(point.Split(',')[1])) Console.ForegroundColor = ConsoleColor.Green;
                    else Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(Map[r, c]);
                }
                Console.WriteLine(); // Nuova riga
            }
        }
        public class MinPQNode<Key> where Key : class
        {
            private Node[] pq;  // store items at indices 1 to n
            private int n;  // number of items on priority queue
            private IComparer<int> comparator;// optional comparator
            public MinPQNode(int initCapacity)
            {
                pq = new Node[initCapacity + 1];
                n = 0;
            }

            public MinPQNode() : this(1)
            {
            }

            public MinPQNode(Node[] keys)
            {
                n = keys.Length;
                pq = new Node[keys.Length + 1];
                for (int i = 0; i < n; i++)
                    pq[i + 1] = keys[i];
                for (int k = n / 2; k >= 1; k--)
                    Sink(k);
                // Assert isMinHeap();
            }

            public bool IsEmpty() { return n == 0; }

            public int Size() { return n; }

            public Node Min()
            {
                if (IsEmpty()) throw new InvalidOperationException("Priority queue underflow");
                return pq[1];
            }

            // resize the underlying array to have the given capacity
            private void Resize(int capacity)
            {
                //Assert capacity > n
                if (capacity <= n) return;
                Node[] temp = new Node[capacity];
                for (int i = 1; i <= n; i++)
                {
                    temp[i] = pq[i];
                }
                pq = temp;
            }

            public void Insert(Node x)
            {
                if (n == pq.Length - 1) Resize(2 * pq.Length);

                pq[++n] = x;
                Swim(n);
                //Assert isMinHeap();
            }

            public Node DelMin()
            {
                if (IsEmpty()) throw new InvalidOperationException("Priority queue underflow");
                Node min = pq[1];
                Exchange(1, n--);
                Sink(1);
                pq[n + 1] = null;
                if ((n > 0) && (n == (pq.Length - 1) / 4)) Resize(pq.Length / 2);
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
                    return (((Node)tpq[i]).TotalScore).CompareTo(((Node)tpq[j]).TotalScore) > 0;
                }
                else
                {
                    return comparator.Compare(pq[i].TotalScore, pq[j].TotalScore) > 0;
                }
            }

            private void Exchange(int i, int j)
            {
                Node swap = pq[i];
                pq[i] = pq[j];
                pq[j] = swap;
            }

            // is pq[1..n] a min heap?
            private bool isMinHeap()
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
            // is subtree of pq[1..n] rooted at k a min heap?
            private bool isMinHeapOrdered(int k)
            {
                if (k > n) return true;
                int left = 2 * k;
                int right = 2 * k + 1;
                if (left <= n && Greater(k, left)) return false;
                if (right <= n && Greater(k, right)) return false;
                return isMinHeapOrdered(left) && isMinHeapOrdered(right);
            }
            public bool EqualMap(bool[,] Map1, bool[,] Map2)
            {
                int nl = Map1.GetLength(0);
                bool eq = true;
                for (int i = 0; i < nl; i++)
                {
                    for (int j = 0; j < nl; j++)
                    {
                        if (Map1[i, j] != Map2[i, j]) return false;
                    }
                }
                return eq;
            }
            public bool Contain(Node x)
            {
                dynamic t = x;
                dynamic tpq = pq;
                bool isContain = false;
                for (int i = 1; i <= n; i++)
                {
                    if (((Node)tpq[i]).Point == ((Node)t).Point &&
                        ((Node)tpq[i]).direction == ((Node)t).direction &&
                        ((Node)tpq[i]).PathMap[int.Parse(((Node)t).Point.Split(',')[0]), int.Parse(((Node)t).Point.Split(',')[1])] == true)
                    //EqualMap(((Node)tpq[i]).PathMap, ((Node)t).PathMap))
                    {
                        isContain = true;
                        break;
                    }
                }
                return isContain;
            }
            public bool Contain(string point, Direction direction)
            {
                //dynamic t = x;
                dynamic tpq = pq;
                bool isContain = false;
                //int puzzleSize = ((Board)x).N; // Update with your Board class property
                //int puzzleSize = ((Board)t).n; // Update with your Board class property
                for (int i = 1; i <= n; i++)
                {
                    //if (((Board)pq[i]).ManhattanMeasure == ((Board)x).ManhattanMeasure &&
                    //    ((Board)pq[i]).HammingMeasure == ((Board)x).HammingMeasure)
                    //if (((Node)tpq[i]).point == ((Node)t[i]).point &&
                    //    ((Node)tpq[i]).direction == ((Node)t[i]).direction)
                    if (((Node)tpq[i]).Point == point &&
                        ((Node)tpq[i]).direction == direction)
                    {
                        isContain = true;
                        break;
                    }
                }
                return isContain;
            }
            public void Remove(Node x)
            {
                dynamic t = x;
                dynamic tpq = pq;
                if (IsEmpty()) throw new InvalidOperationException("Priority queue underflow");
                for (int i = 1; i <= n; i++)
                {
                    if (((Node)tpq[i]).Point == ((Node)t).Point &&
                        ((Node)tpq[i]).direction == ((Node)t).direction &&
                        ((Node)tpq[i]).TotalStep == ((Node)t).TotalStep &&
                        ((Node)tpq[i]).TotalScore == ((Node)t).TotalScore &&
                        ((Node)tpq[i]).PathMap.GetHashCode() == ((Node)t).PathMap.GetHashCode())
                    {
                        for (int j = i + 1; j <= n; j++)
                        {
                            Exchange(j - 1, j);
                        }
                        n--;
                        Sink(1);
                        pq[n + 1] = null;
                        if ((n > 0) && (n == (pq.Length - 1) / 4)) Resize(pq.Length / 2);
                    }
                }
            }
            public Node Select(Key x)
            {
                Node result = new Node();
                dynamic t = x;
                dynamic tpq = pq;
                for (int i = 1; i <= n; i++)
                {
                    if (((Node)tpq[i]).Point == ((Node)t).Point &&
                        ((Node)tpq[i]).direction == ((Node)t).direction)
                    {
                        {
                            result = tpq[i];
                            break;
                        }
                    }
                }
                return result;
            }
        }
        public enum Direction
        {
            Up,
            Right,
            Down,
            Left
        }
        public class Node
        {
            public Node() { }
            public Node(string Point, int Step, int TurnedDirection, Direction direction, bool findNeighbors = true, bool[,] PathMap = null)
            {
                this.Point = Point;
                this.TotalStep = Step;
                this.TurnedDirection += TurnedDirection;
                this.TotalScore = this.TotalStep + this.TurnedDirection * 1000;
                FindMeasure(this.Point, this.TotalStep);
                this.IsTarget = FindIsTarget();
                this.direction = direction;
                if (PathMap == null)
                {
                    PathMap = new bool[n, n];
                }
                this.PathMap = AddPointPathMap(Point, PathMap);
                if (findNeighbors) this.Neighbors = ExplorePositions();
            }
            public string Point { get; set; }
            public int TotalStep { get; set; }
            public int TotalScore { get; set; }
            public int TurnedDirection { get; set; }
            public int ManhattanMeasure { get; set; }
            public bool IsTarget { get; set; }
            public Direction direction { get; set; }
            public bool[,] PathMap = new bool[n, n];
            public List<Node> Neighbors = new List<Node>();
            private void FindMeasure(string point, int step)
            {
                var stringpoint = point.Split(',');
                ManhattanMeasure = Math.Abs(EndPoint[0] - int.Parse(stringpoint[0])) + Math.Abs(EndPoint[1] - int.Parse(stringpoint[1]));
            }
            private bool FindIsTarget()
            {
                var stringpoint = this.Point.Split(',');
                if (stringpoint[0] == EndPoint[0].ToString() && stringpoint[1] == EndPoint[1].ToString()) return true;
                else return false;
            }
            private List<Node> ExplorePositions()
            {
                List<Node> neighborsList = new List<Node>();
                string stringpoint = this.Point;
                int posX = int.Parse(stringpoint.Split(',')[0]);
                int posY = int.Parse(stringpoint.Split(',')[1]);
                Direction TryDirection = this.direction;
                int TurnedDirection = 0;
                bool Turned = false;
                for (int i = 0; i < 4; i++)
                {
                    switch (TryDirection)
                    {
                        case Direction.Right:
                            if (posY + 1 < n)
                                if (Map[posX, posY + 1] != "#" &&
                                    !SearchNodes.Contain(new Node($"{posX},{posY + 1}",
                                                              this.TotalStep + 1,
                                                              this.TurnedDirection + TurnedDirection,
                                                              TryDirection,
                                                              false,
                                                              AddPointPathMap($"{posX},{posY + 1}", this.PathMap))) &&
                                    !PreviousSearchNodes.Contain(new Node($"{posX},{posY + 1}",
                                                              this.TotalStep + 1,
                                                              this.TurnedDirection + TurnedDirection,
                                                              TryDirection,
                                                              false,
                                                              AddPointPathMap($"{posX},{posY + 1}", this.PathMap))))
                                {
                                    neighborsList.Add(new Node($"{posX},{posY + 1}",
                                                              this.TotalStep + 1,
                                                              this.TurnedDirection + TurnedDirection,
                                                              TryDirection,
                                                              false,
                                                              AddPointPathMap($"{posX},{posY + 1}", this.PathMap)));
                                }
                            break;
                        case Direction.Down:
                            if (posX + 1 < n)
                                if (Map[posX + 1, posY] != "#" &&
                                    !SearchNodes.Contain(new Node($"{posX + 1},{posY}",
                                                                this.TotalStep + 1,
                                                                this.TurnedDirection + TurnedDirection,
                                                                TryDirection,
                                                                false,
                                                                AddPointPathMap($"{posX + 1},{posY}", this.PathMap))) &&
                                    !PreviousSearchNodes.Contain(new Node($"{posX + 1},{posY}",
                                                                this.TotalStep + 1,
                                                                this.TurnedDirection + TurnedDirection,
                                                                TryDirection,
                                                                false,
                                                                AddPointPathMap($"{posX + 1},{posY}", this.PathMap))))
                                {
                                    neighborsList.Add(new Node($"{posX + 1},{posY}",
                                                                this.TotalStep + 1,
                                                                this.TurnedDirection + TurnedDirection,
                                                                TryDirection,
                                                                false,
                                                                AddPointPathMap($"{posX + 1},{posY}", this.PathMap)));
                                }

                            break;
                        case Direction.Left:
                            if (posY - 1 > 0)
                                if (Map[posX, posY - 1] != "#" &&
                                    !SearchNodes.Contain(new Node($"{posX},{posY - 1}",
                                                        this.TotalStep + 1,
                                                        this.TurnedDirection + TurnedDirection,
                                                        TryDirection,
                                                        false,
                                                        AddPointPathMap($"{posX},{posY - 1}", this.PathMap))) &&
                                    !PreviousSearchNodes.Contain(new Node($"{posX},{posY - 1}",
                                                        this.TotalStep + 1,
                                                        this.TurnedDirection + TurnedDirection,
                                                        TryDirection,
                                                        false,
                                                        AddPointPathMap($"{posX},{posY - 1}", this.PathMap))))
                                {
                                    neighborsList.Add(new Node($"{posX},{posY - 1}",
                                                        this.TotalStep + 1,
                                                        this.TurnedDirection + TurnedDirection,
                                                        TryDirection,
                                                        false,
                                                        AddPointPathMap($"{posX},{posY - 1}", this.PathMap)));
                                }

                            break;
                        case Direction.Up:
                            if (posX - 1 > 0)
                                if (Map[posX - 1, posY] != "#" &&
                                    !SearchNodes.Contain(new Node($"{posX - 1},{posY}",
                                                                this.TotalStep + 1,
                                                                this.TurnedDirection + TurnedDirection,
                                                                TryDirection,
                                                                false,
                                                                AddPointPathMap($"{posX - 1},{posY}", this.PathMap))) &&
                                    !PreviousSearchNodes.Contain(new Node($"{posX - 1},{posY}",
                                                                this.TotalStep + 1,
                                                                this.TurnedDirection + TurnedDirection,
                                                                TryDirection,
                                                                false,
                                                                AddPointPathMap($"{posX - 1},{posY}", this.PathMap))))
                                {
                                    neighborsList.Add(new Node($"{posX - 1},{posY}",
                                                                this.TotalStep + 1,
                                                                this.TurnedDirection + TurnedDirection,
                                                                TryDirection,
                                                                false,
                                                                AddPointPathMap($"{posX - 1},{posY}", this.PathMap)));
                                }

                            break;
                    }
                    TryDirection = (Direction)(((int)TryDirection + 1) % 4);

                    if ((int)TryDirection == ((int)this.direction + 2) % 4)
                    {
                        TurnedDirection = 2;
                    }
                    else if (TryDirection == this.direction)
                    {
                        TurnedDirection = 0;
                    }
                    else
                    {
                        TurnedDirection = 1;
                    }
                }

                return neighborsList;
            }
        }
    }

}
