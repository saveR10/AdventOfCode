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
using static AOC.SearchAlghoritmhs.ResearchAlgorithmsAttribute;

namespace AOC2016
{
    [ResearchAlgorithmsAttribute(TypologyEnum.TextRules)]
    public class Day7 : Solver, IDay
    {
        public void Part1(object input, bool test, ref object solution)
        {
            int TLSSequences = 0;
            string inputString = (string)input;
            string[] IPs = inputString.Split(Delimiter.delimiter_line, StringSplitOptions.None);
            foreach(var IP in IPs)
            {
                bool SupportTLS = true; 
                List<string> HypernetSequences = SearchHypernetSequences(IP);
                foreach(var hs in HypernetSequences)
                {
                    if(IsABBA(hs))
                    {
                        SupportTLS = false; break;
                    }
                }
                if (SupportTLS)
                {
                    List<string> SupernetSequences = SearchSupernetSequences(IP);
                    foreach (var ss in SupernetSequences)
                    {
                        if (IsABBA(ss))
                        {
                            TLSSequences += 1;
                            break;
                        }
                    }
                }
            }
            solution = TLSSequences;
        }
        public List<string> SearchSupernetSequences(string IP)
        {
            List<string> SupernetSequences = new List<string>();
            string[] SplittedIP = IP.Split(Delimiter.delimiter_SquareBrackets, StringSplitOptions.None);
            SupernetSequences = SplittedIP.Where((x, i) => i % 2 == 0).ToList();
            return SupernetSequences;
        }
        public List<string> SearchHypernetSequences(string IP)
        {
            List<string> HypernetSequences = new List<string>();
            string[] SplittedIP = IP.Split(Delimiter.delimiter_SquareBrackets, StringSplitOptions.None);
            HypernetSequences = SplittedIP.Where((x,i)=>i%2 ==1).ToList();
            return HypernetSequences;
        }
        public bool IsABBA(string hs)
        {
            bool ret = false;
            for(int i = 0; i < hs.Length -3; i++)
            {
                if (hs[i]==hs[i+3] && hs[i+1]==hs[i+2] && hs[i] != hs[i + 1])
                {
                    ret = true;
                }
            }
            return ret;
        }
        List<string> BABSequences;
        public void Part2(object input, bool test, ref object solution)
        {
            int SSLSequences = 0;
            bool isSSLSequence = false;
            string inputString = "";            
            if (test)
            {
                inputString=@"aba[bab]xyz
xyx[xyx]xyx
aaa[kek]eke
zazbz[bzb]cdb";
            }
            else
            {
                inputString = (string)input;
            }
            string[] IPs = inputString.Split(Delimiter.delimiter_line, StringSplitOptions.None);
            foreach (var IP in IPs)
            {
                isSSLSequence = false;
                BABSequences=new List<string>();
                bool SupportSSL = true;
                List<string> HypernetSequences = SearchHypernetSequences(IP);
                foreach (var hs in HypernetSequences)
                {
                    if (IsBAB(hs))
                    {
                        List<string> SupernetSequences = SearchSupernetSequences(IP);
                        foreach (var ss in SupernetSequences)
                        {
                            if (IsABA(ss))
                            {
                                isSSLSequence = true;
                                break;
                            }
                        }
                    }
                }
                if (isSSLSequence) { SSLSequences++; }
            }
            solution = SSLSequences;
        }
        //317 th
        public bool IsBAB(string hs)
        {
            bool ret = false;
            for (int i = 0; i < hs.Length - 2; i++)
            {
                if (hs[i] == hs[i + 2] && hs[i] != hs[i + 1])
                {
                    string seq = hs[i].ToString() + hs[i + 1].ToString() + hs[i + 2].ToString();
                    if (!BABSequences.Contains(seq)) BABSequences.Add(seq);
                    ret = true;
                }
            }
            return ret;
        }
        public bool IsABA(string ss)
        {
            bool ret = false;
            for (int i = 0; i < ss.Length - 2; i++)
            {
                if (ss[i] == ss[i + 2] && ss[i] != ss[i + 1])
                {
                    string seqABA = ss[i].ToString() + ss[i + 1].ToString() + ss[i + 2].ToString();
                    string seqBAB = ss[i+1].ToString()+ ss[i ].ToString() + ss[i + 1].ToString();
                    if (BABSequences.Contains(seqBAB)) ret = true;
                }
            }
            return ret;
        }

    }
}
