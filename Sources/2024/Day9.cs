using AOC;
using AOC.DataStructures.Clustering;
using AOC.Model;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AOC2024
{
    public class Day9 : Solver, IDay
    {
        public void Part1(object input, bool test, ref object solution)
        {
            string inputText = (string)input;
            List<int> Blocks = new List<int>();
            int ID = 0;
            for (int p = 0; p < inputText.Length; p++)
            {
                if (p % 2 == 0)
                {
                    int count = int.Parse(inputText[p].ToString());
                    for (int i = 0; i < count; i++)
                    {
                        Blocks.Add(ID);
                    }
                    ID++;
                }
                else
                {
                    int count = int.Parse(inputText[p].ToString());
                    for (int i = 0; i < count; i++)
                    {
                        Blocks.Add(-1);
                    }
                }
            }

            int FileBlocksNumber = Blocks.Count(b => b != -1);
            bool compactedDisk = false;

            while (!compactedDisk)
            {
                Blocks = MoveFileBlock(FileBlocksNumber, Blocks);
                compactedDisk = CheckFileSystem(FileBlocksNumber, Blocks);
            }
            long checkSum = FindCheckSum(FileBlocksNumber, Blocks);
            solution = checkSum;
        }

        public long FindCheckSum(int fileblocksnumber, List<int> blocks)
        {
            long checksum = 0;
            for (int i = 0; i < blocks.Count; i++)
            {
                if (blocks[i] != -1)
                {
                    checksum += (long)blocks[i] * i;
                }
            }

            return checksum;
        }
        public bool CheckFileSystem(int fileblocksnumber, List<int> blocks)
        {
            for (int i = 0; i < fileblocksnumber; i++)
            {
                if (blocks[i] == -1) return false;
            }
            return true;
        }

        public List<int> MoveFileBlock(int fileblocksnumber, List<int> blocks)
        {
            for (int i = blocks.Count - 1; i >= 0; i--)
            {
                if (blocks[i] != -1)
                {
                    for (int j = 0; j < blocks.Count; j++)
                    {
                        if (blocks[j] == -1) 
                        {
                            blocks[j] = blocks[i];
                            blocks[i] = -1;
                            return blocks;
                        }
                    }
                }
            }

            return blocks;
        }

        //00992111777.44.333....5555.6666.....8888..

        public void Part2(object input, bool test, ref object solution)
        {
            string inputText = (string)input;
            List<int> blocks = new List<int>();
            int ID = 0;

            for (int p = 0; p < inputText.Length; p++)
            {
                if (p % 2 == 0)
                {
                    int count = int.Parse(inputText[p].ToString());
                    for (int i = 0; i < count; i++)
                    {
                        blocks.Add(ID);
                    }
                    ID++;
                }
                else
                {
                    int count = int.Parse(inputText[p].ToString());
                    for (int i = 0; i < count; i++)
                    {
                        blocks.Add(-1);
                    }
                }
            }
            for (int fileID = ID - 1; fileID >= 0; fileID--)
            {
                blocks = MoveFileToLeftMostFreeSpace(fileID, blocks);
            }
            long checkSum = FindCheckSum(blocks);
            solution = checkSum;
        }

        public List<int> MoveFileToLeftMostFreeSpace(int fileID, List<int> blocks)
        {
            int fileSize = blocks.Count(b => b == fileID);
            int indfileID = blocks.IndexOf(fileID); 
            for (int i = 0; i <= blocks.Count - fileSize && i<indfileID; i++)
            {
                bool canFit = true;
                for (int j = 0; j < fileSize; j++)
                {
                    if (blocks[i + j] != -1)
                    {
                        canFit = false;
                        break;
                    }
                }
                if (canFit)
                {
                    for (int j = 0; j < blocks.Count; j++)
                    {
                        if (blocks[j] == fileID)
                        {
                            blocks[j] = -1;
                        }
                    }
                    for (int j = 0; j < fileSize; j++)
                    {
                        blocks[i + j] = fileID;
                    }
                    break;
                }
            }
            return blocks;
        }

        public long FindCheckSum(List<int> blocks)
        {
            long checksum = 0;
            for (int i = 0; i < blocks.Count; i++)
            {
                if (blocks[i] != -1)
                {
                    checksum += (long)blocks[i] * i;
                }
            }
            return checksum;
        }
    }
}