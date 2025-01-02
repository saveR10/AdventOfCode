using AOC;
using AOC.DataStructures.Clustering;
using AOC.Model;
using AOC.SearchAlghoritmhs;
using AOC.Utilities.Dynamic;
using Newtonsoft.Json.Bson;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management.Instrumentation;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Text.RegularExpressions;
using static AOC.SearchAlghoritmhs.ResearchAlgorithmsAttribute;
using static System.Net.Mime.MediaTypeNames;

namespace AOC2024
{
    [ResearchAlgorithms(TypologyEnum.Map)]
    public class Day6 : Solver, IDay
    {
        public static string[,] Map;
        public static string[,] StoryMap;
        public static int n;
        public static int[] GuardPoint = new int[2];
        public static int[] NextPoint = new int[2];
        public Direction dir = Direction.Up;
        public bool GuardPresence = true;

        public void Part1(object input, bool test, ref object solution)
        {
            List<string> inputList = new List<string>();
            inputList = (List<string>)input;
            n = inputList.Count();
            Map = new string[n, n];
            StoryMap = new string[n, n];
            int DistinctPositions = 0;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Map[i, j] = inputList[i].Substring(j, 1);
                    StoryMap[i, j] = inputList[i].Substring(j, 1);
                    if (Map[i, j] == "^")
                    {
                        GuardPoint[0] = i;
                        GuardPoint[1] = j;
                        StoryMap[i, j] += "X";
                    }
                }
            }
            ShowMap();

