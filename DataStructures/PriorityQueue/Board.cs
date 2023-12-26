using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
namespace AOC.DataStructures.PriorityQueue
{
    public class Board
    {
        #region Priority Function
        public double priorityFunction;
        public int SetPriorityFunction(int moves, int heuristic){return moves + heuristic;}
        public int hammingMeasure;
        public double manhattanMeasure;
        public double heuristic;
        public int moves;
        #endregion

        public int n;// Board dimension n
        public int[,] tiles;        // Tiles of the board
        public Board parent;        // Parent board
        public List<Board> neighbors;        // Neighbors' board
        
        // Create a board from an n-by-n array of tiles, where tiles[row][col] = tile at (row, col)
        public Board(int[,] tiles, bool likeParent, int moves)
        {
            this.n = tiles.GetLength(0);
            this.tiles = tiles;
            this.hammingMeasure = hamming();
            this.manhattanMeasure = manhattan();
            //this.heuristic = (double)(n * n * n * manhattanMeasure);
            //this.heuristic = (double)(n * manhattanMeasure);
            this.heuristic = (double)(n * hammingMeasure);
            this.moves = moves;
            this.priorityFunction = SetPriorityFunction(moves, (int)this.heuristic);
            if (likeParent) { this.neighbors = new List<Board>(Neighbors()); }
        }

        //To initialize
        public Board()
        {
        }

        // string representation of this board
        public StringBuilder toRepresentation()
        {
            StringBuilder result = new StringBuilder();
            result.Append("\r\n");
            result.Append(this.n);
            result.Append("\r\n");
            //System.out.print(this.n);
            for (int r = 0; r < n; r++)
            {
                //System.out.println();
                for (int c = 0; c < n; c++)
                {
                    //System.out.print(" "+tiles[r][c]);
                    result.Append((" " + this.tiles[r,c]));
                }
                result.Append("\r\n");
            }
            return result;
        }
        public void toRepresentation(bool debug)
        {
            for (int r = 0; r < n; r++)
            {
                Console.WriteLine();
                for (int c = 0; c < n; c++)
                {
                    Console.Write(" " + tiles[r,c]);
                }
            }
            Console.WriteLine("      " + priorityFunction + " = " + moves + " + " + heuristic);
            Console.Write("\r\n");
        }

        #region Priority Function
        public int hamming()
        {
            int hammingmeasure = 0;
            int desired = 0;
            for (int r = 0; r < n; r++)
            {
                for (int c = 0; c < n; c++)
                {
                    desired += 1;
                    if (tiles[r,c] != desired)
                    {
                        hammingmeasure += 1;
                    }
                }
            }
            if (hammingmeasure == n * n) { hammingmeasure--; }
            return hammingmeasure;
        }

        // sum of Manhattan distances between tiles and goal
        public double manhattan()
        {
            double manhattanmeasure = 0;
            for (int r = 0; r < n; r++)
            {
                for (int c = 0; c < n; c++)
                {
                    if (tiles[r,c] != 0)
                    {
                        manhattanmeasure += Math.Abs((r) - (Math.Ceiling((double)tiles[r,c] / (double)n) - 1)) + Math.Abs(c - ((tiles[r,c] - 1) % n));
                    }
                }
            }
            return manhattanmeasure;
        }
        #endregion
        public StringBuilder ToRepresentation(ref int count)
        {
            StringBuilder result = new StringBuilder();
            result.Append("\r\n");
            result.Append(this.n);
            result.Append("\t");
            result.Append(count);
            count+=1;
            result.Append("\r\n");
            for (int r = 0; r < n; r++)
            {
                for (int c = 0; c < n; c++)
                {
                    result.Append(" " + tiles[r, c]);
                }
                result.Append(Environment.NewLine);
            }
            return result;
        }
        public void ToRepresentation(bool debug)
        {
            for (int r = 0; r < n; r++)
            {
                Console.WriteLine();
                for (int c = 0; c < n; c++)
                {
                    Console.Write(" " + tiles[r, c]);
                }
            }
            Console.Write("      " + priorityFunction + " = " + moves + " + " + heuristic);
            Console.Write("\r\n");
        }

        
        // Is this board the goal board?
        public bool IsGoal()
        {
            return this.manhattanMeasure == 0;
        }

