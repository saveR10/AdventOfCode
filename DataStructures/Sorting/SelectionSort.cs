using System;
using System.Collections.Generic;
using System.IO;
namespace AOC.DataStructures.Sorting
{
    public class SelectionSort
    {
        public static void Sort(IComparable[] a)
        {
            int n = a.Length;
            for (int i = 0; i < n; i++)
            {
                int min = i;
                for (int j = i + 1; j < n; j++)
                {
                    if (Less(a[j], a[min])) min = j;
                }
                Exchange(a, i, min);
                AssertIsSorted(a, 0, i);
            }
            AssertIsSorted(a);
        }

        public static void Sort(object[] a, IComparer<object> comparator)
        {
            int n = a.Length;
            for (int i = 0; i < n; i++)
            {
                int min = i;
                for (int j = i + 1; j < n; j++)
                {
                    if (Less(comparator, a[j], a[min])) min = j;
                }
                Exchange(a, i, min);
                AssertIsSorted(a, comparator, 0, i);
            }
            AssertIsSorted(a, comparator);
        }

        private static bool Less(IComparable v, IComparable w)
        {
            return v.CompareTo(w) < 0;
        }

        private static bool Less(IComparer<object> comparator, object v, object w)
        {
            return comparator.Compare(v, w) < 0;
        }

        private static void Exchange(object[] a, int i, int j)
        {
            object swap = a[i];
            a[i] = a[j];
            a[j] = swap;
        }

        private static void AssertIsSorted(IComparable[] a)
        {
            AssertIsSorted(a, 0, a.Length - 1);
        }

        private static void AssertIsSorted(IComparable[] a, int lo, int hi)
        {
            for (int i = lo + 1; i <= hi; i++)
            {
                if (Less(a[i], a[i - 1])) throw new InvalidOperationException("Array is not sorted");
            }
        }

        private static void AssertIsSorted(object[] a, IComparer<object> comparator)
        {
            AssertIsSorted(a, comparator, 0, a.Length - 1);
        }

        private static void AssertIsSorted(object[] a, IComparer<object> comparator, int lo, int hi)
        {
            for (int i = lo + 1; i <= hi; i++)
            {
                if (Less(comparator, a[i], a[i - 1])) throw new InvalidOperationException("Array is not sorted");
            }
        }

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

            string input_name = "words.txt"; //"tiny.txt";//
            string exercise = "SelectionSort";

            string outputPath = $"../../DataStructures/IO/Output/{exercise}_{input_name}";
            using (StreamWriter writer = new StreamWriter(outputPath))
            {
                writer.WriteLine($"Questo è il risultato dell'esercizio {exercise} con input {input_name} :");

                List<string> lines = new List<string>();
                using (StreamReader reader = new StreamReader($"../../DataStructures/IO/Input/{input_name}"))
                {
                    string line;
                    while ((line = reader.ReadLine()?.Trim()) != null)
                    {
                        lines.AddRange(line.Trim().Split(' '));
                    }
                }

                string[] a = lines.ToArray();
                Sort(a);

                foreach (var item in a)
                {
                    writer.WriteLine(item.ToString());
                }

                writer.WriteLine();

                long endTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                long timeElapsed = endTime - startTime;
                writer.WriteLine($"Execution time in milliseconds: {timeElapsed}");
                writer.WriteLine($"Execution time in seconds: {timeElapsed / 1000.0}");
            }
        }
    }

}

