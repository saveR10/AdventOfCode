using AOC;
using AOC.DataStructures.Clustering;
using AOC.Documents.LINQ;
using AOC.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;

namespace AOC2023
{
    //DA SISTEMARE
    public class Day3 : Solver, IDay
    {
        private const string V = "\r\n";
        static int dim;
        string[,] matrix = new string[dim, dim];
        typeEnum[,] pointType = new typeEnum[dim, dim];
        bool[,] history = new bool[dim, dim];
        int[,] position = new int[dim, dim];
        int somma = 0;

        int Gear_Ratio = 0;
        int Gear_Ratio_Sum = 0;


        public int first_number_row = 0;
        public int first_number_column = 0;
        public int first_number = 0;
        public int second_number_row = 0;
        public int second_number_column = 0;
        public int second_number = 0;
        public void Part1(object input, bool test, ref object solution)
        {
            dim = test ? 10 : 140;
            string inputText = (string)input;
            String[] delimiters = { "\r\n" };
            String[] result = inputText.Split(delimiters, StringSplitOptions.None);
            for (int r = 0; r < dim; r++)
            {
                for (int c = 0; c < dim; c++)
                {
                    matrix[r, c] = result[r][c].ToString();
                    pointType[r, c] = CheckPoint(matrix[r, c], r, c);
                }
            }


            for (int r = 0; r < dim; r++)
            {
                for (int c = 0; c < dim; c++)
                {
                    if (pointType[r, c].Equals(typeEnum.number))
                    {
                        if (CheckAdjacentSymbol(r, c))
                        {
                            if (!CheckHistorycization(TotalNumber(r, c)))
                            {
                                Hystoricization(TotalNumber(r, c));
                            }
                        }
                    }
                }
            }

            solution = somma;
        }

        public bool CheckAdjacentSymbol(int r, int c)
        {
            if (r == 14 && c == 138)
            {
                var a = "";
            }
            if (r > 0) if (CheckPoint(matrix[r - 1, c], r - 1, c).Equals(typeEnum.symbol)) return true;
            if (r < dim-1) if (CheckPoint(matrix[r + 1, c], r + 1, c).Equals(typeEnum.symbol)) return true;
            if (c > 0) if (CheckPoint(matrix[r, c - 1], r, c - 1).Equals(typeEnum.symbol)) return true;
            if (c < dim - 1) if (CheckPoint(matrix[r, c + 1], r, c + 1).Equals(typeEnum.symbol)) return true;
            if (r > 0 && c > 0) if (CheckPoint(matrix[r - 1, c - 1], r - 1, c - 1).Equals(typeEnum.symbol)) return true;
            if (r > 0 && c < dim - 1) if (CheckPoint(matrix[r - 1, c + 1], r - 1, c + 1).Equals(typeEnum.symbol)) return true;
            if (r < dim - 1 && c > 0) if (CheckPoint(matrix[r + 1, c - 1], r + 1, c - 1).Equals(typeEnum.symbol)) return true;
            if (r < dim - 1 && c < dim - 1) if (CheckPoint(matrix[r + 1, c + 1], r + 1, c + 1).Equals(typeEnum.symbol)) return true;
            return false;
        }
        public bool CheckHistorycization(ResponseTotalNumber responseTotalNumber)
        {
            return history[responseTotalNumber.r, responseTotalNumber.starting_column];
        }
        public bool CheckHistorycizationB(int r, int c)
        {
            return history[r, c];
        }

        public class ResponseTotalNumber
        {
            public int number;
            public int r;
            public int starting_column;
        }
        public ResponseTotalNumber TotalNumber(int r, int c)
        {
            ResponseTotalNumber responseTotalNumber = new ResponseTotalNumber();
            int i = 0;
            string string_number = "";
            int number;
            int starting_column = 0;
            while (c + i - 1 >= 0 && int.TryParse(matrix[r, c + i - 1], out _))
            {
                i--;
            }
            starting_column = c + i;
            while (c + i < 140 && int.TryParse(matrix[r, c + i], out _))
            {
                string_number += matrix[r, c + i];
                i++;

            }
            number = int.Parse(string_number);

            if (!CheckPositioning(number, r, starting_column))
            {
                Positioning(number, r, starting_column);
            }

            responseTotalNumber.number = number;
            responseTotalNumber.r = r;
            responseTotalNumber.starting_column = starting_column;
            return responseTotalNumber;
        }

