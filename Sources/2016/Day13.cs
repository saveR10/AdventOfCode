using AOC;
using AOC.Model;
using AOC.SearchAlghoritmhs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC2016
{
    [ResearchAlgorithms(title: "A Maze of Twisty Little Cubicles",
                        typology: ResearchAlgorithmsAttribute.TypologyEnum.Map,
                        resolution: ResearchAlgorithmsAttribute.ResolutionEnum.AlphaStar,
                        difficult: ResearchAlgorithmsAttribute.DifficultEnum.Hard,
                        note: "Calcolo di percorso minimo in una griglia infinita con muri determinati da una formula. BFS per trovare distanza minima e tutte le posizioni raggiungibili entro un numero massimo di passi.")]
    public class Day13 : Solver, IDay
    {
        public static int[] startPosition = new int[2];
        public static int[] targetPosition = new int[2];
        public static int n = 0;
        public static int[,] exploredStates; //contiene gli step per arrivare in quella posizione; default -1.
        public static int minStep = int.MaxValue;
        public static char[,] Map;

        public void Part1(object input, bool test, ref object solution)
        {
            string inputString = (string)input;
            inputString = inputString.Split(Delimiter.delimiter_line, StringSplitOptions.None)[0];
            if (test)
            {
                startPosition[0] = 1; startPosition[1] = 1;
                targetPosition[0] = 4; targetPosition[1] = 7;
            }
            else
            {
                startPosition[0] = 1; startPosition[1] = 1;
                targetPosition[0] = 39; targetPosition[1] = 31;
            }
            n = Math.Max(targetPosition[0], targetPosition[1]) + 10;
            exploredStates = new int[n, n];

            CreateMap(int.Parse(inputString));
            AStar();

        }
        public void AStar()
        {
            // Inizializza la coda di priorità
            var priorityQueue = new MinPQState();

            // Calcola l'euristica iniziale
            int initialHeuristic = FindHeuristic(startPosition);

            // Stato iniziale
            var initialState = new State(startPosition, 0, initialHeuristic);
            priorityQueue.Insert(initialState);

            // Ciclo principale dell'A* 
            while (!priorityQueue.IsEmpty())
            {
                // Estrai lo stato con il costo più basso dalla coda
                var current = priorityQueue.DelMin();
                Console.WriteLine($"Queue.Count:{priorityQueue.Size()} with F = {current.F}");
                //AOC.Utilities.Map.Map.ShowCharMap(Map, Map.GetLength(0), Map.GetLength(1));

                var currentPosition = current.Position;
                var currentFunctionPriority = current.F;
                var currentG = current.G;
                var currentH = current.H;

                // Controlla se è una soluzione
                if (currentPosition[0] == targetPosition[0] && currentPosition[1] == targetPosition[1])
                {
                    minStep = currentG;
                    Console.WriteLine($"Solution found in {currentG} steps!");
                    continue;
                }

                foreach ((int dx, int dy) in new List<(int dx, int dy)> { (1, 0), (-1, 0), (0, 1), (0, -1) })
                {
                    //Controlli sulle dimensioni della mappa
                    var newX = currentPosition[0] + dx;
                    var newY = currentPosition[1] + dy;

                    if (newX < 0 || newY < 0) continue;
                    if (newX > n - 1 || newY > n - 1) throw new Exception($"Mappa di dimensione {n}x{n} troppo piccola!");


                    //Crea nuovo stato
                    int[] newPosition = new int[2];
                    newPosition[0] = newX;
                    newPosition[1] = newY;

                    // Calcola i nuovi valori g e h per il nuovo stato
                    int newG = currentG + 1;  // Costo per arrivare a questo stato
                    int newH = FindHeuristic(newPosition); // Nuova euristica
                    int newF = newG + newH;  // Funzione di costo totale (f = g + h)

                    //Controlla che sia una posizione libera o che la si raggiunga con costo minore
                    if (
                         Map[newPosition[0], newPosition[1]].Equals('.') ||
                         (Map[newPosition[0], newPosition[1]].Equals('O') && exploredStates[newPosition[0], newPosition[1]] > newF)
                       )
                    {
                        // Crea il nuovo stato
                        var newState = new State(newPosition, newG, newH);

                        // Aggiungi il nuovo stato alla coda di priorità
                        priorityQueue.Insert(newState);

                        exploredStates[newPosition[0], newPosition[1]] = newF;
                        Map[newPosition[0], newPosition[1]] = 'O';

                    }
                    else //è un muro o ci sono già stato in meno passi
                    {
                        continue;
                    }

                }
            }
        }


        public void CreateMap(int code)
        {
            Map = new char[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    exploredStates[j, i] = -1;
                    Map[j, i] = (Convert.ToString((i * i + 3 * i + 2 * i * j + j + j * j) + code, 2).Count(c => c == '1') % 2 == 0) ? '.' : '#';
                    if (i == startPosition[0] && j == startPosition[1])
                    {
                        Map[j, i] = 'O';
                        exploredStates[j, i] = 0;
                    }
                }

            }
            AOC.Utilities.Map.Map.ShowCharMap(Map, Map.GetLength(0), Map.GetLength(1));
        }
        public int FindHeuristic(int[] position)
        {
            int total = (Math.Max(position[0], targetPosition[0]) - Math.Min(position[0], targetPosition[0])) +
                (Math.Max(position[1], targetPosition[1]) - Math.Min(position[1], targetPosition[1]));

            return total;
        }

        public class State : IComparable<State>
        {
            public int[] Position = new int[2];
            public int G { get; set; } // Costo accumulato
            public int H { get; set; } // Stima euristica
            public int F => G + H; // Costo totale

            public State(int[] position, int g, int h)
            {
                Position = position;
                G = g;
                H = h;
            }

            // Confronto basato su F (per la priority queue)
            public int CompareTo(State other)
            {
                return F.CompareTo(other.F);
            }


            // Override di Equals per confrontare stati
            public override bool Equals(object obj)
            {
                // Controlla se l'oggetto è di tipo State
                if (obj == null || !(obj is State))
                    return false;

                // Cast a State
                State other = (State)obj;

                // Confronta i due stati usando il loro hash code
                return GetHashCode() == other.GetHashCode();
            }


        }

        public class MinPQState
        {
            private List<State> pq; // Lista per rappresentare l'heap

            public MinPQState()
            {
                pq = new List<State>();
            }

            public bool IsEmpty() => pq.Count == 0;

            public int Size() => pq.Count;

            public void Insert(State state)
            {
                pq.Add(state);
                Swim(pq.Count - 1);
            }

            public State DelMin()
            {
                if (IsEmpty()) throw new InvalidOperationException("Priority queue underflow");
                State min = pq[0];
                Swap(0, pq.Count - 1);
                pq.RemoveAt(pq.Count - 1);
                Sink(0);
                return min;
            }

            private void Swim(int k)
            {
                while (k > 0 && pq[(k - 1) / 2].CompareTo(pq[k]) > 0)
                {
                    Swap(k, (k - 1) / 2);
                    k = (k - 1) / 2;
                }
            }

            private void Sink(int k)
            {
                while (2 * k + 1 < pq.Count)
                {
                    int j = 2 * k + 1;
                    if (j + 1 < pq.Count && pq[j].CompareTo(pq[j + 1]) > 0) j++;
                    if (pq[k].CompareTo(pq[j]) <= 0) break;
                    Swap(k, j);
                    k = j;
                }
            }

            private void Swap(int i, int j)
            {
                State temp = pq[i];
                pq[i] = pq[j];
                pq[j] = temp;
            }
        }
        public void Part2(object input, bool test, ref object solution)
        {
            string inputString = (string)input;
            inputString = inputString.Split(Delimiter.delimiter_line, StringSplitOptions.None)[0];
            if (test)
            {
                startPosition[0] = 1; startPosition[1] = 1;
                targetPosition[0] = 4; targetPosition[1] = 7;
            }
            else
            {
                startPosition[0] = 1; startPosition[1] = 1;
                targetPosition[0] = 39; targetPosition[1] = 31;
            }
            n = Math.Max(targetPosition[0], targetPosition[1]) + 10;
            exploredStates = new int[n, n];

            CreateMap(int.Parse(inputString));
            AStarComplex();
            int count = 0;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (Map[i, j] == 'O') count++;
                }
            }
            solution = count;
        }
        public void AStarComplex()
        {
            // Inizializza la coda di priorità
            var priorityQueue = new MinPQState();

            // Calcola l'euristica iniziale
            int initialHeuristic = FindHeuristic(startPosition);

            // Stato iniziale
            var initialState = new State(startPosition, 0, initialHeuristic);
            priorityQueue.Insert(initialState);

            // Ciclo principale dell'A* 
            while (!priorityQueue.IsEmpty())
            {
                // Estrai lo stato con il costo più basso dalla coda
                var current = priorityQueue.DelMin();
                Console.WriteLine($"Queue.Count:{priorityQueue.Size()} with F = {current.F}");
                //AOC.Utilities.Map.Map.ShowCharMap(Map, Map.GetLength(0), Map.GetLength(1));

                var currentPosition = current.Position;
                var currentFunctionPriority = current.F;
                var currentG = current.G;
                var currentH = current.H;

                foreach ((int dx, int dy) in new List<(int dx, int dy)> { (1, 0), (-1, 0), (0, 1), (0, -1) })
                {
                    //Controlli sulle dimensioni della mappa
                    var newX = currentPosition[0] + dx;
                    var newY = currentPosition[1] + dy;

                    if (newX < 0 || newY < 0) continue;
                    if (newX > n - 1 || newY > n - 1) throw new Exception($"Mappa di dimensione {n}x{n} troppo piccola!");


                    //Crea nuovo stato
                    int[] newPosition = new int[2];
                    newPosition[0] = newX;
                    newPosition[1] = newY;

                    // Calcola i nuovi valori g e h per il nuovo stato
                    int newG = currentG + 1;  // Costo per arrivare a questo stato
                    int newH = FindHeuristic(newPosition); // Nuova euristica
                    int newF = newG + newH;  // Funzione di costo totale (f = g + h)

                    //Controlla che non siano stati fatti più di 50 passi
                    if (newG > 50) continue;

                    //Controlla che sia una posizione libera o che la si raggiunga con costo minore
                    if (
                        Map[newPosition[0], newPosition[1]].Equals('.') ||
                        (Map[newPosition[0], newPosition[1]].Equals('O') && exploredStates[newPosition[0], newPosition[1]] > newF)
                       )
                    {
                        // Crea il nuovo stato
                        var newState = new State(newPosition, newG, newH);

                        // Aggiungi il nuovo stato alla coda di priorità
                        priorityQueue.Insert(newState);

                        exploredStates[newPosition[0], newPosition[1]] = newF;
                        Map[newPosition[0], newPosition[1]] = 'O';

                    }
                    else //è un muro o ci sono già stato in meno passi
                    {
                        continue;
                    }

                }
            }
        }

    }
}