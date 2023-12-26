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
    public class Day2 : Solver, IDay
    {
        public void Part1(object input, bool test, ref object solution)
        {
            string inputText = (string)input;

            int somma = 0;
            int ribbon = 0;
            for (int i = 0; i < inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None).Length-1; i++)
            {
                string line = inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None)[i];
                string[] parts = line.Split(' ');
                string handString = parts[0];
                int l = int.Parse(parts[0].Split('x')[0]);
                int w = int.Parse(parts[0].Split('x')[1]);
                int h = int.Parse(parts[0].Split('x')[2]);
                somma += ((2 * l * w) + (2 * w * h) + (2 * h * l));
                int[] a = new int[3] { l, w, h };
                Array.Sort(a);
                somma += (a[0] * a[1]);
                ribbon += (((a[0] * 2) + (a[1] * 2)) + a[0] * a[1] * a[2]);


            }
            solution = somma;
        }

        public void Part2(object input, bool test, ref object solution)
        {
            string inputText = (string)input;

            int somma = 0;
            int ribbon = 0;
            for (int i = 0; i < inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None).Length - 1; i++)
            {
                string line = inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None)[i];
                string[] parts = line.Split(' ');
                string handString = parts[0];
                int l = int.Parse(parts[0].Split('x')[0]);
                int w = int.Parse(parts[0].Split('x')[1]);
                int h = int.Parse(parts[0].Split('x')[2]);
                somma += ((2 * l * w) + (2 * w * h) + (2 * h * l));
                int[] a = new int[3] { l, w, h };
                Array.Sort(a);
                somma += (a[0] * a[1]);
                ribbon += (((a[0] * 2) + (a[1] * 2)) + a[0] * a[1] * a[2]);


            }
            solution = ribbon;
        }
    }
}