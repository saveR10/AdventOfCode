using AOC;
using AOC.Model;
using AOC.SearchAlghoritmhs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using static AOC.SearchAlghoritmhs.ResearchAlgorithmsAttribute;

namespace AOC2016
{
    [ResearchAlgorithms(title: "Day 9: Explosives in Cyberspace",
                        TypologyEnum.Overflow | TypologyEnum.TextRules | TypologyEnum.Decompressing,       // Problema basato sulla decompressione di dati secondo un formato specifico
                        ResolutionEnum.Regex, // Parte 1 può essere risolta con iterazione semplice, Parte 2 richiede approccio ricorsivo per i marker annidati
                        DifficultEnum.Medium,
                        "Calcolo della lunghezza dei dati decompressi con marker di ripetizione, gestione dei marker annidati nella versione 2")]
    public class Day9 : Solver, IDay
    {
        public void Part1(object input, bool test, ref object solution)
        {
            string inputString = (string)input;
            int decompressedLength = 0;
            string[] inputList = inputString.Split(Delimiter.delimiter_line, StringSplitOptions.None);
            foreach (var line in inputList)
            {
                if (!string.IsNullOrEmpty(line))
                {
                    for (int i = 0; i < line.Length; i++)
                    {
                        string marker = "";
                        if (line[i] == '(')
                        {
                            marker = FindMarker(line, i);
                            if (!string.IsNullOrEmpty(marker))
                            {
                                i = i + marker.Length + 2;
                                int characters = int.Parse(marker.Split(Delimiter.delimiter_x, StringSplitOptions.None)[0]);
                                int times = int.Parse(marker.Split(Delimiter.delimiter_x, StringSplitOptions.None)[1]);
                                string repetition = line.Substring(i, characters);
                                decompressedLength = decompressedLength + (repetition.Length) * times;
                                i = i + characters - 1;
                            }
                        }
                        else
                        {
                            decompressedLength++;
                        }
                    }
                }
            }
            solution = decompressedLength;
        }
        public string FindMarker(string line, int index)
        {
            string marker = "";
            int end = -1;
            for (int i = index; i < line.Length; i++)
            {
                if (line[i] == ')')
                {
                    end = i;
                    break;
                }
            }
            if (end >= 0)
            {
                var a = line.Substring(index + 1, end - index - 1);
                if (a.Split(Delimiter.delimiter_x, StringSplitOptions.None).Count() == 2)
                {
                    marker = a;
                }
            }
            return marker;
        }

        public void Part2(object input, bool test, ref object solution)
        {
            string inputString = (string)input;
            if (test) inputString = @"(25x3)(3x3)ABC(2x3)XY(5x2)PQRSTX(18x9)(3x2)TWO(5x7)SEVEN
(3x3)XYZ
X(8x2)(3x3)ABCY
(27x12)(20x12)(13x14)(7x10)(1x12)A";
            long decompressedLength = 0;
            string[] inputList = inputString.Split(Delimiter.delimiter_line, StringSplitOptions.None);
            foreach (var line in inputList)
            {
                decompressedLength = 0;
                if (!string.IsNullOrEmpty(line))
                {
                    var marker = Regex.Matches(line, @"\(\d+x\d+\)");
                    List<Marker> markers = new List<Marker>();
                    foreach (Match m in marker)
                    {
                        var ma = m.Groups[0].Value.Trim('(').Trim(')').Split('x');
                        markers.Add(new Marker(m.Index, int.Parse(ma[0]), int.Parse(ma[1]), m.Length));
                    }
                    Dictionary<int, int> Multiplicator = new Dictionary<int, int>();//index_multiplicator
                    List<Effect> Effects = new List<Effect>();
                    for (int i = 0; i < line.Length; i++)
                    {
                        if (Effects.Any(e => e.toIndex <= i))
                        {
                            Effects.RemoveAll(e => e.toIndex <= i);
                        }


                        if (markers.Any(m => m.index == i))
                        {
                            var mark = markers.Where(m => m.index == i).FirstOrDefault();
                            Effects.Add(new Effect(mark));
                            i = mark.index + mark.length - 1;
                        }
                        else
                        {
                            Multiplicator.Add(i, GetMultiplicator(Effects));
                        }

                    }
                    foreach(var m in Multiplicator)
                    {
                        decompressedLength += m.Value;
                    }
                    //decompressedLength = Multiplicator.Sum(m => m.Value); //OverFlow
                }
            }
            solution = decompressedLength;
        }
        public int GetMultiplicator(List<Effect> Effects)
        {
            int ret = 1;
            foreach (Effect effect in Effects) 
            {
                ret *= effect.multiplicator;
            
            }
            return ret;
        }
        public class Effect
        {
            public int toIndex { get; set; }
            public int multiplicator { get; set; }
            public Effect(Marker marker)
            {
                this.toIndex = marker.toIndex;
                this.multiplicator = marker.repeat;
            }
        }

        public static int idMarker = 0;
        public class Marker
        {
            public bool decompressed { get; set; } = false;
            public int id { get; set; }
            public int position { get; set; }
            public int index { get; set; }
            public int repeat { get; set; }
            public int toIndex { get; set; }
            public int length { get; set; }
            public Marker(int index, int position, int repeat, int length)
            {
                this.index = index;
                this.position = position;
                this.repeat = repeat;
                this.toIndex = index + length + position;
                this.length = length;
                this.id = idMarker;
                idMarker++;
            }
        }
    }
}
