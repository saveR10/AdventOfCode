using AOC;
using AOC.Model;
using AOC.SearchAlghoritmhs;
using AOC2015;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using static AOC.SearchAlghoritmhs.ResearchAlgorithmsAttribute;

namespace AOC2016
{
    [ResearchAlgorithms(title: "Day 19: An Elephant Named Joseph",
                        typology: TypologyEnum.Game | TypologyEnum.Trolling, //Josephus
                        resolution: ResolutionEnum.NumberTheory | ResolutionEnum.ModularArithmetic,
                        difficult: ResearchAlgorithmsAttribute.DifficultEnum.Hard,
                        note: "Classic Josephus problem(k = 2).Naive simulation is unnecessary; closed - form solution based on powers of two.")]
    public class Day19 : Solver, IDay
    {
        public void Part1(object input, bool test, ref object solution)
        {
            string inputString = (string)input;
            long elfs = long.Parse(inputString);
            int p = 1;

            // Trova la massima potenza di 2 <= elfs
            while (p * 2 <= elfs)
                p *= 2;

            // Formula di Josephus (k = 2)
            solution = 2 * (elfs - p) + 1;


        }
        public void Part2(object input, bool test, ref object solution)
        {
            string inputString = (string)input;
            long elfs = long.Parse(inputString);
            int p = 1;
            //Basta ragionare sulle serie...
            // Trova la massima potenza di 3 <= elfs
            while (p * 3 <= elfs)
                p *= 3;

            if (elfs == p)
                solution = elfs;

            else if (elfs <= 2 * p)
                solution= elfs - p;

            else solution = 2 * elfs - 3 * p;
        }

    }
}