using AOC;
using AOC.DataStructures.Clustering;
using AOC.DataStructures.PriorityQueue;
using AOC.Model;
using AOC.SearchAlghoritmhs;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using static AOC.SearchAlghoritmhs.ResearchAlghoritmsAttribute;
using Solver = AOC.Solver;

namespace AOC2015
{
    [ResearchAlghoritmsAttribute(TypologyEnum.Dijkstra)]
    public class Day9 : Solver, IDay
    {
        public MinPQPath<Path> SearchNodes = new MinPQPath<Path>();
        public class Node
        {
            public Node(string name) { this.name = name; }
            public string name { get; set; }
            public List<Neighbor> Neighbors = new List<Neighbor>();

        }
        public class Neighbor
        {
            public Neighbor(Node node, int distance)
            {
                this.name = node.name;
                this.distance = distance;
                this.Node = node;
            }
            public string name { get; set; }
            public int distance { get; set; }
            public Node Node { get; set; }
        }

        public class Path
        {
            public Node node { get; set; }
            public int TotalDistance { get; set; }
            public string Destination { get; set; }
            public int Places { get; set; }
        }
        public class MinPQPath<Key> where Key : class
        {
            private Path[] pq;  // store items at indices 1 to n
            private int n;  // number of items on priority queue
            private IComparer<int> comparator;// optional comparator
            public MinPQPath(int initCapacity)
            {
                pq = new Path[initCapacity + 1];
                n = 0;
            }

            public MinPQPath() : this(1)
            {
            }

            public MinPQPath(Path[] keys)
            {
                n = keys.Length;
                pq = new Path[keys.Length + 1];
                for (int i = 0; i < n; i++)
                    pq[i + 1] = keys[i];
                for (int k = n / 2; k >= 1; k--)
                    Sink(k);
                // Assert isMinHeap();
            }

            public bool IsEmpty() { return n == 0; }

            public int Size() { return n; }

            public Path Min()
            {
                if (IsEmpty()) throw new InvalidOperationException("Priority queue underflow");
                return pq[1];
            }

            // resize the underlying array to have the given capacity
            private void Resize(int capacity)
            {
                //Assert capacity > n
                if (capacity <= n) return;
                Path[] temp = new Path[capacity];
                for (int i = 1; i <= n; i++)
                {
                    temp[i] = pq[i];
                }
                pq = temp;
            }

            public void Insert(Path x)
            {
                if (n == pq.Length - 1) Resize(2 * pq.Length);

                pq[++n] = x;
                Swim(n);
                //Assert isMinHeap();
            }

            public Path DelMin()
            {
                if (IsEmpty()) throw new InvalidOperationException("Priority queue underflow");
                Path min = pq[1];
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
                    return (((Path)tpq[i]).TotalDistance).CompareTo(((Path)tpq[j]).TotalDistance) > 0;
                }
                else
                {
                    return comparator.Compare(pq[i].TotalDistance, pq[j].TotalDistance) > 0;
                }
            }

            private void Exchange(int i, int j)
            {
                Path swap = pq[i];
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
        }

        public Dictionary<string, Node> Cities = new Dictionary<string, Node>();
        public void AddNode(string name)
        {
            if (!Cities.ContainsKey(name)) Cities.Add(name, new Node(name));

        }
        public void AddDistance(string nameA, string nameB, int distance)
        {
            Neighbor neighborA = new Neighbor(Cities[nameB], distance);
            Neighbor neighborB = new Neighbor(Cities[nameA], distance);
            Cities[nameA].Neighbors.Add(neighborA);
            Cities[nameB].Neighbors.Add(neighborB);

        }
        
        public void Part1(object input, bool test, ref object solution)
        {
            string inputText = (string)input;
            var list = inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None);

            foreach (var item in list)
            {
                if(!string.IsNullOrEmpty(item)) { 
                var line = item.Split(Delimiter.delimiter_space, StringSplitOptions.None);
                AddNode(line[0]);
                AddNode(line[2]);
                AddDistance(line[0], line[2], int.Parse(line[4]));
                }
            }
            int min = int.MaxValue;
            SearchNodes = new MinPQPath<Path>();
            MinPQPath<Path> PreviousSearchNodes = new MinPQPath<Path>();
            Path toEvaluate = new Path();
            StartCities();
            while (!SearchNodes.IsEmpty())
            {
                toEvaluate = SearchNodes.Min();
                if (toEvaluate.Places < Cities.Count)
                {
                    NextMove(toEvaluate);
                }
                else
                {
                    if(toEvaluate.TotalDistance<min) min=toEvaluate.TotalDistance;
                    SearchNodes.DelMin();
                }
            }
            solution = min;
        }
        public void NextMove(Path toEvaluate)
        {
            int min = int.MaxValue;
            string c_min = "";
            foreach(var item in toEvaluate.node.Neighbors)
            {
                if(!toEvaluate.Destination.Contains(item.name))
                if (item.distance < min) { min = item.distance; c_min = item.Node.name; }
            }

            Path np = new Path();
            np.Destination = toEvaluate.Destination + "-" + c_min;
            np.TotalDistance = toEvaluate.TotalDistance+min;
            np.Places = toEvaluate.Places+1;
            np.node = Cities[c_min];
            SearchNodes.DelMin();
            SearchNodes.Insert(np);
        }
        public void NextMove2(Path toEvaluate)
        {
            int max = int.MinValue;
            string c_min = "";
            foreach (var item in toEvaluate.node.Neighbors)
            {
                if (!toEvaluate.Destination.Contains(item.name))
                    if (item.distance > max) { max = item.distance; c_min = item.Node.name; }
            }

            Path np = new Path();
            np.Destination = toEvaluate.Destination + "-" + c_min;
            np.TotalDistance = toEvaluate.TotalDistance + max;
            np.Places = toEvaluate.Places + 1;
            np.node = Cities[c_min];
            SearchNodes.DelMin();
            SearchNodes.Insert(np);
        }
        public void StartCities()
        {
            foreach(var c in Cities)
            {
                Path np = new Path();
                np.node = Cities[c.Value.name];
                np.Destination += c.Value.name;
                np.Places = 1;
                SearchNodes.Insert(np);
            }
        }
        public void Part2(object input, bool test, ref object solution)
        {
            string inputText = (string)input;
            var list = inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None);

            foreach (var item in list)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    var line = item.Split(Delimiter.delimiter_space, StringSplitOptions.None);
                    AddNode(line[0]);
                    AddNode(line[2]);
                    AddDistance(line[0], line[2], int.Parse(line[4]));
                }
            }
            int max = int.MinValue;
            SearchNodes = new MinPQPath<Path>();
            MinPQPath<Path> PreviousSearchNodes = new MinPQPath<Path>();
            Path toEvaluate = new Path();
            StartCities();
            while (!SearchNodes.IsEmpty())
            {
                toEvaluate = SearchNodes.Min();
                if (toEvaluate.Places < Cities.Count)
                {
                    NextMove2(toEvaluate);
                }
                else
                {
                    if (toEvaluate.TotalDistance > max) max = toEvaluate.TotalDistance;
                    SearchNodes.DelMin();
                }
            }
            solution = max;
        }
    }
}
