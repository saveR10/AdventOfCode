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
    [ResearchAlgorithms(TypologyEnum.Map)]
    [ResearchAlgorithms(ResolutionEnum.DFS)]
    [ResearchAlgorithmsAttribute(TypologyEnum.Hashing)] //MD5
    public class Day17 : Solver, IDay
    {
        static readonly List<(int x, int y)> Map = new List<(int x, int y)>()
        {
            { (0, 0) }, {  (0, 1) }, {  (0, 2) },
            { (1, 0) }, {  (1, 1) }, {  (1, 2) },
            { (2, 0) }, {  (2, 1) }, {  (2, 2) },
            { (3, 0) }, {  (3, 1) }, {  (3, 2) }
        };
        public void Part1(object input, bool test, ref object solution)
        {
            string inputString = (string)input;
            
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

        }

    }
}