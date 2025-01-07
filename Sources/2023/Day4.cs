using AOC;
using AOC.DataStructures.Clustering;
using AOC.Documents.LINQ;
using AOC.Model;
using AOC.SearchAlghoritmhs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using static AOC.SearchAlghoritmhs.ResearchAlgorithmsAttribute;
using static System.Net.Mime.MediaTypeNames;

namespace AOC2023
{
    //DA SISTEMARE
    [ResearchAlgorithmsAttribute(ResolutionEnum.Regex)]
    public class Day4 : Solver, IDay
    {
        private const string V = "\r\n";
        int Sum = 0;

        Dictionary<int, int> CardIstances = new Dictionary<int, int>();
        String[] result;
        Dictionary<int, List<int>> my_numbers = new Dictionary<int, List<int>>();
        Dictionary<int, List<int>> winner_numbers = new Dictionary<int, List<int>>();
        string card;
        int card_id;
        int IstancesSum = 0;
        public void Part1(object input, bool test, ref object solution)
        {
            string inputText = (string)input;
            String[] delimiters = { "\r\n" };
            String[] result = inputText.Split(delimiters, StringSplitOptions.None);
            for (int r = 0; r < result.Count(); r++)
            {
                string card = result[r].Split(':')[1].Trim();
                string string_winner_number = card.Split('|')[0].Trim();
                List<int> winner_number = new List<int>();
                foreach (string wn in System.Text.RegularExpressions.Regex.Split(string_winner_number, @"\s+"))
                {
                    winner_number.Add(int.Parse(wn));
                }

                List<int> my_number = new List<int>();

                string string_my_number = card.Split('|')[1].Trim();
                foreach (string mn in System.Text.RegularExpressions.Regex.Split(string_my_number, @"\s+"))
                {
                    my_number.Add(int.Parse(mn));
                }

                int match_prize = 0;
                foreach (int mn in my_number)
                {
                    foreach (int wn in winner_number)
                    {
                        if (mn == wn) match_prize++;
                    }
                }
                double value_card = 0;
                if (match_prize == 0) { }
                else
                {
                    value_card = Math.Pow(2, match_prize - 1);
                }

                Sum += (int)value_card;

                Console.WriteLine(card);
            }
        }


        public void Part2(object input, bool test, ref object solution)
        {
            string inputText = (string)input;
            String[] delimiters = { "\r\n" };
            String[] result = inputText.Split(delimiters, StringSplitOptions.None);
            PrepareData();

            for (int r = 0; r < result.Count(); r++)
            {
                card_id = r + 1;
                int match_prize = 0;
                foreach (int mn in my_numbers[card_id])
                {
                    foreach (int wn in winner_numbers[card_id])
                    {
                        if (mn == wn) match_prize++;
                    }
                }
                if (match_prize > 0)
                {
                    AddCopyIstances(r, match_prize);

                }

                //Sum += (int)value_card;

                Console.WriteLine(card);
            }
            foreach (int item in CardIstances.Values)
            {
                IstancesSum += item;
            }
        }
        public void PrepareData()
        {
            for (int r = 0; r < result.Count(); r++)
            {
                card = result[r].Split(':')[1].Trim();

                string a = result[r].Split(':')[0].Trim();
                string b = System.Text.RegularExpressions.Regex.Split(a, @"\s+")[1];
                card_id = int.Parse(b);

                string string_winner_number = card.Split('|')[0].Trim();
                List<int> list_winn_num = new List<int>();
                foreach (string wn in System.Text.RegularExpressions.Regex.Split(string_winner_number, @"\s+"))
                {
                    list_winn_num.Add(int.Parse(wn));
                }

                string string_my_number = card.Split('|')[1].Trim();
                List<int> list_my_num = new List<int>();
                foreach (string mn in System.Text.RegularExpressions.Regex.Split(string_my_number, @"\s+"))
                {
                    list_my_num.Add(int.Parse(mn));

                }

                winner_numbers.Add(card_id, list_winn_num);
                my_numbers.Add(card_id, list_my_num);
                CardIstances.Add(card_id, 1);
            }
        }

        public void AddCopyIstances(int r, int match_prize)
        {
            for (int i = 0; i < match_prize; i++)
            {
                CardIstances[r + i + 2] += CardIstances[r + 1];
            }
        }
    }
}
