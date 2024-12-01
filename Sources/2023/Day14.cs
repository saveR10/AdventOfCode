using AOC;
using AOC.DataStructures.Clustering;
using AOC.Documents.LINQ;
using AOC.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;

namespace AOC2023
{
    //DA SISTEMARE
    public class Day14 : Solver, IDay
    {
        public static String[] delimiters = { "\r\n", " " };
        public static String[] delimiter_space = { " " };
        public static String[] delimiter_line = { "\r\n" };
        public static String[] delimiter_equals = { "=" };
        public static String[] delimiter_parentesi = { "(", ")", "," };
        public int n = 0;
        public string Platform = "";
        Dictionary<int, string> OldPlatform = new Dictionary<int, string>();
        Dictionary<int, string> NewPlatform = new Dictionary<int, string>();
        bool find_stability = false;

        public string Platform_string = "";
        Dictionary<int, string> PlatformB = new Dictionary<int, string>();
        public void Part1(object input, bool test, ref object solution)
        {
             test = false;

            if (!test)
            {
                Platform = @"..#OO..OO...#..O.OO....OO##O#...O..##.......O.OO..#.O.O#O....#.#O#O####...#.#..#.......O.O..O....#..
.O....#.........OO........O...........#O##O.OOO..O..O.#.#O#O......O..O.O......#O.....###...O..#O....
......O.....OO.O...O.O.O.#...#O...O.O#...O#.O.O.O##.O.....##...OO..#O.O....#.#....O.#O.....##....O..
.##O##...#..###.....O..O#O...........O.##O#OO.O#..OO..OO.#.....OO.O..#.....O..........O..O..O#.O.O..
...O.......O.#O.....OO.#.###....#O.O.##.O..O....O...#.O#O.#.#.O#......O.....#.........O..O##..O.O...
......O#....O..O..O...#..#........#.#..#........O.O#O.OO.O#.OO...#O.O#.O....#O.O.O#....O...O.#.O....
#O#.O....O.O...O..O#..O#..O##O#...........#....O...#.O....OO...O..O..O..OO..#.O...#......##...OO....
..OO#O....#O.O##...O#.OOO..O.O.O...#O#...O.#.OO....#....O..O#O....#..#...#..##.#....O.#...O.....O.O#
...##.#O#..O#..#....O#..OO...#..#......OO##O..O.#..##.#...#.O#..O...O....#....OO#...#..O.O....O.#..#
.#....#O..O...O.#OOO#..OO...O.O..##..#....##O..#....O....O.....#.#..O..#..#.O#...O#O....O....O.O....
...O.O##.OO.O.#O..O....##...O....O.##O##.O#.....#..#.....O.OO#O.O...#...#.O..#.#O...#O.#...O..OOO.OO
.......##......#.O.#.....#.....OO..O.....O#O.O....OO.O...OOO...#O..#O.O...##..O.O.#...O....O...O....
....OOO..#.#OO..O...#..#..OO.O.#.O..#...O....OOO.O.#.OO#.#......#..#.O.O.........O#...O.#.....##.O..
.....#.O..O#..O...O.#..#.......#..OO#.#.....O#O..O...O.O....OO.#OO..O........O.#.O....O....O....OOO.
#...O.O#OO#.....OO..#......O.O..O...##.O#.O.#.OOO#.#...#.......O..O..O...#.O...O.O....#....O....#...
O.O..#...O..O......##.O....O#.O.O..O#.###OOO....#.O....#.#..##O..#..##..O..#O.....O#.O##...#....#O..
#.......O#.#.#O....#...O.O##.....#O##.#OO.O.#.....OO.....O.OO.O..O...O....#.O.O#.OO#.O#..##......#.#
..##.O.O....O..#.O..O.#......#..#O....O...##...#.O.....O.....OO#.......#..OO##.....#......OO...O.O.O
.....O#....#..#......O#.....#O.O#........#...O.#...O.#.O#.O........O.O#OO.O..O..OO#O....#..#..OOO.O.
.#.....OO...OO......OO.OO.O.......#.O#O.......O##...O.#.#......O#.......O.#.....O.....O#...O...#..OO
..#O.O.O..#...........O.O.....O..#..O#..O...#O...#..OO.O##.O...O.....O.#..OOO....O.O....O...OO#.O...
.O.O..O#.....##..O.....O#...#..O.#O....#...............O...O.....OOO.OOO...O.O...O.#OO#.#...#O.....#
O#OO......#.O#..O#OOO.OO..O.O..OO.O.#O...#..O.#OO.O..O....OO#..#.#O....O..O#OO....O...O.OOO.O#O.....
..##O..O.O#..OO.##O#O.O..O.......OO.#....O..O.OO#O..O.O.OOO.##..#...O#......O....#..O.O#..O.......#.
.##.O.....##.OOO..O.#..O.....OO.#.....O#..#.O.#.....#.#OOOO.O...#...OO..#.......OO#O......#O..O.#...
#.#O.O.O....O..OO...#......O.#O#.#....#...OO.....#O.O#OO.#......#..#...#....OOO#OOO.....O.##O......#
...##O.#.O...O.O...#O......O.......O............#.O.#....#......#.OO..OO.O..O..##.....#OO..OO.#OO...
.........#.O..#..OO.O##..O.....#.....O..#.O#O....O.##..#....O#O.O..OO#.O..........OO....#.....O..#O.
.#....#OOO.#....#.........#.#.O.O##.....#O.OO#O#OO...#...O.......#......#..O..O.OO.O...#.##.O..##.#.
##...O#.#...O..O....O...#...OO.......#...#...O##....#.#..###....O.O##..#.......O....#......O#.O...O#
O.##...#O...O...O.#O.#.#.........O...O..O#.OO....O.#....O...O...#......O...#..O##.O..##O..O...#..#.#
.##..O....O..O#.OOO.O.#O#...###.O.......O##..O###.#........O..OO..O#O...O.OOO.#O.O..#O.#..#......O.O
..#O.OO#..#.#...O##...#...O#..O#O..O.O.......#.O#...#...##..O.#..O..O...#..O.O#.O.#...O...#...#.OO#.
.#O...#O.#.#...#.O.##...#O.O.....OO....O...#.....#.#.O..#..#.O...O.O....OO.OO.O...#......#.#..#..O.#
..O..O#..O....OOO..O.....#..#.#.....O....O..O#O..#..#.......O.....#O.#..OO#..#.....#O##....O#.#.O...
..#.O#O.##O#OO...OO....O...OO......O....O###...#...O..O#OOOO...#OO.O.#.O...#.#.O..O.O#.#..O.#O#.O...
O#O...#........O.#........OOO.....O#.....O.O#...#..O........#.O.....#..#...#...OOO.###.O..##....O..#
..#O#O..........#O....O..O.....O...#.O.O..##..OO.O...#O.#.#O#OO.......#...O...O.O...O...#O..#..O##..
#.O.O#OO#O..#O#.O......O...##..#.#.O.....#....#....O#..O...O..O..#O#....O.#O..O#.....#.O#OO.O....O..
....#O...#...#.#..O.#.O.#.O.....OO#O.OO.#O..##...O..#......#.OO...O..O##O.......OO.#O....O#....O...#
O.O..#O.O.O.O.#.O##...O#.OOOO.......O..O.##.O#.O.#OO...OO.........#.#.OO.O.#O.#O..#.#..O..O..O.#O#..
.O..#..OO..O##.....O...##.#.O.O..###O......O.###......#...#....OOO.....O..O......O..O.O..#O##.O...#.
O..#..O...#..O..O.#..#.O....O.#.##.#..#..OO#..O......OO###..O#O##O..O#............##.OO...O...OO#.#.
..O#.....O...#....O#.O....#.O#.....O.O....OO.##.O..#O.OO.O.O..#..#..O#O#.OO...#...........O..O...#.O
#..#.OO........O.....O.....O..O..O.#O#....O..OO..#....O..O.#..#.......O#..O....OOO...O#O.O.....O....
.#......O.......#O#....OO...O#..OOOOO.#..O..#.OO#O.##....#....#.#...#...##..#O..O#......#......O....
..O....OOOO#.#O.##O.O....#.O..O.O...OO..O...#...#...#..OOO#O........O.#O#..#..O..OOO..##...#..#O..O.
O.O#...#O#......#.O#.OO.#.O.O....#..O......O.##O.O...###.............OO.#OO.......O....O.#O..O.O##.O
O.OO.O#...#OOO..#....O..OO.#O#.OO..##O.OO.O....#.#...O...#O..OO......O.O.O........O...O.OOO.#..OO...
..OOOOO.#....#O.OO.O...O###..#O#O.#.....#OO.O......#O...#.#.#...O#.O.O.#OOO#...#O....##.............
#..O.O.#..O..........O..#..OOO#..O.......O.OO....#.#...O....O....O..O.O...O....O.O.#O#..#.....#.##.O
OOO#O...#....O#.....O.....#...#..O....O..........O.....OOO..#..O.........O.#....###..#......#....#..
....O...O.##.O.....#....O......##.....#OO...O#....O........###O.O.#O......O.#..O..#.#...#......OOO..
#O#..OO.O...#...OO....O..O.O.O..O#..#....O....#....#...OO.O#....OOO....#.O.##....#.....O.O.#O.O..O..
.......O..#.O.#...........O..O.....O#OO...O.O#.#...O....#.#.O#..O......O..........#.O.O.#..#.......O
..O.O....O....O#....#...O...#.OO........#....#O.O.O.....OO#.#.O##..OO.#.O..#..#...O#..O.#.O...O.#...
..#..#O#O..............O..O#..O...#..#.O.O.....##.....#OO#O...#.O.....###.OOO.##..O.......O....O.OO#
.OOO.....O#...O.OO....#.......O...#..#OO...#O.#....O....#...#..O...O...##..OOO#..........O.O#..#..OO
.O.#.O.O.............O..O#..##O.#..#O#O....##O#...OO.O.#.#......OO.O...#OO.##.O#O....O.#O...#......O
..#.O...OOOOO##.......O..#.......#.....O..#..O.....O......##...O#.O......#..........#.O..###..O.O.O.
...#...OO#..O.....O.O....OO.......O.O..OO..#O#..O..O...#....#.OO......O.O##.#.....O.......O...O..O..
....##.##.O......#O.#.O...OO...O.#...##.#.#.....O......#..O.O..#.....#..#.OO....O#..O....#.OOO#..O##
...O.O#...O..O..#.O..##.O....#OO.....O.O..O....O....#O...#.O.#.O.O..#O.O...#.#O...O..#O.#O.##...OO..
.O...........O...#O..#.O.O#.......O...O.O.#.O.O......O.O#..OO.#O.O...##...O.#..O...O#..O..OOO#.#....
.......O.#O#O..OO......O...O.#.O..##.#O#..O.O...O......O.......#......O##.O........##O.#....O#......
....O..O..#...O....O.OO...O#.#....O....O..#..#...O...O...#....O.........#.O..O..O.O..#.O............
#.O...#...O..#.......OO..#.O...##...#.....##.O.OOO...............O.#.O#..O..O#...O.O...#OO..O...#O.O
#..OO#.O...#.O....O.....O#...#....#.#O....#.#O.O..O...O.O..#O...#....O..OOO.#.O#.#...#O#.O#.O..O..#.
#...#.O......O#O.#O......#......OOO..O....#..O.#O...######.O#O.....O...#..O...OO..#......O....O.....
...##.O.....O#O...O.#O...##........OOO#O.#O.#....O..OO.#O.##O.O.###.......##.OO...#.....###........#
#..#.......OO..OOO..........OO...O#...OO......#.#.#..#..#.#...O#.O......O....O.#..O.#....#....O.OOOO
#O....O....#.O...O..#.#.O....OO..........#O.#O#..O.....#O.....#O.O..O..#.O##.OO##...O....#O..OO...O.
....#O##.OOO#...O.......OO.OO....O#.O...#.O..#..#..#....O..#.O....O#......O..O.O....O#...#O...#..#O.
..OO#OO..##....O.O.OOO......O....OOOO..#...#...O......#.####.O.....O....##O..#...O.#.O#O........#...
O#.##O#..#....O..O.OO#.##.......O......#...OO..O#.....#...O#O.O..O.....O...O.#..#.O..#O..#..#...#.O#
#.O#.O.....O.##...OO......O.O...O.O.....O...#...............#.O#.#.OOOOO#....##..O..##....OO.##.....
..#...O#.......#...O...O..##....#..O......#...O..O.....O.#.O.O......#O......#....#O..O..#.....O.....
...OO.OO.OOO..O..#O.......O##..#.O.#...O...O..O.O#.......#.#.O.#...O..#.O..O#.OOO..O........O.#..###
#O.......##.O..#.#...O.O#..#.#....#......#O#..#.#..#.#....OO...O.#...O..O#....#.#.#O...........##..#
..#O.O.........#.....#.O..O.O.....#OOO..OO.O.........OOO.#....#.......OO#.#.....O.O..#..#...#.O..O.O
..#..#...O.....#OO.#.....O..OO.#..O..#O...O..O....O..#O.O#O...........OO.O.#.....#..........#.OO.OO.
.O#OO.OO.O.....#....OO#...#O#.#...O.O..O#..OO......O#...#.......OO.#.O.O..#.#.#O.O...O#.#..#.#.##.O#
#.OO.O.#..O..OO#O#.#...O...O#...OO#......#....##OO...O..O..O.O.##...#...##.O.#....#....#..##.OOO..O.
..#....O.....#...O....OO...O...#.#..O..#.O#OO.#..##.....O..#....O..#...#.#.O.O.O.#........#.....#.O.
....##.#......O..O#.##.#........O.........#.##.#.#.#.#.O...O..O.#O#.....#.#...#..O..#O..#...OOOO#O##
...#.#....O..O##O..OO...OOOO##..O.......O.##.OO##.#.....#...O....#.O.#O.O##O..O...#....#O...#...O.#O
.....OO..#.O.O##.OO...#..OO.#.#.##...OO.#...O.O.O....O...O.#.#.#O.........#.OO.....O..###O...O..#..O
.#..#....O...O#O......OO.........O#O#....#.O.#...O..OOO..O.#.O........O.......O..........O...#.#.OO.
.O##...O#...#.O...#......#.#O...##..#O.#.##...#..O.#.OOO..O.OO#O.......##....#O.O.O..#.#....O##.....
.O.O....#.O.##.....#......#...........O........OO.O.O..#O.O#.OO...#.O.#....#..O..O.O.#..O...#O#..#O#
..##O.O#.O.#....#....O...#.##..#..OO..OO.O..O.....#....#.#.O........#O...##.OO.#..#..O.#.......#....
#.........OO....O#.#..O.#..#.O#.#..#.O.....O......###.........O#O.O.O..#......#..#..OO...O..O#...O.#
...O....#...OO#...O..#......#.O........O....#..##OO#.O...O.O...O.#............#...#O.O....O#O......O
...O.#.OO.#....O.......#..O....O#.O...#.OO.#..O....OO..#..OO..O..O.###OO...O.O.#.......OO..OO#...O..
...O.#..#O..OO#...O....#O....#OO.#OO..........#O.OO.O#...##...OO#O..#O..#.#.........#.O.#.........##
O..O.O...O.......O...#O#...OO.#OO...#........#.OO...O..........#...O......O....O..#...O.....#OO....#
O.O.#.#O.O.O.O..#.....OOO....#.#.O#O#...O.#OO.#....#..#..#.OO.OOO#O#........O.....O.#........#.#O...
O..#.O........OO..O..O#.#..O..#..O#....#O#....#O.O.O..OO...O#.O.#O#..#OOO..O..#...#...O.#.OO.O#OO.OO
.#.O..O..O...#.....#...O.O...#.#....OO.O.#.O.O..#O..O#..O#O..O.O#.....O#.O...O..OOO......OO.O.#.#.O.
.O.O.O....OO....OOO....O..#O#O..#....O#....O..O.O...O....O....#..O..#..#.O#.OOO..#.......#.....#..O.";
            }
            else
            {
                Platform = @"O....#....
O.OO#....#
.....##...
OO.#O....O
.O.....O#.
O.#..O.#.#
..O..#O..O
.......O..
#....###..
#OO..#....";
            }

            var plat = Platform.Split(delimiter_line, StringSplitOptions.None);
            n = plat.Count();
            for (int i = 0; i < n; i++)
            {
                OldPlatform.Add(plat.Count() - i, plat[i]);
            }

            long Sum = 0;
            NewPlatform = Move(ref OldPlatform);
            foreach (var item in NewPlatform)
            {
                string riga = item.Value;
                int roundStones = 0;
                for (int i = 0; i < riga.Length; i++)
                {
                    if (riga[i] == 'O') roundStones++;
                }
                Sum += (roundStones * item.Key);
            }

        }
        public Dictionary<int, string> Move(ref Dictionary<int, string> oldplatform)
        {
            Dictionary<int, string> newplatform = new Dictionary<int, string>();
            newplatform = oldplatform;
            for (int c = 0; c < n; c++)
            {
                int freespace = 0;
                for (int r = n; r > 0; r--)
                {
                    if (oldplatform[r][c] == '.')
                    {
                        freespace++;
                    }

                    if (oldplatform[r][c] == 'O' && freespace > 0)
                    {
                        string line = newplatform[r + freespace];
                        line = line.Remove(c, 1);
                        line = line.Insert(c, "O");
                        newplatform[r + freespace] = line;

                        line = newplatform[r];
                        line = line.Remove(c, 1);
                        line = line.Insert(c, ".");
                        newplatform[r] = line;
                    }

                    if (oldplatform[r][c] == '#')
                    {
                        freespace = 0;
                    }
                }
            }

            return newplatform;
        }


