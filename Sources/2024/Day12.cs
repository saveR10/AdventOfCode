using AOC;
using AOC.DataStructures.Clustering;
using AOC.Model;
using AOC.SearchAlghoritmhs;
using AOC.Utilities.Map;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AOC2024
{
    [ResearchAlghoritms(ResearchAlghoritmsAttribute.TypologyEnum.DFS)]
    public class Day12 : Solver, IDay
    {
        public static char[,] GardenMap;
        public static int n;
        public void Part1(object input, bool test, ref object solution)
        {
            List<string> inputList = new List<string>();
            inputList = (List<string>)input;
            n = inputList.Count();
            GardenMap = new char[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    GardenMap[i, j] = inputList[i].Substring(j, 1).ToCharArray()[0];
                }
            }
            int totalPrice = CalculateFencePrice(GardenMap);
            solution = totalPrice;
        }

        public static int CalculateFencePrice(char[,] grid)
        {
            int rows = grid.GetLength(0);
            int cols = grid.GetLength(1);
            bool[,] visited = new bool[rows, cols];
            int totalPrice = 0;

            int[] dX = { -1, 1, 0, 0 };
            int[] dY = { 0, 0, -1, 1 };

            bool IsValid(int x, int y, char plant)
            {
                return x >= 0 && x < rows && y >= 0 && y < cols && !visited[x, y] && grid[x, y] == plant;
            }

            (int area, int perimeter) DFS(int startX, int startY, char plant)
            {
                Stack<(int x, int y)> stack = new Stack<(int, int)>();
                stack.Push((startX, startY));
                int area = 0;
                int perimeter = 0;

                while (stack.Count > 0)
                {
                    var (x, y) = stack.Pop();
                    if (visited[x, y]) continue;

                    visited[x, y] = true;
                    area++;

                    for (int i = 0; i < 4; i++)
                    {
                        int newX = x + dX[i];
                        int newY = y + dY[i];

                        if (newX < 0 || newY < 0 || newX >= rows || newY >= cols || grid[newX, newY] != plant)
                        {
                            perimeter++; // Bordo della regione
                        }
                        else if (!visited[newX, newY])
                        {
                            stack.Push((newX, newY));
                        }
                    }
                }

                return (area, perimeter);
            }

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    if (!visited[r, c])
                    {
                        char plant = grid[r, c];
                        var (area, perimeter) = DFS(r, c, plant);
                        totalPrice += area * perimeter;
                    }
                }
            }

            return totalPrice;
        }

        public void Part2(object input, bool test, ref object solution)
        {
            // Preparazione dell'input
            List<string> inputList = (List<string>)input;
            int n = inputList.Count;
            char[,] GardenMap = new char[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    GardenMap[i, j] = inputList[i][j];
                }
            }

            // Trova le regioni basate sui caratteri
            List<(List<(int, int)>, char)> regions = FindRegions(GardenMap);

            // Calcola i confini
            int[] borderCount = CountBorders(regions);
            int[] discountBorderCount = CountDiscountBorders(regions);

            // Verifica
            Debug.Assert(borderCount.Length == regions.Count);

            // Calcola i prezzi e gli sconti
            int[] price = new int[regions.Count];
            (int, char)[] discounts = new (int, char)[regions.Count];
            for (int i = 0; i < regions.Count; i++)
            {
                price[i] = regions[i].Item1.Count * borderCount[i];
                discounts[i] = (regions[i].Item1.Count * discountBorderCount[i], regions[i].Item2);
            }

            // Risultato finale
            solution = $"Price: {price.Sum()}, Discounted: {discounts.Sum(x => x.Item1)}";
        }

        // Trova le regioni basate sui caratteri presenti nella mappa
        private static List<(List<(int, int)>, char)> FindRegions(char[,] GardenMap)
        {
            int n = GardenMap.GetLength(0);
            var chars = GardenMap.Cast<char>().Distinct(); // Ottieni tutti i caratteri unici nella mappa
            List<(List<(int, int)>, char)> result = new List<(List<(int, int)>, char)>();

            foreach (var c in chars)
            {
                result.AddRange(FindAllRegions(GardenMap, c));
            }

            return result;
        }

        // Trova tutte le regioni per un dato carattere
        private static List<(List<(int y, int x)>, char)> FindAllRegions(char[,] GardenMap, char value)
        {
            int n = GardenMap.GetLength(0);
            List<(int y, int x)> cells = new List<(int y, int x)>();

            for (int y = 0; y < n; y++)
            {
                for (int x = 0; x < n; x++)
                {
                    if (GardenMap[y, x] == value)
                    {
                        cells.Add((y, x));
                    }
                }
            }

            return FindAreas(cells, value);
        }

        // Divide le celle in aree connesse
        private static List<(List<(int, int)>, char)> FindAreas(List<(int y, int x)> cells, char value)
        {
            List<List<(int y, int x)>> areas = new List<List<(int y, int x)>>();
            HashSet<(int, int)> visited = new HashSet<(int, int)>();

            foreach (var cell in cells)
            {
                if (!visited.Contains(cell))
                {
                    List<(int y, int x)> area = new List<(int y, int x)>();
                    ExploreArea(cell, cells, visited, area);
                    areas.Add(area);
                }
            }

            return areas.Select(area => (area, value)).ToList();
        }

        // Esplora un'area connessa
        private static void ExploreArea((int y, int x) cell, List<(int y, int x)> cells, HashSet<(int, int)> visited, List<(int y, int x)> area)
        {
            Stack<(int y, int x)> stack = new Stack<(int y, int x)>();
            stack.Push(cell);

            while (stack.Count > 0)
            {
                var current = stack.Pop();
                if (visited.Add(current)) // Aggiunge se non già visitato
                {
                    area.Add(current);

                    // Controlla le celle adiacenti
                    foreach (var neighbor in GetNeighbors(current, cells))
                    {
                        stack.Push(neighbor);
                    }
                }
            }
        }

        // Ottieni i vicini di una cella
        private static IEnumerable<(int y, int x)> GetNeighbors((int y, int x) cell, List<(int y, int x)> cells)
        {
            int[] dy = { -1, 0, 1, 0 };
            int[] dx = { 0, 1, 0, -1 };

            for (int i = 0; i < 4; i++)
            {
                var neighbor = (cell.y + dy[i], cell.x + dx[i]);
                if (cells.Contains(neighbor))
                {
                    yield return neighbor;
                }
            }
        }


        // Conta i confini per ogni regione
        private static int[] CountBorders(List<(List<(int y, int x)>, char)> regions)
        {
            int[] result = new int[regions.Count];

            for (int i = 0; i < regions.Count; i++)
            {
                var current = regions[i].Item1;
                int count = 0;

                foreach (var cell in current)
                {
                    // Conta i lati aperti
                    count += 4 - GetNeighbors(cell, current).Count();
                }

                result[i] = count;
            }

            return result;
        }

        // Conta i confini per gli sconti
        private static int[] CountDiscountBorders(List<(List<(int y, int x)>, char)> regions)
        {
            int[] result = new int[regions.Count];

            for (int i = 0; i < regions.Count; i++)
            {
                var current = regions[i].Item1;

                // Usa dizionari per calcolare i confini unici
                Dictionary<int, List<int>> horizontalTop = new Dictionary<int, List<int>>();
                Dictionary<int, List<int>> horizontalBottom = new Dictionary<int, List<int>>();
                Dictionary<int, List<int>> verticalLeft = new Dictionary<int, List<int>>();
                Dictionary<int, List<int>> verticalRight = new Dictionary<int, List<int>>();

                foreach (var item in current)
                {
                    // Logica per i bordi
                    if (!current.Any(c => c == (item.y - 1, item.x))) AddToDictionary(horizontalTop, item.y, item.x);
                    if (!current.Any(c => c == (item.y + 1, item.x))) AddToDictionary(horizontalBottom, item.y, item.x);
                    if (!current.Any(c => c == (item.y, item.x - 1))) AddToDictionary(verticalLeft, item.x, item.y);
                    if (!current.Any(c => c == (item.y, item.x + 1))) AddToDictionary(verticalRight, item.x, item.y);
                }

                result[i] += CountLines(horizontalTop);
                result[i] += CountLines(horizontalBottom);
                result[i] += CountLines(verticalLeft);
                result[i] += CountLines(verticalRight);
            }

            return result;
        }

        // Aggiunge a un dizionario
        private static void AddToDictionary(Dictionary<int, List<int>> dict, int key, int value)
        {
            if (!dict.TryGetValue(key, out var list))
            {
                list = new List<int>();
                dict[key] = list;
            }
            list.Add(value);
        }

        // Conta le linee uniche
        private static int CountLines(Dictionary<int, List<int>> dict)
        {
            int result = 0;

            foreach (var list in dict.Values)
            {
                list.Sort();
                int count = 1;
                for (int i = 1; i < list.Count; i++)
                {
                    if (list[i] - list[i - 1] > 1)
                    {
                        count++;
                    }
                }
                result += count;
            }

            return result;
        }


    }
}

