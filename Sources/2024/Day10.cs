using AOC;
using AOC.DataStructures.Clustering;
using AOC.Model;
using AOC.SearchAlghoritmhs;
using AOC.Utilities.Map;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using static AOC.SearchAlghoritmhs.ResearchAlghoritmsAttribute;

namespace AOC2024
{
    [ResearchAlghoritms(TypologyEnum.Map)]
    [ResearchAlghoritms(TypologyEnum.Recursive)]

    public class Day10: Solver, IDay
    {
        public static string[,] TopographicMap;
        public static string[,] StoryMap;
        public static int n;
        public static List<Tuple<int, int>> TrailHeads = new List<Tuple<int, int>>();
        public static int[] HikePoint = new int[2];
        public static int[] NextPoint = new int[2];
        
        public void Part1(object input, bool test, ref object solution)
        {
            List<string> inputList = new List<string>();
            inputList = (List<string>)input;
            n = inputList.Count();
            TopographicMap = new string[n, n];
            StoryMap = new string[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    TopographicMap[i, j] = inputList[i].Substring(j, 1);
                    StoryMap[i, j] = inputList[i].Substring(j, 1);
                    if (TopographicMap[i, j] == "0")
                    {
                        TrailHeads.Add(Tuple.Create(i, j));
                    }
                }
            }
            ShowMap();
            int score = 0;
            foreach(var th in TrailHeads)
            {
                score += FindScore(th);
            }
            solution = score;
        }

        public static int FindScore(Tuple<int, int> trailHead)
        {
            return ExploreMap(trailHead);
        }

        public static int ExploreMap(Tuple<int, int> start)
        {
            int startValue = int.Parse(TopographicMap[start.Item1, start.Item2]);
            bool[,] visited = new bool[n, n];
            HashSet<Tuple<int, int>> countedCells = new HashSet<Tuple<int, int>>();
            int score = 0;

            Explore(start.Item1, start.Item2, startValue, visited, countedCells, ref score, true);

            Console.WriteLine($"Total score: {score}");
            return score;
        }

        private static void Explore(
            int row,
            int col,
            int currentValue,
            bool[,] visited,
            HashSet<Tuple<int, int>> countedCells,
            ref int score,
            bool isInitialCall)
        {
            if (row < 0 || row >= n || col < 0 || col >= n)
                return;

            if (visited[row, col])
                return;

            if (!int.TryParse(TopographicMap[row, col], out int cellValue))
                return;

            if (!isInitialCall && cellValue != currentValue + 1)
                return;

            visited[row, col] = true;

            if (cellValue == 9)
            {
                var cellCoordinates = Tuple.Create(row, col);
                if (!countedCells.Contains(cellCoordinates))
                {
                    countedCells.Add(cellCoordinates);
                    score++;
                }
            }

            Explore(row - 1, col, cellValue, visited, countedCells, ref score, false);
            Explore(row + 1, col, cellValue, visited, countedCells, ref score, false);
            Explore(row, col - 1, cellValue, visited, countedCells, ref score, false);
            Explore(row, col + 1, cellValue, visited, countedCells, ref score, false);

            visited[row, col] = false;
        }

        public void ShowMap()
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
                    Console.Write(TopographicMap[i, j]);
                }
                Console.Write(i);
                Console.WriteLine("");
            }
        }

        public void Part2(object input, bool test, ref object solution)
        {
            List<string> inputList = new List<string>();
            inputList = (List<string>)input;
            n = inputList.Count();
            TopographicMap = new string[n, n];
            StoryMap = new string[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    TopographicMap[i, j] = inputList[i].Substring(j, 1);
                    StoryMap[i, j] = inputList[i].Substring(j, 1);
                    if (TopographicMap[i, j] == "0")
                    {
                        TrailHeads.Add(Tuple.Create(i, j));
                    }
                }
            }
            ShowMap();
            int score = 0;
            foreach (var th in TrailHeads)
            {
                score += FindScore_DistinctPath(th);
            }
            solution = score;
        }

        public static int FindScore_DistinctPath(Tuple<int, int> trailHead)
        {
            return ExploreMapDistinctPath(trailHead);
        }
        public static int ExploreMapDistinctPath(Tuple<int, int> start)
        {
            int startValue = int.Parse(TopographicMap[start.Item1, start.Item2]);
            int score = 0;

            ExploreDistinct(start.Item1, start.Item2, startValue, new bool[n, n], ref score, true);

            Console.WriteLine($"Total score (distinct paths): {score}");
            return score;
        }

        private static void ExploreDistinct(
            int row,
            int col,
            int currentValue,
            bool[,] visited,
            ref int score,
            bool isInitialCall)
        {
            if (row < 0 || row >= n || col < 0 || col >= n)
                return;

            if (visited[row, col])
                return;

            if (!int.TryParse(TopographicMap[row, col], out int cellValue))
                return;

            if (!isInitialCall && cellValue != currentValue + 1)
                return;

            visited[row, col] = true;

            if (cellValue == 9)
            {
                score++;
            }

            ExploreDistinct(row - 1, col, cellValue, (bool[,])visited.Clone(), ref score, false);
            ExploreDistinct(row + 1, col, cellValue, (bool[,])visited.Clone(), ref score, false);
            ExploreDistinct(row, col - 1, cellValue, (bool[,])visited.Clone(), ref score, false);
            ExploreDistinct(row, col + 1, cellValue, (bool[,])visited.Clone(), ref score, false);
        }
    }
}