        public bool CheckPositioning(int number, int r, int c)
        {
            if (position[r, c] == number) { return true; }
            else { return false; }
        }
        public void Positioning(int number, int r, int c)
        {
            position[r, c] = number;
        }
        public typeEnum CheckPoint(string point, int r, int c)
        {
            switch (point)
            {
                case ".":
                    return typeEnum.point;
                    break;
                case "0":
                case "1":
                case "2":
                case "3":
                case "4":
                case "5":
                case "6":
                case "7":
                case "8":
                case "9":
                    return typeEnum.number;
                    break;
                case "*":
                    return typeEnum.gear;
                default:
                    return typeEnum.symbol;
                    break;
            }
        }

        public enum typeEnum
        {
            point = 0,
            number = 1,
            symbol = 2,
            gear = 3
        }

        public void Hystoricization(ResponseTotalNumber responseTotalNumber)
        {
            history[responseTotalNumber.r, responseTotalNumber.starting_column] = true;
            somma += responseTotalNumber.number;
        }

        public void Part2(object input, bool test, ref object solution)
        {
            string inputText = (string)input;
            String[] delimiters = { "\r\n" };
            String[] result = inputText.Split(delimiters, StringSplitOptions.None);

            for (int r = 0; r < 140; r++)
            {
                for (int c = 0; c < 140; c++)
                {
                    matrix[r, c] = result[r][c].ToString();
                    pointType[r, c] = CheckPoint(matrix[r, c], r, c);
                }
            }


            for (int r = 0; r < 140; r++)
            {
                if (r == 137)
                {
                    var a = "";
                }
                for (int c = 0; c < 140; c++)
                {
                    if (pointType[r, c].Equals(typeEnum.gear))
                    {
                        if (CheckInfraGears(r, c))
                        {
                            if (!CheckHistorycizationB(r, c))
                            {
                                HystoricizationB(r, c);
                                Console.WriteLine($"{Gear_Ratio_Sum} - {Gear_Ratio} - [{first_number_row},{first_number_column}]={first_number} X [{second_number_row},{second_number_column}]={second_number}");
                            }
                        }
                    }
                }
            }
        }
        public void HystoricizationB(int r, int c)
        {
            history[r, c] = true;
            Gear_Ratio = first_number * second_number;
            Gear_Ratio_Sum += Gear_Ratio;
        }
        public bool CheckInfraGears(int r, int c)
        {
            int cont = 0;
            first_number_row = 0;
            first_number_column = 0;
            first_number = 0;
            second_number_row = 0;
            second_number_column = 0;
            second_number = 0;

            if (r > 0) if (CheckPoint(matrix[r - 1, c], r - 1, c).Equals(typeEnum.number)) Activate(ref cont, TotalNumber(r - 1, c));
            if (r > 0 && c < 139) if (CheckPoint(matrix[r - 1, c + 1], r - 1, c + 1).Equals(typeEnum.number)) Activate(ref cont, TotalNumber(r - 1, c + 1));
            if (c < 139) if (CheckPoint(matrix[r, c + 1], r, c + 1).Equals(typeEnum.number)) Activate(ref cont, TotalNumber(r, c + 1));
            if (r < 139 && c < 139) if (CheckPoint(matrix[r + 1, c + 1], r + 1, c + 1).Equals(typeEnum.number)) Activate(ref cont, TotalNumber(r + 1, c + 1));
            if (r < 139) if (CheckPoint(matrix[r + 1, c], r + 1, c).Equals(typeEnum.number)) Activate(ref cont, TotalNumber(r + 1, c));
            if (r < 139 && c > 0) if (CheckPoint(matrix[r + 1, c - 1], r + 1, c - 1).Equals(typeEnum.number)) Activate(ref cont, TotalNumber(r + 1, c - 1));
            if (c > 0) if (CheckPoint(matrix[r, c - 1], r, c - 1).Equals(typeEnum.number)) Activate(ref cont, TotalNumber(r, c - 1));
            if (r > 0 && c > 0) if (CheckPoint(matrix[r - 1, c - 1], r - 1, c - 1).Equals(typeEnum.number)) Activate(ref cont, TotalNumber(r - 1, c - 1));

            if (cont == 2) return true;
            else return false;
        }
        public void Activate(ref int cont, ResponseTotalNumber responseTotalNumber)
        {
            if (cont == 0)
            {
                first_number_row = responseTotalNumber.r;
                first_number_column = responseTotalNumber.starting_column;
                first_number = responseTotalNumber.number;
                cont++;
            }
            else if (cont == 1)
            {
                if (first_number_row != responseTotalNumber.r ||
                        first_number_column != responseTotalNumber.starting_column)
                {
                    second_number_row = responseTotalNumber.r;
                    second_number_column = responseTotalNumber.starting_column;
                    second_number = responseTotalNumber.number;
                    cont++;
                }
            }
            else
            {

            }
        }
    }
}
