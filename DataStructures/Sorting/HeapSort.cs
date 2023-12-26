using System;
using System.Collections.Generic;
using System.IO;

namespace AOC.DataStructures.Sorting
{
    public class HeapSort
    {
        // This class should not be instantiated.
        private HeapSort() { }

        public static void Sort(IComparable[] pq)
        {
            int n = pq.Length;

            // heapify phase
            for (int kk = n / 2; kk >= 1; kk--)
                Sink(pq, kk, n);

            // sortdown phase
            int k = n;
            while (k > 1)
            {
                Exchange(pq, 0, k - 1);
                Sink(pq, 1, --k);
            }
        }

        private static void Sink(IComparable[] pq, int k, int n)
        {
            while (2 * k <= n)
            {
                int j = 2 * k;
                if (j < n && Less(pq, j - 1, j))
                    j++;
                if (!Less(pq, k - 1, j - 1)) break;
                Exchange(pq, k - 1, j - 1);
                k = j;
            }
        }

        private static bool Less(IComparable[] pq, int i, int j)
        {
            return pq[i].CompareTo(pq[j]) < 0;
        }

        private static void Exchange(IComparable[] pq, int i, int j)
        {
            IComparable swap = pq[i];
            pq[i] = pq[j];
            pq[j] = swap;
        }

        // print array to standard output
        private static void Show(IComparable[] a)
        {
            foreach (var item in a)
            {
                Console.WriteLine(item);
            }
        }

        public static void Example()
        {
            long startTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();

            string inputName = "words.txt";//"tiny.txt"; //
            string exercise = "HeapSort";

            using (StreamWriter fw = new StreamWriter($"../../DataStructures/IO/Output/{exercise}_{inputName}"))
            {
                fw.WriteLine($"Questo è il risultato dell'esercizio {exercise} con input {inputName} :");

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

                    string[] a = lista.ToArray();

                    Sort(a);

                    for (int i = 0; i < a.Length; i++)
                    {
                        fw.Write(a[i]);
                        if (i == a.Length / 2)
                        {
                            fw.Write(" - elemento centrale");
                        }

                        fw.WriteLine();
                    }
                }

                fw.WriteLine();
                long endTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                long timeElapsed = endTime - startTime;
                fw.WriteLine($"Execution time in milliseconds: {timeElapsed}");
                fw.WriteLine($"Execution time in seconds: {timeElapsed / 1000}");
            }
        }

    }
}



    

