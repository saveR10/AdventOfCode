using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AOC.DataStructures.Clustering
{
    public class QuickFindUF
    {
        private int[] id;    // id[i] = component identifier of i
        private int count;   // number of components

        public QuickFindUF(int n)
        {
            count = n;
            id = new int[n];
            for (int i = 0; i < n; i++)
                id[i] = i;
        }

        public int Count()
        {
            return count;
        }

        public int Find(int p)
        {
            Validate(p);
            return id[p];
        }

        private void Validate(int p)
        {
            int n = id.Length;
            if (p < 0 || p >= n)
            {
                throw new ArgumentException("index " + p + " is not between 0 and " + (n - 1));
            }
        }

        public bool Connected(int p, int q)
        {
            Validate(p);
            Validate(q);
            return id[p] == id[q];
        }

        public void Union(int p, int q)
        {
            Validate(p);
            Validate(q);
            int pID = id[p];   // needed for correctness
            int qID = id[q];   // to reduce the number of array accesses

            // p and q are already in the same component
            if (pID == qID) return;

            for (int i = 0; i < id.Length; i++)
                if (id[i] == pID) id[i] = qID;
            count--;
        }

        public static void Example()
        {
            long startTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();

            string inputName = "largeUF.txt"; //"mediumUF.txt";//""tinyUF.txt";
            string esercizio = "QuickFindUF";

            using (StreamWriter fw = new StreamWriter($"../../DataStructures/IO/Output/{esercizio}_{inputName}"))
            {
                fw.WriteLine($"Questo è il risultato dell'esercizio {esercizio} con input {inputName} :");

                using (StreamReader reader = new StreamReader($"../../DataStructures/IO/Input/{inputName}"))
                {
                    int i = 0;
                    string line = reader.ReadLine();
                    QuickFindUF uf = new QuickFindUF(int.Parse(line));
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
