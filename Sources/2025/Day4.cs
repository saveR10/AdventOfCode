using AOC;
using AOC.DataStructures.Clustering;
using AOC.Model;
using AOC.SearchAlghoritmhs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using static AOC.SearchAlghoritmhs.ResearchAlgorithmsAttribute;
using System.Linq;   // <- importante

namespace AOC2025
{
    //   [ResearchAlgorithmsAttribute(ResolutionEnum.None)]
    public class Day4 : Solver, IDay
    {
        public long sum = 0;
        public void Part1(object input, bool test, ref object solution)
        {
            string[] grid = ((IEnumerable<string>)input).ToArray();
            int h = grid.Length;
            int w = grid[0].Length;
            int accessible = 0;
            for (int r = 0; r < h; r++)
                for (int c = 0; c < w; c++)
                {
                    if (grid[r][c] == '@')
                    {
                        int neighbors = CountNeighbors(r, c);
                        if (neighbors < 4)
                            accessible++;
                    }
                }
            int CountNeighbors(int r, int c)
            {
                int count = 0;
                for (int dr = -1; dr <= 1; dr++)
                    for (int dc = -1; dc <= 1; dc++)
                    {
                        if (dr == 0 && dc == 0) continue;

                        int rr = r + dr;
                        int cc = c + dc;

                        if (rr >= 0 && rr < h && cc >= 0 && cc < w)
                        {
                            if (grid[rr][cc] == '@')
                                count++;
                        }
                    }
                return count;
            }
            solution = accessible;
            //1493
        }


        public void Part2(object input, bool test, ref object solution)
        {
            string[] rows = ((IEnumerable<string>)input).ToArray();
            int h = rows.Length;
            int w = rows[0].Length;

            // Converto in array mutabile (char[][])
            char[][] grid = rows.Select(r => r.ToCharArray()).ToArray();

            int totalRemoved = 0;
            bool removedSomething;

            do
            {
                removedSomething = false;
                List<(int r, int c)> toRemove = new List<(int r, int c)>();

                // Trova gli accessibili in questo round
                for (int r = 0; r < h; r++)
                {
                    for (int c = 0; c < w; c++)
                    {
                        if (grid[r].Count() == 0) continue;

                        if (grid[r][c] == '@')
                        {
                            int neighbors = CountNeighborsComplex(grid, r, c, h, w);
                            if (neighbors < 4)
                                toRemove.Add((r, c));
                        }
                    }
                }

                // Se ce ne sono, rimuovili
                if (toRemove.Count > 0)
                {
                    removedSomething = true;
                    totalRemoved += toRemove.Count;

                    foreach (var (r, c) in toRemove)
                        grid[r][c] = '.';
                }

            } while (removedSomething); // Continua finché c'è qualcosa da rimuovere

            solution = totalRemoved;
            //9194
        }
        private int CountNeighborsComplex(char[][] grid, int r, int c, int h, int w)
        {
            int count = 0;
            for (int dr = -1; dr <= 1; dr++)
            {
                for (int dc = -1; dc <= 1; dc++)
                {
                    if (dr == 0 && dc == 0) continue;

                    int rr = r + dr;
                    int cc = c + dc;

                    if (rr < 0 || rr >= h) continue;
                    if (cc < 0 || cc >= w) continue;

                    if (grid[rr].Count() == 0) continue;


                    if (grid[rr][cc] == '@')
                        count++;
                }
            }
            return count;
        }
    }
}