        public void Part2(object input, bool test, ref object solution)
        {
            test = false;

            if (!test)
            {
                Platform_string = @"..#OO..OO...#..O.OO....OO##O#...O..##.......O.OO..#.O.O#O....#.#O#O####...#.#..#.......O.O..O....#..
.O....#.........OO........O...........#O##O.OOO..O..O.#.#O#O......O..O.O......#O.....###...O..#O....
......O.....OO.O...O.O.O.#...#O...O.O#...O#.O.O.O##.O.....##...OO..#O.O....#.#....O.#O.....##....O..
.##O##...#..###.....O..O#O...........O.##O#OO.O#..OO..OO.#.....OO.O..#.....O..........O..O..O#.O.O..
...O.......O.#O.....OO.#.###....#O.O.##.O..O....O...#.O#O.#.#.O#......O.....#.........O..O##..O.O...
......O#....O..O..O...#..#........#.#..#........O.O#O.OO.O#.OO...#O.O#.O....#O.O.O#....O...O.#.O....
#O#.O....O.O...O..O#..O#..O##O#...........#....O...#.O....OO...O..O..O..OO..#.O...#......##...OO....
..OO#O....#O.O##...O#.OOO..O.O.O...#O#...O.#.OO....#....O..O#O....#..#...#..##.#....O.#...O.....O.O#
...##.#O#..O#..#....O#..OO...#..#......OO##O..O.#..##.#...#.O#..O...O....#....OO#...#..O.O....O.#..#
.#....#O..O...O.#OOO#..OO...O.O..##..#....##O..#....O....O.....#.#..O..#..#.O#...O#O....O....O.O....
...O.O##.OO.O.#O..O....##...O....O.##O##.O#.....#..#.....O.OO#O.O...#...#.O..#.#O...#O.#...O..OOO.OO
.......##......#.O.#.....#.....OO..O.....O#O.O....OO.O...OOO...#O..#O.O...##..O.O.#...O....O...O....
....OOO..#.#OO..O...#..#..OO.O.#.O..#...O....OOO.O.#.OO#.#......#..#.O.O.........O#...O.#.....##.O..
.....#.O..O#..O...O.#..#.......#..OO#.#.....O#O..O...O.O....OO.#OO..O........O.#.O....O....O....OOO.
#...O.O#OO#.....OO..#......O.O..O...##.O#.O.#.OOO#.#...#.......O..O..O...#.O...O.O....#....O....#...
O.O..#...O..O......##.O....O#.O.O..O#.###OOO....#.O....#.#..##O..#..##..O..#O.....O#.O##...#....#O..
#.......O#.#.#O....#...O.O##.....#O##.#OO.O.#.....OO.....O.OO.O..O...O....#.O.O#.OO#.O#..##......#.#
..##.O.O....O..#.O..O.#......#..#O....O...##...#.O.....O.....OO#.......#..OO##.....#......OO...O.O.O
.....O#....#..#......O#.....#O.O#........#...O.#...O.#.O#.O........O.O#OO.O..O..OO#O....#..#..OOO.O.
.#.....OO...OO......OO.OO.O.......#.O#O.......O##...O.#.#......O#.......O.#.....O.....O#...O...#..OO
..#O.O.O..#...........O.O.....O..#..O#..O...#O...#..OO.O##.O...O.....O.#..OOO....O.O....O...OO#.O...
.O.O..O#.....##..O.....O#...#..O.#O....#...............O...O.....OOO.OOO...O.O...O.#OO#.#...#O.....#
O#OO......#.O#..O#OOO.OO..O.O..OO.O.#O...#..O.#OO.O..O....OO#..#.#O....O..O#OO....O...O.OOO.O#O.....
..##O..O.O#..OO.##O#O.O..O.......OO.#....O..O.OO#O..O.O.OOO.##..#...O#......O....#..O.O#..O.......#.
.##.O.....##.OOO..O.#..O.....OO.#.....O#..#.O.#.....#.#OOOO.O...#...OO..#.......OO#O......#O..O.#...
#.#O.O.O....O..OO...#......O.#O#.#....#...OO.....#O.O#OO.#......#..#...#....OOO#OOO.....O.##O......#
...##O.#.O...O.O...#O......O.......O............#.O.#....#......#.OO..OO.O..O..##.....#OO..OO.#OO...
.........#.O..#..OO.O##..O.....#.....O..#.O#O....O.##..#....O#O.O..OO#.O..........OO....#.....O..#O.
.#....#OOO.#....#.........#.#.O.O##.....#O.OO#O#OO...#...O.......#......#..O..O.OO.O...#.##.O..##.#.
##...O#.#...O..O....O...#...OO.......#...#...O##....#.#..###....O.O##..#.......O....#......O#.O...O#
O.##...#O...O...O.#O.#.#.........O...O..O#.OO....O.#....O...O...#......O...#..O##.O..##O..O...#..#.#
.##..O....O..O#.OOO.O.#O#...###.O.......O##..O###.#........O..OO..O#O...O.OOO.#O.O..#O.#..#......O.O
..#O.OO#..#.#...O##...#...O#..O#O..O.O.......#.O#...#...##..O.#..O..O...#..O.O#.O.#...O...#...#.OO#.
.#O...#O.#.#...#.O.##...#O.O.....OO....O...#.....#.#.O..#..#.O...O.O....OO.OO.O...#......#.#..#..O.#
..O..O#..O....OOO..O.....#..#.#.....O....O..O#O..#..#.......O.....#O.#..OO#..#.....#O##....O#.#.O...
..#.O#O.##O#OO...OO....O...OO......O....O###...#...O..O#OOOO...#OO.O.#.O...#.#.O..O.O#.#..O.#O#.O...
O#O...#........O.#........OOO.....O#.....O.O#...#..O........#.O.....#..#...#...OOO.###.O..##....O..#
..#O#O..........#O....O..O.....O...#.O.O..##..OO.O...#O.#.#O#OO.......#...O...O.O...O...#O..#..O##..
#.O.O#OO#O..#O#.O......O...##..#.#.O.....#....#....O#..O...O..O..#O#....O.#O..O#.....#.O#OO.O....O..
....#O...#...#.#..O.#.O.#.O.....OO#O.OO.#O..##...O..#......#.OO...O..O##O.......OO.#O....O#....O...#
O.O..#O.O.O.O.#.O##...O#.OOOO.......O..O.##.O#.O.#OO...OO.........#.#.OO.O.#O.#O..#.#..O..O..O.#O#..
.O..#..OO..O##.....O...##.#.O.O..###O......O.###......#...#....OOO.....O..O......O..O.O..#O##.O...#.
O..#..O...#..O..O.#..#.O....O.#.##.#..#..OO#..O......OO###..O#O##O..O#............##.OO...O...OO#.#.
..O#.....O...#....O#.O....#.O#.....O.O....OO.##.O..#O.OO.O.O..#..#..O#O#.OO...#...........O..O...#.O
#..#.OO........O.....O.....O..O..O.#O#....O..OO..#....O..O.#..#.......O#..O....OOO...O#O.O.....O....
.#......O.......#O#....OO...O#..OOOOO.#..O..#.OO#O.##....#....#.#...#...##..#O..O#......#......O....
..O....OOOO#.#O.##O.O....#.O..O.O...OO..O...#...#...#..OOO#O........O.#O#..#..O..OOO..##...#..#O..O.
O.O#...#O#......#.O#.OO.#.O.O....#..O......O.##O.O...###.............OO.#OO.......O....O.#O..O.O##.O
O.OO.O#...#OOO..#....O..OO.#O#.OO..##O.OO.O....#.#...O...#O..OO......O.O.O........O...O.OOO.#..OO...
..OOOOO.#....#O.OO.O...O###..#O#O.#.....#OO.O......#O...#.#.#...O#.O.O.#OOO#...#O....##.............
#..O.O.#..O..........O..#..OOO#..O.......O.OO....#.#...O....O....O..O.O...O....O.O.#O#..#.....#.##.O
OOO#O...#....O#.....O.....#...#..O....O..........O.....OOO..#..O.........O.#....###..#......#....#..
....O...O.##.O.....#....O......##.....#OO...O#....O........###O.O.#O......O.#..O..#.#...#......OOO..
#O#..OO.O...#...OO....O..O.O.O..O#..#....O....#....#...OO.O#....OOO....#.O.##....#.....O.O.#O.O..O..
.......O..#.O.#...........O..O.....O#OO...O.O#.#...O....#.#.O#..O......O..........#.O.O.#..#.......O
..O.O....O....O#....#...O...#.OO........#....#O.O.O.....OO#.#.O##..OO.#.O..#..#...O#..O.#.O...O.#...
..#..#O#O..............O..O#..O...#..#.O.O.....##.....#OO#O...#.O.....###.OOO.##..O.......O....O.OO#
.OOO.....O#...O.OO....#.......O...#..#OO...#O.#....O....#...#..O...O...##..OOO#..........O.O#..#..OO
.O.#.O.O.............O..O#..##O.#..#O#O....##O#...OO.O.#.#......OO.O...#OO.##.O#O....O.#O...#......O
..#.O...OOOOO##.......O..#.......#.....O..#..O.....O......##...O#.O......#..........#.O..###..O.O.O.
...#...OO#..O.....O.O....OO.......O.O..OO..#O#..O..O...#....#.OO......O.O##.#.....O.......O...O..O..
....##.##.O......#O.#.O...OO...O.#...##.#.#.....O......#..O.O..#.....#..#.OO....O#..O....#.OOO#..O##
...O.O#...O..O..#.O..##.O....#OO.....O.O..O....O....#O...#.O.#.O.O..#O.O...#.#O...O..#O.#O.##...OO..
.O...........O...#O..#.O.O#.......O...O.O.#.O.O......O.O#..OO.#O.O...##...O.#..O...O#..O..OOO#.#....
.......O.#O#O..OO......O...O.#.O..##.#O#..O.O...O......O.......#......O##.O........##O.#....O#......
....O..O..#...O....O.OO...O#.#....O....O..#..#...O...O...#....O.........#.O..O..O.O..#.O............
#.O...#...O..#.......OO..#.O...##...#.....##.O.OOO...............O.#.O#..O..O#...O.O...#OO..O...#O.O
#..OO#.O...#.O....O.....O#...#....#.#O....#.#O.O..O...O.O..#O...#....O..OOO.#.O#.#...#O#.O#.O..O..#.
#...#.O......O#O.#O......#......OOO..O....#..O.#O...######.O#O.....O...#..O...OO..#......O....O.....
...##.O.....O#O...O.#O...##........OOO#O.#O.#....O..OO.#O.##O.O.###.......##.OO...#.....###........#
#..#.......OO..OOO..........OO...O#...OO......#.#.#..#..#.#...O#.O......O....O.#..O.#....#....O.OOOO
#O....O....#.O...O..#.#.O....OO..........#O.#O#..O.....#O.....#O.O..O..#.O##.OO##...O....#O..OO...O.
....#O##.OOO#...O.......OO.OO....O#.O...#.O..#..#..#....O..#.O....O#......O..O.O....O#...#O...#..#O.
..OO#OO..##....O.O.OOO......O....OOOO..#...#...O......#.####.O.....O....##O..#...O.#.O#O........#...
O#.##O#..#....O..O.OO#.##.......O......#...OO..O#.....#...O#O.O..O.....O...O.#..#.O..#O..#..#...#.O#
#.O#.O.....O.##...OO......O.O...O.O.....O...#...............#.O#.#.OOOOO#....##..O..##....OO.##.....
..#...O#.......#...O...O..##....#..O......#...O..O.....O.#.O.O......#O......#....#O..O..#.....O.....
...OO.OO.OOO..O..#O.......O##..#.O.#...O...O..O.O#.......#.#.O.#...O..#.O..O#.OOO..O........O.#..###
#O.......##.O..#.#...O.O#..#.#....#......#O#..#.#..#.#....OO...O.#...O..O#....#.#.#O...........##..#
..#O.O.........#.....#.O..O.O.....#OOO..OO.O.........OOO.#....#.......OO#.#.....O.O..#..#...#.O..O.O
..#..#...O.....#OO.#.....O..OO.#..O..#O...O..O....O..#O.O#O...........OO.O.#.....#..........#.OO.OO.
.O#OO.OO.O.....#....OO#...#O#.#...O.O..O#..OO......O#...#.......OO.#.O.O..#.#.#O.O...O#.#..#.#.##.O#
#.OO.O.#..O..OO#O#.#...O...O#...OO#......#....##OO...O..O..O.O.##...#...##.O.#....#....#..##.OOO..O.
..#....O.....#...O....OO...O...#.#..O..#.O#OO.#..##.....O..#....O..#...#.#.O.O.O.#........#.....#.O.
....##.#......O..O#.##.#........O.........#.##.#.#.#.#.O...O..O.#O#.....#.#...#..O..#O..#...OOOO#O##
...#.#....O..O##O..OO...OOOO##..O.......O.##.OO##.#.....#...O....#.O.#O.O##O..O...#....#O...#...O.#O
.....OO..#.O.O##.OO...#..OO.#.#.##...OO.#...O.O.O....O...O.#.#.#O.........#.OO.....O..###O...O..#..O
.#..#....O...O#O......OO.........O#O#....#.O.#...O..OOO..O.#.O........O.......O..........O...#.#.OO.
.O##...O#...#.O...#......#.#O...##..#O.#.##...#..O.#.OOO..O.OO#O.......##....#O.O.O..#.#....O##.....
.O.O....#.O.##.....#......#...........O........OO.O.O..#O.O#.OO...#.O.#....#..O..O.O.#..O...#O#..#O#
..##O.O#.O.#....#....O...#.##..#..OO..OO.O..O.....#....#.#.O........#O...##.OO.#..#..O.#.......#....
#.........OO....O#.#..O.#..#.O#.#..#.O.....O......###.........O#O.O.O..#......#..#..OO...O..O#...O.#
...O....#...OO#...O..#......#.O........O....#..##OO#.O...O.O...O.#............#...#O.O....O#O......O
...O.#.OO.#....O.......#..O....O#.O...#.OO.#..O....OO..#..OO..O..O.###OO...O.O.#.......OO..OO#...O..
...O.#..#O..OO#...O....#O....#OO.#OO..........#O.OO.O#...##...OO#O..#O..#.#.........#.O.#.........##
O..O.O...O.......O...#O#...OO.#OO...#........#.OO...O..........#...O......O....O..#...O.....#OO....#
O.O.#.#O.O.O.O..#.....OOO....#.#.O#O#...O.#OO.#....#..#..#.OO.OOO#O#........O.....O.#........#.#O...
O..#.O........OO..O..O#.#..O..#..O#....#O#....#O.O.O..OO...O#.O.#O#..#OOO..O..#...#...O.#.OO.O#OO.OO
.#.O..O..O...#.....#...O.O...#.#....OO.O.#.O.O..#O..O#..O#O..O.O#.....O#.O...O..OOO......OO.O.#.#.O.
.O.O.O....OO....OOO....O..#O#O..#....O#....O..O.O...O....O....#..O..#..#.O#.OOO..#.......#.....#..O.";
            }
            else
            {
                Platform_string = @"O....#....
O.OO#....#
.....##...
OO.#O....O
.O.....O#.
O.#..O.#.#
..O..#O..O
.......O..
#....###..
#OO..#....";
            }

            var plat = Platform_string.Split(delimiter_line, StringSplitOptions.None);
            n = plat.Count();
            for (int i = 0; i < n; i++)
            {
                PlatformB.Add(plat.Count() - i, plat[i]);
            }

            /*
150 - 14718883 - 96001
151 - 14814903 - 96020
152 - 14910917 - 96014
153 - 15006920 - 96003
154 - 15102905 - 95985
155 - 15198876 - 95971
156 - 15294838 - 95962
157 - 15390799 - 95961
158 - 15486780 - 95981
159 - 15582781 - 96001
160 - 15678801 - 96020
161 - 15774815 - 96014
162 - 15870818 - 96003
163 - 15966803 - 95985
164 - 16062774 - 95971
165 - 16158736 - 95962
166 - 16254697 - 95961
167 - 16350678 - 95981
168 - 16446679 - 96001
169 - 16542699 - 96020
170 - 16638713 - 96014
171 - 16734716 - 96003
172 - 16830701 - 95985
173 - 16926672 - 95971
174 - 17022634 - 95962
175 - 17118595 - 95961
176 - 17214576 - 95981
177 - 17310577 - 96001             */
            //95985//too low
            //96001,96020,96014,96003,95985,95971,95962,95961,95981

            long a = (164 - 1) % 9;
            long b = (165 - 1) % 9;
            long c = (166 - 1) % 9;
            long d = (167 - 1) % 9;
            long e = (168 - 1) % 9;
            long f = (168 - 1) % 9;
            long g = (169 - 1) % 9;
            long h = (170 - 1) % 9;
            long ii = (171 - 1) % 9;
            long j = (172 - 1) % 9;
            long k = (173 - 1) % 9;
            long l = (1000000000 - 1) % 9;
            //65 non è
            //69 non è
            long Load = 0;
            long singleLoad = 0;
            for (int cycle = 0; cycle < 10000000; cycle++)
            {
                PlatformB = TiltNorth(ref PlatformB);
                PlatformB = TiltWest(ref PlatformB);
                PlatformB = TiltSouth(ref PlatformB);
                PlatformB = TiltEast(ref PlatformB);
                singleLoad = 0;
                foreach (var item in PlatformB)
                {
                    string riga = item.Value;
                    int roundStones = 0;
                    for (int i = 0; i < riga.Length; i++)
                    {
                        if (riga[i] == 'O') roundStones++;
                    }
                    singleLoad += (roundStones * item.Key);
                    Load += (roundStones * item.Key);
                }
                //if (cycle%10000==0)
                //{
                Console.WriteLine($"{cycle} - {Load} - {singleLoad}");
                //}
            }
        }
        public Dictionary<int, string> TiltWest(ref Dictionary<int, string> oldplatform)
        {
            Dictionary<int, string> newplatform = new Dictionary<int, string>();
            newplatform = oldplatform;

            for (int r = n; r > 0; r--)
            {
                int freespace = 0;
                for (int c = 0; c < n; c++)
                {
                    if (oldplatform[r][c] == '.')
                    {
                        freespace++;
                    }

                    if (oldplatform[r][c] == 'O' && freespace > 0)
                    {
                        string line = newplatform[r];
                        line = line.Remove(c, 1);
                        line = line.Insert(c - freespace, "O");
                        newplatform[r] = line;
                    }

                    if (oldplatform[r][c] == '#')
                    {
                        freespace = 0;
                    }
                }
            }
            return newplatform;
        }