        // All neighboring boards
        public IEnumerable<Board> Neighbors()
        {
            List<Board> neighborsList = new List<Board>();
            int[,] tNext = new int[n, n];
            for (int r = 0; r < n; r++)
            {
                for (int c = 0; c < n; c++)
                {
                    if (tiles[r, c] == 0)
                    {
                        if (c > 0) // Board switching blank left
                        {
                            tNext = Twin(tiles);
                            tNext[r, c - 1] = 0;
                            tNext[r, c] = tiles[r, c - 1];
                            Board bNext = new Board(tNext, false, this.moves + 1);
                            neighborsList.Add(bNext);
                        }
                        if (r > 0) // Board switching blank above
                        {
                            tNext = Twin(tiles);
                            tNext[r - 1, c] = 0;
                            tNext[r, c] = tiles[r - 1, c];
                            Board bNext = new Board(tNext, false, this.moves + 1);
                            neighborsList.Add(bNext);
                        }
                        if (c < n - 1) // Board switching blank right
                        {
                            tNext = Twin(tiles);
                            tNext[r, c + 1] = 0;
                            tNext[r, c] = tiles[r, c + 1];
                            Board bNext = new Board(tNext, false, this.moves + 1);
                            neighborsList.Add(bNext);
                        }
                        if (r < n - 1) // Board switching blank below
                        {
                            tNext = Twin(tiles);
                            tNext[r + 1, c] = 0;
                            tNext[r, c] = tiles[r + 1, c];
                            Board bNext = new Board(tNext, false, this.moves + 1);
                            neighborsList.Add(bNext);
                        }
                    }
                    if (neighborsList.Count != 0) { return neighborsList; }
                }
            }
            return neighborsList;
        }

        // A board obtained by copying tiles
        public int[,] Twin(int[,] tNext)
        {
            int[,] bNext = new int[n, n];
            for (int r = 0; r < n; r++)
            {
                for (int c = 0; c < n; c++)
                {
                    bNext[r, c] = tiles[r, c];
                }
            }
            return bNext;
        }

        // Generate tiles
        public static int[,] GenerateTiles(int n)
        {
            int[,] mytiles = new int[n, n];
            List<int> controlList = new List<int>();
            Random random = new Random();
            int rand;
            for (int r = 0; r < n; r++)
            {
                for (int c = 0; c < n; c++)
                {
                    do { rand = random.Next(n * n); }
                    while (controlList.Contains(rand));
                    mytiles[r, c] = rand;
                    controlList.Add(rand);
                }
            }
            return mytiles;
        }

        // Unit testing (not graded)
        public static void Example()
        {
            long startTime = DateTime.Now.Ticks;
            string line = "3"; //"5"
            int n = int.Parse(line);
            string exercise = "Board";
            string outputPath = $"../../DataStructures/IO/Output/{exercise}_{n}.txt";


            //int[,] tiles = GenerateTiles(n);
            int[,] tiles = new int[n, n];
            tiles[0,0] = 4;
            tiles[0,1] = 5;
            tiles[0,2] = 6;
            tiles[1,0] = 1;
            tiles[1,1] = 2;
            tiles[1,2] = 3;
            tiles[2,0] = 7;
            tiles[2,1] = 8;
            tiles[2,0] = 9;
            Board initial = new Board(tiles, true, 0);
            long endTime=0;
            long timeElapsed=0;

            try
            {
                // Solve the puzzle (Solver class needs to be implemented)
                Solver solver = new Solver(initial);

                StringBuilder result = new StringBuilder();
                endTime = DateTime.Now.Ticks;
                timeElapsed = endTime - startTime;

                using (StreamWriter writer = new StreamWriter(outputPath))
                {
                    writer.WriteLine($"Questo è il risultato dell'esercizio {exercise} con quadrato di ordine {n} :");

                    if (!solver.IsSolvable())
                    {
                        Console.WriteLine("No solution possible");
                        writer.WriteLine("No solution possible");
                    }
                    else
                    {
                        int count = 1;
                        Console.WriteLine("Minimum number of moves = " + solver.Moves());
                        writer.WriteLine("Minimum number of moves = " + solver.Moves());
                        foreach (Board board in solver.Sol())
                            result.Append(board.ToRepresentation(ref count));
                    }
                    Console.WriteLine(result);
                    writer.Write(result);
                    writer.WriteLine($"Execution time in milliseconds: {timeElapsed / TimeSpan.TicksPerMillisecond}");
                    writer.WriteLine($"Execution time in seconds: {timeElapsed / TimeSpan.TicksPerSecond}");                   
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            Console.WriteLine("Execution time in nanoseconds: " + timeElapsed);
            Console.WriteLine("Execution time in milliseconds: " + timeElapsed / 10000);
            Console.WriteLine("Execution time in seconds: " + timeElapsed / 10000000);
        }
    }
}
