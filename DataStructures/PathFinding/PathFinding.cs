using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC.DataStructures.PathFinding
{
    using AOC.DataStructures.InnerSource;
    using System;
    using System.Collections.Generic;

    public class Pathfinding
    {
        #region BFS
        //BFS(Breadth-First Search)
        //L'algoritmo BFS esplora un grafo o una griglia in ampiezza. È utile per trovare il percorso più breve in un
        //grafo non pesato. Ecco un'implementazione generica di BFS in C# per una griglia.
        public static List<(int, int)> GetShortestPathBFS((int x, int y) start, (int x, int y) target, int[,] grid)
        {
            int rows = grid.GetLength(0);
            int cols = grid.GetLength(1);

            // Direzioni di movimento: su, giù, sinistra, destra
            int[] dx = { -1, 1, 0, 0 };
            int[] dy = { 0, 0, -1, 1 };

            // Coda per BFS
            Queue<(int x, int y, List<(int, int)> path)> queue = new Queue<(int, int, List<(int, int)>)>();
            queue.Enqueue((start.x, start.y, new List<(int, int)> { start }));

            // Set per tenere traccia delle posizioni visitate
            HashSet<(int, int)> visited = new HashSet<(int, int)>();
            visited.Add(start);

            while (queue.Count > 0)
            {
                var (x, y, path) = queue.Dequeue();

                // Se siamo arrivati al target, restituiamo il percorso
                if ((x, y) == target)
                {
                    return path;
                }

                // Prova tutte le direzioni
                for (int i = 0; i < 4; i++)
                {
                    int nx = x + dx[i];
                    int ny = y + dy[i];

                    // Controlla se è una posizione valida
                    if (nx >= 0 && ny >= 0 && nx < rows && ny < cols && grid[nx, ny] == 0 && !visited.Contains((nx, ny)))
                    {
                        var newPath = new List<(int, int)>(path) { (nx, ny) };
                        queue.Enqueue((nx, ny, newPath));
                        visited.Add((nx, ny));
                    }
                }
            }

            // Se non troviamo un percorso, restituiamo una lista vuota
            return new List<(int, int)>();
        }
        public static void ExampleBFS()
        {
            int[,] grid = new int[,] {
                { 0, 0, 0 },
                { 1, 1, 0 },
                { 0, 0, 0 }
            };
            var path = Pathfinding.GetShortestPathBFS((0, 0), (2, 2), grid);
            Console.WriteLine("Percorso trovato:");
            foreach (var step in path)
            {
                Console.WriteLine($"({step.Item1}, {step.Item2})");
            }
        }
        #endregion
        #region DFS
        //DFS(Depth-First Search)
        //L'algoritmo DFS esplora un grafo o una griglia in profondità. Può essere usato per trovare tutti i percorsi,
        //ma non garantisce di trovare il più breve. La versione ricorsiva è molto comune.

        public static List<(int, int)> GetPathDFS((int x, int y) start, (int x, int y) target, int[,] grid)
        {
            int rows = grid.GetLength(0);
            int cols = grid.GetLength(1);

            // Direzioni di movimento: su, giù, sinistra, destra
            int[] dx = { -1, 1, 0, 0 };
            int[] dy = { 0, 0, -1, 1 };

            List<(int, int)> path = new List<(int, int)>();
            HashSet<(int, int)> visited = new HashSet<(int, int)>();

            bool DFS((int x, int y) current)
            {
                // Se siamo al target, finiamo
                if (current == target)
                {
                    path.Add(current);
                    return true;
                }

                visited.Add(current);

                for (int i = 0; i < 4; i++)
                {
                    int nx = current.x + dx[i];
                    int ny = current.y + dy[i];

                    if (nx >= 0 && ny >= 0 && nx < rows && ny < cols && grid[nx, ny] == 0 && !visited.Contains((nx, ny)))
                    {
                        if (DFS((nx, ny)))
                        {
                            path.Add(current);
                            return true;
                        }
                    }
                }

                return false;
            }

            if (DFS(start))
            {
                path.Reverse();
                return path;
            }

            return new List<(int, int)>(); // Nessun percorso trovato
        }
        public static void  ExampleDFS()
        {
            int[,] grid = new int[,] {
            { 0, 0, 0 },
            { 1, 1, 0 },
            { 0, 0, 0 }
            };

            var path = GetPathDFS((0, 0), (2, 2), grid);
            Console.WriteLine("Percorso trovato:");
            foreach (var step in path)
            {
                Console.WriteLine($"({step.Item1}, {step.Item2})");
            }
        }
        #endregion
    }
}