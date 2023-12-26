using AOC;
using AOC.DataStructures.Clustering;
using AOC.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AOC2015
{
    public class Day1 : Solver, IDay
    {
        public void Part1(object input, bool test, ref object solution)
        {
            string inputText = (string)input;

            int r;
            int counter = 0;
            bool trovato = false;
            int indice = 0;
            for (int i=0;i<inputText.Length;i++)
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

            solution = indice;
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
