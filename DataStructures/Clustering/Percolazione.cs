using System;
using System.IO;
using System.Runtime.Remoting.Messaging;

namespace AOC.DataStructures.Clustering
{
    public class Percolazione
    {
        public bool[] Color; // black (false) or white (true)
        private int[] Parent; // parent[i] = parent of i
        private int[] Size; // size[i] = number of elements in subtree rooted at i
        private int Count; // number of components
        private static int SizeGrid;
        private bool IsPercolation = false;

        public Percolazione(int n)
        {
            Count = n * n - 2 * n + 2; // number of clusters
            Parent = new int[n * n];
            Color = new bool[n * n];
            Size = new int[n * n];
            for (int i = 0; i < n * n; i++)
            {
                if (i > 0 && i < n) { Parent[i] = 0; } // first row
                else if (i >= n && i <= (n * n - n)) { Parent[i] = i; } // central elements of the body
                else if (i > (n * n - n)) { Parent[i] = n * n - n; } // last row
                Size[i] = 1;
            }
        }

        public int CountComponents()
        {
            return Count;
        }

        public int Find(int p)
        {
            Validate(p);
            while (p != Parent[p])
                p = Parent[p];
            return p;
        }

        private void Validate(int p)
        {
            int n = Parent.Length;
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

            if ((Parent[rootP] == 0 && Parent[rootQ] == (SizeGrid * SizeGrid - SizeGrid)) ||
                ((Parent[rootQ] == 0 && Parent[rootP] == (SizeGrid * SizeGrid - SizeGrid))))
            {
                this.IsPercolation = true;
                Count--;
                return;
            }

            // make smaller root point to larger one
            if (Size[rootP] < Size[rootQ])
            {
                if (rootQ == 0 || rootP == 0)
                {
                    Parent[rootP] = 0;
                    Parent[rootQ] = 0;
                    Size[rootQ] += Size[rootP];
                }
                else if (rootQ == SizeGrid * SizeGrid - SizeGrid || rootP == SizeGrid * SizeGrid - SizeGrid)
                {
                    Parent[rootP] = SizeGrid * SizeGrid - SizeGrid;
                    Parent[rootQ] = SizeGrid * SizeGrid - SizeGrid;
                    Size[rootQ] += Size[rootP];
                }
                else
                {
                    Parent[rootP] = rootQ;
                    Size[rootQ] += Size[rootP];
                }
            }
            else
            {
                if (Parent[rootQ] == 0 || Parent[rootP] == 0)
                {
                    Parent[rootQ] = 0;
                    Parent[rootP] = 0;
                    Size[rootP] += Size[rootQ];
                }
                else if (Parent[rootQ] == SizeGrid * SizeGrid - SizeGrid || Parent[rootP] == SizeGrid * SizeGrid - SizeGrid)
                {
                    Parent[rootQ] = SizeGrid * SizeGrid - SizeGrid;
                    Parent[rootP] = SizeGrid * SizeGrid - SizeGrid;
                    Size[rootP] += Size[rootQ];
                }
                else
                {
                    Parent[rootQ] = rootP;
                    Size[rootP] += Size[rootQ];
                }
            }
            Count--;
        }

