using AOC;
using AOC.DataStructures.Clustering;
using AOC.DataStructures.ParsingInput;
using AOC.Model;
using AOC.SearchAlghoritmhs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text.RegularExpressions;
using static AOC.SearchAlghoritmhs.ResearchAlgorithmsAttribute;

namespace AOC2025
{
    [ResearchAlgorithms(TypologyEnum.Map)]
    [ResearchAlgorithms(ResolutionEnum.DP)]    
    public class Day7 : Solver, IDay
    {
        public static string[,] Map;
        public static int c;
        public static int r;
        public int[] startingPoint = new int[2];
        public int rowTargeting = 0;
        public static bool InitializedBeam = false;
        public static int splitted = 0;
        public void Part1(object input, bool test, ref object solution)
        {
            string inputText = (string)input;
            r = inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None).Count();
            c = inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None)[0].Length;

            Map = new string[r, c];
            int i = 0;
            foreach (string line in inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None))
            {
                if (!string.IsNullOrEmpty(line))
                {
                    for (int j = 0; j < c; j++)
                    {
                        Map[i, j] = line.Substring(j, 1);
                        if (Map[i, j] == "S")
                        {
                            startingPoint[0] = i;
                            startingPoint[1] = j;
                        }
                    }
                }
                i++;
            }
            ShowMap(Map);

            var map = InputMapParser.Parse(
                inputText,
                out var specials,
                ignoreEmptyLines: true,
                padToMaxWidth: true
            );
            ShowMap(map);

            while (rowTargeting != r)
            {
                DownwardTachyon();
                ShowMap(Map);
            }

            solution = splitted;
        }

        public void DownwardTachyon()
        {
            if (InitializedBeam)
            {
                for (int i = 0; i < r; i++)
                {
                    for (int j = 0; j < c; j++)
                    {
                        if (i == r - 1)
                            continue;

                        if (Map[i, j] == "|" && Map[i + 1, j] == "^")
                        {
                            Map[i + 1, j - 1] = "|";
                            Map[i + 1, j + 1] = "|";
                            splitted++;
                        }

                        if (Map[i, j] == "|" && Map[i + 1, j] == ".")
                        {
                            Map[i + 1, j] = "|";
                        }

                    }
                }
                rowTargeting = r;
            }
            else
            {
                Map[startingPoint[0] + 1, startingPoint[1]] = "|";
                rowTargeting = startingPoint[0] + 1;
                InitializedBeam = true;
            }
        }
        public void ShowMap(string[,] map)
        {
            Console.WriteLine("\n----------------------\n");
            Console.WriteLine();
            for (int i = 0; i < r; i++)
            {
                for (int j = 0; j < c; j++)
                {
                    Console.Write(map[i, j]);
                }
                Console.Write(i);
                Console.WriteLine("");
            }
        }


        public void Part2(object input, bool test, ref object solution)
        {
            string inputText = (string)input;
            r = inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None).Count();
            c = inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None)[0].Length;

            Map = new string[r, c];
            int i = 0;
            foreach (string line in inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None))
            {
                if (!string.IsNullOrEmpty(line))
                {
                    for (int j = 0; j < c; j++)
                    {
                        Map[i, j] = line.Substring(j, 1);
                        if (Map[i, j] == "S")
                        {
                            startingPoint[0] = i;
                            startingPoint[1] = j;
                        }
                    }
                }
                i++;
            }

            int startRow = startingPoint[0];
            int startCol = startingPoint[1];

            BigInteger[] curr = new BigInteger[c];
            curr[startCol] = 1; 

            BigInteger timelines = 0; 
            for (int row = startRow; row < r; row++)
            {
                BigInteger[] next = new BigInteger[c];

                for (int col = 0; col < c; col++)
                {
                    if (curr[col] == 0) continue;
                    BigInteger ways = curr[col];
                    int belowR = row + 1;

                    // se scende fuori dalla mappa -> timeline terminata 
                    if (belowR >= r)
                    {
                        timelines += ways;
                        continue;
                    }

                    string below = Map[belowR, col];

                    if (below == "^")
                    {
                        int leftCol = col - 1;
                        int rightCol = col + 1;

                        if (leftCol < 0)
                            timelines += ways; // esce a sinistra
                        else
                            next[leftCol] += ways;

                        if (rightCol >= c)
                            timelines += ways; // esce a destra
                        else
                            next[rightCol] += ways;
                    }
                    else if (below == ".")
                    {
                        // continua dritto
                        next[col] += ways;
                    }
                    else
                    {
                        timelines += ways;
                    }
                }

                curr = next;
            }
            solution = timelines.ToString();
        }

    }
}
