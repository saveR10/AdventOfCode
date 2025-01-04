using AOC;
using AOC.DataStructures.Clustering;
using AOC.Model;
using AOC.SearchAlghoritmhs;
using Newtonsoft.Json.Bson;


using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography;
using System.Text;
using static AOC.SearchAlghoritmhs.ResearchAlgorithmsAttribute;

namespace AOC2016
{
    [ResearchAlgorithms(TypologyEnum.Gate)]
    public class Day10 : Solver, IDay
    {
        public void Part1(object input, bool test, ref object solution)
        {
            string inputString = (string)input;
            //Dictionary<int, List<int>> bot_chip = new Dictionary<int, List<int>>();
            Queue<Bot> bot_chip = new Queue<Bot>();
            Dictionary<int,int> outer = new Dictionary<int, int>();
            string[] botsInstructions = inputString.Split(Delimiter.delimiter_line, StringSplitOptions.None);
            List<Tuple<int, string, int, string, int>> Handing = new List<Tuple<int, string, int, string, int>>();
            foreach (var instruction in botsInstructions)
            {
                if (!string.IsNullOrEmpty(instruction))
                {
                    if (instruction.StartsWith("value"))
                    {
                        var chip = int.Parse(instruction.Split(Delimiter.delimiter_space, StringSplitOptions.None)[1]);
                        var bot = int.Parse(instruction.Split(Delimiter.delimiter_space, StringSplitOptions.None)[5]);
                        if (!BotExists(bot_chip, bot))
                        {
                            var c = new List<int>();
                            c.Add(chip);
                            bot_chip.Enqueue(new Bot() { bot_id = bot, chip = c });
                        }
                        else
                        {
                            bot_chip.Where(b => b.bot_id == bot).FirstOrDefault().chip.Add(chip);
                        }
                    }
                    else
                    {
                        var bot = int.Parse(instruction.Split(Delimiter.delimiter_space, StringSplitOptions.None)[1]);
                        var lower_out = instruction.Split(Delimiter.delimiter_space, StringSplitOptions.None)[5];
                        var lower_num = int.Parse(instruction.Split(Delimiter.delimiter_space, StringSplitOptions.None)[6]);
                        var higher_out = instruction.Split(Delimiter.delimiter_space, StringSplitOptions.None)[10];
                        var higher_num = int.Parse(instruction.Split(Delimiter.delimiter_space, StringSplitOptions.None)[11]);

                        Handing.Add(new Tuple<int, string, int, string, int>(bot, lower_out, lower_num, higher_out, higher_num));
                    }
                }
            }
            while (bot_chip.Count > 0)
            {
                Bot EvaluateBot= bot_chip.Dequeue();
                if (EvaluateBot.chip.Count() == 2)
                {
                    var hand = Handing.Where(h=>h.Item1.Equals(EvaluateBot.bot_id)).FirstOrDefault();
                    var lower = EvaluateBot.chip.Min();
                    var higher = EvaluateBot.chip.Max();
                    if (hand.Item2 == "bot")
                    {

                    }
                    else
                    {

                    }
                }
                else
                {
                    bot_chip.Enqueue(EvaluateBot);
                }
            }
        }
        public bool BotExists(Queue<Bot> chip_bot, int newBot)
        {
            bool ret = false;
            foreach (var cb in chip_bot)
            {
                if (cb.bot_id == newBot) return true;
            }
            return ret;
        }
        public class Bot
        {
            public int bot_id;
            public List<int> chip = new List<int>();

        }
        public void Part2(object input, bool test, ref object solution)
        {
        }

    }
}
