using AOC;
using AOC.DataStructures.Clustering;
using AOC.Model;
using AOC.SearchAlghoritmhs;
 
 
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Threading;
using System.Xml.Schema;
using static AOC.SearchAlghoritmhs.ResearchAlghoritmsAttribute;
using AOC.Utilities.Dynamic;
using System.Text.RegularExpressions;

namespace AOC2017
{
    public class Day4 : Solver, IDay
    {
        public void Part1(object input, bool test, ref object solution)
        {
            int validate = 0;
            string inputText = (string)input;
            foreach (var data in inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None))
            {
                if (!string.IsNullOrEmpty(data))
                {
                    if (PassPhraseValidator(data)) validate++;
                }
            }
            solution = validate;
        }
        public bool PassPhraseValidator(string data)
        {
            bool esito = true;
            var passphrases = Regex.Split(data, @"\s+");
            foreach (var passphrase in passphrases)
            {
                if (passphrases.Where(p => p.Equals(passphrase)).Count() > 1) return false;
            }
            return esito;
        }
        public void Part2(object input, bool test, ref object solution)
        {
            int validate = 0;
            string inputText;
            if (test)
            {
                inputText = @"abcde fghij
abcde xyz ecdab
a ab abc abd abf abj
iiii oiii ooii oooi oooo
oiii ioii iioi iiio";
            }
            else 
            {
                inputText = (string)input;
            }
            foreach (var data in inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None))
            {
                if (!string.IsNullOrEmpty(data))
                {
                    if (SortedPassPhraseValidator(data)) validate++;
                }
            }
            solution = validate;
        }
        public bool SortedPassPhraseValidator(string data)
        {
            bool esito = true;
            var passphrases = Regex.Split(data, @"\s+");
            List<string> SortedPassPhrase = new List<string>();
            foreach(var passphrase in passphrases)
            {
                SortedPassPhrase.Add(SortString(passphrase));
            }

            foreach (var passphrase in SortedPassPhrase)
            {
                if (SortedPassPhrase.Where(p => p.Equals(passphrase)).Count() > 1) return false;
            }
            return esito;
        }
        static string SortString(string input)
        {
            return new string(input.OrderBy(c => c).ToArray());
        }
    }
}
