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
    public class Day18 : Solver, IDay
    {
        public static string[,] Map;
        public static string[,] AntinodesMap;
        public static int n;
        public static int[] StartPosition = new int[2];
        public static int[] EndPoint = new int[2];

        public void Part1(object input, bool test, ref object solution)
        {
            string inputText = (string)input;

            var bytePositions = inputText.Split('\n')
                                 .Select(line => line.Split(',').Select(int.Parse).ToArray())
                                 .ToArray();

            // Define the grid dimensions
            int gridSize; // Example case (0 to 6), use 71 for the full problem
            if (test)
            {
                gridSize = 7;
            }
            else gridSize = 71;
            StartPosition[0] = 0; StartPosition[1] = 0;
            EndPoint[0] = gridSize-1; EndPoint[1] = gridSize-1;
            char[,] grid = new char[gridSize, gridSize];

            // Initialize grid
            for (int x = 0; x < gridSize; x++)
                for (int y = 0; y < gridSize; y++)
                    grid[x, y] = '.';

            // Corrupt memory based on falling bytes
            int byteLimit; // Simulate up to 1024 bytes
            if (test) byteLimit = 12; else byteLimit=1024;
            for (int i = 0; i < Math.Min(byteLimit, bytePositions.Length); i++)
            {
                int x = bytePositions[i][0];
                int y = bytePositions[i][1];
                grid[y, x] = '#';
            }
            AOC.Utilities.Map.Map.ShowCharMap(grid, gridSize, gridSize);

            // Find shortest path using BFS
            int shortestPath = FindShortestPath(grid, gridSize);

            Console.WriteLine($"Minimum number of steps to reach the exit: {shortestPath}");
        }

        static int FindShortestPath(char[,] grid, int gridSize)
        {
            var directions = new (int dx, int dy)[] { (0, 1), (1, 0), (0, -1), (-1, 0) };
            var queue = new Queue<(int x, int y, int steps)>();
            var visited = new HashSet<(int, int)>();

            queue.Enqueue((0, 0, 0)); // Start at top-left corner
            visited.Add((0, 0));

            while (queue.Count > 0)
            {
                var (x, y, steps) = queue.Dequeue();

                // Check if we reached the bottom-right corner
                if (x == gridSize - 1 && y == gridSize - 1)
                    return steps;

                // Explore neighbors
                foreach (var (dx, dy) in directions)
                {
                    int nx = x + dx;
                    int ny = y + dy;

                    if (nx >= 0 && ny >= 0 && nx < gridSize && ny < gridSize &&
                        grid[nx, ny] != '#' && !visited.Contains((nx, ny)))
                    {
                        queue.Enqueue((nx, ny, steps + 1));
                        visited.Add((nx, ny));
                    }
                }
            }

            // If no path is found, return -1 (or another value to indicate failure)
            return -1;
        }

        public void Part2(object input, bool test, ref object solution)
        {
            string inputText = (string)input;

            var bytePositions = inputText.Split('\n')
                                 .Select(line => line.Split(',').Select(int.Parse).ToArray())
                                 .ToArray();

            // Define the grid dimensions
            int gridSize; // Example case (0 to 6), use 71 for the full problem
            if (test)
            {
                gridSize = 7;
            }
            else gridSize = 71;
            StartPosition[0] = 0; StartPosition[1] = 0;
            EndPoint[0] = gridSize - 1; EndPoint[1] = gridSize - 1;
            char[,] grid = new char[gridSize, gridSize];

            // Initialize grid
            for (int x = 0; x < gridSize; x++)
                for (int y = 0; y < gridSize; y++)
                    grid[x, y] = '.';

            // Corrupt memory based on falling bytes
            int byteLimit; // Simulate up to 1024 bytes
            if (test) byteLimit = 12; else byteLimit = 1024;
            // Simulate the falling bytes
            for (int i = 0; i < bytePositions.Length; i++)
            {
                int x = bytePositions[i][0];
                int y = bytePositions[i][1];

                // Corrupt the byte in the grid
                grid[x, y] = '#';

                // Check if there is still a path from (0, 0) to (gridSize-1, gridSize-1)
                if (!PathExists(grid, gridSize))
                {
                    Console.WriteLine($"The first blocking byte is at: {x},{y}");
                    break;
                }
            }
            AOC.Utilities.Map.Map.ShowCharMap(grid, gridSize, gridSize);

            // Find shortest path using BFS
            int shortestPath = FindShortestPath(grid, gridSize);

            Console.WriteLine($"Minimum number of steps to reach the exit: {shortestPath}");
        }
        static bool PathExists(char[,] grid, int gridSize)
        {
            var directions = new (int dx, int dy)[] { (0, 1), (1, 0), (0, -1), (-1, 0) };
            var queue = new Queue<(int x, int y)>();
            var visited = new HashSet<(int, int)>();

            queue.Enqueue((0, 0)); // Start at top-left corner
            visited.Add((0, 0));

            while (queue.Count > 0)
            {
                var (x, y) = queue.Dequeue();

                // Check if we reached the bottom-right corner
                if (x == gridSize - 1 && y == gridSize - 1)
                    return true;

                // Explore neighbors
                foreach (var (dx, dy) in directions)
                {
                    int nx = x + dx;
                    int ny = y + dy;

                    if (nx >= 0 && ny >= 0 && nx < gridSize && ny < gridSize &&
                        grid[nx, ny] != '#' && !visited.Contains((nx, ny)))
                    {
                        queue.Enqueue((nx, ny));
                        visited.Add((nx, ny));
                    }
                }
            }

            // If no path is found, return false
            return false;
        }
    }
}
