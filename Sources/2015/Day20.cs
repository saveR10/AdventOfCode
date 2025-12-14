using AOC;
using AOC.Model;
using AOC.SearchAlghoritmhs;
using System;
using static AOC.SearchAlghoritmhs.ResearchAlgorithmsAttribute;
using Solver = AOC.Solver;

namespace AOC2015
{
    [ResearchAlgorithms(title: "Day 20: Infinite Elves and Infinite Houses",
                        TypologyEnum.Combinatorial,
                        ResolutionEnum.BruteForce | ResolutionEnum.NumberTheory,
                        DifficultEnum.Medium,
                        "Calcolo dei divisori di un numero per determinare i regali ricevuti dalle case; gestione dei limiti degli elfi nella consegna")]
    public class Day20 : Solver, IDay
    {
        public void Part1(object input, bool test, ref object solution)
        {
            string inputText = (string)input;
            var targetPresent = inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None);
            long present = 0;
            long houseNumber = 0;

            solution = FindLowestHouse(int.Parse(targetPresent[0]));
        }
        int FindLowestHouse(int targetPresents)
        {
            int house = 1;

            while (true)
            {
                int sum = 0;

                // Calcola i divisori di `house`
                for (int elf = 1; elf <= Math.Sqrt(house); elf++)
                {
                    if (house % elf == 0)
                    {
                        int other = house / elf;

                        // Aggiungi contributo dell'elfo `elf`
                        sum += elf * 10;

                        // Aggiungi contributo del divisore complementare, se diverso
                        if (elf != other)
                        {
                            sum += other * 10;
                        }
                    }
                }

                if (sum >= targetPresents)
                {
                    return house;
                }

                house++;
            }
        }
        public void Part2(object input, bool test, ref object solution)
        {
            string inputText = (string)input;
            var targetPresent = inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None);
            long present = 0;
            long houseNumber = 0;

            solution = FindLowestHouseComplex(int.Parse(targetPresent[0]));

        }
        int FindLowestHouseComplex(int targetPresents)
        {
            int house = 1;

            while (true)
            {
                int sum = 0;

                // Trova tutti i divisori di `house`
                for (int elf = 1; elf <= Math.Sqrt(house); elf++)
                {
                    if (house % elf == 0)
                    {
                        int other = house / elf;

                        // Considera il contributo dell'elfo `elf`, se valido
                        if (house / elf <= 50)
                        {
                            sum += elf * 11;
                        }

                        // Considera il contributo del divisore complementare, se valido e diverso
                        if (elf != other && house / other <= 50)
                        {
                            sum += other * 11;
                        }
                    }
                }

                if (sum >= targetPresents)
                {
                    return house;
                }

                house++;
            }
        }
    }
}