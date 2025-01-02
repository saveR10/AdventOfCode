using AOC;
using AOC.DataStructures.Clustering;
using AOC.Model;
using AOC.SearchAlghoritmhs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using static AOC.SearchAlghoritmhs.ResearchAlgorithmsAttribute;

namespace AOC2024
{
    [ResearchAlgorithms(TypologyEnum.Map)]
    public class Day8 : Solver, IDay
    {
        public static string[,] Map;
        public static string[,] AntinodesMap;
        public static int n;
        public void Part1(object input, bool test, ref object solution)
        {
            string inputText = (string)input;
            n = inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None)[0].Length;
            Map = new string[n, n];
            AntinodesMap = new string[n, n];
            int i = 0;
            foreach (string line in inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None))
            {
                if (!string.IsNullOrEmpty(line))
                {
                    for (int j = 0; j < n; j++)
                    {
                        Map[i, j] = line.Substring(j, 1);
                        AntinodesMap[i, j] = line.Substring(j, 1);
                    }
                }
                i++;
            }
            ShowMap(Map);


            for (int r = 0; r < n; r++)
            {
                for (int c = 0; c < n; c++)
                {
                    if (Map[r, c] == ".")
                        continue; // Salta i punti rappresentati da "."

                    for (int k = 0; k < n; k++)
                    {
                        for (int l = 0; l < n; l++)
                        {
                            // Salta il confronto con sé stesso
                            if (r == k && c == l)
                                continue;

                            // Salta i punti rappresentati da "."
                            if (Map[k, l] == ".")
                                continue;

                            // Controlla se i punti sono uguali
                            if (Map[r, c] == Map[k, l])
                            {
                                CreateAntinodes(r, c, k, l);
                            }
                        }
                    }
                }
            }
            ShowMap(AntinodesMap);
            solution = CountAntinodes();
        }
        public int CountAntinodes()
        {
            int antinodes = 0;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (AntinodesMap[i, j] == "#") antinodes++;
                }
            }
            return antinodes;
        }
        public void CreateAntinodes(int r, int c, int k, int l)
        {
            int diffX = Math.Max(r, k) - Math.Min(r, k);
            int diffY = Math.Max(c, l) - Math.Min(c, l);
            int rr = r - k;
            int cc = c - l;
            int kk = k - r;
            int ll = l - c;

            if (r + rr<n && r +rr>=0 && c + cc < n && c + cc >= 0)
                AntinodesMap[r + rr, c + cc] = "#";
            

            if (k + kk < n && k + kk >= 0 && l + ll < n && l + ll >= 0) 
                AntinodesMap[k +kk , l +ll] = "#";

            //ShowMap(AntinodesMap);

        }

        public void ShowMap(string[,] map)
        {
            Console.WriteLine("\n----------------------\n");

            for (int i = 0; i < n; i++)
            {
                Console.Write(i);
            }
            Console.WriteLine();
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    //Console.ForegroundColor = ConsoleColor.Green;
                    //Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(map[i, j]);
                }
                Console.Write(i);
                Console.WriteLine("");
            }
        }

        public void Part2(object input, bool test, ref object solution)
        {
            string inputText = (string)input;
            n = inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None)[0].Length;
            Map = new string[n, n];
            AntinodesMap = new string[n, n];
            int i = 0;
            foreach (string line in inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None))
            {
                if (!string.IsNullOrEmpty(line))
                {
                    for (int j = 0; j < n; j++)
                    {
                        Map[i, j] = line.Substring(j, 1);
                        AntinodesMap[i, j] = line.Substring(j, 1);
                    }
                }
                i++;
            }
            ShowMap(Map);


            for (int r = 0; r < n; r++)
            {
                for (int c = 0; c < n; c++)
                {
                    if (Map[r, c] == ".")
                        continue; // Salta i punti rappresentati da "."

                    for (int k = 0; k < n; k++)
                    {
                        for (int l = 0; l < n; l++)
                        {
                            // Salta il confronto con sé stesso
                            if (r == k && c == l)
                                continue;

                            // Salta i punti rappresentati da "."
                            if (Map[k, l] == ".")
                                continue;

                            // Controlla se i punti sono uguali
                            if (Map[r, c] == Map[k, l])
                            {
                                CreateResonancyAntinodes(r, c, k, l);
                            }
                        }
                    }
                }
            }
            ShowMap(AntinodesMap);
            solution = CountResonancyAntinodes();
            //1324 too low
        }
        public void CreateResonancyAntinodes(int r, int c, int k, int l)
        {
            //bool resonancy = false;
            int diffX = Math.Max(r, k) - Math.Min(r, k);
            int diffY = Math.Max(c, l) - Math.Min(c, l);
            int rrr = r - k;
            int rr = rrr;
            int ccc = c - l;
            int cc = ccc;
            int kkk = k - r;
            int kk = kkk;
            int lll = l - c;
            int ll = lll;

            while (r + rrr < n && r + rrr >= 0 && c + ccc < n && c + ccc >= 0)
            {
                //resonancy = true;
                AntinodesMap[r + rrr, c + ccc] = "#";
                rrr = rrr + rr;
                ccc = ccc + cc;
            }

            while (k + kkk < n && k + kkk >= 0 && l + lll < n && l + lll>= 0)
            {
                //resonancy = true;
                AntinodesMap[k + kkk, l + lll] = "#";
                lll = lll + ll;
                kkk = kkk + kk;
            }
            //if (resonancy) //Se sono in questo metodo le due antenne sono già in risonanza! a prescindenre se nella mappa si inseriscano antinodi, da qualche parte fuori mappa ci saranno sicuramente.
            {
                AntinodesMap[r, c] = "#";
                AntinodesMap[k, l] = "#";
            }

            //ShowMap(AntinodesMap);

        }
        public int CountResonancyAntinodes()
        {
            int antinodes = 0;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    //if (AntinodesMap[i, j].Contains("#"))
                        //antinodes += AntinodesMap[i, j].Count(c => c == '#');
                        if (AntinodesMap[i, j] == "#") antinodes++;

                }
            }
            return antinodes;
        }
    }
}
