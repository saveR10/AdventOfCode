using System;
using System.Collections.Generic;
using System.IO;
namespace AOC.DataStructures.Sorting
{

    public class ShellSort
    {
        public static void Sort(IComparable[] a)
        {
            int n = a.Length;

            // 3x+1 increment sequence:  1, 4, 13, 40, 121, 364, 1093, ...
            int h = 1;
            while (h < n / 3) h = 3 * h + 1;

            while (h >= 1)
            {
                // h-sort the array
                for (int i = h; i < n; i++)
                {
                    for (int j = i; j >= h && Less(a[j], a[j - h]); j -= h)
                    {
                        Exchange(a, j, j - h);
                    }
                }
                AssertIsHsorted(a, h);
                h /= 3;
            }
            AssertIsSorted(a);
        }

        // is v < w ?
        private static bool Less(IComparable v, IComparable w)
        {
            return v.CompareTo(w) < 0;
        }

        // exchange a[i] and a[j]
        private static void Exchange(object[] a, int i, int j)
        {
            object swap = a[i];
            a[i] = a[j];
            a[j] = swap;
        }

        private static bool IsSorted(IComparable[] a)
        {
            for (int i = 1; i < a.Length; i++)
            {
                if (Less(a[i], a[i - 1])) return false;
            }
            return true;
        }

        // is the array h-sorted?
        private static bool IsHsorted(IComparable[] a, int h)
        {
            for (int i = h; i < a.Length; i++)
            {
                if (Less(a[i], a[i - h])) return false;
            }
            return true;
        }

        private static void AssertIsSorted(IComparable[] a)
        {
            if (!IsSorted(a))
            {
                throw new InvalidOperationException("Array is not sorted");
            }
        }

        private static void AssertIsHsorted(IComparable[] a, int h)
        {
            if (!IsHsorted(a, h))
            {
                throw new InvalidOperationException("Array is not h-sorted");
            }
        }

        public static void Example()
        {
            long startTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();

            string input_name = "words.txt";//"tiny.txt";
            string exercise = "ShellSort";

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
