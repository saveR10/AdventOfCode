using AOC;
using AOC.Model;
using AOC.Utilities.Math;
using Microsoft.VisualBasic;
 
 
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net.Configuration;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AOC2023
{
    public class Day22 : Solver, IDay
    {
        public int[,,] Space3D;
        public Dictionary<int, Brick> Bricks;
        public int n;
        public void Part1(object input, bool test, ref object solution)
        {
            List<string> inputList = new List<string>();
            inputList = (List<string>)input;

            n = 500;
            Space3D = new int[10, 10, n];

            Bricks = new Dictionary<int, Brick>();
            int nb = 0;
            foreach (var b in inputList)
            {
                nb++;
                Bricks.Add(nb, new Brick(b, nb, ref Space3D));
            }

            //ShowLayer();

            int FreeBlock = 1;
            while (FreeBlock != 0)
            {
                Console.WriteLine("ApplyGravity");
                Console.WriteLine($"FreeBlock:{FreeBlock}");
                FreeBlock = 0;
                for (int i = 1; i <= nb; i++)
                {
                    FreeBlock += ApplyGravity(i);
                }
                //ShowLayer();
            }

            int Disintegrable = 0;
            //SupportTo
            foreach (var b in Bricks)
            {
                if (b.Value.ExtendedDimension == 2)
                {
                    if (!Space3D[b.Value.BodyBrick[0][0], b.Value.BodyBrick[0][1], b.Value.minZ + b.Value.BrickLength].Equals(0))
                    {
                        if (!b.Value.SupportTo.Contains(Space3D[b.Value.BodyBrick[0][0], b.Value.BodyBrick[0][1], b.Value.minZ + b.Value.BrickLength]))
                        {
                            b.Value.SupportTo.Add(Space3D[b.Value.BodyBrick[0][0], b.Value.BodyBrick[0][1], b.Value.minZ + b.Value.BrickLength]);
                        }
                    }

                }
                else
                {
                    for (int l = 0; l < b.Value.BrickLength; l++)
                    {
                        if (!Space3D[b.Value.BodyBrick[l][0], b.Value.BodyBrick[l][1], b.Value.BodyBrick[l][2] + 1].Equals(0))
                        {
                            if (!b.Value.SupportTo.Contains(Space3D[b.Value.BodyBrick[l][0], b.Value.BodyBrick[l][1], b.Value.BodyBrick[l][2] + 1]))
                            {
                                b.Value.SupportTo.Add(Space3D[b.Value.BodyBrick[l][0], b.Value.BodyBrick[l][1], b.Value.BodyBrick[l][2] + 1]);
                            }
                        }
                    }
                }
            }

            //Support From
            foreach (var b in Bricks)
            {
                if (b.Value.SupportTo.Count != 0)
                {
                    for (int i = 0; i < b.Value.SupportTo.Count; i++)
                    {
                        Bricks[b.Value.SupportTo[i]].SupportFrom.Add(b.Value.name_brick);
                    }
                }
            }

            bool f_Disintegrable = false;
            foreach (var b in Bricks)
            {
                f_Disintegrable = true;
                if (b.Value.SupportTo.Count() == 0)
                {
                    Disintegrable++;
                }
                else
                {
                    foreach (var bb in b.Value.SupportTo)
                    {

                        if (Bricks[bb].SupportFrom.Count() <= 1)
                        {
                            f_Disintegrable = false;
                            break;
                        }
                    }
                    if (f_Disintegrable) Disintegrable++;
                }

            }

            solution = Disintegrable;
        }

        public void ShowLayer()
        {
            Console.WriteLine();
            Console.WriteLine($"New Show Layer #############################################################################");
            for (int lz = 0; lz < n; lz++)
            {
                Console.WriteLine($"------------------------------Layer {lz}-----------------------------------------------");
                for (int ly = 0; ly < n; ly++)
                {
                    for (int lx = 0; lx < n; lx++)
                    {
                        Console.Write(Space3D[lx, ly, lz]);
                    }
                    Console.WriteLine();
                }
            }
        }
        public int ApplyGravity(int BrickNumber)
        {
            int direction = Bricks[BrickNumber].ExtendedDimension;
            int l = Bricks[BrickNumber].BrickLength;
            var bb = Bricks[BrickNumber].BodyBrick;
            bool freeSpace = true;
            for (int i = 0; i < l; i++)
            {
                if (direction == 3)
                {
                    if (bb[i][2] > 0)
                    {
                        if (!Space3D[bb[i][0], bb[i][1], bb[i][2] - 1].Equals(0))
                        {
                            freeSpace = false; break;
                        }
                    }
                    else
                    {
                        freeSpace = false; break;
                    }
                }
                else if (direction == 2)
                {
                    if (Bricks[BrickNumber].minZ > 0)
                    {
                        if (!Space3D[bb[i][0], bb[i][1], Bricks[BrickNumber].minZ - 1].Equals(0))
                        {
                            freeSpace = false; break;
                        }
                    }
                    else
                    {
                        freeSpace = false; break;
                    }
                    break;
                }
                else
                {
                    if (bb[i][2] > 0)
                    {
                        if (!Space3D[bb[i][0], bb[i][1], bb[i][2] - 1].Equals(0))
                        {
                            freeSpace = false; break;
                        }
                    }
                    else
                    {
                        freeSpace = false; break;
                    }
                }
            }

            if (freeSpace)
            {
                if (direction == 3)
                {
                    Space3D[Bricks[BrickNumber].BodyBrick[0][0], Bricks[BrickNumber].BodyBrick[0][1], Bricks[BrickNumber].BodyBrick[0][2]] = 0;
                    Space3D[Bricks[BrickNumber].BodyBrick[0][0], Bricks[BrickNumber].BodyBrick[0][1], Bricks[BrickNumber].BodyBrick[0][2] - 1] = BrickNumber;
                    Bricks[BrickNumber].BodyBrick[0][2] -= 1;
                    return 1;
                }
                else if (direction == 2)
                {
                    Space3D[Bricks[BrickNumber].BodyBrick[0][0], Bricks[BrickNumber].BodyBrick[0][1], Bricks[BrickNumber].minZ + Bricks[BrickNumber].BrickLength - 1] = 0;
                    Space3D[Bricks[BrickNumber].BodyBrick[0][0], Bricks[BrickNumber].BodyBrick[0][1], Bricks[BrickNumber].minZ - 1] = BrickNumber;
                    Bricks[BrickNumber].minZ -= 1;
                    for (int len = 0; len < l; len++)
                    {
                        Bricks[BrickNumber].BodyBrick[len][2] -= 1;
                    }
                    return 1;
                }
                else
                {
                    for (int len = 0; len < l; len++)
                    {
                        Space3D[Bricks[BrickNumber].BodyBrick[len][0], Bricks[BrickNumber].BodyBrick[len][1], Bricks[BrickNumber].BodyBrick[len][2]] = 0;
                        Bricks[BrickNumber].BodyBrick[len][2] -= 1;
                        Space3D[Bricks[BrickNumber].BodyBrick[len][0], Bricks[BrickNumber].BodyBrick[len][1], Bricks[BrickNumber].BodyBrick[len][2]] = BrickNumber;
                    }
                    return 1;
                }
            }
            return 0;
        }

        public class Brick
        {
            public Brick(string b, int nb, ref int[,,] Space3D)
            {
                this.name_brick = nb;
                this.Space3D = Space3D;
                var s = b.Split(Delimiter.delimiter_tilde, StringSplitOptions.None)[0].Split(Delimiter.delimiter_comma, StringSplitOptions.None);
                start[0] = int.Parse(s[0]);
                start[1] = int.Parse(s[1]);
                start[2] = int.Parse(s[2]);
                var e = b.Split(Delimiter.delimiter_tilde, StringSplitOptions.None)[1].Split(Delimiter.delimiter_comma, StringSplitOptions.None);
                end[0] = int.Parse(e[0]);
                end[1] = int.Parse(e[1]);
                end[2] = int.Parse(e[2]);

                for (int d = 0; d < 3; d++)
                {
                    if (start[d] != end[d]) RunningBrick(start, end, d);
                }
                if (this.BodyBrick == null)
                {
                    RunningSingleBrick(start);
                }
            }
            public int[,,] Space3D;
            public int name_brick;
            public int[] start = new int[3];
            public int[] end = new int[3];
            public int BrickLength;
            public List<int[]> BodyBrick;
            public int ExtendedDimension; //x=0,y=1,z=2
            public int minZ = 0;
            public bool f_disintegrable = false;
            public List<int> DependentlyBrick;
            public List<Brick> TempSupportTo = new List<Brick>();
            public List<Brick> TempSupportFrom = new List<Brick>();
            public int tempCount = 0;
            public List<int> SupportFrom = new List<int>();
            public List<int> SupportTo = new List<int>();
            public void RunningBrick(int[] start, int[] end, int d)
            {
                ExtendedDimension = d;
                BrickLength = Math.Max(start[d], end[d]) - Math.Min(start[d], end[d]) + 1;
                if (BrickLength == 0)
                {
                    var proo = "";
                }
                if (d == 2) { minZ = Math.Min(start[d], end[d]); }
                BodyBrick = new List<int[]>();
                bool runned = false;
                if (d == 0)
                {
                    int x = Math.Min(start[d], end[d]);
                    int y = start[1];
                    int z = start[2];
                    while (!runned)
                    {
                        Space3D[x, y, z] = name_brick;
                        if (x >= (Math.Max(start[d], end[d]))) runned = true;
                        BodyBrick.Add(new int[3] { x, y, z });
                        x++;
                    }
                }
                if (d == 1)
                {
                    int x = start[0];
                    int y = Math.Min(start[d], end[d]);
                    int z = start[2];
                    while (!runned)
                    {
                        Space3D[x, y, z] = name_brick;
                        if (y >= (Math.Max(start[d], end[d]))) runned = true;
                        BodyBrick.Add(new int[3] { x, y, z });
                        y++;
                    }
                }
                if (d == 2)
                {
                    int x = start[0];
                    int y = start[1];
                    int z = Math.Min(start[d], end[d]);
                    while (!runned)
                    {
                        Space3D[x, y, z] = name_brick;
                        if (z >= (Math.Max(start[d], end[d]))) runned = true;
                        BodyBrick.Add(new int[3] { x, y, z });
                        z++;
                    }
                }
            }
            public void RunningSingleBrick(int[] start)
            {
                ExtendedDimension = 3;
                BrickLength = 1;
                BodyBrick = new List<int[]>();
                int x = start[0];
                int y = start[1];
                int z = start[2];
                Space3D[x, y, z] = name_brick;
                BodyBrick.Add(new int[3] { x, y, z });
            }
        }

        public void Part2(object input, bool test, ref object solution)
        {
            List<string> inputList = new List<string>();
            inputList = (List<string>)input;

            n = 500;
            Space3D = new int[10, 10, n];

            Bricks = new Dictionary<int, Brick>();
            int nb = 0;
            foreach (var b in inputList)
            {
                nb++;
                Bricks.Add(nb, new Brick(b, nb, ref Space3D));
            }

            //ShowLayer();

            int FreeBlock = 1;
            while (FreeBlock != 0)
            {
                Console.WriteLine("ApplyGravity");
                Console.WriteLine($"FreeBlock:{FreeBlock}");
                FreeBlock = 0;
                for (int i = 1; i <= nb; i++)
                {
                    FreeBlock += ApplyGravity(i);
                }
                //ShowLayer();
            }

            int Disintegrable = 0;
            //SupportTo
            foreach (var b in Bricks)
            {
                if (b.Value.ExtendedDimension == 2)
                {
                    if (!Space3D[b.Value.BodyBrick[0][0], b.Value.BodyBrick[0][1], b.Value.minZ + b.Value.BrickLength].Equals(0))
                    {
                        if (!b.Value.SupportTo.Contains(Space3D[b.Value.BodyBrick[0][0], b.Value.BodyBrick[0][1], b.Value.minZ + b.Value.BrickLength]))
                        {
                            b.Value.SupportTo.Add(Space3D[b.Value.BodyBrick[0][0], b.Value.BodyBrick[0][1], b.Value.minZ + b.Value.BrickLength]);
                            //b.Value.TempSupportTo.Add(Space3D[b.Value.BodyBrick[0][0], b.Value.BodyBrick[0][1], b.Value.minZ + b.Value.BrickLength], -1);
                            b.Value.TempSupportTo.Add(Bricks[Space3D[b.Value.BodyBrick[0][0], b.Value.BodyBrick[0][1], b.Value.minZ + b.Value.BrickLength]]);
                        }
                    }

                }
                else
                {
                    for (int l = 0; l < b.Value.BrickLength; l++)
                    {
                        if (!Space3D[b.Value.BodyBrick[l][0], b.Value.BodyBrick[l][1], b.Value.BodyBrick[l][2] + 1].Equals(0))
                        {
                            if (!b.Value.SupportTo.Contains(Space3D[b.Value.BodyBrick[l][0], b.Value.BodyBrick[l][1], b.Value.BodyBrick[l][2] + 1]))
                            {
                                b.Value.SupportTo.Add(Space3D[b.Value.BodyBrick[l][0], b.Value.BodyBrick[l][1], b.Value.BodyBrick[l][2] + 1]);
                                //b.Value.TempSupportTo.Add(Space3D[b.Value.BodyBrick[l][0], b.Value.BodyBrick[l][1], b.Value.BodyBrick[l][2] + 1], -1);
                                b.Value.TempSupportTo.Add(Bricks[Space3D[b.Value.BodyBrick[l][0], b.Value.BodyBrick[l][1], b.Value.BodyBrick[l][2] + 1]]);
                            }
                        }
                    }
                }
            }

            //Support From
            foreach (var b in Bricks)
            {
                if (b.Value.SupportTo.Count != 0)
                {
                    for (int i = 0; i < b.Value.SupportTo.Count; i++)
                    {
                        if (!Bricks[b.Value.SupportTo[i]].SupportFrom.Contains(b.Value.name_brick))
                        {
                            Bricks[b.Value.SupportTo[i]].SupportFrom.Add(b.Value.name_brick);
                            Bricks[b.Value.SupportTo[i]].TempSupportFrom.Add(Bricks[b.Value.name_brick]);
                        }
                        else
                        {

                        }
                    }
                }
            }


            bool f_Disintegrable = false;
            foreach (var b in Bricks)
            {
                f_Disintegrable = true;
                if (b.Value.SupportTo.Count() == 0)
                {
                    Disintegrable++;
                    b.Value.f_disintegrable = true;
                }
                else
                {
                    foreach (var bb in b.Value.SupportTo)
                    {
                        if (Bricks[bb].SupportFrom.Count() <= 1)
                        {
                            f_Disintegrable = false;
                            break;
                        }
                    }
                    if (f_Disintegrable)
                    {
                        Disintegrable++;
                        b.Value.f_disintegrable = true;
                    }
                }
            }
            int n_bricks_non_disintegrable = 0;
            int conta = 0;
            foreach (var b in Bricks)
            {
                if (b.Value.f_disintegrable == false)
                {
                    Dictionary<int, Brick> DependableBricks = new Dictionary<int, Brick>();
                    Dictionary<int, Brick> BelowBricks = new Dictionary<int, Brick>();
                    BelowBricks.Add(b.Value.name_brick, b.Value);
                    ClimbTower(b.Value.name_brick, ref DependableBricks, ref BelowBricks);
                    conta += DependableBricks.Count();
                    n_bricks_non_disintegrable++;
                }
            }


            solution = conta;
            //98703 too high
            //98281 too high
            //97943 too high
            //97942 too high
        }

        public void ClimbTower(int nameBrick, ref Dictionary<int, Brick> DependableBricks, ref Dictionary<int, Brick> BelowBricks)
        {
            foreach (var b in Bricks[nameBrick].TempSupportTo)
            {
                if (!DependableBricks.ContainsKey(b.name_brick))
                {
                    bool dependent = true;
                    foreach (var sf in b.TempSupportFrom)
                    {
                        if (BelowBricks.ContainsKey(sf.name_brick))
                        {
                        }
                        else
                        {
                            dependent = false;
                        }
                    }
                    if (dependent) if (!DependableBricks.ContainsKey(b.name_brick))
                        {
                            DependableBricks.Add(b.name_brick, b);
                            BelowBricks.Add(b.name_brick, b);
                        }
                }
            }
            
            foreach (var b in Bricks[nameBrick].TempSupportTo)
            {
                ClimbTower(b.name_brick, ref DependableBricks, ref BelowBricks);
            }




        }
    }
}
