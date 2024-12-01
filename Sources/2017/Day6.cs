using AOC;
using AOC.DataStructures.Clustering;
using AOC.Model;
using AOC.SearchAlghoritmhs;
using NUnit.Framework.Interfaces;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Threading;
using System.Xml.Schema;
using static AOC.SearchAlghoritmhs.ResearchAlghoritmsAttribute;
using AOC.Utilities.Dynamic;
using System.Text.RegularExpressions;

namespace AOC2017
{
    public class Day6 : Solver, IDay
    {
        public void Part1(object input, bool test, ref object solution)
        {
            int cycles=0;
            List<string> Banks = new List<string>();
            string inputText = (string)input;
            string InitialBanks = "";
            foreach (var data in inputText.Split(Delimiter.delimiter_Tab, StringSplitOptions.None))
            {
                if (!string.IsNullOrEmpty(data))
                {
                    InitialBanks += $"{data}-";
                }
            }
            InitialBanks = InitialBanks.Trim('-');
            Banks.Add(InitialBanks);
            bool trovato = false;
            while (!trovato)
            {
                int maxBank = FindMaxBank(Banks.Last());
                string newBanks = Redistribute(Banks.Last(),maxBank);
                if (Banks.Contains(newBanks)) trovato = true;
                Banks.Add(newBanks);
                cycles++;
            }
            solution = cycles;
        }
        public string Redistribute(string LastBank, int maxBank)
        {
            string newBlocks = "";
            List<int> Blocks = new List<int>();
            foreach (var bank in LastBank.Split('-'))
            {
                Blocks.Add(int.Parse(bank));
            }

            int posMax = Blocks.IndexOf(maxBank);
            int PosDistributingMemory = posMax;
            int DistributingMemory = maxBank;
            Blocks[PosDistributingMemory] = 0;
            while (DistributingMemory > 0)
            {
                PosDistributingMemory++;
                PosDistributingMemory = PosDistributingMemory % Blocks.Count();
                Blocks[PosDistributingMemory] = Blocks[PosDistributingMemory]+1;
                DistributingMemory--;
            }

            foreach (var block in Blocks)
            {
                    newBlocks += $"{block}-";
            }
            newBlocks= newBlocks.Trim('-');

            return newBlocks;
        }
        public int FindMaxBank(string LastBank)
        {
            List<int> Blocks = new List<int>();
            foreach (var bank in LastBank.Split('-'))
            {
                Blocks.Add(int.Parse(bank));
            }
            return Blocks.Max();
        }
        public void Part2(object input, bool test, ref object solution)
        {
            int LoopCycles = 0;
            int cycles = 0;
            List<string> Banks = new List<string>();
            string inputText = (string)input;
            string InitialBanks = "";
            foreach (var data in inputText.Split(Delimiter.delimiter_Tab, StringSplitOptions.None))
            {
                if (!string.IsNullOrEmpty(data))
                {
                    InitialBanks += $"{data}-";
                }
            }
            InitialBanks = InitialBanks.Trim('-');
            Banks.Add(InitialBanks);
            bool trovato = false;
            while (!trovato)
            {
                int maxBank = FindMaxBank(Banks.Last());
                string newBanks = Redistribute(Banks.Last(), maxBank);
                if (Banks.Contains(newBanks))
                {
                    trovato = true;
                    LoopCycles = Banks.Count() - Banks.IndexOf(newBanks);
                }
                Banks.Add(newBanks);
                cycles++;
            }
            solution = LoopCycles;
        }
    }
}