        public Dictionary<int, string> TiltEast(ref Dictionary<int, string> oldplatform)
        {
            Dictionary<int, string> newplatform = new Dictionary<int, string>();
            newplatform = oldplatform;

            for (int r = n; r > 0; r--)
            {
                int freespace = 0;
                for (int c = n; c > 0; c--)
                {
                    if (oldplatform[r][c - 1] == '.')
                    {
                        freespace++;
                    }

                    if (oldplatform[r][c - 1] == 'O' && freespace > 0)
                    {
                        string line = newplatform[r];
                        line = line.Remove(c - 1, 1);
                        line = line.Insert(c - 1 + freespace, "O");
                        newplatform[r] = line;
                    }

                    if (oldplatform[r][c - 1] == '#')
                    {
                        freespace = 0;
                    }
                }
            }
            return newplatform;
        }

        public Dictionary<int, string> TiltNorth(ref Dictionary<int, string> oldplatform)
        {
            Dictionary<int, string> newplatform = new Dictionary<int, string>();
            newplatform = oldplatform;
            for (int c = 0; c < n; c++)
            {
                int freespace = 0;
                for (int r = n; r > 0; r--)
                {
                    if (oldplatform[r][c] == '.')
                    {
                        freespace++;
                    }

                    if (oldplatform[r][c] == 'O' && freespace > 0)
                    {
                        string line = newplatform[r + freespace];
                        line = line.Remove(c, 1);
                        line = line.Insert(c, "O");
                        newplatform[r + freespace] = line;

                        line = newplatform[r];
                        line = line.Remove(c, 1);
                        line = line.Insert(c, ".");
                        newplatform[r] = line;
                    }

                    if (oldplatform[r][c] == '#')
                    {
                        freespace = 0;
                    }
                }
            }

            return newplatform;
        }
        public Dictionary<int, string> TiltSouth(ref Dictionary<int, string> oldplatform)
        {
            Dictionary<int, string> newplatform = new Dictionary<int, string>();
            newplatform = oldplatform;
            for (int c = 0; c < n; c++)
            {
                int freespace = 0;
                for (int r = 0; r < n; r++)
                {
                    if (oldplatform[r + 1][c] == '.')
                    {
                        freespace++;
                    }

                    if (oldplatform[r + 1][c] == 'O' && freespace > 0)
                    {
                        string line = newplatform[r + 1 - freespace];
                        line = line.Remove(c, 1);
                        line = line.Insert(c, "O");
                        newplatform[r + 1 - freespace] = line;

                        line = newplatform[r + 1];
                        line = line.Remove(c, 1);
                        line = line.Insert(c, ".");
                        newplatform[r + 1] = line;
                    }

                    if (oldplatform[r + 1][c] == '#')
                    {
                        freespace = 0;
                    }
                }
            }

            return newplatform;
        }
    }
}
