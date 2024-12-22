using AOC;
using AOC.DataStructures.Clustering;
using AOC.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.Text.RegularExpressions;

namespace AOC2024
{
    public class Day22 : Solver, IDay
    {
        public void Part1(object input, bool test, ref object solution)
        {
            List<int> buyers = new List<int>();
            int nTest = 0;
            List<string> inputText = (List<string>)input;
            if (test)
            {
                switch (nTest)
                {
                    case 0:
                        //4 buyers diversi,
                        //SecretNumber: 2000th SecretNumber
                        //1: 8685429
                        //10: 4700978
                        //100: 15273692
                        //2024: 8667524
                        break;
                    case 1:
                        inputText = new List<string>();
                        inputText.Add("123"); //I segreti dovrebbero essere: 15887950, 16495136, 527345, 704524, 1553684, 12683156, 11100544, 12249484, 7753432, 5908254
                        break;
                }
            }
            foreach (string buyer in inputText)
            {
                if (!string.IsNullOrEmpty(buyer))
                {
                    buyers.Add(int.Parse(buyer));
                }
            }

            // Calcola la somma
            long result = Sum2000thSecretNumbers(buyers);

            // Stampa il risultato
            Console.WriteLine($"La somma del 2000° numero segreto generato da ogni acquirente è: {result}");
            solution = result;
        }


        static long NextSecret(long secret)
        {
            // Primo passo
            secret ^= (secret * 64);
            secret %= 16777216;

            // Secondo passo
            secret ^= (secret / 32);
            secret %= 16777216;

            // Terzo passo
            secret ^= (secret * 2048);
            secret %= 16777216;

            return secret;
        }

        // Calcola il 2000° numero per un dato numero iniziale
        static long SimulateBuyer(long initialSecret)
        {
            long secret = initialSecret;
            for (int i = 0; i < 2000; i++)
            {
                secret = NextSecret(secret);
            }
            return secret;
        }

        // Calcola la somma del 2000° numero per tutti gli acquirenti
        static long Sum2000thSecretNumbers(List<int> buyers)
        {
            long sum = 0;
            foreach (var buyer in buyers)
            {
                sum += SimulateBuyer(buyer);
            }
            return sum;
        }

        public void Part2(object input, bool test, ref object solution)
        {
            List<int> buyers = new List<int>();
            int nTest = 0;
            List<string> inputText = (List<string>)input;
            if (test)
            {
                switch (nTest)
                {
                    case 0:
                        inputText = new List<string>();
                        inputText.Add("1");
                        inputText.Add("2");
                        inputText.Add("3");
                        inputText.Add("2024");
                        break;
                    case 1:
                        inputText = new List<string>();
                        inputText.Add("123");
                        // Segreti: Prezzi di vendita (variazione)
                        // 123: 3 
                        //15887950: 0(-3)
                        //16495136: 6(6)
                        //527345: 5(-1)
                        //704524: 4(-1)
                        // 1553684: 4(0)
                        //12683156: 6(2)
                        //11100544: 4(-2)
                        //12249484: 4(0)
                        // 7753432: 2(-2)

                        //La variazione da ricercare è -1,-1,0,2
                        break;
                }
            }
            foreach (string buyer in inputText)
            {
                if (!string.IsNullOrEmpty(buyer))
                {
                    buyers.Add(int.Parse(buyer));
                }
            }

            //Dictionary<string, int> BestPrice = new Dictionary<string, int>();
            //Dictionary<string, int> BestPrice_ = new Dictionary<string, int>();
            //Dictionary<string, int> Total_Variations_Prices = new Dictionary<string, int>();
            //Dictionary<string, int> Total_Variations_Occurrences = new Dictionary<string, int>();
            Dictionary<string, int> Total_Variations_Bananas = new Dictionary<string, int>();

            foreach (var buyer in buyers)
            {
                var Variations_Prices_Occurrences = FindBestSellPrice(buyer);
                foreach(var k in Variations_Prices_Occurrences.Item1.Keys)
                {
                    if (k == "-2,1,-1,3")
                    {
                        Console.WriteLine($"buyer: {buyer}, PreviousVariations: {k}, Bananas: {Variations_Prices_Occurrences.Item1[k]}");
                    }
                    if (!Total_Variations_Bananas.ContainsKey(k))
                    {
                        Total_Variations_Bananas.Add(k, Variations_Prices_Occurrences.Item1[k]);
                    }
                    else
                    {
                        Total_Variations_Bananas[k] += Variations_Prices_Occurrences.Item1[k];
                    }
                    /* if (Total_Variations_Prices.ContainsKey(k))
                     {
                         if (Total_Variations_Prices[k] != Variations_Prices_Occurrences.Item1[k])
                         {
                             //PROBLEMA!
                         }
                     }
                     if (!Total_Variations_Prices.ContainsKey(k))
                     {
                         Total_Variations_Prices.Add(k, Variations_Prices_Occurrences.Item1[k]);
                         Total_Variations_Occurrences.Add(k, Variations_Prices_Occurrences.Item2[k]);
                     }
                     else
                     {
                         Total_Variations_Occurrences[k]+=1;
                     }*/
                }
                //BestPrice.Add(maxP.Item1,maxP.Item2);
            }
            int maxBananas= 0;
            foreach(var k in Total_Variations_Bananas.Keys)
            {
                if (Total_Variations_Bananas[k] > maxBananas)
                {
                    maxBananas = Total_Variations_Bananas[k];
                    Console.WriteLine($"PreviousVariations: {k}, MaxBananas: {Total_Variations_Bananas[k]}");
                }
            }
            solution = maxBananas;
            // Stampa il risultato
            //Console.WriteLine($"La somma del 2000° numero segreto generato da ogni acquirente è: {result}");
            //solution = result;

        }

