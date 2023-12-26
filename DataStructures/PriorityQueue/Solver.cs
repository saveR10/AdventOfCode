using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC.DataStructures.PriorityQueue
{

        public class Solver
        {
            public int moves;
            public int totalMoves = 0;
            public bool isSolvable;
            public Board Solution;

            // Find a solution to the initial board (using the A* algorithm)
            public Solver(Board initial)
            {
                MinPQBoard<Board> previousSearchNodesPQ = new MinPQBoard<Board>();
                MinPQBoard<Board> searchNodesPQ = new MinPQBoard<Board>();
                searchNodesPQ.Insert(initial);
                Board toEvaluate = new Board();
                bool debug = false;
                while (!searchNodesPQ.IsEmpty())
                {
                    toEvaluate = searchNodesPQ.Min();
               

                    if (toEvaluate.IsGoal())
                    {
                        Console.WriteLine("Soluzione trovata");
                        isSolvable = true;
                        Solution = toEvaluate;
                        this.moves = toEvaluate.moves;
                        return;
                    }
                    foreach (Board board in toEvaluate.neighbors)
                    {
                        Board NewBoard = new Board(board.tiles, true, board.moves);
                        if ((!searchNodesPQ.Contain(NewBoard)) && (!previousSearchNodesPQ.Contain(NewBoard)))
                        {
                            NewBoard.parent = toEvaluate;
                            searchNodesPQ.Insert(NewBoard);
                        }
                        else // If arriving at this old configuration with fewer moves, replace the old node with the new one
                        {
                            Board OldBoard = searchNodesPQ.Select(NewBoard);
                            if (OldBoard.priorityFunction != 0)
                            {
                                if (NewBoard.moves < OldBoard.moves)
                                {
                                    NewBoard.parent = toEvaluate;
                                    searchNodesPQ.Remove(OldBoard);
                                    searchNodesPQ.Insert(NewBoard);
                                }
                            }
                            else
                            {
                                OldBoard = previousSearchNodesPQ.Select(NewBoard);
                                if (NewBoard.moves < OldBoard.moves)
                                {
                                    NewBoard.parent = toEvaluate;
                                    previousSearchNodesPQ.Remove(OldBoard);
                                    searchNodesPQ.Insert(NewBoard);
                                }
                            }
                        }
                    }
                    searchNodesPQ.Remove(toEvaluate);
                    if (!previousSearchNodesPQ.Contain((toEvaluate))) { previousSearchNodesPQ.Insert(toEvaluate); }
                }
            }

            // Sequence of boards in the shortest solution; null if unsolvable
            public IEnumerable<Board> Sol()
            {
                List<Board> sol = new List<Board>();
                for (int i = 0; i < this.moves; i++)
                {
                    Board board = new Board(Solution.tiles, false, this.totalMoves);
                    board.tiles = Solution.tiles;
                    sol.Insert(0, board);
                    this.Solution = Nested(Solution);
                }
                return sol;
            }

            public Board Nested(Board board)
            {
                return board.parent;
            }

            // Is the initial board solvable?
            public bool IsSolvable()
            {
                return isSolvable;
            }

            // Min number of moves to solve initial board; -1 if unsolvable
            public int Moves()
            {
                return isSolvable ? moves : -1;
            }

            // Test client (see below)
            public static void Example()
            {
            }
        }
    }
