using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AOC.DataStructures.Clustering
{
    public class QuickUnionFindUF
    {
        private int[] parent;  // parent[i] = parent of i
        private int count;     // number of components

        public QuickUnionFindUF(int n)
        {
            parent = new int[n];
            count = n;
            for (int i = 0; i < n; i++)
            {
                parent[i] = i;
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
            parent[rootP] = rootQ;
            count--;
        }

        public static void Example()
        {
            long startTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();

            string inputName = "largeUF.txt"; //"mediumUF.txt";//"tinyUF.txt";
            string esercizio = "QuickUnionFindUF";

            using (StreamWriter fw = new StreamWriter($"../../DataStructures/IO/Output/{esercizio}_{inputName}"))
            {
                fw.WriteLine($"Questo è il risultato dell'esercizio {esercizio} con input {inputName} :");

                using (StreamReader reader = new StreamReader($"../../DataStructures/IO/Input/{inputName}"))
                {
                    int i = 0;
                    string line = reader.ReadLine();
                    QuickUnionFindUF uf = new QuickUnionFindUF(int.Parse(line));
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

