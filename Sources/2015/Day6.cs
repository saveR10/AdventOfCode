using AOC;
using AOC.DataStructures.Clustering;
using AOC.Model;
using AOC.SearchAlghoritmhs;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using static AOC.SearchAlghoritmhs.ResearchAlgorithmsAttribute;

namespace AOC2015
{
    [ResearchAlgorithms(TypologyEnum.Map)]
    public class Day6 : Solver, IDay
    {
        public int n = 1000;

        public bool[,] MapLights;
        public ulong[,] MapBrightnessLights;
            
        public void Part1(object input, bool test, ref object solution)
        {
            MapLights = new bool[n, n];
            string inputText = (string)input;
            var list = inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None);
            foreach(var item in list)
            {
                bool toggle = false;
                bool turnTo = false;
                if (string.IsNullOrEmpty(item)){ }
                else
                {
                    var line = item.Split(Delimiter.delimiter_space, StringSplitOptions.None);
                    string command = line[0];
                    if (command == "turn")
                    {
                        turnTo = (line[1]=="on"?true:false);
                    }
                    else
                    {
                        toggle = true;
                    }

                    int[] rangesFrom=new int[2];
                    int[] rangesTo = new int[2];

                    rangesFrom[0] = int.Parse(line[line.Length-3].Split(Delimiter.delimiter_comma,StringSplitOptions.None)[0]);
                    rangesFrom[1] = int.Parse(line[line.Length - 3].Split(Delimiter.delimiter_comma, StringSplitOptions.None)[1]);
                    rangesTo[0] = int.Parse(line[line.Length-1].Split(Delimiter.delimiter_comma, StringSplitOptions.None)[0]);
                    rangesTo[1] = int.Parse(line[line.Length - 1].Split(Delimiter.delimiter_comma, StringSplitOptions.None)[1]);
                    if (toggle)
                    {
                        SwitchLights(turnTo, rangesFrom, rangesTo,toggle);
                    }
                    else
                    {
                        SwitchLights(turnTo,rangesFrom,rangesTo);
                    }
                }
                
            }

            int lights = 0;
            for (int x = 0; x < n; x++)
            {
                for (int y = 0; y < n; y++)
                {
                    if (MapLights[x, y]) lights++;
                }
            }
            solution = lights;
        }
        public void SwitchBrightnessLights(bool turnTo, int[] rangesFrom, int[] rangesTo, bool toggle = false)
        {
            for (int x = rangesFrom[0]; x <= rangesTo[0]; x++)
            {
                for (int y = rangesFrom[1]; y <= rangesTo[1]; y++)
                {
                    if (toggle)
                    {
                        MapBrightnessLights[x, y] += 2;
                    }
                    else
                    {
                        if (turnTo) MapBrightnessLights[x, y] += 1;
                        else
                        {
                            if (MapBrightnessLights[x, y] > 0) MapBrightnessLights[x, y] -= 1;
                            else continue;
                        }
                    }
                }
            }
        }
        public void SwitchLights(bool turnTo, int[] rangesFrom, int[] rangesTo,bool toggle=false)
        {
            for (int x = rangesFrom[0]; x <= rangesTo[0]; x++)
            {
                for (int y = rangesFrom[1]; y <= rangesTo[1]; y++)
                {
                    if (toggle) MapLights[x, y] = !MapLights[x, y];
                    else
                    {
                        MapLights[x, y] = turnTo;
                    }
                }
            }
        }

        public void Part2(object input, bool test, ref object solution)
        {
            MapBrightnessLights = new ulong[n, n];
            string inputText = (string)input;
            var list = inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None);
            foreach (var item in list)
            {
                bool toggle = false;
                bool turnTo = false;
                if (string.IsNullOrEmpty(item)) { }
                else
                {
                    var line = item.Split(Delimiter.delimiter_space, StringSplitOptions.None);
                    string command = line[0];
                    if (command == "turn")
                    {
                        turnTo = (line[1] == "on" ? true : false);
                    }
                    else
                    {
                        toggle = true;
                    }

                    int[] rangesFrom = new int[2];
                    int[] rangesTo = new int[2];

                    rangesFrom[0] = int.Parse(line[line.Length - 3].Split(Delimiter.delimiter_comma, StringSplitOptions.None)[0]);
                    rangesFrom[1] = int.Parse(line[line.Length - 3].Split(Delimiter.delimiter_comma, StringSplitOptions.None)[1]);
                    rangesTo[0] = int.Parse(line[line.Length - 1].Split(Delimiter.delimiter_comma, StringSplitOptions.None)[0]);
                    rangesTo[1] = int.Parse(line[line.Length - 1].Split(Delimiter.delimiter_comma, StringSplitOptions.None)[1]);
                    if (toggle)
                    {
                        SwitchBrightnessLights(turnTo, rangesFrom, rangesTo, toggle);
                    }
                    else
                    {
                        SwitchBrightnessLights(turnTo, rangesFrom, rangesTo);
                    }
                }

            }

            ulong lights = 0;
            for (int x = 0; x < n; x++)
            {
                for (int y = 0; y < n; y++)
                {
                    lights+=(MapBrightnessLights[x, y]) ;
                }
            }
            solution = lights;
        }
    }
}