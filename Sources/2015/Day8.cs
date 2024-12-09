using AOC;
using AOC.DataStructures.Clustering;
using AOC.Model;
using AOC.SearchAlghoritmhs; 
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using static AOC.SearchAlghoritmhs.ResearchAlghoritmsAttribute;

namespace AOC2015
{
    [ResearchAlghoritmsAttribute(TypologyEnum.Escaping)]
    public class Day8 : Solver, IDay
    {
        public void Part1(object input, bool test, ref object solution)
        {
            string inputText = (string)input;
            var list = inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None);
            int total_number_character_string_literals = 0;
            int total_number_character_memory = 0;

            foreach (var line in list)
            {
                int number_character_string_literals = 0;
                int number_character_memory = 0;
                bool inString = false;
                bool escaping = false;
                bool hexadecimal = false;
                int pos_hexadecimal=0;
                for (int i = 0; i < line.Length; i++)
                {
                    var a = line[i];
                    
                    if (a=='"' && !escaping) inString = !inString;
                    else
                    {
                        if (hexadecimal)
                        {
                            pos_hexadecimal -= 1;
                            if (pos_hexadecimal == 0) { hexadecimal = false; }
                        }
                        else if (escaping)
                        {
                            if (a == '"' || a == '\\') { number_character_memory++;  }
                            if (a == 'x') { number_character_memory++; hexadecimal = true; pos_hexadecimal = 2; }
                            escaping = false;
                        }
                        else if (inString)
                        {
                            if (a != '"' && a != '\\') number_character_memory++;
                            if (a == '\\') escaping = true;
                        }
                    }
                    number_character_string_literals++;
                }
                total_number_character_memory += number_character_memory;
                total_number_character_string_literals+= number_character_string_literals;
            }
            solution = total_number_character_string_literals - total_number_character_memory;
        }
        public void Part2(object input, bool test, ref object solution)
        {
            string inputText = (string)input;
            var list = inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None);
            int total_number_character_string_literals = 0;
            int total_number_character_memory = 0;

            foreach (var line in list)
            {
                int number_character_string_literals = 0;
                int number_character_memory = 0;
                bool inString = false;
                bool escaping = false;
                bool hexadecimal = false;
                int pos_hexadecimal = 0;
                for (int i = 0; i < line.Length; i++)
                {
                    var a = line[i];

                    if (a == '"' && !escaping) inString = !inString;
                    else
                    {
                        if (hexadecimal)
                        {
                            pos_hexadecimal -= 1;
                            if (pos_hexadecimal == 0) { hexadecimal = false; }
                        }
                        else if (escaping)
                        {
                            if (a == '"' || a == '\\') { number_character_memory++; }
                            if (a == 'x') { number_character_memory++; hexadecimal = true; pos_hexadecimal = 2; }
                            escaping = false;
                        }
                        else if (inString)
                        {
                            if (a != '"' && a != '\\') number_character_memory++;
                            if (a == '\\') escaping = true;
                        }
                    }
                    number_character_string_literals++;
                }
                total_number_character_memory += number_character_memory;
                total_number_character_string_literals += number_character_string_literals;
            }

            int total_number_character_string_literals_new = 0;
            int total_number_character_memory_new = 0;
            foreach (var line in list)
            {
                if (line == "")
                {
                    continue;
                }
                //# placeholder for backslash from double quotes
                //| placeholder for backslash from backslash
                string newline =line.Replace("\"","#").Replace("\\","|").Replace("#","\\\"").Replace("|","\\\\");
                newline="\""+newline+"\"";
                int number_character_string_literals = 0;
                int number_character_memory = 0;
                bool inString = false;
                bool escaping = false;
                bool hexadecimal = false;
                int pos_hexadecimal = 0;
                for (int i = 0; i < newline.Length; i++)
                {
                    var a = newline[i];
                    if (a == '"' && !escaping) inString = !inString;
                    else
                    {
                        if (hexadecimal)
                        {
                            pos_hexadecimal -= 1;
                            if (pos_hexadecimal == 0) { hexadecimal = false; }
                        }
                        else if (escaping)
                        {
                            if (a == '"' || a == '\\') { number_character_memory++; }
                            if (a == 'x') { number_character_memory++; hexadecimal = true; pos_hexadecimal = 2; }
                            escaping = false;
                        }
                        else if (inString)
                        {
                            if (a != '"' && a != '\\') number_character_memory++;
                            if (a == '\\') escaping = true;
                        }
                    }
                    number_character_string_literals++;
                }
                total_number_character_memory_new += number_character_memory;
                total_number_character_string_literals_new += number_character_string_literals;
            }
            solution = total_number_character_string_literals_new -total_number_character_string_literals ;
        }
    }
}
