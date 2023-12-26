using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AOC.DataStructures.Sorting
{
    public class MergeSort
    {
        private MergeSort() { }

        private static void Merge(IComparable[] a, IComparable[] aux, int lo, int mid, int hi)
        {
            for (int k = lo; k <= hi; k++)
            {
                aux[k] = a[k];
            }

            int i = lo, j = mid + 1;
            for (int k = lo; k <= hi; k++)
            {
                if (i > mid) a[k] = aux[j++];
                else if (j > hi) a[k] = aux[i++];
                else if (Less(aux[j], aux[i])) a[k] = aux[j++];
                else a[k] = aux[i++];
            }
        }

        public static void Sort(IComparable[] a)
        {
            IComparable[] aux = new IComparable[a.Length];
            Sort(a, aux, 0, a.Length - 1);
            AssertIsSorted(a);
        }

        private static void Sort(IComparable[] a, IComparable[] aux, int lo, int hi)
        {
            if (hi <= lo) return;
            int mid = lo + (hi - lo) / 2;
            Sort(a, aux, lo, mid);
            Sort(a, aux, mid + 1, hi);
            Merge(a, aux, lo, mid, hi);
        }

        private static bool Less(IComparable v, IComparable w)
        {
            return v.CompareTo(w) < 0;
        }

        private static void AssertIsSorted(IComparable[] a)
        {
            AssertIsSorted(a, 0, a.Length - 1);
        }

        private static void AssertIsSorted(IComparable[] a, int lo, int hi)
        {
            for (int i = lo + 1; i <= hi; i++)
            {
                if (Less(a[i], a[i - 1])) throw new InvalidOperationException("Array is not sorted.");
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
            string Exercise= "MergeSort";
            string inputName = "words.txt";//"tiny.txt";

            StreamWriter sw = new StreamWriter($"../../DataStructures/IO/Output/{Exercise}_{inputName}");
            sw.WriteLine($"Questo è il risultato dell'esercizio MergeSort con input {inputName} :");
            sw.WriteLine();

            StreamReader reader = new StreamReader($"../../DataStructures/IO/Input/{inputName}");
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

            MergeSort.Sort(a);

            foreach (var item in a)
            {
                sw.WriteLine(item);
            }

            sw.WriteLine();
            sw.Close();
        }
    }
}