        public static void Example()
        {
            long startTime = DateTime.Now.Ticks;

            // Initialization of variables
            string lezione = "Lezione1";
            string inputName = "percolazione.txt";
            string esercizio = "Percolazione";

            // Connect to the file; create it if necessary. Attach a buffer to the file. Use the buffer to write to the file
            using (StreamWriter fw = new StreamWriter($"../../DataStructures/IO/Output/{esercizio}_{inputName}"))
            {
                fw.Write("Questo è il risultato dell'esercizio " + esercizio + " con input " + inputName + " :");
                fw.WriteLine();

                Random random = new Random();

                using (StreamReader reader = new StreamReader($"../../DataStructures/IO/Input/{inputName}"))
                {
                    string line = reader.ReadLine();
                    while (line != null)
                    {
                        SizeGrid = int.Parse(line.Split(' ')[0]);
                        int trials = int.Parse(line.Split(' ')[1]);
                        int t = 1; // trials
                        double[] statistics = new double[trials];
                        fw.WriteLine();
                        fw.WriteLine();
                        fw.Write("Griglia quadrata con N=" + SizeGrid + " e numero di prove T=" + trials);
                        fw.WriteLine();
                        while (trials != 0)
                        {
                            int u = 0; // unions
                            int c = 0; // colors
                            fw.Write("Prova numero " + t + " - ");
                            Percolazione perc = new Percolazione(SizeGrid);
                            while (!perc.IsPercolation)
                            {
                                int randNum = random.Next(SizeGrid * SizeGrid);
                                if (!perc.Color[randNum])
                                {
                                    perc.Color[randNum] = true;
                                    c++; // ;)
                                    int[] pos = Localize(randNum);

                                    if (pos[0] > 0 && !perc.IsPercolation)
                                    {
                                        int otherNum = randNum - SizeGrid;
                                        if (perc.Color[otherNum])
                                        {
                                            if (!perc.Connected(randNum, otherNum))
                                            {
                                                u++;
                                                perc.Union(randNum, otherNum);
                                            }
                                        }
                                    }
                                    if (pos[0] < SizeGrid - 1 && !perc.IsPercolation)
                                    {
                                        int otherNum = randNum + SizeGrid;
                                        if (perc.Color[otherNum])
                                        {
                                            if (!perc.Connected(randNum, otherNum))
                                            {
                                                u++;
                                                perc.Union(randNum, otherNum);
                                            }
                                        }
                                    }
                                    if (pos[1] > 0 && !perc.IsPercolation)
                                    {
                                        int otherNum = randNum - 1;
                                        if (perc.Color[otherNum])
                                        {
                                            if (!perc.Connected(randNum, otherNum))
                                            {
                                                u++;
                                                perc.Union(randNum, otherNum);
                                            }
                                        }
                                    }
                                    if (pos[1] < SizeGrid - 1 && !perc.IsPercolation)
                                    {
                                        int otherNum = randNum + 1;
                                        if (perc.Color[otherNum])
                                        {
                                            if (!perc.Connected(randNum, otherNum))
                                            {
                                                u++;
                                                perc.Union(randNum, otherNum);
                                            }
                                        }
                                    }
                                }
                            }

                            statistics[t - 1] = ((double)c / (SizeGrid * SizeGrid));
                            fw.Write("Frazione per la percolazione: " + c + " su " + SizeGrid * SizeGrid + " pari al " + statistics[t - 1]);
                            fw.WriteLine();

                            trials--;
                            t++;

                        }
                        double mean = 0;
                        double std = 0;
                        double interval = 0;
                        foreach (double elem in statistics)
                        {
                            mean += elem;
                        }
                        mean = mean / (double)(t - 1);
                        foreach (double elem in statistics)
                        {
                            std += Math.Pow((elem - mean), 2);
                        }
                        std = (Math.Sqrt(std / (t - 2)));

                        interval = 1.96 * std / Math.Sqrt(t - 1);

                        fw.Write("Media del campione: " + mean);
                        fw.WriteLine();
                        fw.Write("Deviazione standard del campione: " + std);
                        fw.WriteLine();
                        fw.Write("Intervallo di confidenza al 95%: [" + (mean - interval) + " , " + (mean + interval) + "]");
                        fw.WriteLine();

                        line = reader.ReadLine();
                    }


                    //bw.Write("Il numero di cluster è: "+perc.count());
                    fw.WriteLine();
                    long endTime = DateTime.Now.Ticks;
                    long timeElapsed = endTime - startTime;
                    fw.WriteLine();
                    fw.Write("Execution time in ticks: " + timeElapsed);
                    fw.WriteLine();
                    fw.Write("Execution time in milliseconds: " + timeElapsed / TimeSpan.TicksPerMillisecond);
                    fw.WriteLine();
                    fw.Write("Execution time in seconds: " + timeElapsed / TimeSpan.TicksPerSecond);
                    fw.WriteLine();
                    // Close the buffer and the file writer to make sure the file is saved properly
                    fw.Close();

                }

            }
        }

        public static int[] Localize(int randNum)
        {
            int[] pos = new int[2];
            pos[0] = randNum / SizeGrid; // row's grid
            pos[1] = randNum % SizeGrid; // column's grid
            return pos;
        }
    }
}