        static (Dictionary<string, int>, Dictionary<string, int>) FindBestSellPrice(int initialSecret)
        {
            Dictionary<string, int> Variations_Prices = new Dictionary<string, int>();
            Dictionary<string, int> Variations_CountOccurences = new Dictionary<string, int>();
            long secret = initialSecret;
            int maxPrice = 0;
            int price = int.Parse(initialSecret.ToString().Substring(initialSecret.ToString().Length - 1, 1));
            int PreviousPrice = price;
            List<int> PreviousVariations = new List<int>();
            for (int i = 0; i < 2000; i++)
            {
                secret = NextSecret(secret);
                var secretString = secret.ToString();
                price = int.Parse(secretString.Substring(secretString.Length - 1, 1));
                if (PreviousVariations.Count >= 4)
                {
                    ShiftLeft(PreviousVariations, 1);
                    PreviousVariations.RemoveAt(3);
                    PreviousVariations.Add(price - PreviousPrice);
                    PreviousPrice = price;
                }
                else
                {
                    PreviousVariations.Add(price - PreviousPrice);
                    PreviousPrice = price;
                }
                if (i >= 4)
                {
                    //if (price >= maxPrice)
                    //{
                    maxPrice = int.Parse(secretString.Substring(secretString.Length - 1, 1));
                    var PreviousVariations_String = string.Join(",", PreviousVariations.ToArray());
                    //}
                    if (!Variations_Prices.ContainsKey(PreviousVariations_String))
                    {
                        Variations_Prices.Add(PreviousVariations_String, maxPrice);
                    }
                    if (!Variations_CountOccurences.ContainsKey(PreviousVariations_String))
                    {
                        Variations_CountOccurences.Add(PreviousVariations_String, 1);
                    }
                    else
                    {
                        Variations_CountOccurences[PreviousVariations_String] += 1;
                    }
                }

            }

            return (Variations_Prices, Variations_CountOccurences);
        }
        public static void ShiftLeft<T>(List<T> lst, int shifts)
        {
            for (int i = shifts; i < lst.Count; i++)
            {
                lst[i - shifts] = lst[i];
            }

            for (int i = lst.Count - shifts; i < lst.Count; i++)
            {
                lst[i] = default(T);
            }
        }
    }
}
