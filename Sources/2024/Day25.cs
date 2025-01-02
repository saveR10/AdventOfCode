using AOC;
using AOC.DataStructures.Clustering;
using AOC.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AOC2024
{
    public class Day25 : Solver, IDay
    {
        public void Part1(object input, bool test, ref object solution)
        {
            string inputText = (string)input;
            string[] blocks = inputText.Trim().Split(Delimiter.delimiter_white_line, StringSplitOptions.None);
            Dictionary<int, char[,]> schematics = new Dictionary<int, char[,]>();
            int idSchematic = 0;
            foreach (var block in blocks)
            {
                string[] lines = block.Trim().Split(Delimiter.delimiter_line, StringSplitOptions.None);

                int rows = lines.Length;
                int cols = lines[0].Length;

                char[,] schematic = new char[rows, cols];

                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        schematic[i, j] = lines[i][j];
                    }
                }
                schematics.Add(idSchematic, schematic);
                idSchematic++;
            }
            Dictionary<int, bool> IsLock = new Dictionary<int, bool>();
            foreach(var s in schematics)
            {
                bool firstRowAllHash = IsRowAll(s.Value, 0, '#');
                bool lastRowAllDots = IsRowAll(s.Value, s.Value.GetLength(0) - 1, '.');

                if (firstRowAllHash && lastRowAllDots) IsLock[s.Key] = true;
                else IsLock[s.Key] = false;
                Console.WriteLine($"Griglia {s.Key}:");
                Console.WriteLine($"  IsLock '#': {IsLock[s.Key]}");

                PrintGrid(s.Value);
                Console.WriteLine();
            }
            int fit = 0;
            foreach (var lockEntry in IsLock)
            {
                if (lockEntry.Value)
                {
                    int lockId = lockEntry.Key;
                    char[,] lockGrid = schematics[lockId];

                    foreach (var keyEntry in IsLock)
                    {
                        if (!keyEntry.Value)
                        {
                            int keyId = keyEntry.Key;
                            char[,] keyGrid = schematics[keyId];

                            Console.WriteLine($"Comparing Lock {lockId} with Key {keyId}");
                            bool fits = DoLockAndKeyFit(lockGrid, keyGrid);
                            if (fits) fit++;
                        }
                    }
                }
            }
            solution = fit;
        }
        bool DoLockAndKeyFit(char[,] lockGrid, char[,] keyGrid)
        {
            int height = lockGrid.GetLength(0); 
            int width = lockGrid.GetLength(1);

            if (height != keyGrid.GetLength(0) || width != keyGrid.GetLength(1))
            {
                return false; 
            }
            for (int col = 0; col < width; col++)
            {
                int sum = 0;
                for (int row = 0; row < height; row++)
                {
                    if (lockGrid[row, col] == '#') sum++;
                    if (keyGrid[row, col] == '#') sum++;
                }

                // Controlla che la somma non superi l'altezza della colonna
                if (sum > height)
                {
                    return false; // La serratura e la chiave non "fittano"
                }
            }

            return true; // La serratura e la chiave "fittano"
        }
        static void PrintGrid(char[,] grid)
        {
            int rows = grid.GetLength(0);
            int cols = grid.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Console.Write(grid[i, j]);
                }
                Console.WriteLine();
            }
        }

        static bool IsRowAll(char[,] grid, int row, char expected)
        {
            int cols = grid.GetLength(1);
            for (int j = 0; j < cols; j++)
            {
                if (grid[row, j] != expected)
                {
                    return false;
                }
            }
            return true;
        }
        public void Part2(object input, bool test, ref object solution)
        {
            string inputText = (string)input;
            foreach (string pair in inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None))
            {
                if (!string.IsNullOrEmpty(pair))
                {
                }
            }
        }
    }
}
