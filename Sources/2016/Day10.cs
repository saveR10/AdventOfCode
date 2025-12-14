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
using System.Numerics;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography;
using System.Text;
using static AOC.SearchAlghoritmhs.ResearchAlgorithmsAttribute;
using static AOC2016.Day10;

namespace AOC2016
{
    [ResearchAlgorithms(title: "Day 10: Balance Bots",
                        TypologyEnum.Gate ,  // Gestione di gruppi di bot che scambiano microchip (simile a clustering di valori) e distribuzione di oggetti
                        ResolutionEnum.None,       
                        DifficultEnum.Medium,
                        "Simulazione di un sistema di robot che scambiano microchip secondo regole logiche; identificazione del bot responsabile e calcolo dei prodotti nei contenitori di output")]

    public class Day10 : Solver, IDay
    {
        public void Part1(object input, bool test, ref object solution)
        {
            string inputString = (string)input;
            Queue<Bot> bot_chip = new Queue<Bot>();//Dict che contiene id del bot/outer e la lista di chip
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
                        if (!BotExists(bot_chip, "bot" + bot))
                        {
                            var c = new List<int>();
                            c.Add(chip);
                            bot_chip.Enqueue(new Bot() { bot_id = "bot" + bot, chip = c });
                        }
                        else
                        {
                            bot_chip.Where(b => b.bot_id == "bot" + bot).FirstOrDefault().chip.Add(chip);
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
            int findMin = test ? 2 : 17;
            int findMax = test ? 5 : 61;
                

            while (bot_chip.Count > 0 && !bot_chip.All(bc=>bc.bot_id.StartsWith("output") && bc.chip.Count>0))
            {
                Bot EvaluateBot= bot_chip.Dequeue();
                if (EvaluateBot.chip.Count() == 2)
                {
                    var hand = Handing.Where(h => h.Item1.Equals(int.Parse(EvaluateBot.bot_id.Split(new String[] { "bot"}, StringSplitOptions.None)[1]))).FirstOrDefault();
                    var lower = EvaluateBot.chip.Min();
                    var higher = EvaluateBot.chip.Max();
                    if((lower==findMax || lower== findMin) && (higher == findMax || higher == findMin))
                    {
                        solution = EvaluateBot.bot_id.Split(new String[] { "bot" }, StringSplitOptions.None)[1];
                    }
                    if (!BotExists(bot_chip, $"{hand.Item2}{hand.Item3}"))
                    {
                        var c = new List<int>();
                        c.Add(lower);
                        bot_chip.Enqueue(new Bot() { bot_id = $"{hand.Item2}{hand.Item3}", chip = c });
                    }
                    else
                    {
                        bot_chip.Where(b => b.bot_id == $"{hand.Item2}{hand.Item3}").FirstOrDefault().chip.Add(lower);
                    }

                    if (!BotExists(bot_chip, $"{hand.Item4}{hand.Item5}"))
                    {
                        var c = new List<int>();
                        c.Add(higher);
                        bot_chip.Enqueue(new Bot() { bot_id = $"{hand.Item4}{hand.Item5}", chip = c });
                    }
                    else
                    {
                        bot_chip.Where(b => b.bot_id == $"{hand.Item4}{hand.Item5}").FirstOrDefault().chip.Add(higher);
                    }
                }
                else
                {
                    bot_chip.Enqueue(EvaluateBot);
                }
            }

        }
        public bool BotExists(Queue<Bot> chip_bot, string newBot)
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
            public string bot_id;
            public List<int> chip = new List<int>();

        }
        public void Part2(object input, bool test, ref object solution)
        {
            string inputString = (string)input;
            Queue<Bot> bot_chip = new Queue<Bot>();//Dict che contiene id del bot/outer e la lista di chip
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
                        if (!BotExists(bot_chip, "bot" + bot))
                        {
                            var c = new List<int>();
                            c.Add(chip);
                            bot_chip.Enqueue(new Bot() { bot_id = "bot" + bot, chip = c });
                        }
                        else
                        {
                            bot_chip.Where(b => b.bot_id == "bot" + bot).FirstOrDefault().chip.Add(chip);
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

            while (bot_chip.Count > 0 && !bot_chip.All(bc => bc.bot_id.StartsWith("output") && bc.chip.Count > 0))
            {
                Bot EvaluateBot = bot_chip.Dequeue();
                if (EvaluateBot.chip.Count() == 2)
                {
                    var hand = Handing.Where(h => h.Item1.Equals(int.Parse(EvaluateBot.bot_id.Split(new String[] { "bot" }, StringSplitOptions.None)[1]))).FirstOrDefault();
                    var lower = EvaluateBot.chip.Min();
                    var higher = EvaluateBot.chip.Max();
                    if (!BotExists(bot_chip, $"{hand.Item2}{hand.Item3}"))
                    {
                        var c = new List<int>();
                        c.Add(lower);
                        bot_chip.Enqueue(new Bot() { bot_id = $"{hand.Item2}{hand.Item3}", chip = c });
                    }
                    else
                    {
                        bot_chip.Where(b => b.bot_id == $"{hand.Item2}{hand.Item3}").FirstOrDefault().chip.Add(lower);
                    }

                    if (!BotExists(bot_chip, $"{hand.Item4}{hand.Item5}"))
                    {
                        var c = new List<int>();
                        c.Add(higher);
                        bot_chip.Enqueue(new Bot() { bot_id = $"{hand.Item4}{hand.Item5}", chip = c });
                    }
                    else
                    {
                        bot_chip.Where(b => b.bot_id == $"{hand.Item4}{hand.Item5}").FirstOrDefault().chip.Add(higher);
                    }
                }
                else
                {
                    bot_chip.Enqueue(EvaluateBot);
                }
            }
            int ret = 1;
            foreach(var o in bot_chip)
            {
                if(int.Parse(o.bot_id.Split(new String[] { "output" }, StringSplitOptions.None)[1]) < 3)
                {
                    ret *= o.chip.First();
                    Console.WriteLine($"{o.bot_id} - {o.chip.First()} - {o.chip.Count}");
                }
            }
            solution = ret;
        }
    }
}