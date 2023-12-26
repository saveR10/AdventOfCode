using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AOC.DataStructures.AlghoritmsOptimization
{
    public class ThreeSum
    {
        public static void Example()
        {
            // Initialization of variables
            long startTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            string inputName = "Three8Ints.txt";//"4Kints.txt";//Three8Ints.txt"; // Change this to the appropriate input file
            string esercizio = "ThreeSum";

            // Connect to the file; create it if necessary. Attach a buffer to the file. Use the buffer to write to the file
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

                int[] input = new int[vectReader.Count];
                for (int i = 0; i < vectReader.Count; i++)
                {
                    input[i] = int.Parse(vectReader[i].Trim());
                }

                int count = Count(input);

                writer.Write($"Il risultato è {count}");
                writer.WriteLine();

                long endTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                long timeElapsed = endTime - startTime;
                writer.Write($"Execution time in milliseconds: {timeElapsed}");
                writer.WriteLine();
            }
        }

        public static int Count(int[] a)
        {
            int n = a.Length;
            int count = 0;
            for (int i = 0; i < n; i++)
            {
                for (int j = i + 1; j < n; j++)
                {
                    for (int k = j + 1; k < n; k++)
                    {
                        if (a[i] + a[j] + a[k] == 0)
                        {
                            count++;
                        }
                    }
                }
            }
            return count;
        }
    }
}
