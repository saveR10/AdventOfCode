using AOC;
using AOC.DataStructures.Clustering;
using AOC.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;

namespace AOC2015
{
    public class Day25 : Solver, IDay
    {
        public void Part1(object input, bool test, ref object solution)
        {
            string inputText = (string)input;
            long[][] matrix = new long[2000][];
            foreach(var i in matrix)
            {
                long[] j = new long[2000];
            }
            matrix[0][0] = long.Parse(inputText);
        }




        public void Part2(object input, bool test, ref object solution)
        {
            string inputText = (string)input;

            int r;
            int counter = 0;
            bool trovato = false;
            int indice = 0;
            for (int i = 0; i < inputText.Length; i++)
            {
                if (inputText[i] == '(')
                {
                    counter += 1;
                }
                else
                {
                    counter -= 1;
                }

                if (counter == -1 && !trovato)
                {
                    indice = i;
                    trovato = true;
                }
            }

            solution = counter;
        }



    }
}