            while (GuardPresence)
            {
                SetNextPoint();
                MoveNextPoint();
                //ShowMap();
            }
            solution = ScanMap();
        }

        public void ShowMap()
        {
            Console.WriteLine("\n----------------------\n");

            for (int i = 0; i < n; i++)
            {
                Console.Write(i);
            }
            Console.WriteLine();
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (GuardPoint[0] == i && GuardPoint[1] == j) Console.ForegroundColor = ConsoleColor.Green;
                    else Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(Map[i, j]);
                }
                Console.Write(i);
                Console.WriteLine("");
            }
            Console.WriteLine($"GuardPoint: {GuardPoint[0]} {GuardPoint[1]}");
            Console.WriteLine($"NextPoint: {NextPoint[0]} {NextPoint[1]}");
            Console.WriteLine($"LoopObstacles: {LoopObstacles}");
        }
        public enum Direction
        {
            Up,
            Right,
            Down,
            Left
        }
        public Direction TurnRight(Direction dir)
        {
            return (Direction)(((int)dir + 1) % 4);
        }
        public bool SetNextPoint()
        {
            isTurned = false;
            int test_r = GuardPoint[0]; int test_c = GuardPoint[1];
            NextPoint[0] = test_r; NextPoint[1] = test_c;

            SetTestCoordinates(ref test_r, ref test_c);
            CheckGuardPresence(test_r, test_c);

            if (GuardPresence)
            {
                if (Map[test_r, test_c].Equals("#"))
                {
                    dir = TurnRight(dir);
                    Map[GuardPoint[0], GuardPoint[1]] = "X";
                    StoryMap[GuardPoint[0], GuardPoint[1]] += TraceToFind(dir);
                    NextPoint[0] = GuardPoint[0];
                    NextPoint[1] = GuardPoint[1];
                    isTurned = true;
                }
                else
                {
                    switch (dir)
                    {
                        case Direction.Up:
                            NextPoint[0] -= 1;
                            break;
                        case Direction.Right:
                            NextPoint[1] += 1;
                            break;
                        case Direction.Down:
                            NextPoint[0] += 1;
                            break;
                        case Direction.Left:
                            NextPoint[1] -= 1;
                            break;
                    }

                }
            }
            return isTurned;
        }
        public void SetTestCoordinates(ref int test_r, ref int test_c)
        {
            if (dir.Equals(Direction.Up))
            {
                test_r--;
            }
            else if (dir.Equals(Direction.Right))
            {
                test_c++;
            }
            else if (dir.Equals(Direction.Down))
            {
                test_r++;
            }
            else if (dir.Equals(Direction.Left))
            {
                test_c--;
            }
        }
        public bool CheckGuardPresence(int test_r, int test_c)
        {
            if (test_r >= n || test_r < 0 || test_c >= n || test_c < 0)
            {
                GuardPresence = false;
            }
            return true;
        }
        public void MoveNextPoint()
        {
            CheckGuardPresence(NextPoint[0], NextPoint[1]);

            if (GuardPresence)
            {
                GuardPoint[0] = NextPoint[0];
                GuardPoint[1] = NextPoint[1];
                Map[GuardPoint[0], GuardPoint[1]] = "X";
                StoryMap[GuardPoint[0], GuardPoint[1]] += "T";
                TraceDirection();
            }
        }
        public void TraceDirection()
        {
            switch (dir)
            {
                case Direction.Up:
                    StoryMap[GuardPoint[0], GuardPoint[1]] += "^";
                    break;
                case Direction.Right:
                    StoryMap[GuardPoint[0], GuardPoint[1]] += ">";
                    break;
                case Direction.Down:
                    StoryMap[GuardPoint[0], GuardPoint[1]] += "V";
                    break;
                case Direction.Left:
                    StoryMap[GuardPoint[0], GuardPoint[1]] += "<";
                    break;
            }

        }
        public int ScanMap()
        {
            int ExploredPosition = 0;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (Map[i, j] == "^" || Map[i, j] == "X" || Map[i, j] == "+") ExploredPosition++;
                }
            }
            return ExploredPosition;
        }




        //PART 2
        public int LoopObstacles = 0;
        public string[,] TempMap;
        public string[,] TempStoryMap;
        public int[] TempGuardPoint = new int[2];
        public int[] TempNextPoint = new int[2];
        public Direction TempDir;
        public bool TempGuardPresence;
        public int[] StartPoint = new int[2];
        public bool isTurned;
        public bool isSettedNextPoint;
        public bool TempIsSettedNextPoint;
        public bool TempIsTurned;
        public bool isLoop;
        public bool Show = false;
        public bool ShowLoop = false;
        //Risolto con il bruteforce: ho perso un sacco di tempo (giorni!) perché avevo trascurato un dettaglio:
        //se ho già testato un punto verificando se aggiungendo un ostacolo la guardia rimane bloccata, se ripasso
        //successivamente per lo stesso punto (ma ovviamente da una diversa direzione, altrimenti il percorso di
        //default sarebbe in loop), bisogna mettere la condizione di non ritestare lo stesso punto!
        public void Part2(object input, bool test, ref object solution)
        {
            List<string> inputList = new List<string>();
            inputList = (List<string>)input;
            n = inputList.Count();
            Map = new string[n, n];
            StoryMap = new string[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Map[i, j] = inputList[i].Substring(j, 1);
                    StoryMap[i, j] = inputList[i].Substring(j, 1);
                    if (Map[i, j] == "^")
                    {
                        GuardPoint[0] = i;
                        GuardPoint[1] = j;
                        StoryMap[i, j] += "^";
                        Map[i, j] = "X";
                        StartPoint[0] = i;
                        StartPoint[1] = j;
                    }
                }
            }
            if (Show) ShowMap();
            while (GuardPresence)
            {
                isSettedNextPoint = false;
                isTurned = false;

                Set_NextPoint();
                if (isSettedNextPoint && GuardPresence)
                {
                    if (NextPoint[0]==8 && NextPoint[1] == 72)
                    {

                    }
                    if (Show) 
                        ShowMap();
                    CheckLoop();
                    MoveNextPoint();
                    if (Show) 
                        ShowMap();
                }
            }
            solution = ScanLoopMap();//LoopObstacles;//
            //2162
            ShowObstaclesMap();
        }
        public void ShowObstaclesMap()
        {
            Console.WriteLine("\n----------------------\n");

            for (int i = 0; i < n; i++)
            {
                Console.Write(i);
            }
            Console.WriteLine();
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (StoryMap[i, j].Contains("O")) Console.Write("O");
                    else Console.Write(Map[i, j]);
                }
                Console.Write(i);
                Console.WriteLine("");
            }
            Console.WriteLine($"GuardPoint: {GuardPoint[0]} {GuardPoint[1]}");
            Console.WriteLine($"NextPoint: {NextPoint[0]} {NextPoint[1]}");
            Console.WriteLine($"LoopObstacles: {LoopObstacles}");

        }
        public bool Set_NextPoint()
        {
            int test_r = GuardPoint[0];
            int test_c = GuardPoint[1];
            NextPoint[0] = test_r;
            NextPoint[1] = test_c;

            SetTestCoordinates(ref test_r, ref test_c);
            CheckGuardPresence(test_r, test_c);

            if (GuardPresence)
            {
                if (Map[test_r, test_c].Equals("#"))
                {
                    isSettedNextPoint = false;
                    isTurned = true;
                    dir = TurnRight(dir);
                    StoryMap[GuardPoint[0], GuardPoint[1]] += TraceToFind(dir);
                    NextPoint[0] = GuardPoint[0];
                    NextPoint[1] = GuardPoint[1];
                }
                else
                {
                    switch (dir)
                    {
                        case Direction.Up:
                            NextPoint[0] -= 1;
                            break;
                        case Direction.Right:
                            NextPoint[1] += 1;
                            break;
                        case Direction.Down:
                            NextPoint[0] += 1;
                            break;
                        case Direction.Left:
                            NextPoint[1] -= 1;
                            break;
                    }
                    isTurned = false;
                    isSettedNextPoint = true;
                }
            }
            return isTurned;
        }
        public void CheckLoop()
        {
            if (GuardPresence && !StoryMap[NextPoint[0], NextPoint[1]].Contains("T"))
            {
                isLoop = false;

                TempGuardPoint = new int[2];
                TempMap = new string[n, n];
                TempStoryMap = new string[n, n];
                TempNextPoint = new int[2];

                TempGuardPoint = (int[])GuardPoint.Clone();
                TempMap = (string[,])Map.Clone();
                TempStoryMap = (string[,])StoryMap.Clone();
                TempDir = TurnRightB(dir);
                TempStoryMap[TempGuardPoint[0], TempGuardPoint[1]] += TraceToFind(TempDir);
                TempMap[NextPoint[0], NextPoint[1]] = "#";
                TempStoryMap[NextPoint[0], NextPoint[1]] = "#";
                TempGuardPresence = true;

                if (ShowLoop) 
                    ShowLoopMap();

                while (TempGuardPresence && !isLoop)
                {
                    TempIsSettedNextPoint = false;
                    TempIsTurned = false;

                    SetNextPointLoop();
                    if (ShowLoop)
                        ShowLoopMap();
                    if (!isLoop && TempIsSettedNextPoint)
                    {
                        MoveNextPointLoop();
                        if (ShowLoop)
                            ShowLoopMap();
                    }
                }
                if (isLoop)
                {
                    LoopObstacles++;
                    StoryMap[NextPoint[0], NextPoint[1]] += "O";
                    //ShowMap();
                    //ShowLoopMap();
                }
            }
        }
        public void ShowLoopMap()
        {
            Console.WriteLine("\n--------TEMP----------\n");

            for (int i = 0; i < n; i++)
            {
                Console.Write(i);
            }
            Console.WriteLine();
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (TempGuardPoint[0] == i && TempGuardPoint[1] == j) Console.ForegroundColor = ConsoleColor.Green;
                    else Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(TempMap[i, j]);

                }
                Console.Write(i);
                Console.WriteLine("");
            }
            Console.WriteLine($"TempGuardPoint: {TempGuardPoint[0]} {TempGuardPoint[1]}");
            Console.WriteLine($"TempNextPoint: {TempNextPoint[0]} {TempNextPoint[1]}");

        }
        public Direction TurnRightB(Direction TempDir)
        {
            return (Direction)(((int)TempDir + 1) % 4);
        }
        public bool SetNextPointLoop()
        {
            int test_r = TempGuardPoint[0];
            int test_c = TempGuardPoint[1];
            TempNextPoint[0] = test_r;
            TempNextPoint[1] = test_c;

            SetTestCoordinatesLoop(ref test_r, ref test_c);
            CheckGuardPresenceLoop(test_r, test_c);
            string toFind = TraceToFind(TempDir);

            if (TempGuardPresence)
            {
                //if (TempStoryMap[test_r, test_c].Contains(toFind))
                //{
                //    isLoop = true;
                //    return true;
                //}
                if (TempMap[test_r, test_c].Equals("#"))
                {
                    TempIsSettedNextPoint = false;
                    TempIsTurned = true;
                    TempDir = TurnRightB(TempDir);
                    TempStoryMap[TempGuardPoint[0], TempGuardPoint[1]] += TraceToFind(TempDir);
                    TempNextPoint[0] = TempGuardPoint[0];
                    TempNextPoint[1] = TempGuardPoint[1];
                }
                else
                {
                    switch (TempDir)
                    {
                        case Direction.Up:
                            TempNextPoint[0] -= 1;
                            break;
                        case Direction.Right:
                            TempNextPoint[1] += 1;
                            break;
                        case Direction.Down:
                            TempNextPoint[0] += 1;
                            break;
                        case Direction.Left:
                            TempNextPoint[1] -= 1;
                            break;
                    }
                    TempIsTurned = false;
                    TempIsSettedNextPoint = true;
                }
            }
            return isLoop;
        }
        public string TraceToFind(Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    return "^";
                case Direction.Right:
                    return ">";
                case Direction.Down:
                    return "V";
                case Direction.Left:
                    return "<";
            }
            return "";
        }
        public void SetTestCoordinatesLoop(ref int test_r, ref int test_c)
        {
            if (TempDir.Equals(Direction.Up))
            {
                test_r--;
            }
            else if (TempDir.Equals(Direction.Right))
            {
                test_c++;
            }
            else if (TempDir.Equals(Direction.Down))
            {
                test_r++;
            }
            else if (TempDir.Equals(Direction.Left))
            {
                test_c--;
            }
        }
        public bool CheckGuardPresenceLoop(int test_r, int test_c)
        {
            if (test_r >= n || test_r < 0 || test_c >= n || test_c < 0)
            {
                TempGuardPresence = false;
            }
            return true;
        }
        public void MoveNextPointLoop()
        {
            CheckGuardPresenceLoop(TempNextPoint[0], TempNextPoint[1]);

            if (TempGuardPresence)
            {
                if (TempStoryMap[TempNextPoint[0], TempNextPoint[1]].Contains(TraceToFind(TempDir)))
                {
                    isLoop = true;
                }
                else
                {
                    TempGuardPoint[0] = TempNextPoint[0];
                    TempGuardPoint[1] = TempNextPoint[1];
                    TempMap[TempGuardPoint[0], TempGuardPoint[1]] = "X";
                    TempStoryMap[TempGuardPoint[0], TempGuardPoint[1]] += TraceToFind(TempDir);
                }
            }
        }
        public int ScanLoopMap()
        {
            int CreatedLoops = 0;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (StoryMap[i, j].Contains("O")) CreatedLoops++;
                }
            }
            return CreatedLoops;
        }
    }
}