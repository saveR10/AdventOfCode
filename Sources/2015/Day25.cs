using AOC;
using AOC.DataStructures.Clustering;
using AOC.Model;
using AOC.SearchAlghoritmhs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using static AOC.SearchAlghoritmhs.ResearchAlgorithmsAttribute;

namespace AOC2015
{
    [ResearchAlgorithms(title: "Day 25: Let It Snow",
                        TypologyEnum.Overflow,        // Applicazione diretta della formula ricorsiva
                        ResolutionEnum.ModularArithmetic, // Generazione di sequenze modulari                   
                        DifficultEnum.Medium,
                        "Calcolo del codice per una posizione specifica in una griglia infinita, usando numeri generati ricorsivamente con moltiplicazione e modulo")]
    public class Day25 : Solver, IDay
    {
        public void Part1(object input, bool test, ref object solution)
        {
            string inputText = (string)input;
            int targetR = int.Parse(inputText.Split(' ')[0]);
            int targetC = int.Parse(inputText.Split(' ')[1]);
            long[,] matrix = new long[6000, 6000];
            long last = 0;
            for (int r = 0; r < 6000; r++)
            {
                for (int c = 0; c <= r; c++)
                {
                    if((r - c)==0 && c == 0)
                    {
                        matrix[r - c, c] = 20151125;
                        last = 20151125;
                    }
                    else
                    {
                        BigInteger a = last;
                        BigInteger b = 252533;
                        BigInteger p = a * b;
                        matrix[r - c, c] = (long)(p % (BigInteger)33554393);
                        last = matrix[r - c, c];
                    }
                    if(matrix[targetR - 1, targetC - 1] !=null && matrix[targetR - 1, targetC - 1] != 0)
                    {
                        solution = matrix[targetR-1, targetC-1];
                        break;
                    }
                }
                if (solution != null) break;
            }
        }



        public void Part2(object input, bool test, ref object solution)
        {
            
        }
    }
}
