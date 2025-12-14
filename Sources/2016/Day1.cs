using AOC;
using AOC.DataStructures.Clustering;
using AOC.Model;
using AOC.SearchAlghoritmhs;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using static AOC.SearchAlghoritmhs.ResearchAlgorithmsAttribute;

namespace AOC2016
{
    [ResearchAlgorithms(title: "Day 1: No Time for a Taxicab",
                        TypologyEnum.Map,       // Simulazione di spostamenti su griglia
                        ResolutionEnum.None,    // Nessun algoritmo complesso, solo simulazione passo-passo
                        DifficultEnum.WarmUp,
                        "Calcolo della distanza di Manhattan percorsa in una griglia seguendo istruzioni di rotazione e avanzamento; ricerca della prima posizione visitata due volte")]
    public class Day1 : Solver, IDay
    {
        public enum Direction
        {
            Up,
            Right,
            Down,
            Left
        }
        public void Part1(object input, bool test, ref object solution)
        {
            string inputText = (string)input;
            Direction dir = Direction.Up;
            int[] pos = new int[2];
            List<string> Instructions = new List<string>();

            foreach (string s in inputText.Split(Delimiter.delimiter_comma, StringSplitOptions.None))
            {
                string step = s.Trim();
                if (step.StartsWith("R"))
                {
                    dir = (Direction)(((int)dir + 1) % 4);
                }
                else
                {
                    int a = ((int)dir - 1) % 4;
                    if (a < 0) { a = a + 4; }
                    dir = (Direction)a;
                }
                switch (dir)
                {
                    case Direction.Up:
                        pos[1] += int.Parse(step.Substring(1, step.Length - 1));
                        break;
                    case Direction.Right:
                        pos[0] += int.Parse(step.Substring(1, step.Length - 1));
                        break;
                    case Direction.Down:
                        pos[1] -= int.Parse(step.Substring(1, step.Length - 1));
                        break;
                    case Direction.Left:
                        pos[0] -= int.Parse(step.Substring(1, step.Length - 1));
                        break;
                }
            }
            solution = pos[0] + pos[1];
        }




        public void Part2(object input, bool test, ref object solution)
        {
            string inputText = "";
            if (test)
            {
                inputText = "R8, R4, R4, R8";
            }
            else
            {
                inputText = (string)input;
            }

            Direction dir = Direction.Up;
            int[] pos = new int[2];
            pos[0] = 0;
            pos[1] = 0;
            int[] pos_pre = new int[2];
            int[] pos_post = new int[2];
            Dictionary<(int, int), bool> map = new Dictionary<(int, int), bool>();

            List<string> Instructions = new List<string>();
            bool founded = false;
            foreach (string s in inputText.Split(Delimiter.delimiter_comma, StringSplitOptions.None))
            {
                string step = s.Trim();
                if (step.StartsWith("R"))
                {
                    dir = (Direction)(((int)dir + 1) % 4);
                }
                else
                {
                    int a = ((int)dir - 1) % 4;
                    if (a < 0) { a = a + 4; }
                    dir = (Direction)a;
                }
                pos_pre[0] = pos[0];
                pos_pre[1] = pos[1];
                switch (dir)
                {
                    case Direction.Up:
                        pos[1] += int.Parse(step.Substring(1, step.Length - 1));
                        pos_post = pos;
                        break;
                    case Direction.Right:
                        pos[0] += int.Parse(step.Substring(1, step.Length - 1));
                        pos_post = pos;
                        break;
                    case Direction.Down:
                        pos[1] -= int.Parse(step.Substring(1, step.Length - 1));
                        pos_post = pos;
                        break;
                    case Direction.Left:
                        pos[0] -= int.Parse(step.Substring(1, step.Length - 1));
                        pos_post = pos;
                        break;
                }

                if (pos_pre[0] != pos_post[0])
                {
                    int num = Math.Abs(pos_post[0] - pos_pre[0]);
                    int num_var = pos_post[0];
                    for (int i = 0; i < num; i++)
                    {
                        if (map.ContainsKey((num_var, pos[1])) && map[(num_var, pos[1])])
                        {
                            founded = true; solution = Math.Abs(num_var) + Math.Abs(pos[1]); break;
                        }
                        map[(num_var, pos[1])] = true;
                        if ((num_var - pos_pre[0]) > 0) num_var--;
                        else num_var++;
                    }
                }
                else
                {
                    int num = Math.Abs(pos_post[1] - pos_pre[1]);
                    int num_var = pos_post[1];
                    for (int i = 0; i < num; i++)
                    {
                        if (map.ContainsKey((pos[0], num_var)) && map[(pos[0], num_var)]) { 
                        founded = true; solution= Math.Abs(pos[0])+ Math.Abs(num_var); break; }
                        map[(pos[0], num_var)] = true;
                        if ((num_var - pos_pre[1]) > 0) num_var--;
                        else num_var++;
                    }

                }
                if (map.ContainsKey((0,0))) { }
                    else map[(0, 0)] = true;
                if (founded) break;
                Console.WriteLine($"Posizione precedente: {pos_pre[0]},{pos_pre[1]} \t Posizione attuale: {pos_post[0]},{pos_post[1]}");
            }
            solution = solution;
        }
    }
}
