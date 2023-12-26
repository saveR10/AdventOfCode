using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace AOC.DataStructures.Sorting
{
    public class InsertionSort
    {
        public static void Sort<T>(T[] a) where T : IComparable<T>
        {
            int n = a.Length;
            for (int i = 1; i < n; i++)
            {
                for (int j = i; j > 0 && Less(a[j], a[j - 1]); j--)
                {
                    Exchange(a, j, j - 1);
                }
                Debug.Assert(IsSorted(a, 0, i));
            }
            Debug.Assert(IsSorted(a));
        }

        private static bool Less<T>(T v, T w) where T : IComparable<T>
        {
            return v.CompareTo(w) < 0;
        }

        private static void Exchange<T>(T[] a, int i, int j)
        {
            T swap = a[i];
            a[i] = a[j];
            a[j] = swap;
        }

        private static bool IsSorted<T>(T[] a) where T : IComparable<T>
        {
            return IsSorted(a, 0, a.Length);
        }

        private static bool IsSorted<T>(T[] a, int lo, int hi) where T : IComparable<T>
        {
            for (int i = lo + 1; i < hi; i++)
            {
                if (Less(a[i], a[i - 1])) return false;
            }
            return true;
        }

        private static void Show<T>(T[] a)
        {
            foreach (var item in a)
            {
                Console.WriteLine(item);
            }
        }

        public static void Example()
        {
            string Alghoritm = "InsertionSort";
            string inputName = "words.txt"; // "tiny.txt";//

            using (StreamWriter writer = new StreamWriter($"../../DataStructures/IO/Output/{Alghoritm}_{inputName}"))
            {
                writer.WriteLine($"Questo è il risultato dell'esercizio InsertionSort con input {inputName} :");
                writer.WriteLine();

                using (StreamReader reader = new StreamReader($"../../DataStructures/IO/Input/{inputName}"))
                {
                    string line = reader.ReadLine().Trim();
                    List<string> list = new List<string>();
                    while (line != null)
                    {
                        int n = line.Trim().Split(' ').Length;
                        for (int i = 0; i < n; i++)
                        {
                            list.Add(line.Split(' ')[i]);
                        }

                        line = reader.ReadLine();
                    }

                    string[] a = list.ToArray();

                    InsertionSort.Sort(a);

                    foreach (var item in a)
                    {
                        writer.WriteLine(item);
                    }

                    writer.WriteLine();
                    writer.Close();

                }

            }
            
        }
    }
}
