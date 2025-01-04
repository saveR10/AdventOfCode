using AOC;
using AOC.DataStructures.Clustering;
using AOC.DataStructures.PriorityQueue;
using AOC.Model;
using AOC.SearchAlghoritmhs;
using AOC.Utilities.Map;
using AOC.Utilities.Math;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization; 
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;
using System.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using static AOC.SearchAlghoritmhs.ResearchAlgorithmsAttribute;
using static AOC2015.Day19;
using static AOC2015.Day9;
using Solver = AOC.Solver;

namespace AOC2015
{
    [ResearchAlgorithms(ResolutionEnum.NumberTheory)]
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