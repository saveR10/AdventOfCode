using AOC;
using AOC.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC
{
    public class Solver : ReaderInput
    {
        public int year = 0;
        public int day = 0;
        public string part;
        public object solution;
        public bool test = false;
        public Solver(int year, int day, string part)
        {
            this.year = year;
            this.day = day;
            this.part = part;
        }
        public Solver()
        {
        }
        public object FetchInput(InputType t)
        {
            var revertest = "terzocommit";
            if (part.Contains("T"))
            {
                switch (t)
                {
                    case InputType.Text:
                        return TestText(year, day);
                    case InputType.Lines:
                        return TestLines(year, day);
                    case InputType.TextList:
                        return TestText(year, day).ToList();
                    case InputType.LinesList:
                        return TestLines(year, day).ToList();
                }
            }


            switch (t)
            {
                case InputType.Text:
                    return InputText(year, day);
                case InputType.Lines:
                    return InputLines(year, day);
                case InputType.TextList:
                    return InputText(year, day).ToList();
                case InputType.LinesList:
                    return InputLines(year, day).ToList();
            }
            return null;
        }
        public void RunPuzzle(object input)
        {
            var PuzzleDay = Activator.CreateInstance(Type.GetType($"AOC{year}.Day{day}")) as IDay;
            if (part.Contains("T")) test = true;
            switch (part)
            {
                case "1":
                case "1T":
                    PuzzleDay.Part1(input, test, ref solution);
                    break;
                case "2":
                case "2T":
                    PuzzleDay.Part2(input, test, ref solution);
                    break;
            }
        }
    }
}
