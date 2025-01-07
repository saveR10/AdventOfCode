using AOC;
using AOC.Model;
using AOC.SearchAlghoritmhs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using static AOC.SearchAlghoritmhs.ResearchAlgorithmsAttribute;
using static AOC2016.Day15;

namespace AOC2016
{
    [ResearchAlgorithms(TypologyEnum.Cronometers)]
    [ResearchAlgorithms(TypologyEnum.Overflow)]
    [ResearchAlgorithms(ResolutionEnum.ModularArithmetic)]
    [ResearchAlgorithms(ResolutionEnum.Regex)]
    public class Day15 : Solver, IDay
    {
        public void Part1(object input, bool test, ref object solution)
        {
            string inputString = (string)input;
            var Instructions = inputString.Split(Delimiter.delimiter_line, StringSplitOptions.None);
            string pattern = @"Disc #(\d+) has (\d+) positions; at time=0, it is at position (\d+)";
            List<Disc> Discs = new List<Disc>();
            foreach (var i in Instructions)
            {
                Match match = Regex.Match(i, pattern);
                if (match.Success)
                {
                    int discNumber = int.Parse(match.Groups[1].Value);
                    int positions = int.Parse(match.Groups[2].Value);
                    int startPosition = int.Parse(match.Groups[3].Value);

                    Console.WriteLine($"Disc Number: {discNumber}, Positions: {positions}, Start Position: {startPosition}");
                    Discs.Add(new Disc(discNumber, positions, startPosition));
                }
            }


            for (int t = 0; t < 100; t++)
            {
                bool Ntry= true;
                foreach (var d in Discs)
                {
                    Console.WriteLine($"Disc Number: {d.Number}, ProjectionPosition: {(d.ProjectionPosition + t) % (d.Positions)}");
                    if ((d.ProjectionPosition + t) % (d.Positions)!= 0) Ntry= false;
                }
                if (Ntry == true)
                {

                }
                Console.WriteLine();
            }
            int result = FindFirstValidTime(Discs);
            solution = result;
        }
        static int FindFirstValidTime(List<Disc> discs)
        {
            int time = 0;
            int step = 1;

            for (int i = 0; i < discs.Count; i++)
            {
                int positions = discs[i].Positions;
                int startPosition = discs[i].StartPosition;

                // Incrementa il tempo finché la condizione non è soddisfatta
                while ((startPosition + time + i + 1) % positions != 0)
                {
                    time += step;
                }

                // Aggiorna il passo (step) usando il MCM
                step = Lcm(step, positions);
            }

            return time;
        }

        // Funzione per calcolare il MCM (Minimo Comune Multiplo)
        static int Lcm(int a, int b)
        {
            return a * (b / Gcd(a, b));
        }

        // Funzione per calcolare il MCD (Massimo Comune Divisore) usando l'algoritmo di Euclide
        static int Gcd(int a, int b)
        {
            while (b != 0)
            {
                int temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }

        public void ShowDiscs(List<Disc> Discs)
        {
            Console.WriteLine();
            int MaxOffSet = Discs.Max(d => d.Positions);
            Console.WriteLine("|".PadLeft(MaxOffSet));
            foreach (var discs in Discs.OrderBy(d=>d.Number))
            {
                Console.Write(string.Join("", Enumerable.Range(1, discs.Positions-1)).PadLeft(MaxOffSet));
                Console.Write("O");
                Console.WriteLine();
            }
        }
        public class Disc 
        {
            public Disc(int number, int positions, int startPosition)
            {
                Number = number;
                Positions = positions;
                StartPosition = startPosition;
                ProjectionPosition = (startPosition + number)%positions;
            }
            public int ProjectionPosition { get; set; } //Posizione del disco quando la capsula arriva a quel livello
            public int Number { get; set; }
            public int Positions { get; set; }
            public int StartPosition { get; set; }
        }
        public void Part2(object input, bool test, ref object solution)
        {
            string inputString = (string)input;
            inputString += "Disc #7 has 11 positions; at time=0, it is at position 0.\n";
            var Instructions = inputString.Split(Delimiter.delimiter_line, StringSplitOptions.None);
            string pattern = @"Disc #(\d+) has (\d+) positions; at time=0, it is at position (\d+)";
            List<Disc> Discs = new List<Disc>();
            foreach (var i in Instructions)
            {
                Match match = Regex.Match(i, pattern);
                if (match.Success)
                {
                    int discNumber = int.Parse(match.Groups[1].Value);
                    int positions = int.Parse(match.Groups[2].Value);
                    int startPosition = int.Parse(match.Groups[3].Value);

                    Console.WriteLine($"Disc Number: {discNumber}, Positions: {positions}, Start Position: {startPosition}");
                    Discs.Add(new Disc(discNumber, positions, startPosition));
                }
            }

            //for (int t = 0; t < 1000; t++)
            //{
            //    foreach (var d in Discs)
            //    {
            //        Console.WriteLine($"Disc Number: {d.Number}, Position: {(d.StartPosition+t)%(d.Positions)}");
            //    }
            //    Console.WriteLine();
            //}

            for (int t = 0; t < 100; t++)
            {
                bool Ntry = true;
                foreach (var d in Discs)
                {
                    Console.WriteLine($"Disc Number: {d.Number}, ProjectionPosition: {(d.ProjectionPosition + t) % (d.Positions)}");
                    if ((d.ProjectionPosition + t) % (d.Positions) != 0) Ntry = false;
                }
                if (Ntry == true)
                {

                }
                Console.WriteLine();
            }
            int result = FindFirstValidTime(Discs);


            solution = result;
        }        
    }
}