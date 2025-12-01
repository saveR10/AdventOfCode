using AOC;
using AOC.Model;
using AOC.SearchAlghoritmhs;
using AOC2015;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using static AOC.SearchAlghoritmhs.ResearchAlgorithmsAttribute;

namespace AOC2016
{
    public class Day16 : Solver, IDay
    {
        public void Part1(object input, bool test, ref object solution)
        {
            string inputString = (string)input;
            var data = inputString.Split(Delimiter.delimiter_line, StringSplitOptions.None)[0];
            int FillDisk = test ? 20 : 272;
            while (data.Length < FillDisk)
            {
                var a = data;
                var b = string.Join("", data.Reverse());
                b = b.Replace("1", "2").Replace("0", "1").Replace("2", "0");
                data = $"{a}0{b}";
            }
            var checksum = data.Substring(0, FillDisk);
            do
            {
                var newCheckSum = "";
                for (int i = 0; i < checksum.Length - 1; i += 2)
                {
                    if (checksum[i] == checksum[i + 1]) newCheckSum += "1";
                    else newCheckSum += "0";
                }
                checksum = newCheckSum;
            } while (checksum.Length % 2 == 0);
            solution = checksum;
        }

        public void Part2(object input, bool test, ref object solution)
        {
            string inputString = (string)input;
            var InitialData = inputString.Split(Delimiter.delimiter_line, StringSplitOptions.None)[0];
            int FillDisk = test ? 20 : 35651584;
            int diskLength = 35651584;
            //BitArray[] ba = new BitArray[diskLength];
            
            // Generazione dei dati
            bool[] data = GenerateDragonCurve(InitialData, diskLength);

            // Calcolo della checksum
            string checksum = CalculateChecksum(data, diskLength);

            Console.WriteLine($"Checksum: {checksum}");
            solution=checksum;
        }

        static bool[] GenerateDragonCurve(string initialState, int length)
        {
            // Convertire lo stato iniziale in un array di bool
            bool[] data = new bool[length];
            int currentLength = initialState.Length;

            for (int i = 0; i < currentLength; i++)
                data[i] = initialState[i] == '1';

            // Generazione del dragon curve
            while (currentLength < length)
            {
                int start = currentLength;
                data[currentLength++] = false; // Aggiungere lo '0'

                // Invertire e appendere
                for (int i = start - 1; i >= 0 && currentLength < length; i--)
                    data[currentLength++] = !data[i];
            }

            return data;
        }

        static string CalculateChecksum(bool[] data, int length)
        {
            // Calcolo della checksum
            bool[] checksum = new bool[length];
            Array.Copy(data, checksum, length);

            int currentLength = length;

            while (currentLength % 2 == 0)
            {
                int newLength = currentLength / 2;
                for (int i = 0; i < newLength; i++)
                {
                    checksum[i] = checksum[2 * i] == checksum[2 * i + 1];
                }
                currentLength = newLength;
            }

            // Convertire il risultato in stringa
            char[] result = new char[currentLength];
            for (int i = 0; i < currentLength; i++)
                result[i] = checksum[i] ? '1' : '0';

            return new string(result);
        }
    }
}