using AOC;
using AOC.DataStructures.Clustering;
using AOC.Model;
using AOC.SearchAlghoritmhs;
using NUnit.Framework.Interfaces;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Threading;
using System.Xml.Schema;
using static AOC.SearchAlghoritmhs.ResearchAlghoritmsAttribute;
using AOC.Utilities.Dynamic;

namespace AOC2017
{
    [ResearchAlghoritmsAttribute(TypologyEnum.Map)] 
    public class Day3 : Solver, IDay
    {
        public void Part1(object input, bool test, ref object solution)
        {
            int step = 0;
            string inputText = (string)input;
            foreach (var data in inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None))
            {
                if (!string.IsNullOrEmpty(data))
                {
                    step = GenerateSpiral(int.Parse(data));
                }
            }
            solution = step;
        }
        public int GenerateSpiral(int data)
        {
            int init = 1;
            int step = 0;
            int radius = 0;
            int rank = 1;
            int maxRank = 1;
            while (init != data)
            {
                if (init == 1 || init == maxRank)
                {
                    radius++;
                    rank = rank + 2; //1,3,5,7,9,...
                    maxRank = SetMaxRank(rank); //1,9,25,49,...
                }
                init++;
                step = ManhattanMeasure(init, rank, radius);
            }
            return step;
        }
        static int ManhattanMeasure(int init, int rank, int radius)
        {
            int RankPosition;
            if (rank > 1)
                RankPosition = (init - (int)Math.Pow(rank - 2, 2)) % (rank - 1);
            else
                RankPosition = 0;
            return radius + CalculateTriangular(RankPosition, rank);
        }
        static int CalculateTriangular(int RankPosition, int rank)
        {
            if (RankPosition == 0) return rank / 2;
            else if (RankPosition == rank / 2) return 0;
            else return Math.Abs(rank / 2 - RankPosition);


        }
        public int SetMaxRank(int rank)
        {
            return rank * rank;
        }
        public void Part2(object input, bool test, ref object solution)
        {
            int step = 0;
            string inputText = (string)input;
            foreach (var data in inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None))
            {
                if (!string.IsNullOrEmpty(data))
                {
                    step = GenerateMatrix(int.Parse(data));
                }
            }
            solution = step;
        }
        int[,] Matrix;
        int r;
        int c;
        public int GenerateMatrix(int data)
        {
            int init = 1;
            int step = 0;
            int rank = 1;
            int radius = 0;
            int maxNumberRank = 1;
            int maxRank=0;
            int NeighboursSum=0;
            r = 300;
            c = 300;
            Matrix = new int[600, 600];
            Matrix[300, 300] = 1;
            while (NeighboursSum < data)
            {
                if (init == 1 || init == maxRank)
                {
                    radius++;
                    rank = rank + 2; //1,3,5,7,9,...
                    maxNumberRank = SetMaxNumberRank(rank) - SetMaxNumberRank(rank - 2); //1,8,16,...
                    maxRank = SetMaxNumberRank(rank); 
                    c++;
                }
                else
                {
                    Move(init, rank, maxNumberRank, radius);

                }
                init++;
                NeighboursSum = FindNeighbors();
                Matrix[r, c] = NeighboursSum;
                ShowMatrix();
                //step = ManhattanMeasure(init, rank, radius);
            }
            return NeighboursSum;
        }
        public void ShowMatrix()
        {
            int randColor = 1;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("###########################\n");
            for (int i = 5; i > -5; i--)
            {
                for (int j = -5; j < 5; j++)
                {
                    switch (randColor%2)
                    {
                        case 0:
                            Console.ForegroundColor = ConsoleColor.Red;
                            break;
                        case 1:
                            Console.ForegroundColor = ConsoleColor.Green;
                            break;
                        case 2:
                            Console.ForegroundColor = ConsoleColor.Blue;
                            break;

                    }
                    Console.Write(Matrix[r + i, c + j]);
                    randColor++;
                }
                Console.Write("\n");
            }
        }
        public void Move(int init, int rank, int maxNumberRank, int radius)
        {
            int RankAbsPosition;
            if (rank > 1)
                RankAbsPosition = (init - (int)Math.Pow(rank - 2, 2));
            else
                RankAbsPosition = 0;
            //;return //radius + CalculateTriangular(RankPosition, rank);

            if (RankAbsPosition > 0 && RankAbsPosition < maxNumberRank / 4) r++;
            else if (RankAbsPosition >= maxNumberRank / 4 && RankAbsPosition < maxNumberRank / 2) c--;
            else if (RankAbsPosition >= maxNumberRank / 2 && RankAbsPosition < 3 * maxNumberRank / 4) r--;
            else if (RankAbsPosition >= 3 * maxNumberRank / 4 && RankAbsPosition < maxNumberRank) c++;
        }
        public int FindNeighbors()
        {
            int sum = 0;
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    sum += Matrix[r + i, c + j];
                }

            }
            return sum;

        }
        public int SetMaxNumberRank(int rank)
        {
            return rank * rank;
        }

    }
}
