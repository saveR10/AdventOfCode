using AOC;
using AOC.DataStructures.Clustering;
using AOC.Documents.LINQ;
using AOC.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text.RegularExpressions;
using static AOC2023.Day5;
using static System.Net.Mime.MediaTypeNames;

namespace AOC2023
{
    //DA SISTEMARE
    public class Day6 : Solver, IDay
    {
        public static String[] delimiters = { "\r\n", " " };
        public static String[] delimiter_space = { " " };
        public static String[] delimiter_line = { "\r\n" };
        public void Part1(object input, bool test, ref object solution)
        {
            string Time = "";
            string Distance = "";
            List<GenericResource> Resources = new List<GenericResource>();

            test = false;

            if (!test)
            {
                Time = "62 64 91 90";
                Distance = "553 1010 1473 1074";
            }
            else
            {
                Time = "7 15 30";
                Distance = "9 40 200";
            }

            List<int> Times = new List<int>();
            List<int> Distances = new List<int>();

            for (int i = 0; i < Time.Split(delimiter_space, StringSplitOptions.None).Count(); i++)
            {
                Times.Add(int.Parse(Time.Split(delimiter_space, StringSplitOptions.None)[i]));
                Distances.Add(int.Parse(Distance.Split(delimiter_space, StringSplitOptions.None)[i]));
            }
            int power_ways = 1;
            for (int r = 0; r < Times.Count(); r++)
            {
                int ways = 0;
                int race_time = Times[r];
                int race_distance = Distances[r];
                for (int h = 1; h < race_time; h++) //hold the button
                {
                    int acceleration = h; // mm/ms
                    int time_remain = race_time - acceleration;
                    int distance_reached = time_remain * acceleration;
                    if (distance_reached > race_distance) { ways++; }
                }
                power_ways *= ways;
            }
        }


        public void Part2(object input, bool test, ref object solution)
        {
            string Time = "";
            string Distance = "";
            List<GenericResource> Resources = new List<GenericResource>();

            test = false;

            if (!test)
            {
                Time = "62649190";
                Distance = "553101014731074";
            }
            else
            {
                Time = "71530";
                Distance = "940200";
            }

            List<BigInteger> Times = new List<BigInteger>();
            List<BigInteger> Distances = new List<BigInteger>();

            for (int i = 0; i < Time.Split(delimiter_space, StringSplitOptions.None).Count(); i++)
            {
                Times.Add(BigInteger.Parse(Time.Split(delimiter_space, StringSplitOptions.None)[i]));
                Distances.Add(BigInteger.Parse(Distance.Split(delimiter_space, StringSplitOptions.None)[i]));
            }

            /*TEST
             * 
             *  Time = "71530";
                Distance = "940200";
            Time-14 ->  
            (Time-x ) *x>Distance  
             *   71530*14 - x^x > distance
             * x^2 -1.001.420+940200<0
             * 
             * x^2-61220
             * 
             * 
             * 
                Time= "62649190"; 
                Distance="553101014731074";
             * Quando h=10633311 raggiungo per la prima volta il traguardo
            hold_t=x;
            fly_t=race_t-hold_t=race_t-x;
            distance = x*fly_t
            
            hold_t=x;
            fly_t=race_t-x;
            distance = x*fly_t

            hold_t=x;
            fly_t=race_t-x;
            distance = x*(race_t-x)

            hold_t=x;
            fly_t=race_t-x;
            distance = x*race_t-x^2
            
            x^2-x*race_t +distance=0

            -race_T 
            race_T^2 +- 4*distance

              +-  1.712.516.948.731.804
             */

            BigInteger power_ways = 1;
            for (int r = 0; r < Times.Count(); r++)
            {
                int t = 0;
                BigInteger ways = 0;
                BigInteger race_time = Times[r];
                BigInteger race_distance = Distances[r];
                //h start = 10633311 il primo a cui riesce a raggiungere
                //h end = 52015879 l'ultimo acui lo raggiungere

                int res = 52015879 - 10633311 + 1;
                //il risultato è 41382569

                for (BigInteger h = 52015879; h < race_time; h++) //hold the button
                {
                    BigInteger acceleration = h; // mm/ms
                    BigInteger time_remain = race_time - acceleration;
                    BigInteger distance_reached = time_remain * acceleration;
                    if (distance_reached > race_distance)
                    {
                        ways++;
                    }
                    else
                    {
                        var a = 2;
                    }
                    Console.WriteLine($"Race {r} - Hold button for {h} - ways {ways}");
                }
                power_ways *= ways;
            }
        }

    }
}
