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
using System;
using System.Collections.Generic;
using System.Linq;
using AOC.DataStructures.Clustering;
using AOC.DataStructures.ParsingInput;

namespace AOC2025
{
    [ResearchAlgorithmsAttribute(TypologyEnum.Clustering)]
    public class Day8 : Solver, IDay
    {
        public class Point3D
        {
            public int X;
            public int Y;
            public int Z;

            public Point3D(int x, int y, int z)
            {
                X = x; Y = y; Z = z;
            }
        }

        public class Edge
        {
            public int A;
            public int B;
            public double Dist;

            public Edge(int a, int b, double d)
            {
                A = a; B = b; Dist = d;
            }
        }
       
        public void Part1(object input, bool test, ref object solution)
        {
            string inputText = (string)input;

            List<Point3D> points = Input3DPoint.Parse(inputText);
            int n = points.Count;

            // Genera coppie (N^2)
            List<Edge> edges = new List<Edge>(n * (n - 1) / 2);

            for (int i = 0; i < n; i++)
            {
                for (int j = i + 1; j < n; j++)
                {
                    edges.Add(new Edge(
                        i,
                        j,
                        Distance(points[i], points[j])
                    ));
                }
            }

            // Ordina le distanze crescenti
            edges.Sort(delegate (Edge e1, Edge e2)
            {
                return e1.Dist.CompareTo(e2.Dist);
            });

            WeightedQuickUnionFindUF uf = new WeightedQuickUnionFindUF(n);

            int K = (test) ? 10 : 1000;
            int limit = Math.Min(K, edges.Count);

            for (int i = 0; i < limit; i++)
            {
                uf.Union(edges[i].A, edges[i].B);
            }

            // Calcola cluster
            Dictionary<int, int> clusterSizes = new Dictionary<int, int>();

            for (int i = 0; i < n; i++)
            {
                int root = uf.Find(i);
                if (!clusterSizes.ContainsKey(root))
                    clusterSizes[root] = 0;
                clusterSizes[root]++;
            }

            // Prendi le 3 più grandi
            List<int> largest3 = clusterSizes.Values
                .OrderByDescending(v => v)
                .Take(3)
                .ToList();

            long result = (long)largest3[0] * largest3[1] * largest3[2];
            solution = result;
        }

        public void Part2(object input, bool test, ref object solution)
        {
            string inputText = (string)input;

            List<Point3D> points = Input3DPoint.Parse(inputText);
            int n = points.Count;

            // Genera coppie (N^2)
            List<Edge> edges = new List<Edge>(n * (n - 1) / 2);

            for (int i = 0; i < n; i++)
            {
                for (int j = i + 1; j < n; j++)
                {
                    edges.Add(new Edge(
                        i,
                        j,
                        Distance(points[i], points[j])
                    ));
                }
            }

            // Ordina le distanze crescenti
            edges.Sort(delegate (Edge e1, Edge e2)
            {
                return e1.Dist.CompareTo(e2.Dist);
            });

            WeightedQuickUnionFindUF uf = new WeightedQuickUnionFindUF(n);

            int limit = edges.Count;

            for (int i = 0; i < limit; i++)
            {
                uf.Union(edges[i].A, edges[i].B);
                if (uf.IsAllConnected())
                {
                    solution = points[edges[i].A].X * points[edges[i].B].X;
                    break;
                }
            }

            
        }

        

        private static double Distance(Point3D a, Point3D b)
        {
            long dx = a.X - b.X;
            long dy = a.Y - b.Y;
            long dz = a.Z - b.Z;
            return Math.Sqrt(dx * dx + dy * dy + dz * dz);
        }
    }
}