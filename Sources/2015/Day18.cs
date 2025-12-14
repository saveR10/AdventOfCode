using AOC;
using AOC.Model;
using AOC.SearchAlghoritmhs;
using AOC.Utilities.Map;
using System;
using System.Linq;
using static AOC.SearchAlghoritmhs.ResearchAlgorithmsAttribute;
using Solver = AOC.Solver;

namespace AOC2015
{
    [ResearchAlgorithms(title: "Day 18: Like a GIF For Your Yard",
                        TypologyEnum.Map | TypologyEnum.Game, //Conway's Game of Life
                        ResolutionEnum.BruteForce,
                        DifficultEnum.Medium,
                        "Simulazione di un automa cellulare su griglia 2D (Game of Life), aggiornamento simultaneo delle celle basato sugli stati dei vicini")]

    public class Day18 : Solver, IDay
    {
        public void Part1(object input, bool test, ref object solution)
        {

            string inputText = (string)input;
            var inputlist = inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None);
            int dimGrid = inputlist.Length - 1;
            bool[,] grid = new bool[dimGrid, dimGrid];
            for (int r = 0; r < dimGrid; r++)
            {
                for (int c = 0; c < dimGrid; c++)
                {
                    if (inputlist[r][c].Equals('#')) grid[r, c] = true;
                }
            }
            int step = test ? 4 : 100;
            for (int s = 0; s < step; s++)
            {
                bool[,] NewGrid = new bool[dimGrid, dimGrid];
                for (int r = 0; r < dimGrid; r++)
                {
                    for (int c = 0; c < dimGrid; c++)
                    {
                        int Neighbors = 0;
                        if (r > 0) if (grid[r - 1, c]) Neighbors++;
                        if (r > 0 && c < dimGrid - 1) if (grid[r - 1, c + 1]) Neighbors++;
                        if (c < dimGrid - 1) if (grid[r, c + 1]) Neighbors++;
                        if (c < dimGrid - 1 && r < dimGrid - 1) if (grid[r + 1, c + 1]) Neighbors++;
                        if (r < dimGrid - 1) if (grid[r + 1, c]) Neighbors++;
                        if (r < dimGrid - 1 && c > 0) if (grid[r + 1, c - 1]) Neighbors++;
                        if (c > 0) if (grid[r, c - 1]) Neighbors++;
                        if (c > 0 && r > 0) if (grid[r - 1, c - 1]) Neighbors++;
                        if (grid[r, c])
                        {
                            if (Neighbors == 2 || Neighbors == 3) NewGrid[r, c] = true;
                        }
                        else
                        {
                            if (Neighbors == 3) NewGrid[r, c] = true;
                        }
                    }
                }
                grid = NewGrid;
                //Map.ShowBooleanMap(NewGrid,dimGrid);
            }
            solution = grid.Cast<bool>().ToList().Count(va => va);
        }




        public void Part2(object input, bool test, ref object solution)
        {
            string inputText = (string)input;
            var inputlist = inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None);
            int dimGrid = test? inputlist.Length:inputlist.Length - 1;
            bool[,] grid = new bool[dimGrid, dimGrid];
            for (int r = 0; r < dimGrid; r++)
            {
                for (int c = 0; c < dimGrid; c++)
                {
                    if (inputlist[r][c].Equals('#')) grid[r, c] = true;
                }
            }
            grid[0, 0] = true;
            grid[dimGrid - 1, 0] = true;
            grid[0, dimGrid - 1] = true;
            grid[dimGrid - 1, dimGrid - 1] = true;
            Map.ShowBooleanMap(grid, dimGrid);

            Console.WriteLine();
            int step = test ? 5 : 100;
            for (int s = 0; s < step; s++)
            {
                bool[,] NewGrid = new bool[dimGrid, dimGrid];
                for (int r = 0; r < dimGrid; r++)
                {
                    for (int c = 0; c < dimGrid; c++)
                    {
                        int Neighbors = 0;
                        if (r > 0) if (grid[r - 1, c]) Neighbors++;
                        if (r > 0 && c < dimGrid - 1) if (grid[r - 1, c + 1]) Neighbors++;
                        if (c < dimGrid - 1) if (grid[r, c + 1]) Neighbors++;
                        if (c < dimGrid - 1 && r < dimGrid - 1) if (grid[r + 1, c + 1]) Neighbors++;
                        if (r < dimGrid - 1) if (grid[r + 1, c]) Neighbors++;
                        if (r < dimGrid - 1 && c > 0) if (grid[r + 1, c - 1]) Neighbors++;
                        if (c > 0) if (grid[r, c - 1]) Neighbors++;
                        if (c > 0 && r > 0) if (grid[r - 1, c - 1]) Neighbors++;
                        if (grid[r, c])
                        {
                            if (Neighbors == 2 || Neighbors == 3) NewGrid[r, c] = true;
                        }
                        else
                        {
                            if (Neighbors == 3) NewGrid[r, c] = true;
                        }
                    }
                }
                grid = NewGrid;
                grid[0, 0] = true;
                grid[dimGrid-1, 0] = true;
                grid[0, dimGrid-1] = true;
                grid[dimGrid-1, dimGrid - 1] = true;
               //Map.ShowBooleanMap(grid,dimGrid);
            }
            solution = grid.Cast<bool>().ToList().Count(va => va);
        }
    }
}