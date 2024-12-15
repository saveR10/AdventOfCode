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
    public class Day14 : Solver, IDay
    {
        int height, width, seconds;
        public static int[,] Map;
        public static int t;
        public void Part1(object input, bool test, ref object solution)
        {
            string inputText = (string)input;
            height = test ? 7 : 103;
            width = test ? 11 : 101;
            seconds = 100;
            Map = new int[height, width];
            List<(int x, int y, int vx, int vy)> robots = new List<(int, int, int, int)>();
            foreach (string r in inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None))
            {
                if (!string.IsNullOrEmpty(r))
                {
                    var rob = r.Split(' ');
                    robots.Add((
                                int.Parse(rob[0].Split('=')[1].Split(',')[1]),
                                int.Parse(rob[0].Split('=')[1].Split(',')[0]),
                                int.Parse(rob[1].Split('=')[1].Split(',')[1]),
                                int.Parse(rob[1].Split('=')[1].Split(',')[0])
                               ));
                }
            }
            t = 0;
            while (t <= seconds)
            {
                // Calcola la nuova mappa per il tempo t
                foreach (var robot in robots)
                {
                    //t=0   x=4,y=2 -3,2
                    //t=1   x=1,y=4 
                    //t=2   x=5,y=6 
                    //t=3   x=2,y=8 
                    //t=4   x=6,y=10 
                    //t=5   x=3,y=1

                    int currentX = ((robot.x + robot.vx * t) + height) % height;
                    int currentY = ((robot.y + robot.vy * t) + width) % width;
                    int previousX = ((robot.x + robot.vx * (t - 1)) + height) % height;
                    int previousY = ((robot.y + robot.vy * (t - 1)) + width) % width;

                    // Gestione dei bordi negativi
                    if (previousX < 0) previousX += height;
                    if (previousY < 0) previousY += width;
                    if (currentX < 0) currentX += height;
                    if (currentY < 0) currentY += width;

                    // Aggiorna la mappa
                    if (t > 0)
                    {
                        Map[previousX, previousY] -= 1; // Rimuove 1 dalla posizione precedente
                    }
                    Map[currentX, currentY] += 1; // Aggiunge 1 alla nuova posizione
                }

                // Mostra la mappa aggiornata
                ShowMap(Map);

                // Incrementa il tempo
                t++;
            }
            int SafetyFactors = CalculateSafetyFactor(Map);

            solution = SafetyFactors;
        }
        public int CalculateSafetyFactor(int[,] map)
        {
            int factor1 = 0;
            for (int i = 0; i < height / 2; i++)
            {
                for (int j = 0; j < width / 2; j++)
                {
                    factor1 += map[i, j];
                }
            }

            int factor2 = 0;
            for (int i = 0; i < height / 2; i++)
            {
                for (int j = (width / 2)+1; j < width; j++)
                {
                    factor2 += map[i, j];
                }
            }

            int factor3 = 0;
            for (int i = (height / 2)+1; i < height ; i++)
            {
                for (int j = 0; j < width / 2; j++)
                {
                    factor3 += map[i, j];
                }
            }

            int factor4 = 0;
            for (int i = (height / 2) + 1; i < height; i++)
            {
                for (int j = (width / 2) + 1; j < width; j++)
                {
                    factor4 += map[i, j];
                }
            }

            Console.WriteLine($"{factor1}*{factor2}*{factor3}*{factor4}");
            return factor1*factor2*factor3*factor4;
        }
        public void ShowMap(int[,] map)
        {
            Console.WriteLine($"---------------------t: {t}----------------------\n");
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    //Console.ForegroundColor = ConsoleColor.Green;
                    //Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(map[i, j]);
                }
                Console.WriteLine("");
            }
        }

        public static int TotalRobots;
        public void Part2(object input, bool test, ref object solution)
        {
            string inputText = (string)input;
            height = test ? 7 : 103;
            width = test ? 11 : 101;
            seconds = 100;
            TotalRobots = 0;
            Map = new int[height, width];
            List<(int x, int y, int vx, int vy)> robots = new List<(int, int, int, int)>();
            foreach (string r in inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None))
            {
                if (!string.IsNullOrEmpty(r))
                {
                    var rob = r.Split(' ');
                    robots.Add((
                                int.Parse(rob[0].Split('=')[1].Split(',')[1]),
                                int.Parse(rob[0].Split('=')[1].Split(',')[0]),
                                int.Parse(rob[1].Split('=')[1].Split(',')[1]),
                                int.Parse(rob[1].Split('=')[1].Split(',')[0])
                               ));
                }
            }
            TotalRobots= robots.Count;
            t = 0;
            int treeFigure = 0;
            while (true)
            {
                foreach (var robot in robots)
                {
                    //t=0   x=4,y=2 -3,2
                    //t=1   x=1,y=4 
                    //t=2   x=5,y=6 
                    //t=3   x=2,y=8 
                    //t=4   x=6,y=10 
                    //t=5   x=3,y=1

                    int currentX = ((robot.x + robot.vx * t) + height) % height;
                    int currentY = ((robot.y + robot.vy * t) + width) % width;
                    int previousX = ((robot.x + robot.vx * (t - 1)) + height) % height;
                    int previousY = ((robot.y + robot.vy * (t - 1)) + width) % width;

                    if (previousX < 0) previousX += height;
                    if (previousY < 0) previousY += width;
                    if (currentX < 0) currentX += height;
                    if (currentY < 0) currentY += width;

                    if (t > 0)
                    {
                        Map[previousX, previousY] -= 1;
                    }
                    Map[currentX, currentY] += 1;
                }

                if (CheckPattern(Map))
                {
                    treeFigure = t;
                    ShowTreeMap(Map);
                    break;
                }

                if (t % 1000000 == 0)
                {
                    ShowTreeMap(Map);
                }
                //ShowTreeMap(Map);
                //Console.ReadLine();
                t++;
            }
            ShowTreeMap(Map);

            solution = treeFigure;
        }
        public void ShowTreeMap(int[,] map)
        {
            Console.WriteLine($"---------------------t: {t}----------------------\n");
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (map[i, j] == 0) Console.ForegroundColor = ConsoleColor.White;
                    else Console.ForegroundColor = ConsoleColor.Green;
                    //Console.ForegroundColor = ConsoleColor.Green;
                    //Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(map[i, j]);
                }
                Console.WriteLine("");
            }
        }
        public bool CheckPattern(int[,] map)
        {
            bool findTree = false;
            int countBelowTree = 0;
            int countTrunkTree = 0;
            for (int i = (height/2)+1; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (map[i, j] != 0) countBelowTree += map[i, j];
                }
            }

            for (int i = (height / 2) + 1; i < height; i++)
            {
                for (int j = width * 3/10; j < width * 6/10; j++)
                {
                    if (map[i, j] != 0) countTrunkTree += map[i, j];
                }
            }

            if (countBelowTree >= TotalRobots * 65 / 100 || countTrunkTree > TotalRobots * 40/100)
            {
                findTree = true;
            }

            bool overlapping = false;
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (map[i, j] != 0 && map[i, j] != 1)
                    {
                        overlapping = true;
                        break;
                    }
                    if (overlapping) break;

                }
            }
            if (!overlapping)
            {
                return true;
            }

            return findTree;
        }
    }
}
