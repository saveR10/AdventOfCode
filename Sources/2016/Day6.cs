using AOC;
using AOC.Model;
using AOC.SearchAlghoritmhs;
using System;
using System.Collections.Generic;
using System.Linq;
using static AOC.SearchAlghoritmhs.ResearchAlgorithmsAttribute;

namespace AOC2016
{
    [ResearchAlgorithms(title: "Day 6: Signals and Noise",
                        TypologyEnum.TextRules,          // Analisi di testo e frequenze dei caratteri
                        ResolutionEnum.None,             // Nessun algoritmo complesso, solo conteggio frequenze
                        DifficultEnum.Easy,
                        "Decodifica messaggi jammati scegliendo il carattere più frequente (Part 1) o meno frequente (Part 2) per ciascuna posizione")]
    public class Day6 : Solver, IDay
    {
        public void Part1(object input, bool test, ref object solution)
        {
            string solutiontemp = "";
            string message = (string)input;
            int rowlegth = message.Split(Delimiter.delimiter_line, StringSplitOptions.None)[0].Length;
            string[] lines = message.Split(Delimiter.delimiter_line, StringSplitOptions.None);
            for(int c = 0; c < rowlegth; c++)
            {
                Dictionary<char, int> letters = new Dictionary<char, int>();
                for(int r = 0; r < lines.Length; r++)
                {
                    if (!letters.ContainsKey(lines[r][c])) letters.Add(lines[r][c], 1);
                    else letters[lines[r][c]] += 1;
                }
                
                solutiontemp += letters.Keys.Where(l => letters[l].Equals(int.Parse(letters.Values.Max().ToString()))).First();
            }
            solution=solutiontemp;
        }

        public void Part2(object input, bool test, ref object solution)
        {
            string solutiontemp = "";
            string message = (string)input;
            int rowlegth = message.Split(Delimiter.delimiter_line, StringSplitOptions.None)[0].Length;
            string[] lines = message.Split(Delimiter.delimiter_line, StringSplitOptions.None);
            for (int c = 0; c < rowlegth; c++)
            {
                Dictionary<char, int> letters = new Dictionary<char, int>();
                for (int r = 0; r < lines.Length; r++)
                {
                    if (!letters.ContainsKey(lines[r][c])) letters.Add(lines[r][c], 1);
                    else letters[lines[r][c]] += 1;
                }

                solutiontemp += letters.Keys.Where(l => letters[l].Equals(int.Parse(letters.Values.Min().ToString()))).First();
            }
            solution = solutiontemp;
        }
    }
}
