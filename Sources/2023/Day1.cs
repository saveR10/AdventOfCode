using AOC;
using AOC.DataStructures.Clustering;
using AOC.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AOC2023
{
    public class Day1 : Solver, IDay
    {
        public void Part1(object input, bool test, ref object solution)
        {
            int somma = 0;
            string inputText = (string)input;
            foreach (string line in inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None))
            {
                if (line.Length > 0)
                {
                    bool primo = false;
                    bool secondo = false;

                    int[] digit_pair = new int[2];
                    int ind = 0;
                    for (int i = 0; (i < line.Length) && (!primo); i++)
                    {
                        if (int.TryParse(line[i].ToString(), out _))
                        {
                            digit_pair[0] = int.Parse(line[i].ToString());
                            ind = i;
                            primo = true;
                        }
                    }
                    for (int i = line.Length - 1; (i >= ind) && !secondo; i--)
                    {
                        if (int.TryParse(line[i].ToString(), out _))
                        {
                            digit_pair[1] = int.Parse(line[i].ToString());
                            secondo = true;
                        }
                    }
                    if (!secondo)
                    {
                        digit_pair[1] = digit_pair[0];
                    }
                    somma += int.Parse(digit_pair[0].ToString() + digit_pair[1].ToString());
                }
            }
            solution = somma;
        }


        private static String[] units = { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

        public void Part2(object input, bool test, ref object solution)
        {
            string inputText = (string)input;
            int riga = 0;
            int somma = 0;
            foreach (string line in inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None))
            {
                if (line.Length > 0)
                {
                    riga++;
                    bool primo = false;
                    bool secondo = false;

                    int[] digit_pair = new int[2];
                    int ind_primo = 0;
                    Dictionary<int, string> positionWords = FindWords(line);

                    for (int i = 0; (i < line.Length) && (!primo); i++)
                    {
                        if (positionWords?.Keys?.Count() != 0 && i == positionWords.Keys.Min())
                        {
                            int index = positionWords.Keys.Min();
                            string value = ConvertNumber(positionWords[index]).ToString();
                            digit_pair[0] = int.Parse(value);
                            ind_primo = index;
                            primo = true;
                        }
                        else if (int.TryParse(line[i].ToString(), out _))
                        {
                            digit_pair[0] = int.Parse(line[i].ToString());
                            ind_primo = i;
                            primo = true;
                        }
                    }
                    for (int i = line.Length - 1; i > ind_primo && (!secondo); i--)
                    {
                        if (positionWords.Keys.Count() != 0 && i == positionWords.Keys.Max())
                        {
                            int index = positionWords.Keys.Max();
                            string value = ConvertNumber(positionWords[index]).ToString();
                            digit_pair[1] = int.Parse(value);
                            secondo = true;
                        }
                        else if (int.TryParse(line[i].ToString(), out _))
                        {
                            digit_pair[1] = int.Parse(line[i].ToString());
                            secondo = true;
                        }
                    }
                    if (!secondo)
                    {
                        digit_pair[1] = digit_pair[0];
                    }
                    int sommaparz = int.Parse(digit_pair[0].ToString() + digit_pair[1].ToString());
                    somma += sommaparz;
                    
                    Console.WriteLine($"{sommaparz}   {riga} \t\t  {line}");
                }
            }
            solution= somma;
        }
        public Dictionary<int, string> FindWords(string line)
        {
            Dictionary<int, string> positionWords = new Dictionary<int, string>();
            foreach (string unit in units)
            {
                if (line.Contains(unit))
                {
                    for (int i = 0; i < line.Length - unit.Length + 1; i++)
                    {
                        if (line.Substring(i, unit.Length) == unit)
                        {
                            positionWords.Add(i, unit);
                        }
                    }
                }
            }
            return positionWords;
        }

        public int ConvertNumber(string number)
        {
            int ret = 0;
            switch (number.ToUpper())
            {
                case "ONE":
                    ret = 1;
                    break;
                case "TWO":
                    ret = 2;
                    break;
                case "THREE":
                    ret = 3;
                    break;
                case "FOUR":
                    ret = 4;
                    break;
                case "FIVE":
                    ret = 5;
                    break;
                case "SIX":
                    ret = 6;
                    break;
                case "SEVEN":
                    ret = 7;
                    break;
                case "EIGHT":
                    ret = 8;
                    break;
                case "NINE":
                    ret = 9;
                    break;
            }
            return ret;
        }
    }
}
