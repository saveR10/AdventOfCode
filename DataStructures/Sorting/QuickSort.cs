using System;
using System.Collections.Generic;
using System.IO;

namespace AOC.DataStructures.Sorting
{
    public class QuickSort
    {
        private static Random random = new Random();

        public static void Sort(IComparable[] a)
        {
            Shuffle(a); // Shuffle the array before sorting
            Sort(a, 0, a.Length - 1);
            if (!IsSorted(a))
            {
                throw new InvalidOperationException("Array is not sorted");
            }
        }

        private static void Sort(IComparable[] a, int lo, int hi)
        {
            if (hi <= lo) return;
            int j = Partition(a, lo, hi);
            Sort(a, lo, j - 1);
            Sort(a, j + 1, hi);
        }

        private static int Partition(IComparable[] a, int lo, int hi)
        {
            int i = lo;
            int j = hi + 1;
            IComparable v = a[lo];
            while (true)
            {
                while (Less(a[++i], v))
                {
                    if (i == hi) break;
                }
                while (Less(v, a[--j]))
                {
                    if (j == lo) break;
                }
                if (i >= j) break;
                Exchange(a, i, j);
            }
            Exchange(a, lo, j);
            return j;
        }

        private static void Shuffle(IComparable[] a)
        {
            int n = a.Length;
            for (int i = 0; i < n; i++)
            {
                int r = i + random.Next(n - i);
                IComparable temp = a[i];
                a[i] = a[r];
                a[r] = temp;
            }
        }

        private static bool Less(IComparable v, IComparable w)
        {
            return v.CompareTo(w) < 0;
        }

        private static void Exchange(IComparable[] a, int i, int j)
        {
            IComparable temp = a[i];
            a[i] = a[j];
            a[j] = temp;
        }

        private static bool IsSorted(IComparable[] a)
        {
            for (int i = 1; i < a.Length; i++)
            {
                if (Less(a[i], a[i - 1])) return false;
            }
            return true;
        }
        public static void Example()
        {
            long startTime = DateTime.Now.Ticks;

            // Initialization of variables
            string inputName = "tiny.txt";//"words.txt";//
            string exercise = "QuickSort";

            // Connect to the file; create it if necessary. Attach a buffer to the file. Use the buffer to write to the file
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

                string[] a = lista.ToArray();

                QuickSort.Sort(a);

                for (int i = 0; i < a.Length; i++)
                {
                    fw.Write(a[i]?.ToString());
                    fw.WriteLine();
                }
            }

            fw.WriteLine();
            long endTime = DateTime.Now.Ticks;
            long timeElapsed = endTime - startTime;
            fw.Write($"Execution time in ticks: {timeElapsed}");
            fw.WriteLine();
            fw.Write($"Execution time in milliseconds: {timeElapsed / TimeSpan.TicksPerMillisecond}");
            fw.WriteLine();
            fw.Write($"Execution time in seconds: {timeElapsed / TimeSpan.TicksPerSecond}");
            fw.WriteLine();
            // Close the buffer and the file writer to make sure the file is saved properly
            fw.WriteLine();
            fw.Close();
        }

    }
}