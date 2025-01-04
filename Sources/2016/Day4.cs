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
using static AOC.SearchAlghoritmhs.ResearchAlgorithmsAttribute;

namespace AOC2016
{
    [ResearchAlgorithmsAttribute(TypologyEnum.Hashing)] //Shift Cipher
    [ResearchAlgorithmsAttribute(TypologyEnum.TextRules)]
    public class Day4 : Solver, IDay
    {
        Dictionary<char, int> letters;
        public void Part1(object input, bool test, ref object solution)
        {
            int conta = 0;
            string inputText = (string)input;
            foreach (string t in inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None))
            {
                if (!string.IsNullOrEmpty(t))
                {
                    letters = new Dictionary<char, int>();
                    string[] rooms = t.Split(Delimiter.delimiter_SquareBrackets, StringSplitOptions.None)[0].Split(Delimiter.delimiter_dash, StringSplitOptions.None);
                    int sectorID = int.Parse(rooms[rooms.Length - 1]);

                    string checkSum = t.Split(Delimiter.delimiter_SquareBrackets, StringSplitOptions.None)[1];
                    for (int i = 0; i < rooms.Length - 1; i++)
                    {
                        foreach (var l in rooms[i])
                        {
                            if (!letters.ContainsKey(l)) letters.Add(l, 1);
                            else letters[l] += 1;
                        }
                    }
                    if (IsRoom(checkSum))
                    {
                        conta += sectorID;
                    }
                    if (IsTwin(checkSum))
                    {

                    }
                }
            }
            solution = conta;
        }
        public bool IsTwin(string checkSum)
        {
            bool ret = false;
            Dictionary<char, int> counterchar = new Dictionary<char, int>();
            foreach (var c in checkSum)
            {
                if (!counterchar.ContainsKey(c)) counterchar.Add(c, 1);
                else counterchar[c] += 1;
            }
            if (counterchar.Any(c => c.Value > 1))
            {

            }
            return ret;
        }
        public bool IsRoom(string checkSum)
        {
            int indice = 0;
            int num = 0;
            bool ret = false;
            int max = 0;
            char key;
            foreach (var c in checkSum)
            {
                if (letters.ContainsKey(c))
                {

                    if (IsMaxOccurency(c))
                    {
                        ret = true;
                        letters.Remove(c);
                    }
                    else
                    {
                        ret = false;
                        break;
                    }
                }
                else
                {
                    ret = false;
                    break;
                }
            }
            return ret;
        }

        public bool IsMaxOccurency(char c)
        {
            bool ret = true;
            int occ = letters[c];
            foreach (var l in letters.Keys)
            {
                if (letters[l] > occ)
                {
                    ret = false;
                }
            }
            return ret;
        }
        public void Part2(object input, bool test, ref object solution)
        {
            int conta = 0;
            string inputText = "";
            if (test)
            {
                inputText = "qzmt-zixmtkozy-ivhz-343[ztimq]";
            }
            else
            {
                inputText = (string)input;
            }
            foreach (string t in inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None))
            {
                if (!string.IsNullOrEmpty(t))
                {
                    letters = new Dictionary<char, int>();
                    string[] rooms = t.Split(Delimiter.delimiter_SquareBrackets, StringSplitOptions.None)[0].Split(Delimiter.delimiter_dash, StringSplitOptions.None);
                    int sectorID = int.Parse(rooms[rooms.Length - 1]);

                    string checkSum = t.Split(Delimiter.delimiter_SquareBrackets, StringSplitOptions.None)[1];
                    for (int i = 0; i < rooms.Length - 1; i++)
                    {
                        foreach (var l in rooms[i])
                        {
                            if (!letters.ContainsKey(l)) letters.Add(l, 1);
                            else letters[l] += 1;
                        }
                    }
                    if (IsRoom(checkSum))
                    {
                        string encryptedName = "";
                        for (int i = 0; i < rooms.Length - 1; i++)
                        {
                            encryptedName += rooms[i]+" ";
                        }

                        string decryptedName = CaesarChiper(encryptedName,sectorID);
                        Console.WriteLine(decryptedName);
                        if (decryptedName.Contains("northpole object storage")) solution = sectorID;
                    }
                    if (IsTwin(checkSum)) { }
                }
            }
        }
        public string CaesarChiper(string encryptedName,int sectorID)
        {
            string decryptedName = "";
            int toShift = sectorID % 26;
            foreach(var c in encryptedName)
            {
                char l = c;
                if (((int)l + toShift) > 122) l = (char)(l - 26);
                if (l == 32)
                {
                    decryptedName += (char)(l);
                }
                else
                {
                    decryptedName += (char)(l+toShift);
                }
            }
            return decryptedName;
        }
    }
}
