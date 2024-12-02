using AOC;
using AOC.DataStructures.Clustering;
using AOC.Model;
using AOC.SearchAlghoritmhs;
 
 
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using static AOC.SearchAlghoritmhs.ResearchAlghoritmsAttribute;

namespace AOC2016
{
    [ResearchAlghoritmsAttribute(TypologyEnum.Hashing)]
    public class Day5 : Solver, IDay
    {
        public void Part1(object input, bool test, ref object solution)
        {
            int somma = 0;
            string secretkey = "";
            string numberToAddSecretKey = "";
            bool founded = false;
            secretkey = (string)input;
            secretkey = secretkey.Split(Delimiter.delimiter_line, StringSplitOptions.None)[0];
            for (int i = 0; i < 10000000; i++)
            {
                //numberToAddSecretKey = i.ToString().PadLeft(6);
                string md5 = CreateMD5(secretkey+i);
                if (md5.StartsWith("00000"))
                {
                    founded = true;
                    solution+=md5[5].ToString();
                    if (solution.ToString().Length == 8)
                    {
                        break;
                    }
                }
            }
        }

        public static string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                //return  HexadecimalEncoding.ToHexString(hashBytes); // .NET 5 +

                // Convert the byte array to hexadecimal string prior to .NET 5
                StringBuilder sb = new System.Text.StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }
        public void Part2(object input, bool test, ref object solution)
        {
            string solutiontemp = "00000000";
            List<int> indexes = new List<int>();
            int somma = 0;
            string secretkey = "";
            string numberToAddSecretKey = "";
            bool founded = false;
            secretkey = (string)input;
            secretkey = secretkey.Split(Delimiter.delimiter_line, StringSplitOptions.None)[0];
            for (int i = 0; i < 80000000; i++)
            {
                //numberToAddSecretKey = i.ToString().PadLeft(6);
                string md5 = CreateMD5(secretkey + i);
                if (md5.StartsWith("00000"))
                {
                    int ind = 0;
                    founded = true;
                    if (int.TryParse(md5[5].ToString(), out ind))
                    {
                        if (!indexes.Contains(ind) && ind < 8)
                        {
                            solutiontemp = solutiontemp.Substring(0,ind)+ md5[6].ToString()+solutiontemp.Substring(ind+1,7-ind);
                            indexes.Add(ind);
                        }
                        Console.WriteLine(solutiontemp);
                        if (indexes.Count == 8)
                        {
                            solution=solutiontemp;
                            break;
                        }
                    }
                    
                }
            }
        }

    }
}
