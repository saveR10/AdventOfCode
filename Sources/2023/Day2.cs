using AOC;
using AOC.DataStructures.Clustering;
using AOC.Documents.LINQ;
using AOC.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;

namespace AOC2023
{
    public class Day2 : Solver, IDay
    {
        public void Part1(object input, bool test, ref object solution)
        {
            int Game = 0;
            int IDSums = 0;
            int PossibleGames = 0;

            char sep = ':';
            char sep_sets = ';';
            char sep_col = ',';
            char sep_cubes = ' ';

            string inputText = (string)input;
            foreach (string line in inputText.Split(new char[] { '\r', '\n' }))
            {
                if (line.Length > 0)
                {
                    bool impossible = false;
                    Game++;
                    string infoGame = line;
                    infoGame = line.Split(sep)[1];
                    string[] Sets = new string[infoGame.Split(sep_sets).Count()];

                    for (int i = 0; i < infoGame.Split(sep_sets).Count(); i++)
                    {
                        Sets[i] = infoGame.Split(sep_sets)[i];
                    }


                    foreach (string set in Sets)
                    {
                        int colorRed = 0;
                        int colorGreen = 0;
                        int colorBlue = 0;
                        foreach (string exctractCubes in set.Split(sep_col))
                        {
                            int number = Convert.ToInt32(exctractCubes.Split(sep_cubes)[1]);
                            string color = exctractCubes.Split(sep_cubes)[2];

                            switch (color)
                            {
                                case "red":
                                    colorRed += number;
                                    break;
                                case "blue":
                                    colorBlue += number;
                                    break;
                                case "green":
                                    colorGreen += number;
                                    break;
                            }
                        }
                        if (colorRed > 12 || colorGreen > 13 || colorBlue > 14)
                        {
                            impossible = true;
                        }

                    }

                    if (!impossible)
                    {
                        IDSums += Game;
                        Console.WriteLine($"Game {Game} - IDSums  {IDSums}");
                    }
                }
            }
            solution = IDSums;
        }

        public void Part2(object input, bool test, ref object solution)
        {
            char sep = ':';
            char sep_sets = ';';
            char sep_col = ',';
            char sep_cubes = ' ';
            int Game = 0;
            int powerSums = 0;
            string inputText = (string)input;
            foreach (string line in inputText.Split(new char[] { '\r', '\n' }))
            {
                if (line.Length > 0)
                {
                    int minRed = 0;
                    int minBlue = 0;
                    int minGreen = 0;
                    Game++;
                    string infoGame = line;
                    infoGame = line.Split(sep)[1];
                    string[] Sets = new string[infoGame.Split(sep_sets).Count()];

                    for (int i = 0; i < infoGame.Split(sep_sets).Count(); i++)
                    {
                        Sets[i] = infoGame.Split(sep_sets)[i];
                    }
                    foreach (string set in Sets)
                    {
                        foreach (string exctractCubes in set.Split(sep_col))
                        {
                            int number = Convert.ToInt32(exctractCubes.Split(sep_cubes)[1]);
                            string color = exctractCubes.Split(sep_cubes)[2];

                            switch (color)
                            {
                                case "red":
                                    if (number > minRed) minRed = number;
                                    break;
                                case "blue":
                                    if (number > minBlue) minBlue = number;
                                    break;
                                case "green":
                                    if (number > minGreen) minGreen = number;
                                    break;
                            }
                        }

                    }
                    int power = minRed * minGreen * minBlue;
                    powerSums += power;
                }
            }
            solution = powerSums;
        }
    }
}
