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
                    rank=rank+2; //1,3,5,7,9,...
                    maxRank= SetMaxRank(rank); //1,9,25,49,...
                }
                init++;
                step = ManhattanMeasure(init, rank, radius);
            }
            return step;
        }
        static int ManhattanMeasure(int init, int rank,int radius)
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
            else return Math.Abs(rank/2 - RankPosition);


        }
        public int SetMaxRank(int rank)
        {
            return rank *rank;
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
            int maxNumberRank = 1;
            r = 300;
            c = 300;
            Matrix = new int[600, 600];
            Matrix[300, 300] = 1;
            while (init < data)
            {
                if (init == 1 || init == maxNumberRank)
                {
                    rank = rank + 2; //1,3,5,7,9,...
                    maxNumberRank = SetMaxNumberRank(rank); //1,9,25,49,...
                    c++;
                }
                init++;
                //Move(init, rank, radius);
                int NeighboursSum = FindNeighbors();
                Matrix[r,c] = NeighboursSum;
                //step = ManhattanMeasure(init, rank, radius);
            }
            return step;
        }
        public void Move(int init, int rank, int radius)
        {
            int RankPosition;
            if (rank > 1)
                RankPosition = (init - (int)Math.Pow(rank - 2, 2)) % (rank - 1);
            else
                RankPosition = 0;
            //;return //radius + CalculateTriangular(RankPosition, rank);
        }
        public int FindNeighbors()
        {
            int sum = 0;
            for(int i = -1; i < 2;i++)
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
