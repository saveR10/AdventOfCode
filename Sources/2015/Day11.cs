using AOC;
using AOC.DataStructures.Clustering;
using AOC.DataStructures.PriorityQueue;
using AOC.Model;
using AOC.SearchAlghoritmhs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using static AOC.SearchAlghoritmhs.ResearchAlghoritmsAttribute;
using Solver = AOC.Solver;

namespace AOC2015
{
    [ResearchAlghoritmsAttribute(TypologyEnum.TextRules)]
    public class Day11 : Solver, IDay
    {
        public int digitNumber = 7;
        public void Part1(object input, bool test, ref object solution)
        {
            string inputText = (string)input;
            var inputpass = inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None)[0];
            bool Founded = false;
            string password = inputpass;
            while (!Founded)
            {
                password = IncreasingPassword(password, digitNumber);
                if (CheckRule1(password)) if (CheckRule2(password)) if (CheckRule3(password)) Founded = true;
            }
            solution = password;
        }
        
        public string IncreasingPassword(string old, int digitNumber)
        {
            string newPass = "";
            while (string.IsNullOrEmpty(newPass))
            {
                if (old[digitNumber] < 122)
                {
                    for (int i = 0; i < 8; i++)
                    {
                        if (i == digitNumber)
                        {
                            newPass += Convert.ToChar(old[i] + 1);
                        }
                        else if(i>digitNumber && old[i]==122)
                        {
                            newPass += Convert.ToChar(97);
                        }
                        else
                        {
                            newPass += old[i];
                        }
                    }
                }
                else
                {
                    digitNumber -= 1;
                }
                if (digitNumber == 1) 
                    {

                    } 
            }
            return newPass;
        }

        public bool CheckRule1(string password)
        {
            bool ret = false;
            string memory = "";
            int conta = 0;
            foreach(var c in password)
            {
                if (string.IsNullOrEmpty(memory)) memory += c;
                else
                {
                    if (c == memory[memory.Length - 1] + 1) { conta++; memory += c; }
                    else { conta = 0; memory = ""; memory += c; }
                    if (conta == 2) return true;
                }
            }

            return ret;
        }
        public bool CheckRule2(string password)
        {
            foreach (var c in password)
            {
                if (c == 'i' || c == 'o' || c == 'l') return false;
            }

            return true;
        }
        public bool CheckRule3(string password)
        {
            bool ret = false;
            string memory = "";
            int conta = 0;
            foreach (var c in password)
            {
                if (string.IsNullOrEmpty(memory)) memory += c;
                else
                {
                    if (c == memory[memory.Length - 1]) { conta++; memory = ""; }
                    else { memory = ""; memory += c; }
                    if (conta == 2) return true;
                }
            }

            return ret;
        }

        public void Part2(object input, bool test, ref object solution)
        {
            string inputText = (string)input;
            
            var inputpass = inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None)[0];
            bool Founded = false;
            string password = "hepxxyzz";
            while (!Founded)
            {
                password = IncreasingPassword(password, digitNumber);
                if (CheckRule1(password)) if (CheckRule2(password)) if (CheckRule3(password)) Founded = true;
            }
            solution = password;
        }


    }
}
