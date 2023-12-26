using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AOC.DataStructures.Clustering
{
    public class WeightedQuickUnionFindUF
    {
        private int[] parent;   // parent[i] = parent of i
        private int[] size;     // size[i] = number of elements in subtree rooted at i
        private int count;      // number of components

        public WeightedQuickUnionFindUF(int n)
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

        public static void Example()
        {
            long startTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();

            string inputName = "tinyUF.txt"; //"largeUF.txt"; ////"mediumUF.txt";//
            string esercizio = "WeightedQuickUnionFindUF";

            using (StreamWriter fw = new StreamWriter($"../../DataStructures/IO/Output/{esercizio}_{inputName}"))
            {
                fw.WriteLine($"Questo è il risultato dell'esercizio {esercizio} con input {inputName} :");

                using (StreamReader reader = new StreamReader($"../../DataStructures/IO/Input/{inputName}"))
                {
                    int i = 0;
                    string line = reader.ReadLine();
                    WeightedQuickUnionFindUF uf = new WeightedQuickUnionFindUF(int.Parse(line));
                    line = reader.ReadLine();
                    while (line != null)
                    {
                        int p = int.Parse(line.Split(' ')[0]);
                        int q = int.Parse(line.Split(' ')[1]);
                        line = reader.ReadLine();
                        if (uf.Connected(p, q)) continue;
                        i++;
                        uf.Union(p, q);
                        if (i % 100 == 0)
                        {
                            Console.WriteLine(i + " " + p + " " + q);
                            fw.WriteLine(i + " " + p + " " + q);
                        }
                    }

                    fw.WriteLine("Il numero di cluster è: " + uf.Count());
                    long endTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                    long timeElapsed = endTime - startTime;

                    fw.WriteLine($"Execution time in milliseconds: {timeElapsed}");
                    fw.WriteLine($"Execution time in seconds: {timeElapsed / 1000}");
                }
            }
        }
    }
}
