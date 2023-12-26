using AOC;
using AOC.DataStructures.Clustering;
using AOC.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AOC2023
{
    public class Day25 : Solver, IDay
    {
        public class WeightedQuickUnionFindUFWires
        {
            private int[] parent;   // parent[i] = parent of i
            private int[] size;     // size[i] = number of elements in subtree rooted at i
            private int count;      // number of components

            public WeightedQuickUnionFindUFWires(int n)
            {
                count = n;
                parent = new int[n];
                size = new int[n];
                for (int i = 0; i < n; i++)
                {
                    parent[i] = i;
                    size[i] = 1;
                }
            }

            public int Count()
            {
                return count;
            }

            public int Find(int p)
            {
                Validate(p);
                while (p != parent[p])
                    p = parent[p];
                return p;
            }

            private void Validate(int p)
            {
                int n = parent.Length;
                if (p < 0 || p >= n)
                {
                    throw new ArgumentException("index " + p + " is not between 0 and " + (n - 1));
                }
            }

            public bool Connected(int p, int q)
            {
                return Find(p) == Find(q);
            }

            public void Union(int p, int q)
            {
                int rootP = Find(p);
                int rootQ = Find(q);
                if (rootP == rootQ) return;

                // make smaller root point to larger one
                if (size[rootP] < size[rootQ])
                {
                    parent[rootP] = rootQ;
                    size[rootQ] += size[rootP];
                }
                else
                {
                    parent[rootQ] = rootP;
                    size[rootP] += size[rootQ];
                }
                count--;
            }
        }
        public void Part1(object input, bool test, ref object solution)
        {
            List<string> inputList = new List<string>();
            inputList = (List<string>)input;
            List<string> inputIntList = new List<string>();
            inputIntList = inputList;
            Dictionary<string, int> Parts = new Dictionary<string, int>();

            foreach (string item in inputList)
            {
                var a = item.Split(':')[0];
                if (!Parts.ContainsKey(a))
                {
                    Parts.Add(a, Parts.Count());
                }
                var b = item.Split(':')[1].Trim().Split(' ');
                foreach (string item2 in b)
                {
                    if (!Parts.ContainsKey(item2))
                    {
                        Parts.Add(item2, Parts.Count());
                    }
                }
            }

            foreach(var item in Parts.Keys)
            {
                for (int r = 0; r < inputIntList.Count; r++)
                {
                    inputIntList[r]=inputIntList[r].Replace(item, Parts[item].ToString());
                }

            }

            int n = Parts.Count();
            WeightedQuickUnionFindUFWires uf = new WeightedQuickUnionFindUFWires(n);
            foreach (string item in inputList)
            {
                var a = item.Split(':')[0];
                var b = item.Split(':')[1].Trim().Split(' ');
                foreach (string item2 in b)
                {
                    uf.Union(Parts[a], Parts[item2]);


                }
            }

        }
        public static int clustNumber { get; set; }
        public static void Connect(Node nodeA, Node nodeB)
        {
            nodeA.Wired.Add(nodeB);
            nodeB.Wired.Add(nodeA);
            if (nodeA.cluster != 0)
            {
                nodeB.cluster = nodeA.cluster;
            }
            else if (nodeB.cluster != 0)
            {
                nodeA.cluster = nodeB.cluster;
            }
            else
            {
                clustNumber++;
                nodeA.cluster = clustNumber;
                nodeB.cluster = nodeA.cluster;
            }
        }
        public class Node
        {
            public int cluster { get; set; }
            public string name { get; set; }
            public Node() { }
            public List<Node> Wired = new List<Node>();
            public Node(string name)
            {
                this.name = name;
            }

        }


        public void Part2(object input, bool test, ref object solution)
        {
        }




    }
}
