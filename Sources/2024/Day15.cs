using AOC;
using AOC.DataStructures.Clustering;
using AOC.Model;
using AOC.SearchAlghoritmhs;
using AOC.Utilities.Drawing;
using AOC.Utilities.Dynamic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AOC2024
{
    [ResearchAlghoritms(ResearchAlghoritmsAttribute.TypologyEnum.Drawing)]
    [ResearchAlghoritms(ResearchAlghoritmsAttribute.TypologyEnum.Map)]
    [ResearchAlghoritms(ResearchAlghoritmsAttribute.TypologyEnum.Game)] //Sokoban
    public class Day15 : Solver, IDay
    {
        public static string[,] Map;
        public static int n;
        public static int[] Robot = new int[2];
        public void Part1(object input, bool test, ref object solution)
        {
            string inputText = (string)input;
            int nTest = 1;

            if (test)
                switch (nTest)
                {
                    case 0:
                        break;
                    case 1:
                        inputText = "##########\r\n#..O..O.O#\r\n#......O.#\r\n#.OO..O.O#\r\n#..O@..O.#\r\n#O#..O...#\r\n#O..O..O.#\r\n#.OO.O.OO#\r\n#....O...#\r\n##########\r\n\r\n<vv>^<v^>v>^vv^v>v<>v^v<v<^vv<<<^><<><>>v<vvv<>^v^>^<<<><<v<<<v^vv^v>^\r\nvvv<<^>^v^^><<>>><>^<<><^vv^^<>vvv<>><^^v>^>vv<>v<<<<v<^v>^<^^>>>^<v<v\r\n><>vv>v^v^<>><>>>><^^>vv>v<^^^>>v^v^<^^>v^^>v^<^v>v<>>v^v^<v>v^^<^^vv<\r\n<<v<^>>^^^^>>>v^<>vvv^><v<<<>^^^vv^<vvv>^>v<^^^^v<>^>vvvv><>>v^<<^^^^^\r\n^><^><>>><>^^<<^^v>>><^<v>^<vv>>v>>>^v><>^v><<<<v>>v<v<v>vvv>^<><<>^><\r\n^>><>^v<><^vvv<^^<><v<<<<<><^v<<<><<<^^<v<^^^><^>>^<v^><<<^>>^v<v^v<v^\r\n>^>>^v>vv>^<<^v<>><<><<v<<v><>v<^vv<<<>^^v^>^^>>><<^v>>v^v><^^>>^<>vv^\r\n<><^^>^^^<><vvvvv^v<v<<>^v<v>v<<^><<><<><<<^^<<<^<<>><<><^^^>^^<>^>v<>\r\n^^>vv<^v^v<vv>^<><v<^v>^^^>>>^^vvv^>vvv<>>>^<^>>>>>^<<^v>^vvv<>^<><<v>\r\nv^^>>><<^^<>>^v^<v^vv<>v^<<>^<^v^v><^<<<><<^<v><v<>vv>>v><v^<vv<>v^<<^";
                        break;
                }
            n = inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None)[0].Length;
            Map = new string[n, n];

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Map[i, j] = inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None)[i].Substring(j, 1).ToString();
                    if (Map[i, j] == "@")
                    {
                        Robot[0] = i;
                        Robot[1] = j;
                    }
                }
            }

            List<char> DirectionInstructions = new List<char>();
            for (int i = n + 1; i < inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None).Count(); i++)
            {
                for (int j = 0; j < inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None)[i].Length; j++)
                {
                    DirectionInstructions.Add(inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None)[i].Substring(j, 1).ToCharArray()[0]);
                }
            }

            ShowMap(Map);
            foreach (var d in DirectionInstructions)
            {
                Move(d);
                //ShowMap(Map,d);
            }
            solution = FindGPSCoordinates();
        }

        public int FindGPSCoordinates()
        {
            int ret = 0;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (Map[i, j] == "O") ret += (100 * i + j);
                }
            }
            return ret;
        }
        public void Move(char d)
        {
            // Clona la mappa attuale
            string[,] NewMap = (string[,])Map.Clone();

            // Verifica se c'è almeno uno spazio libero nella direzione specificata
            if (CheckFreeSpace(d))
            {
                int x = Robot[0];
                int y = Robot[1];

                // Scorri nella direzione specificata fino a trovare un muro o il bordo
                switch (d)
                {
                    case '<': // Sinistra
                        for (int j = y - 1; j >= 0; j--)
                        {
                            if (Map[x, j] == "#") break; // Muro
                            NewMap[x, j] = Map[x, j + 1];
                            if (Map[x, j] == ".") break;
                        }
                        break;

                    case '>': // Destra
                        for (int j = y + 1; j < n; j++)
                        {
                            if (Map[x, j] == "#") break; // Muro
                            NewMap[x, j] = Map[x, j - 1];
                            if (Map[x, j] == ".") break;
                        }
                        break;

                    case '^': // Su
                        for (int i = x - 1; i >= 0; i--)
                        {
                            if (Map[i, y] == "#") break; // Muro
                            NewMap[i, y] = Map[i + 1, y];
                            if (Map[i, y] == ".") break;
                        }
                        break;

                    case 'v': // Giù
                        for (int i = x + 1; i < n; i++)
                        {
                            if (Map[i, y] == "#") break; // Muro
                            NewMap[i, y] = Map[i - 1, y];
                            if (Map[i, y] == ".") break;
                        }
                        break;
                }
                NewMap[Robot[0], Robot[1]] = ".";
                SetRobotPosition(NewMap, ref Robot[0], ref Robot[1]);
                // Aggiorna la mappa
                Map = NewMap;
            }
        }
        public int SetRobotPosition(string[,] newmap, ref int Robot_x, ref int Robot_y)
        {
            int ret = 0;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (newmap[i, j] == "@")
                    {
                        Robot_x = i;
                        Robot_y = j;
                        return ret;
                    }
                }
            }
            return ret;
        }
        public bool CheckFreeSpace(char d)
        {
            int x = Robot[0];
            int y = Robot[1];

            // Scorri nella direzione specificata fino a trovare un muro o il bordo
            switch (d)
            {
                case '<': // Sinistra
                    for (int j = y - 1; j >= 0; j--)
                    {
                        if (Map[x, j] == "#") break; // Muro
                        if (Map[x, j] == ".") return true; // Spazio libero
                    }
                    break;

                case '>': // Destra
                    for (int j = y + 1; j < n; j++)
                    {
                        if (Map[x, j] == "#") break; // Muro
                        if (Map[x, j] == ".") return true; // Spazio libero
                    }
                    break;

                case '^': // Su
                    for (int i = x - 1; i >= 0; i--)
                    {
                        if (Map[i, y] == "#") break; // Muro
                        if (Map[i, y] == ".") return true; // Spazio libero
                    }
                    break;

                case 'v': // Giù
                    for (int i = x + 1; i < n; i++)
                    {
                        if (Map[i, y] == "#") break; // Muro
                        if (Map[i, y] == ".") return true; // Spazio libero
                    }
                    break;
            }

            return false; // Nessuno spazio libero trovato
        }
        public void ShowMap(string[,] map, char d = ' ')
        {
            Console.WriteLine($"------------------- d={d} -----------------------\n");
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    //Console.ForegroundColor = ConsoleColor.Green;
                    //Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(map[i, j]);
                }
                Console.WriteLine("");
            }
        }

        public static string[,] ExpandedMap;
        public static int nx;
        public static int ny;
        public void Part2(object input, bool test, ref object solution)
        {
            string inputText = (string)input;
            int nTest = 1;

            if (test)
                switch (nTest)
                {
                    case 0:
                        inputText = "#######\r\n#...#.#\r\n#.....#\r\n#..OO@#\r\n#..O..#\r\n#.....#\r\n#######\r\n\r\n<vv<<^^<<^^";
                        break;
                    case 1:
                        inputText = "##########\r\n#..O..O.O#\r\n#......O.#\r\n#.OO..O.O#\r\n#..O@..O.#\r\n#O#..O...#\r\n#O..O..O.#\r\n#.OO.O.OO#\r\n#....O...#\r\n##########\r\n\r\n<vv>^<v^>v>^vv^v>v<>v^v<v<^vv<<<^><<><>>v<vvv<>^v^>^<<<><<v<<<v^vv^v>^\r\nvvv<<^>^v^^><<>>><>^<<><^vv^^<>vvv<>><^^v>^>vv<>v<<<<v<^v>^<^^>>>^<v<v\r\n><>vv>v^v^<>><>>>><^^>vv>v<^^^>>v^v^<^^>v^^>v^<^v>v<>>v^v^<v>v^^<^^vv<\r\n<<v<^>>^^^^>>>v^<>vvv^><v<<<>^^^vv^<vvv>^>v<^^^^v<>^>vvvv><>>v^<<^^^^^\r\n^><^><>>><>^^<<^^v>>><^<v>^<vv>>v>>>^v><>^v><<<<v>>v<v<v>vvv>^<><<>^><\r\n^>><>^v<><^vvv<^^<><v<<<<<><^v<<<><<<^^<v<^^^><^>>^<v^><<<^>>^v<v^v<v^\r\n>^>>^v>vv>^<<^v<>><<><<v<<v><>v<^vv<<<>^^v^>^^>>><<^v>>v^v><^^>>^<>vv^\r\n<><^^>^^^<><vvvvv^v<v<<>^v<v>v<<^><<><<><<<^^<<<^<<>><<><^^^>^^<>^>v<>\r\n^^>vv<^v^v<vv>^<><v<^v>^^^>>>^^vvv^>vvv<>>>^<^>>>>>^<<^v>^vvv<>^<><<v>\r\nv^^>>><<^^<>>^v^<v^vv<>v^<<>^<^v^v><^<<<><<^<v><v<>vv>>v><v^<vv<>v^<<^";
                        break;
                }
            n = inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None)[0].Length;
            Map = new string[n, n];

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Map[i, j] = inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None)[i].Substring(j, 1).ToString();
                    if (Map[i, j] == "@")
                    {
                        Robot[0] = i;
                        Robot[1] = j;
                    }
                }
            }

            List<char> DirectionInstructions = new List<char>();
            for (int i = n + 1; i < inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None).Count(); i++)
            {
                for (int j = 0; j < inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None)[i].Length; j++)
                {
                    DirectionInstructions.Add(inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None)[i].Substring(j, 1).ToCharArray()[0]);
                }
            }

            ExpandedMap = ExpandMap(Map, n);
            ny = n * 2;
            nx = n;
            //ShowExpandedMap(ExpandedMap);
            SetRobotPositionExpanded(ExpandedMap, ref Robot[0], ref Robot[1]);
            int step = 0;
            var (pa, pc, m) = CountBoxes();
            Console.WriteLine($"Parentesi di apertura:{pa}, Parentesi di chiusura:{pc}, Muri:{m}");
            foreach (var d in DirectionInstructions)
            {
                if (step == 257)
                {

                }
                MoveExpanded(d);
                //ShowExpandedMap(ExpandedMap, d, step);
                //MatrixToImage.MatrixToJpeg(ExpandedMap, $"C:\\Users\\s.cecere\\source\\repos\\AOC\\AOC\\Test\\2024\\jpeg_day15\\{step}.jpeg");
                (pa, pc, m) = CountBoxes();
                Console.WriteLine($"Parentesi di apertura:{pa}, Parentesi di chiusura:{pc}, Muri:{m}");
                if (m < 804)
                {

                }
                step++;
            }
            ShowExpandedMap(ExpandedMap, 't', step);
            solution = FindGPSCoordinatesExpanded();
            //1448072 too high
        }
        public (int,int,int) CountBoxes()
        {
            int retOpenBoxes = 0;
            int retCloseBoxes = 0;
            int retWall = 0;
            for (int i = 0; i < nx; i++)
            {
                for (int j = 0; j < ny; j++)
                {
                    if (ExpandedMap[i, j] == "[") retOpenBoxes += 1;
                    if (ExpandedMap[i, j] == "]") retCloseBoxes += 1;
                    if (ExpandedMap[i, j] == "#") retWall += 1;
                }
            }
            return (retOpenBoxes,retCloseBoxes,retWall);
        }
        public int FindGPSCoordinatesExpanded()
        {
            int ret = 0;
            for (int i = 0; i < nx; i++)
            {
                for (int j = 0; j < ny; j++)
                {
                    if (ExpandedMap[i, j] == "[") ret += (100 * i + j);
                }
            }
            return ret;
        }

        public int SetRobotPositionExpanded(string[,] newmap, ref int Robot_x, ref int Robot_y)
        {
            int ret = 0;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < ny; j++)
                {
                    if (newmap[i, j] == "@")
                    {
                        Robot_x = i;
                        Robot_y = j;
                        return ret;
                    }
                }
            }
            return ret;
        }
        public bool CheckFreeSpaceExpanded(char d)
        {
            int x = Robot[0];
            int y = Robot[1];
            bool freeSpace = true;
            bool CheckFreeSpaceExpandedBoxesRecursive(int xr, int yr)
            {
                switch (d)
                {
                    case '^':
                        for (int i = xr - 1; i >= xr - 1; i--)
                        {
                            if (ExpandedMap[i, yr] == "#")
                            {
                                freeSpace = false;
                                return false;
                            }
                            if (ExpandedMap[i, yr] == "[")
                            {
                                freeSpace = SetFreeSpace(freeSpace, (CheckFreeSpaceExpandedBoxesRecursive(i, yr)));
                                freeSpace = SetFreeSpace(freeSpace, (CheckFreeSpaceExpandedBoxesRecursive(i, yr + 1)));
                            }
                            if (ExpandedMap[i, yr] == "]")
                            {
                                freeSpace = SetFreeSpace(freeSpace, (CheckFreeSpaceExpandedBoxesRecursive(i, yr - 1)));
                                freeSpace = SetFreeSpace(freeSpace, (CheckFreeSpaceExpandedBoxesRecursive(i, yr)));
                            }
                            if (ExpandedMap[i, yr] == ".") return true;
                        }
                        break;

                    case 'v':
                        for (int i = xr + 1; i <= xr + 1; i++)
                        {
                            if (ExpandedMap[i, yr] == "#")
                            {
                                freeSpace = false;
                                return false;
                            }
                            if (ExpandedMap[i, yr] == "[")
                            {
                                freeSpace = SetFreeSpace(freeSpace, (CheckFreeSpaceExpandedBoxesRecursive(i, yr)));
                                freeSpace = SetFreeSpace(freeSpace, (CheckFreeSpaceExpandedBoxesRecursive(i, yr + 1)));
                            }
                            if (ExpandedMap[i, yr] == "]")
                            {
                                freeSpace = SetFreeSpace(freeSpace, (CheckFreeSpaceExpandedBoxesRecursive(i, yr - 1)));
                                freeSpace = SetFreeSpace(freeSpace, (CheckFreeSpaceExpandedBoxesRecursive(i, yr)));
                            }
                            if (ExpandedMap[i, yr] == ".") return true;
                        }
                        break;
                }
                return freeSpace;
            }

            switch (d)
            {
                case '<':
                    for (int j = y - 1; j >= 0; j--)
                    {
                        if (ExpandedMap[x, j] == "#") return false;
                        if (ExpandedMap[x, j] == ".") return true;
                    }
                    break;

                case '>':
                    for (int j = y + 1; j < ny; j++)
                    {
                        if (ExpandedMap[x, j] == "#") return false;
                        if (ExpandedMap[x, j] == ".") return true;
                    }
                    break;

                case '^':
                    for (int i = x - 1; i >= 0; i--)
                    {
                        if (ExpandedMap[i, y] == "#") break;
                        if (ExpandedMap[i, y] == "[")
                        {
                            SetFreeSpace(freeSpace, (CheckFreeSpaceExpandedBoxesRecursive(i, y)));
                            SetFreeSpace(freeSpace, (CheckFreeSpaceExpandedBoxesRecursive(i, y + 1)));
                            if (!freeSpace) return false;
                        }
                        if (ExpandedMap[i, y] == "]")
                        {
                            SetFreeSpace(freeSpace, (CheckFreeSpaceExpandedBoxesRecursive(i, y - 1)));
                            SetFreeSpace(freeSpace, (CheckFreeSpaceExpandedBoxesRecursive(i, y)));
                            if (!freeSpace) return false;
                        }

                        if (ExpandedMap[i, y] == ".") return true;
                    }
                    break;

                case 'v':
                    for (int i = x + 1; i < n; i++)
                    {
                        if (ExpandedMap[i, y] == "#") break;
                        if (ExpandedMap[i, y] == "[")
                        {
                            SetFreeSpace(freeSpace, CheckFreeSpaceExpandedBoxesRecursive(i, y));
                            SetFreeSpace(freeSpace, (CheckFreeSpaceExpandedBoxesRecursive(i, y + 1)));
                            if (!freeSpace) return false;
                        }
                        if (ExpandedMap[i, y] == "]")
                        {
                            SetFreeSpace(freeSpace, (CheckFreeSpaceExpandedBoxesRecursive(i, y - 1)));
                            SetFreeSpace(freeSpace, (CheckFreeSpaceExpandedBoxesRecursive(i, y)));
                            if (!freeSpace) return false;
                        }

                        if (ExpandedMap[i, y] == ".") return true;
                    }
                    break;
            }

            return false;
        }
        public bool SetFreeSpace(bool freeSpaceOld, bool freeSpace)
        {
            if (freeSpaceOld && freeSpace)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void MoveExpanded(char d)
        {
            string[,] NewMap = (string[,])ExpandedMap.Clone();

            if (CheckFreeSpaceExpanded(d))
            {
                int x = Robot[0];
                int y = Robot[1];
                HashSet<string> TransportedBoxes = new HashSet<string>();
                Queue<string> BoxesToTransport = new Queue<string>();
                void MoveExpandedRecursive(int xr, int yr)
                {
                    while (BoxesToTransport.Count() != 0)
                    {
                        var StringBox = BoxesToTransport.Dequeue().ToString().Split(Delimiter.delimiter_comma, StringSplitOptions.None);
                        int[] boxe = new int[2] { int.Parse(StringBox[0]), int.Parse(StringBox[1]) };
                        MoveBox(d, boxe[0], boxe[1], ref NewMap, ref BoxesToTransport, ref TransportedBoxes);
                        TransportedBoxes.Add($"{boxe[0]},{boxe[1]}");
                    }
                }

                switch (d)
                {
                    case '<':
                        for (int j = y - 1; j >= 0; j--)
                        {
                            if (ExpandedMap[x, j] == "#") break;
                            NewMap[x, j] = ExpandedMap[x, j + 1];
                            if (ExpandedMap[x, j] == ".") break;
                        }
                        break;
                    case '>':
                        for (int j = y + 1; j < ny; j++)
                        {
                            if (ExpandedMap[x, j] == "#") break;
                            NewMap[x, j] = ExpandedMap[x, j - 1];
                            if (ExpandedMap[x, j] == ".") break;
                        }
                        break;
                    case '^':
                        for (int i = x - 1; i >= x - 1; i--)
                        {
                            if (ExpandedMap[i, y] == ".")
                            {
                                NewMap[i, y] = ExpandedMap[i + 1, y];
                            }
                            else if (ExpandedMap[i, y] == "[")
                            {
                                //Sposta da sotto verso sopra
                                NewMap[i, y] = ExpandedMap[i + 1, y];
                                NewMap[i, y + 1] = ".";

                                //Ricorsiva verso l'alto
                                BoxesToTransport.Enqueue($"{i},{y}");
                                MoveExpandedRecursive(i, y);
                            }
                            else if (ExpandedMap[i, y] == "]")
                            {
                                //Sposta da sotto verso sopra
                                NewMap[i, y - 1] = ".";
                                NewMap[i, y] = ExpandedMap[i + 1, y];

                                //Ricorsiva verso l'alto
                                BoxesToTransport.Enqueue($"{i},{y - 1}");
                                MoveExpandedRecursive(i, y - 1);
                            }
                            else if (ExpandedMap[i, y] == ".")
                            {
                                NewMap[i, y] = ExpandedMap[i + 1, y];
                            }
                            else
                            {
                                throw new Exception("Else non previsto");
                            }
                            break;
                        }
                        break;

                    case 'v':
                        for (int i = x + 1; i <= x + 1; i++)
                        {
                            if (ExpandedMap[i, y] == ".")
                            {
                                NewMap[i, y] = ExpandedMap[i - 1, y];
                            }
                            else if (ExpandedMap[i, y] == "[")
                            {
                                //Sposta da sopra verso sotto
                                NewMap[i, y] = ExpandedMap[i - 1, y];
                                NewMap[i, y + 1] = ".";

                                //Ricorsiva verso l'alto
                                BoxesToTransport.Enqueue($"{i},{y}");
                                MoveExpandedRecursive(i, y);
                            }
                            else if (ExpandedMap[i, y] == "]")
                            {
                                //Sposta da sotto verso sopra
                                NewMap[i, y - 1] = ".";
                                NewMap[i, y] = ExpandedMap[i - 1, y];

                                //Ricorsiva verso l'alto
                                BoxesToTransport.Enqueue($"{i},{y - 1}");
                                MoveExpandedRecursive(i, y - 1);
                            }
                            else if (ExpandedMap[i, y] == ".")
                            {
                                NewMap[i, y] = ExpandedMap[i - 1, y];
                            }
                            else
                            {
                                throw new Exception("Else non previsto");
                            }
                            break;
                        }
                        break;

                }
                NewMap[Robot[0], Robot[1]] = ".";
                SetRobotPositionExpanded(NewMap, ref Robot[0], ref Robot[1]);
                // Aggiorna la mappa
                ExpandedMap = NewMap;
            }
        }
        public void CheckBoxes(char d, int x, int y, ref Queue<string> BoxesToTransport, ref HashSet<string> TransportedBoxes)
        {
            switch (d)
            {
                case '^':
                    if (ExpandedMap[x - 1, y] == "[")
                    {
                        if (!BoxesToTransport.Contains($"{x - 1},{y}")) BoxesToTransport.Enqueue($"{x - 1},{y}");
                    }
                    else if (ExpandedMap[x - 1, y] == "]")
                    {
                        if (!BoxesToTransport.Contains($"{x - 1},{y - 1}")) BoxesToTransport.Enqueue($"{x - 1},{y - 1}");
                    }

                    if (ExpandedMap[x - 1, y + 1] == "[")
                    {
                        if (!BoxesToTransport.Contains($"{x - 1},{y + 1}")) BoxesToTransport.Enqueue($"{x - 1},{y + 1}");
                    }
                    break;
                case 'v':
                    if (ExpandedMap[x + 1, y] == "[")
                    {
                        if (!BoxesToTransport.Contains($"{x + 1},{y}")) BoxesToTransport.Enqueue($"{x + 1},{y}");
                    }
                    else if (ExpandedMap[x + 1, y] == "]")
                    {
                        if (!BoxesToTransport.Contains($"{x + 1},{y - 1}")) BoxesToTransport.Enqueue($"{x + 1},{y - 1}");
                    }

                    if (ExpandedMap[x + 1, y + 1] == "[")
                    {
                        if (!BoxesToTransport.Contains($"{x + 1},{y + 1}")) BoxesToTransport.Enqueue($"{x + 1},{y + 1}");
                    }

                    break;
            }
        }
        public void MoveBox(char d, int x, int y, ref string[,] NewMap, ref Queue<string> BoxToTransport, ref HashSet<string> TransportedBoxes)
        {
            CheckBoxes(d, x, y, ref BoxToTransport, ref TransportedBoxes);
            switch (d)
            {
                case '^':
                    NewMap[x - 1, y] = ExpandedMap[x, y];
                    NewMap[x - 1, y + 1] = ExpandedMap[x, y + 1];
                    if (NewMap[x - 1, y - 1] == "[") NewMap[x - 1, y - 1] = ".";
                    if (NewMap[x - 1, y + 2] == "]") NewMap[x - 1, y + 2] = ".";
                    break;
                case 'v':
                    NewMap[x + 1, y] = ExpandedMap[x, y];
                    NewMap[x + 1, y + 1] = ExpandedMap[x, y + 1];
                    if (NewMap[x + 1, y - 1] == "[") NewMap[x + 1, y - 1] = ".";
                    if (NewMap[x + 1, y + 2] == "]") NewMap[x + 1, y + 2] = ".";
                    break;
            }
        }
        public void ShowExpandedMap(string[,] map, char d = ' ', int step = 0)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"step:{step}------------------- d={d} -----------------------\n");
            for (int j = 0; j < ny; j++)
            {
                Console.Write(j);
            }
            Console.WriteLine();

            for (int i = 0; i < nx; i++)
            {
                for (int j = 0; j < ny; j++)
                {
                    if(map[i, j]=="@")Console.ForegroundColor = ConsoleColor.Blue;
                    if (map[i, j] == "[") Console.ForegroundColor = ConsoleColor.Green;
                    if (map[i, j] == "]") Console.ForegroundColor = ConsoleColor.Yellow;
                    if (map[i, j] == "#") Console.ForegroundColor = ConsoleColor.Magenta;
                    if (map[i, j] == ".") Console.ForegroundColor = ConsoleColor.White;

                    Console.Write(map[i, j]);
                }
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(i);
                Console.WriteLine("");
            }
        }
        public string[,] ExpandMap(string[,] originalMap, int originalSize)
        {
            int newSize = originalSize;
            var expandedMap = new string[newSize, originalSize * 2];

            for (int i = 0; i < originalSize; i++)
            {
                for (int j = 0; j < originalSize; j++)
                {
                    string tile = originalMap[i, j];
                    switch (tile)
                    {
                        case "#":
                            expandedMap[i, j * 2] = "#";
                            expandedMap[i, j * 2 + 1] = "#";
                            break;
                        case "O":
                            expandedMap[i, j * 2] = "[";
                            expandedMap[i, j * 2 + 1] = "]";
                            break;
                        case ".":
                            expandedMap[i, j * 2] = ".";
                            expandedMap[i, j * 2 + 1] = ".";
                            break;
                        case "@":
                            expandedMap[i, j * 2] = "@";
                            expandedMap[i, j * 2 + 1] = ".";
                            break;
                    }
                }
            }
            return expandedMap;
        }
    }
}
