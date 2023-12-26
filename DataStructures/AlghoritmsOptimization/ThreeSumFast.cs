using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AOC.DataStructures.AlghoritmsOptimization
{
    public class ThreeSumFast
    {
        private ThreeSumFast() { }

        private static bool ContainsDuplicates(int[] a)
        {
            for (int i = 1; i < a.Length; i++)
            {
                if (a[i] == a[i - 1]) return true;
            }
            return false;
        }

        public static void PrintAll(int[] a)
        {
            int n = a.Length;
            Array.Sort(a);
            if (ContainsDuplicates(a)) throw new ArgumentException("array contains duplicate integers");
            for (int i = 0; i < n; i++)
            {
                for (int j = i + 1; j < n; j++)
                {
                    int k = Array.BinarySearch(a, -(a[i] + a[j]));
                    if (k > j) Console.WriteLine(a[i] + " " + a[j] + " " + a[k]);
                }
            }
        }

        public static int Count(int[] a)
        {
            int n = a.Length;
            Array.Sort(a);
            if (ContainsDuplicates(a)) throw new ArgumentException("array contains duplicate integers");
            int count = 0;
            for (int i = 0; i < n; i++)
            {
                for (int j = i + 1; j < n; j++)
                {
                    int k = Array.BinarySearch(a, -(a[i] + a[j]));
                    if (k > j) count++;
                }
            }
            return count;
        }

        public static void Example()
        {
            long startTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            string lezione = "Lezione1";
            string inputName = "16Kints.txt"; // Change this to the appropriate input file
            string esercizio = "ThreeSumFast";

            using (StreamWriter writer = new StreamWriter($"../../DataStructures/IO/Output/{esercizio}_{inputName}"))
            {
                writer.Write($"Questo è il risultato dell'esercizio {esercizio} con input {inputName} :");
                writer.WriteLine();

                List<string> vectReader = new List<string>();
                using (StreamReader reader = new StreamReader($"../../DataStructures/IO/Input/{inputName}"))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        vectReader.Add(line);
                    }
                }

                int[] input = vectReader.Select(int.Parse).ToArray();

                int count = Count(input);

                writer.Write($"Il risultato è {count}");
                writer.WriteLine();

                long endTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                long timeElapsed = endTime - startTime;
                writer.Write($"Execution time in milliseconds: {timeElapsed}");
                writer.WriteLine();
            }
        }
    }
}

