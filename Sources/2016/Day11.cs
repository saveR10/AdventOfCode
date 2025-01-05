using AOC;
using AOC.DataStructures.Clustering;
using AOC.Model;
using AOC.SearchAlghoritmhs;
using Newtonsoft.Json.Bson;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics.Eventing.Reader;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography;
using System.Text;
using static AOC.SearchAlghoritmhs.ResearchAlgorithmsAttribute;
using static AOC2016.Day10;
using static AOC2024.Day24;

namespace AOC2016
{
    [ResearchAlgorithms(TypologyEnum.Game)] //River Crossing Puzzle
    [ResearchAlgorithms(ResolutionEnum.DFS)]
    [ResearchAlgorithms(ResolutionEnum.BFS)]
    [ResearchAlgorithms(ResolutionEnum.AlphaStar)]
    [ResearchAlgorithms(ResolutionEnum.Dijkstra)]
    [ResearchAlgorithms(DifficultEnum.Legend)]
    public class Day11 : Solver, IDay
    {
        public int c = 0;

        public int minStep = int.MaxValue;
        public int totalObjects = 0;
        public Dictionary<string, int> exploredStates = new Dictionary<string, int>();

        public void Part1(object input, bool test, ref object solution)
        {
            Dictionary<int, List<string>> floors = new Dictionary<int, List<string>>();//Dict che contiene floor e oggetti
            string inputString = (string)input;
            string[] Notes = inputString.Split(Delimiter.delimiter_line, StringSplitOptions.None);
            if (test)
            {
                floors.Add(1, new List<string>()); floors[1].Add("HM"); floors[1].Add("LM"); floors[1].Add("E"); totalObjects += floors[1].Count();
                floors.Add(2, new List<string>()); floors[2].Add("HG"); totalObjects += floors[2].Count();
                floors.Add(3, new List<string>()); floors[3].Add("LG"); totalObjects += floors[3].Count();
                floors.Add(4, new List<string>());
            }
            else
            {
                floors.Add(1, new List<string>()); floors[1].Add("SG"); floors[1].Add("SM"); floors[1].Add("PG"); floors[1].Add("PM"); floors[1].Add("E"); totalObjects += floors[1].Count();
                floors.Add(2, new List<string>()); floors[2].Add("TG"); floors[2].Add("RG"); floors[2].Add("RM"); floors[2].Add("CG"); floors[2].Add("CM"); totalObjects += floors[2].Count();
                floors.Add(3, new List<string>()); floors[3].Add("TM"); totalObjects += floors[3].Count();
                floors.Add(4, new List<string>());
            }


            //DFS(floors, 0); //Va in Overflow di memoria, forse perché genera troppe List di stringhe; controlla il performance profiler
            //DFSWithStack(floors); //Ci mette troppo tempo: prima raggiunge uno stack di 60000 e poi pian piano migliora.
            BFS(floors); //Arriva massimo a 12000, poi migliora molto velocemente.
            solution = minStep;
        }
        public void BFS(Dictionary<int, List<string>> initialFloors)
        {
            // Coda per simulare il BFS
            Queue<Tuple<Dictionary<int, List<string>>, int>> queue = new Queue<Tuple<Dictionary<int, List<string>>, int>>();

            // Dizionario per tracciare stati esplorati e il numero minimo di passi per raggiungerli
            Dictionary<string, int> exploredStates = new Dictionary<string, int>();

            // Stato iniziale
            queue.Enqueue(new Tuple<Dictionary<int, List<string>>, int>(CloneDictionary(initialFloors), 0));

            while (queue.Count > 0)
            {
                Console.WriteLine($"Queue size: {queue.Count}");

                // Estrai lo stato corrente dalla coda
                var current = queue.Dequeue();
                var floors = current.Item1;
                var currentStep = current.Item2;

                // Verifica se l'obiettivo è stato raggiunto
                if (floors[1].Count == 0 && floors[2].Count == 0 && floors[3].Count == 0 && floors[4].Count == totalObjects)
                {
                    minStep = currentStep;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Solution found!");
                    ShowFloors(floors);
                    Console.ForegroundColor = ConsoleColor.White;
                    return; // Fermati alla prima soluzione trovata
                }

                // Trova il piano dell'ascensore
                int elevator = Elevator(floors);

                // Genera tutti i possibili nuovi stati
                foreach (var targetFloor in floors.Keys.Where(f => Math.Abs(f - elevator) == 1)) // Piani adiacenti
                {
                    // Trova tutte le combinazioni di oggetti (singoli e coppie)
                    foreach (var objectsToMove in GetObjectsCombinations(floors[elevator].Where(o => o != "E").ToList()))
                    {
                        // Crea un nuovo stato
                        var newFloors = CloneDictionary(floors);

                        // Sposta gli oggetti al nuovo piano
                        foreach (var obj in objectsToMove)
                        {
                            newFloors[elevator].Remove(obj);
                            newFloors[targetFloor].Add(obj);
                        }

                        // Sposta l'ascensore
                        newFloors[elevator].Remove("E");
                        newFloors[targetFloor].Add("E");

                        // Verifica la condizione di sicurezza
                        if (!SafetyCondition(newFloors))
                        {
                            continue; // Stato non valido
                        }

                        // Calcola l'hash del nuovo stato
                        string newHashCode = GetHashCodeFloor(newFloors);

                        // Aggiungi lo stato solo se è nuovo o migliore
                        if (!exploredStates.ContainsKey(newHashCode) || exploredStates[newHashCode] > currentStep + 1)
                        {
                            queue.Enqueue(new Tuple<Dictionary<int, List<string>>, int>(newFloors, currentStep + 1));
                            exploredStates[newHashCode] = currentStep + 1; // Aggiorna il numero di passi
                        }
                        else
                        {
                            newFloors.Clear();
                        }
                    }
                }
            }
        }
        public void DFSWithStack(Dictionary<int, List<string>> initialFloors)
        {
            // Stack per simulare il DFS iterativo
            Stack<Tuple<Dictionary<int, List<string>>, int>> stack = new Stack<Tuple<Dictionary<int, List<string>>, int>>();

            // Aggiungi lo stato iniziale allo stack
            stack.Push(new Tuple<Dictionary<int, List<string>>, int>(CloneDictionary(initialFloors), 0));

            // Dizionario per tracciare stati esplorati e il numero minimo di passi per raggiungerli
            Dictionary<string, int> exploredStates = new Dictionary<string, int>();

            while (stack.Count > 0)
            {
                Console.WriteLine(stack.Count);
                // Estrai lo stato corrente dallo stack
                var current = stack.Pop();
                var floors = current.Item1;
                var currentStep = current.Item2;

                // Verifica se l'obiettivo è stato raggiunto
                if (floors[1].Count == 0 && floors[2].Count == 0 && floors[3].Count == 0 && floors[4].Count == totalObjects)
                {
                    if (currentStep < minStep)
                    {
                        minStep = currentStep;
                        Console.ForegroundColor = ConsoleColor.Green;
                        ShowFloors(floors);
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    continue; // Non serve esplorare ulteriormente
                }

                // Trova il piano dell'ascensore
                int elevator = Elevator(floors);

                // Genera tutti i possibili nuovi stati
                foreach (var targetFloor in floors.Keys.Where(f => Math.Abs(f - elevator) == 1)) // Piani adiacenti
                {
                    // Trova tutte le combinazioni di oggetti (singoli e coppie)
                    foreach (var objectsToMove in GetObjectsCombinations(floors[elevator].Where(o => o != "E").ToList()))
                    {
                        // Crea un nuovo stato
                        var newFloors = CloneDictionary(floors);

                        // Sposta gli oggetti al nuovo piano
                        foreach (var obj in objectsToMove)
                        {
                            newFloors[elevator].Remove(obj);
                            newFloors[targetFloor].Add(obj);
                        }

                        // Sposta l'ascensore
                        newFloors[elevator].Remove("E");
                        newFloors[targetFloor].Add("E");

                        // Verifica la condizione di sicurezza
                        if (!SafetyCondition(newFloors))
                        {
                            continue; // Stato non valido
                        }

                        // Calcola l'hash del nuovo stato
                        string newHashCode = GetHashCodeFloor(newFloors);

                        // Aggiungi lo stato solo se è nuovo o migliore
                        if (!exploredStates.ContainsKey(newHashCode) || exploredStates[newHashCode] > currentStep + 1)
                        {
                            stack.Push(new Tuple<Dictionary<int, List<string>>, int>(newFloors, currentStep + 1));
                            exploredStates[newHashCode] = currentStep + 1; // Aggiorna il numero di passi
                        }
                        else
                        {
                            newFloors.Clear();
                        }

                    }
                }
            }
        }
        public void DFS(Dictionary<int, List<string>> floors, int step)
        {
            Console.WriteLine($"c: {c}, step: {step}"); c++;
            //ShowFloors(floors);

            //Verifica condizione target
            if (floors[1].Count == 0 && floors[2].Count == 0 && floors[3].Count == 0 && floors[4].Count == totalObjects)
            {
                if (step < minStep)
                {
                    minStep = step;
                    Console.ForegroundColor = ConsoleColor.Green;
                    ShowFloors(floors);
                    Console.ForegroundColor = ConsoleColor.White;
                    floors.Clear();
                    return;
                }
                else
                {
                    floors.Clear();
                }
            }

            //Genera un nuovo stato del sistema
            int e = Elevator(floors);
            foreach (var f in floors.Except(floors.Where(f => f.Key == e || Math.Abs(f.Key - e) > 1)))
            {
                //Trova tutte le combinazioni di oggetti singole e doppie, escluso E
                foreach (var o in GetObjectsCombinations(floors[e].Except(floors[e].Where(fe => fe.Equals("E"))).ToList()))
                {
                    Dictionary<int, List<string>> newFloors = new Dictionary<int, List<string>>();
                    newFloors = CloneDictionary(floors);

                    //Sposta gli oggetti al nuovo piano
                    foreach (var obj in o)
                    {
                        newFloors[e].Remove(obj);
                    }
                    newFloors[f.Key].AddRange(o);

                    //Sposta l'ascensore al nuovo piano
                    newFloors[e].Remove("E");
                    newFloors[f.Key].Add("E");

                    //Verifica se è una condizione di sicurezza
                    if (!SafetyCondition(newFloors))
                    {
                        newFloors.Clear();
                        continue;
                    }

                    // Calcola l'hash del nuovo stato
                    string newHashCode = GetHashCodeFloor(newFloors);

                    // Genera solo se è un nuovo stato o ottenuto in minor numero di passi
                    if (!exploredStates.ContainsKey(newHashCode) || step + 1 < exploredStates[newHashCode])
                    {
                        if (exploredStates.ContainsKey(newHashCode))
                        {
                            exploredStates.Remove(newHashCode);
                        }
                        exploredStates[newHashCode] = step + 1; // Aggiorna il numero di passi
                        DFS(newFloors, step + 1);
                    }
                    else
                    {
                        newFloors.Clear();
                    }
                }
            }
        }
        public bool SafetyCondition(Dictionary<int, List<string>> floors)
        {
            foreach (var floor in floors)
            {
                // Ottieni gli oggetti sul piano
                var items = floor.Value;

                // Estrai i microchip e i generatori
                var microchips = items.Where(i => i.EndsWith("M")).ToList();
                var generators = items.Where(i => i.EndsWith("G")).Select(g => g.Substring(0, g.Length - 1)).ToHashSet();

                // Se ci sono microchip, verifica le condizioni di sicurezza
                foreach (var chip in microchips)
                {
                    string element = chip.Substring(0, chip.Length - 1); // Nome dell'elemento del microchip
                    if (!generators.Contains(element) && generators.Any())
                    {
                        // Il microchip è danneggiato se il suo generatore non è presente, ma ci sono altri generatori sul piano.
                        return false;
                    }
                }
            }

            // Se tutte le condizioni di sicurezza sono rispettate, ritorna true
            return true;
        }
        public void ShowFloors(Dictionary<int, List<string>> floors)
        {
            foreach (var f in floors.OrderByDescending(f => f.Key))
            {
                Console.Write($"F{f.Key}");
                foreach (var o in floors[f.Key])
                {
                    Console.Write($" {o} ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
        //Crea le combinazioni di singole e coppie, ottimizzato per la memoria
        public static IEnumerable<List<string>> GetObjectsCombinations(List<string> input)
        {
            //var result = new List<List<string>>();

            var inputList = input.ToList(); // Converti in lista per accedere tramite indice

            // Aggiungi le combinazioni singole
            foreach (var item in inputList)
            {
                yield return new List<string> { item };
                //result.Add(new List<string> { item });
            }

            // Aggiungi le combinazioni di coppie
            for (int i = 0; i < inputList.Count; i++)
            {
                for (int j = i + 1; j < inputList.Count; j++)
                {
                    //result.Add(new List<string> { inputList[i], inputList[j] });
                    yield return new List<string> { inputList[i], inputList[j] };
                }
            }

            //return result;
        }

        public string GetHashCodeFloor(Dictionary<int, List<string>> newFloors)
        {
            string ret = "";
            // Itera sui piani (chiavi del dizionario) in ordine crescente
            foreach (var floor in newFloors.OrderBy(f => f.Key))
            {
                // Aggiunge la chiave del piano al risultato
                ret += $"{floor.Key}:";

                // Itera sugli oggetti del piano in ordine alfabetico
                foreach (var obj in floor.Value.OrderBy(o => o))
                {
                    ret += $"{obj},";
                }

                // Aggiunge un separatore tra i piani
                ret += "|";
            }
            return ret;
        }
        Dictionary<int, List<string>> CloneDictionary(Dictionary<int, List<string>> original)
        {
            var clone = new Dictionary<int, List<string>>();

            foreach (var kvp in original)
            {
                // Clonare ogni List<string> per avere copie indipendenti
                clone[kvp.Key] = new List<string>(kvp.Value);
            }

            return clone;
        }
        public int Elevator(Dictionary<int, List<string>> floors)
        {
            foreach (var f in floors)
            {
                if (f.Value.Contains("E")) return f.Key;
            }
            return -1;
        }
        public static bool Dijkstra = false;
        public void Part2(object input, bool test, ref object solution)
        {
            Dictionary<int, List<string>> floors = new Dictionary<int, List<string>>();//Dict che contiene floor e oggetti
            string inputString = (string)input;
            string[] Notes = inputString.Split(Delimiter.delimiter_line, StringSplitOptions.None);
            floors.Add(1, new List<string>()); floors[1].Add("SG"); floors[1].Add("SM"); floors[1].Add("PG"); floors[1].Add("PM"); floors[1].Add("EG"); floors[1].Add("EM"); floors[1].Add("DG"); floors[1].Add("DM"); floors[1].Add("E"); totalObjects += floors[1].Count();
            floors.Add(2, new List<string>()); floors[2].Add("TG"); floors[2].Add("RG"); floors[2].Add("RM"); floors[2].Add("CG"); floors[2].Add("CM"); totalObjects += floors[2].Count();
            floors.Add(3, new List<string>()); floors[3].Add("TM"); totalObjects += floors[3].Count();
            floors.Add(4, new List<string>());

            //DFS(floors, 0); 
            //DFSWithStack(floors); 
            //BFS(floors);
            AStar(floors);
            solution = minStep;
            //63, ... per qualche mtivo mi conta step +2; ho provato ad eseguirlo Part1 con A* e mi veniva 39 invece di 37. Forse perché non ho inserito il primo stato? Boh!
        }
        public void AStar(Dictionary<int, List<string>> initialFloors)
        {
            // Inizializza la coda di priorità
            var priorityQueue = new MinPQState();

            // Dizionario per tenere traccia degli stati esplorati
            var exploredStates = new Dictionary<string, int>();

            // Calcola l'euristica iniziale
            int initialHeuristic = FindHeuristic(initialFloors);

            // Stato iniziale
            var initialState = new State(initialFloors, 1, 0, initialHeuristic);
            priorityQueue.Insert(initialState);
            //exploredStates[initialState.GetHashCode()] = initialState.F; // Inserisci il primo stato nella mappa

            // Ciclo principale dell'A* 
            while (!priorityQueue.IsEmpty())
            {
                // Estrai lo stato con il costo più basso dalla coda
                var current = priorityQueue.DelMin();
                //Console.WriteLine($"Queue.Count:{priorityQueue.Size()}, Exploring state: {current.GetHashCode()} with F = {current.F}");
                Console.WriteLine($"Queue.Count:{priorityQueue.Size()} with F = {current.F}");
                var floors = current.Floors;
                ShowFloors(floors);
                var currentFunctionPriority = current.F;
                var currentG = current.G;
                var currentH = current.H;
                var elevator = current.Elevator;

                // Controlla se è una soluzione (tutti gli oggetti sono nella destinazione)
                if (floors[1].Count == 0 && floors[2].Count == 0 && floors[3].Count == 0 && floors[4].Count == totalObjects)
                {
                    minStep = currentG;
                    Console.WriteLine($"Solution found in {currentG} steps!");
                    ShowFloors(current.Floors);  // Stampa lo stato finale
                    //return; // Fermati alla prima soluzione trovata
                    continue;
                }

                // Ottieni i piani accessibili dall'ascensore
                foreach (var targetFloor in floors.Keys.Where(f => Math.Abs(f - elevator) == 1))
                {
                    // Genera tutte le combinazioni valide di oggetti da spostare
                    foreach (var objectsToMove in GetObjectsCombinations(floors[elevator].Where(o => o != "E").ToList()))
                    {
                        // Crea un nuovo stato
                        var newFloors = CloneDictionary(floors);

                        foreach (var obj in objectsToMove)
                        {
                            newFloors[elevator].Remove(obj);
                            newFloors[targetFloor].Add(obj);
                        }

                        newFloors[elevator].Remove("E");
                        newFloors[targetFloor].Add("E");

                        // Controlla se il nuovo stato è sicuro
                        if (!SafetyCondition(newFloors))
                        {
                            //Console.WriteLine("Safety condition failed, skipping state.");
                            continue;
                        }

                        // Calcola i nuovi valori g e h per il nuovo stato
                        int newG = current.G + 1;  // Costo per arrivare a questo stato
                        int newH = FindHeuristic(newFloors); // Nuova euristica
                        int newF = newG + newH;  // Funzione di costo totale (f = g + h)

                        // Crea il nuovo stato
                        var newState = new State(newFloors, targetFloor, newG, newH);

                        string newHashCode = GetHashCodeFloor(newFloors);
                        
                        // Verifica se lo stato è già stato esplorato o se ha un costo migliore
                        //if (!exploredStates.ContainsKey(newState.GetHashCode()) || exploredStates[newState.GetHashCode()] > newG)
                        if (!exploredStates.ContainsKey(newHashCode) || exploredStates[newHashCode] > newF)
                        {
                            // Aggiungi il nuovo stato alla coda di priorità
                            priorityQueue.Insert(newState);

                            // Aggiorna l'esplorazione con il nuovo stato e il suo costo
                            exploredStates[newHashCode] = newF;
                        }
                    }
                }
            }
        }

        public int FindHeuristic(Dictionary<int, List<string>> floors)
        {
            int total = 0;
            if (Dijkstra)
            {
                total = 0;
            }
            else
            {
                int bonusLevel = 40*totalObjects; //40 punti bonus per il livello 4
                int bonusCoupling = 100 * totalObjects; //100 punti bonus per accoppiamento al quarto livello
                //1 punto per ogni livello
                total = totalObjects * 4*4 + bonusLevel + bonusCoupling;
                foreach (var f in floors.Keys)
                {
                    total -= (floors[f].Count * 4*4* f);
                    if (f == 4)
                    {
                        total -= (floors[f].Count * 40); //Bonus livello 4
                        foreach (var o in floors[f])  //Bonus accoppiamento livello 4
                        {
                            //Se
                            if(o.EndsWith("M"))
                            {
                                if (floors[f].Where(fl => fl.EndsWith("G") && fl.StartsWith(o.Substring(0, 1))).Count()>0) total -= bonusCoupling/totalObjects;
                            }
                        }
                    }
                }
            }
            return total;
        }
        public class State : IComparable<State>
        {
            public Dictionary<int, List<string>> Floors { get; set; } // Stato dei piani
            public int Elevator { get; set; } // Piano attuale dell'ascensore
            public int G { get; set; } // Costo accumulato
            public int H { get; set; } // Stima euristica
            public int F => G + H; // Costo totale

            public State(Dictionary<int, List<string>> floors, int elevator, int g, int h)
            {
                Floors = floors;
                Elevator = elevator;
                G = g;
                H = h;
            }

            // Confronto basato su F (per la priority queue)
            public int CompareTo(State other)
            {
                return F.CompareTo(other.F);
            }

            // Metodo per generare un hash unico dello stato
            public override int GetHashCode()
            {
                return string.Join(";", Floors.OrderBy(f => f.Key)
                                .Select(f => $"{f.Key}:{string.Join(",", f.Value.OrderBy(x => x))}"))
                                .GetHashCode();
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


            public override string ToString()
            {
                return $"Elevator: {Elevator}, Floors: {string.Join(" | ", Floors.Select(f => $"Floor {f.Key}: {string.Join(",", f.Value)}"))}";
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

    }
}