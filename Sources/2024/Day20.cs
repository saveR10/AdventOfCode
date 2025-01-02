using AOC;
using AOC.DataStructures.Clustering;
using AOC.Model;
using AOC.SearchAlghoritmhs;
using AOC.Utilities.Map;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AOC2024
{
    [ResearchAlgorithms(ResearchAlgorithmsAttribute.TypologyEnum.Map,ResearchAlgorithmsAttribute.ResolutionEnum.BFS)]
    public class Day20 : Solver, IDay
    {
        public static string[,] Map;
        public static string[,] StoryMap;
        public static int n;
        public static int[] StartPosition = new int[2];
        public static int[] EndPoint = new int[2];
        public static bool _test;
        public void Part1(object input, bool test, ref object solution)
        {
            _test = test;
            string inputText = (string)input;
            n = inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None)[0].Length;
            Map = new string[n, n];
            StoryMap = new string[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Map[i, j] = inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None)[i].Substring(j, 1).ToString();
                    StoryMap[i, j] = inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None)[i].Substring(j, 1).ToString();
                    if (Map[i, j] == "S")
                    {
                        StartPosition[0] = i;
                        StartPosition[1] = j;
                        Map[i, j] = "S";
                        StoryMap[i, j] = "X";
                    }
                    if (Map[i, j] == "E")
                    {
                        EndPoint[0] = i;
                        EndPoint[1] = j;
                    }
                }
            }
            AOC.Utilities.Map.Map.ShowStringMap(Map);
            var (start, end) = FindStartAndEnd(Map);
            int baselineTime = BFS(Map, start, end, false);
            List<(int Row, int Col, int TimeSaved)> cheats = FindCheats(Map, start, end, baselineTime);

            Console.WriteLine($"Number of cheats saving at least 100 picoseconds: {cheats.Count}");
            foreach (var cheat in cheats)
            {
                Console.WriteLine($"Cheat at ({cheat.Row}, {cheat.Col}) saves {cheat.TimeSaved} picoseconds");
            }
            solution = cheats.Count;
        }
        static ((int Row, int Col), (int Row, int Col)) FindStartAndEnd(string[,] grid)
        {
            (int Row, int Col) start = (0, 0);
            (int Row, int Col) end = (0, 0);

            for (int r = 0; r < grid.GetLength(0); r++)
            {
                for (int c = 0; c < grid.GetLength(1); c++)
                {
                    if (grid[r, c] == "S") start = (r, c);
                    if (grid[r, c] == "E") end = (r, c);
                }
            }

            return (start, end);
        }

        static int BFS(string[,] grid, (int Row, int Col) start, (int Row, int Col) end, bool allowCheat)
        {
            int rows = grid.GetLength(0);
            int cols = grid.GetLength(1);
            var directions = new (int Row, int Col)[] { (-1, 0), (1, 0), (0, -1), (0, 1) };
            var queue = new Queue<(int Row, int Col, int Steps, bool Cheated)>();
            var visited = new HashSet<(int Row, int Col, bool Cheated)>();

            queue.Enqueue((start.Row, start.Col, 0, false));

            while (queue.Count > 0)
            {
                var (row, col, steps, cheated) = queue.Dequeue();

                if ((row, col) == end)
                    return steps;

                if (visited.Contains((row, col, cheated)))
                    continue;

                visited.Add((row, col, cheated));

                foreach (var (dRow, dCol) in directions)
                {
                    int newRow = row + dRow;
                    int newCol = col + dCol;

                    if (newRow >= 0 && newRow < rows && newCol >= 0 && newCol < cols)
                    {
                        string cell = grid[newRow, newCol];
                        if (cell == "." || cell == "E")
                            queue.Enqueue((newRow, newCol, steps + 1, cheated));
                        else if (cell == "#" && allowCheat && !cheated)
                            queue.Enqueue((newRow, newCol, steps + 1, true));
                    }
                }
            }

            return int.MaxValue; // No path found
        }

        static List<(int Row, int Col, int TimeSaved)> FindCheats(string[,] grid, (int Row, int Col) start, (int Row, int Col) end, int baselineTime)
        {
            List<(int Row, int Col, int TimeSaved)> cheats = new List<(int Row, int Col, int TimeSaved)>();
            int rows = grid.GetLength(0);
            int cols = grid.GetLength(1);

            for (int r = 0; r < rows; r++)
            {
                if (r == 1)
                {

                }
                for (int c = 0; c < cols; c++)
                {
                    if (c == 8)
                    {

                    }
                    if (grid[r, c] == "#")
                    {
                        grid[r, c] = "."; // Temporarily make it walkable
                        int cheatedTime = BFS(grid, start, end, false);
                        grid[r, c] = "#"; // Revert the wall
                        int timeSaved = baselineTime - cheatedTime;

                        if (!_test)
                        {
                            if (timeSaved >= 100)
                            {
                                cheats.Add((r, c, timeSaved));
                            }
                        }
                        else
                        {
                            if (timeSaved >= 1)
                            {
                                cheats.Add((r, c, timeSaved));
                            }
                        }
                    }
                }
            }

            return cheats;
        }
        static (int dx, int dy)[] directions = new (int, int)[]
        {
            (0, 1),   // destra
            (1, 0),   // giù
            (0, -1),  // sinistra
            (-1, 0)   // su
        };

        static (int, int) FindPosition(string[,] map, char target)
        {
            for (int i = 0; i < map.GetLength(0); i++)
                for (int j = 0; j < map.GetLength(1); j++)
                    if (map[i, j][0] == target)
                        return (i, j);
            return (-1, -1);
        }

        /*public void Part2(object input, bool test, ref object solution)
        {
            _test = test;
            string inputText = (string)input;
            n = inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None)[0].Length;
            Map = new string[n, n];
            StoryMap = new string[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Map[i, j] = inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None)[i].Substring(j, 1).ToString();
                    StoryMap[i, j] = inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None)[i].Substring(j, 1).ToString();
                    if (Map[i, j] == "S")
                    {
                        StartPosition[0] = i;
                        StartPosition[1] = j;
                        Map[i, j] = "S";
                        StoryMap[i, j] = "X";
                    }
                    if (Map[i, j] == "E")
                    {
                        EndPoint[0] = i;
                        EndPoint[1] = j;
                    }
                }
            }
            AOC.Utilities.Map.Map.ShowStringMap(Map);
            var (start, end) = FindStartAndEnd(Map);
            int baselineTime = ExtendedBFS(Map, start, end, false);
            List<(int Row, int Col, int TimeSaved)> cheats = FindExtendedCheats(Map, start, end, baselineTime);

            Console.WriteLine($"Number of cheats saving at least 100 picoseconds: {cheats.Count}");
            foreach (var cheat in cheats)
            {
                Console.WriteLine($"Cheat at ({cheat.Row}, {cheat.Col}) saves {cheat.TimeSaved} picoseconds");
            }
            solution = cheats.Count;
        }*/
        public static int maxCheat = 20;
        static int ExtendedBFS(string[,] grid, (int Row, int Col) start, (int Row, int Col) end, bool allowCheat, int ncheat = 0)
        {
            int rows = grid.GetLength(0);
            int cols = grid.GetLength(1);
            var directions = new (int Row, int Col)[] { (-1, 0), (1, 0), (0, -1), (0, 1) };
            var queue = new Queue<(int Row, int Col, int Steps, bool Cheated, int Cheat)>();
            var visited = new HashSet<(int Row, int Col, bool Cheated)>();

            queue.Enqueue((start.Row, start.Col, 0, false, ncheat));

            while (queue.Count > 0)
            {
                var (row, col, steps, cheated, cheat) = queue.Dequeue();

                if ((row, col) == end)
                    return steps;

                if (visited.Contains((row, col, cheated)))
                    continue;

                visited.Add((row, col, cheated));

                foreach (var (dRow, dCol) in directions)
                {
                    int newRow = row + dRow;
                    int newCol = col + dCol;

                    if (newRow >= 0 && newRow < rows && newCol >= 0 && newCol < cols)
                    {
                        string cell = grid[newRow, newCol];
                        if (cell == "." || cell == "E")
                            queue.Enqueue((newRow, newCol, steps + 1, cheated, cheat));
                        else if (cell == "#" && allowCheat && cheat < maxCheat)
                            queue.Enqueue((newRow, newCol, steps + 1, true, cheat + 1));
                    }
                }
            }

            return int.MaxValue; // No path found
        }

        static List<(int Row, int Col, int TimeSaved)> FindExtendedCheats(
            string[,] grid,
            (int Row, int Col) start,
            (int Row, int Col) end,
            int baselineTime)
        {
            List<(int Row, int Col, int TimeSaved)> cheats = new List<(int Row, int Col, int TimeSaved)>();
            int rows = grid.GetLength(0);
            int cols = grid.GetLength(1);

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    if (grid[r, c] == "#")
                    {
                        grid[r, c] = "."; // Temporarily make it walkable
                        int cheatedTime = ExtendedBFS(grid, start, end, true, 1);
                        grid[r, c] = "#"; // Revert the wall
                        int timeSaved = baselineTime - cheatedTime;

                        if (!_test)
                        {
                            if (timeSaved >= 100)
                            {
                                cheats.Add((r, c, timeSaved));
                            }
                        }
                        else
                        {
                            if (timeSaved >= 50)
                            {
                                cheats.Add((r, c, timeSaved));
                            }
                        }
                    }
                }
            }

            return cheats;
        }
        public void Part2(object input, bool test, ref object solution)
        {
            // Dividi in righe
            string[] lines = input.ToString().Split(Delimiter.delimiter_line,StringSplitOptions.None);

            // Crea la matrice bidimensionale
            int rows = lines.Length;
            int cols = lines[0].Length;
            char[,] grid = new char[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    grid[i, j] = lines[i][j];
                }
            }

            // Stampa la griglia per verifica
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Console.Write(grid[i, j]);
                }
                Console.WriteLine();
            }
            int N = grid.GetLength(0);

            // Funzione per verificare se una posizione è valida
            bool InGrid(int i, int j) => i >= 0 && i < N && j >= 0 && j < N;

            int si = 0, sj = 0, ei = 0, ej = 0;

            // Trova le coordinate di "S" e "E"
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    if (grid[i, j] == 'S') { si = i; sj = j; }
                    if (grid[i, j] == 'E') { ei = i; ej = j; }
                }
            }

            // Direzioni di movimento
            int[][] directions = new int[][]
            {
            new int[] { 1, 0 },
            new int[] { 0, 1 },
            new int[] { -1, 0 },
            new int[] { 0, -1 }
            };

            // Determina il percorso originale
            List<(int, int)> path = new List<(int, int)> { (si, sj) };
            while (path.Last() != (ei, ej))
            {
                (int i, int j) = path.Last();
                foreach (var dir in directions)
                {
                    int ii = i + dir[0];
                    int jj = j + dir[1];
                    if (!InGrid(ii, jj)) continue;
                    if (path.Count > 1 && (ii, jj) == path[path.Count - 2]) continue;
                    if (grid[ii, jj] == '#') continue;

                    path.Add((ii, jj));
                    break;
                }
            }

            int og = path.Count - 1;

            // Calcolo dei tempi
            Dictionary<(int, int), int> times = new Dictionary<(int, int), int>();
            for (int t = 0; t < path.Count; t++)
            {
                times[path[t]] = og - t;
            }

            int maxLen = 20;
            Dictionary<(int, int, int, int), int> saved = new Dictionary<(int, int, int, int), int>();
            Dictionary<int, int> counts = new Dictionary<int, int>();
            int ans = 0;

            foreach (var (i, j) in path)
            {
                for (int ii = i - maxLen; ii <= i + maxLen; ii++)
                {
                    for (int jj = j - maxLen; jj <= j + maxLen; jj++)
                    {
                        int timeUsed = Math.Abs(ii - i) + Math.Abs(jj - j);
                        if (!InGrid(ii, jj) || timeUsed > maxLen || grid[ii, jj] == '#') continue;

                        if (!times.ContainsKey((ii, jj))) continue;

                        int remT = times[(ii, jj)];
                        int savedValue = og - (times[(i, j)] + remT + timeUsed);
                        saved[(i, j, ii, jj)] = savedValue;
                    }
                }
            }

            // Calcolo del risultato
            foreach (var value in saved.Values)
            {
                if (value >= 0)
                {
                    if (!counts.ContainsKey(value)) counts[value] = 0;
                    counts[value]++;
                }
                if (value >= 100) ans++;
            }

            Console.WriteLine(ans);
            solution = ans;
        }
        
    }
}