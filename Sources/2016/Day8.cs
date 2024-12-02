using AOC;
using AOC.DataStructures.Clustering;
using AOC.Model;
using AOC.SearchAlghoritmhs;
using Newtonsoft.Json.Bson;
 
 
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography;
using System.Text;
using static AOC.SearchAlghoritmhs.ResearchAlghoritmsAttribute;

namespace AOC2016
{
    public class Day8 : Solver, IDay
    {
        public void Part1(object input, bool test, ref object solution)
        {
            string inputString = (string)input;
            string[] inputList = inputString.Split(Delimiter.delimiter_line,StringSplitOptions.None);
            bool[,] display;
            if (test) display = new bool[3,7];
            else display = new bool[6,50];
            int lights = 0;
            foreach(var line in inputList)
            {
                if (line.StartsWith("rect"))
                {
                    //lit+=CountLit(display);
                    //ResetDisplay(display);
                    int x=int.Parse(line.Split(Delimiter.delimiter_space, StringSplitOptions.None)[1].Split(Delimiter.delimiter_x, StringSplitOptions.None)[0]);
                    int y = int.Parse(line.Split(Delimiter.delimiter_space, StringSplitOptions.None)[1].Split(Delimiter.delimiter_x, StringSplitOptions.None)[1]);
                    for (int j = 0; j < y; j++)
                    {
                        for (int i = 0; i < x; i++)
                        {
                            display[j,i] = true;
                        }
                    }
                }
                else if (line.StartsWith("rotate"))
                {
                    var a = line.Split(Delimiter.delimiter_space, StringSplitOptions.None);
                    if (a[1].Equals("column"))
                    {
                        int column = int.Parse(a[2].Split(Delimiter.delimiter_equals, StringSplitOptions.None)[1]);
                        int by = int.Parse(a[4]);
                        RotateDisplay(display, "x", column, by);
                    }
                    else if(line.Split(Delimiter.delimiter_space, StringSplitOptions.None)[1].Equals("row"))
                    {
                        int row = int.Parse(a[2].Split(Delimiter.delimiter_equals, StringSplitOptions.None)[1]);
                        int by = int.Parse(a[4]);
                        RotateDisplay(display, "y", row, by);
                    }
                }
                ShowDisplay(display);
            }
            lights=CountLit(display);
            solution = lights;
        }
        public void RotateDisplay(bool[,] display,string which,int number, int by)
        {
            if(which=="x")
            {
                bool[] temp = new bool[display.GetLength(0)];
                by = by % display.GetLength(0);
                for (int i = 0; i < display.GetLength(0); i++)
                {
                    temp[(i + by)% display.GetLength(0)] = display[i, number];
                }

                for(int i=0;i< display.GetLength(0); i++)
                {
                    display[i, number] = temp[i];
                }
            }
            else if (which == "y")
            {
                bool[] temp = new bool[display.GetLength(1)];
                by = by % display.GetLength(1);
                for (int j = 0; j < display.GetLength(1); j++)
                {
                    temp[(j + by) % display.GetLength(1)] = display[number,j];
                }

                for (int j = 0; j < display.GetLength(1); j++)
                {
                    display[number,j] = temp[j];
                }
            }
        }
        public int CountLit(bool[,] display)
        {
            int lit = 0;
            for (int j = 0; j < display.GetLength(0); j++)
            {
                for (int i = 0; i < display.GetLength(1); i++)
                {
                    if(display[j, i]) lit++;
                }
            }
            return lit;
        }

        public void ResetDisplay(bool[,] display)
        {
            for (int j = 0; j < display.GetLength(0); j++)
            {
                for (int i = 0; i < display.GetLength(1); i++)
                {
                    display[j, i] = false;
                }
            }
        }
        public void ShowDisplay(bool[,] display)
        {
            for (int j = 0; j < display.GetLength(0); j++)
            {
                for (int i = 0; i < display.GetLength(1); i++)
                {
                    if (display[j, i]) Console.Write("#");
                    else Console.Write(".");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
        public void Part2(object input, bool test, ref object solution)
        {
        //See console output in Part 1
        }
    }
}
