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
    [ResearchAlghoritms(ResearchAlghoritmsAttribute.TypologyEnum.BFS)]
    public class Day20 : Solver, IDay
    {
        public static string[,] Map;
        public static string[,] StoryMap;
        public static int n;
        public static int[] StartPosition = new int[2];
        public static int[] EndPoint = new int[2];
        //public static MinPQNode<Node> SearchNodes;
        //public static MinPQNode<Node> PreviousSearchNodes;
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
                            cheats.Add((r, c, timeSaved));
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

        static List<(int Row, int Col, int TimeSaved)> FindExtendedCheats(string[,] grid, (int Row, int Col) start, (int Row, int Col) end, int baselineTime, int maxCheatTime)
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
                        grid[r, c] = "."; // Temporaneamente rende la cella camminabile
                        int cheatedTime = ExtendedBFS(grid, start, end, maxCheatTime);
                        grid[r, c] = "#"; // Ripristina il muro
                        int timeSaved = baselineTime - cheatedTime;

                        if (cheatedTime < baselineTime) // Solo cheat validi
                        {
                            cheats.Add((r, c, timeSaved));
                            Console.WriteLine($"Cheat at ({r}, {c}): Saves {timeSaved} picoseconds");
                        }
                    }
                }
            }

            return cheats;
        }
        /*static int ExtendedBFS(string[,] grid, (int Row, int Col) start, (int Row, int Col) end, int maxCheatTime)
        {
            int rows = grid.GetLength(0);
            int cols = grid.GetLength(1);
            var directions = new (int Row, int Col)[] { (-1, 0), (1, 0), (0, -1), (0, 1) };
            var queue = new Queue<(int Row, int Col, int Steps, bool Cheated, int CheatTime)>();
            var visited = new HashSet<(int Row, int Col, bool Cheated)>();

            queue.Enqueue((start.Row, start.Col, 0, false, 0));

            while (queue.Count > 0)
            {
                var (row, col, steps, cheated, cheatTime) = queue.Dequeue();

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
                            queue.Enqueue((newRow, newCol, steps + 1, cheated, cheatTime));
                        else if (cell == "#" && !cheated && cheatTime < maxCheatTime)
                            queue.Enqueue((newRow, newCol, steps + 1, true, cheatTime + 1));
                    }
                }
            }

            return int.MaxValue; // No path found
        }*/

        static (int, int) FindPosition(string[,] map, char target)
        {
            for (int i = 0; i < map.GetLength(0); i++)
                for (int j = 0; j < map.GetLength(1); j++)
                    if (map[i, j][0] == target)
                        return (i, j);
            return (-1, -1);
        }

        static int ExtendedBFS(string[,] grid, (int Row, int Col) start, (int Row, int Col) end, int maxCheatTime)
        {
            int rows = grid.GetLength(0);
            int cols = grid.GetLength(1);
            var directions = new (int Row, int Col)[] { (-1, 0), (1, 0), (0, -1), (0, 1) };

            var queue = new Queue<(int Row, int Col, int Steps, bool Cheated, int CheatTime)>();
            var visited = new HashSet<(int Row, int Col, bool Cheated, int CheatTime)>();

            queue.Enqueue((start.Row, start.Col, 0, false, 0));

            while (queue.Count > 0)
            {
                var (row, col, steps, cheated, cheatTime) = queue.Dequeue();

                if ((row, col) == end)
                    return steps;

                if (visited.Contains((row, col, cheated, cheatTime)))
                    continue;

                visited.Add((row, col, cheated, cheatTime));

                foreach (var (dRow, dCol) in directions)
                {
                    int newRow = row + dRow;
                    int newCol = col + dCol;

                    if (newRow >= 0 && newRow < rows && newCol >= 0 && newCol < cols)
                    {
                        string cell = grid[newRow, newCol];

                        // Movimento normale
                        if (cell == "." || cell == "E")
                        {
                            queue.Enqueue((newRow, newCol, steps + 1, cheated, cheatTime));
                        }
                        // Movimento con cheat (se possibile)
                        else if (cell == "#" && !cheated && cheatTime + 1 <= maxCheatTime)
                        {
                            queue.Enqueue((newRow, newCol, steps + 1, true, cheatTime + 1));
                        }
                    }
                }
            }

            return int.MaxValue; // Nessun percorso trovato
        }

        static List<(int Row, int Col, int TimeSaved)> FindCheatsWithExtendedRules(string[,] grid, (int Row, int Col) start, (int Row, int Col) end, int baselineTime, int maxCheatTime)
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
                        grid[r, c] = "."; // Temporaneamente rendi la cella percorribile
                        int cheatedTime = ExtendedBFS(grid, start, end, maxCheatTime);
                        grid[r, c] = "#"; // Ripristina il muro

                        int timeSaved = baselineTime - cheatedTime;

                        // Verifica se il tempo risparmiato è positivo e valido
                        if (_test)
                        {
                            if (timeSaved >= 50)
                            {
                                cheats.Add((r, c, timeSaved));
                            }
                        }
                        else
                        {
                            if (timeSaved >= 100)
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
            _test = test;
            string inputText = (string)input;
            n = inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None)[0].Length;
            Map = new string[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Map[i, j] = inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None)[i].Substring(j, 1).ToString();
                    if (Map[i, j] == "S")
                    {
                        StartPosition[0] = i;
                        StartPosition[1] = j;
                        Map[i, j] = "S";
                    }
                    if (Map[i, j] == "E")
                    {
                        EndPoint[0] = i;
                        EndPoint[1] = j;
                    }
                }
            }

            var (start, end) = FindStartAndEnd(Map);
            int baselineTime = ExtendedBFS(Map, start, end, 0);

            List<(int Row, int Col, int TimeSaved)> cheats = FindCheatsWithExtendedRules(Map, start, end, baselineTime, 20);
            int nT = _test ? 50 : 100;
            Console.WriteLine($"Number of cheats saving at least {nT} picoseconds: {cheats.Count}");
            foreach (var cheat in cheats)
            {
                Console.WriteLine($"Cheat at ({cheat.Row}, {cheat.Col}) saves {cheat.TimeSaved} picoseconds");
            }

            solution = cheats.Count;
        }

    }
}
