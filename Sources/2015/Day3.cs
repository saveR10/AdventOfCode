using AOC;
using AOC.DataStructures.Clustering;
using AOC.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;

namespace AOC2015
{
    public class Day3 : Solver, IDay
    {
        public bool[,] houses = new bool[1000, 1000];

        public void Part1(object input, bool test, ref object solution)
        {
            string inputText = (string)input;

            int x = 500;
            int y = 500;
            houses[500, 500] = true;
            int Houses = 0;
            int xs = 500;
            int ys = 500;
            int xr = 500;
            int yr = 500;
            for (int i = 0; i < inputText.Length; i++)
            {
                char dir = inputText[i];
                if (dir == '^') y += 1;
                if (dir == 'v') y -= 1;
                if (dir == '>') x += 1;
                if (dir == '<') x -= 1;
                houses[x, y] = true;
            }
            for (int i = 0; i < 1000; i++)
            {
                for (int j = 0; j < 1000; j++)
                {
                    if (houses[i, j] != null && houses[i, j] == true) Houses++;
                }
            }
            solution = Houses;

        }


        public void Part2(object input, bool test, ref object solution)
        {
            string inputText = (string)input;
            int Houses = 0;

            int xs = 500;
            int ys = 500;
            int xr = 500;
            int yr = 500;
            houses[500, 500] = true;
            for (int i = 0; i < inputText.Length; i++)
            {
                char dir = inputText[i];
                if (i % 2 == 0)
                {
                    if (dir == '^') ys += 1;
                    if (dir == 'v') ys -= 1;
                    if (dir == '>') xs += 1;
                    if (dir == '<') xs -= 1;
                    houses[xs, ys] = true;
                }
                else
                {
                    if (dir == '^') yr += 1;
                    if (dir == 'v') yr -= 1;
                    if (dir == '>') xr += 1;
                    if (dir == '<') xr -= 1;
                    houses[xr, yr] = true;
                }
            }
            for (int i = 0; i < 1000; i++)
            {
                for (int j = 0; j < 1000; j++)
                {
                    if (houses[i, j] != null && houses[i, j] == true) Houses++;
                }
            }
            solution = Houses;
        }
    }
}
