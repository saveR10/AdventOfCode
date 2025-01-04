using AOC;
using AOC.DataStructures.Cache;
using AOC.DataStructures.Clustering;
using AOC.Model;
using AOC.SearchAlghoritmhs;
using AOC.Utilities.Dynamic;
using AOC.Utilities.Math;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Text.RegularExpressions;
using static AOC.DataStructures.Cache.Cache;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using System.Threading;
using System.Linq.Expressions;

namespace AOC2024
{
    [ResearchAlgorithms(ResearchAlgorithmsAttribute.TypologyEnum.Keypad)]
    [ResearchAlgorithms(ResearchAlgorithmsAttribute.ResolutionEnum.Cache)]
    [ResearchAlgorithms(ResearchAlgorithmsAttribute.ResolutionEnum.Overflow)]
    public class Day21 : Solver, IDay
    {
        static readonly Dictionary<char, (int x, int y)> NumericKeypad = new Dictionary<char, (int x, int y)>()
        {
            { '7', (0, 0) }, { '8', (0, 1) }, { '9', (0, 2) },
            { '4', (1, 0) }, { '5', (1, 1) }, { '6', (1, 2) },
            { '1', (2, 0) }, { '2', (2, 1) }, { '3', (2, 2) },
            { ' ', (3, 0) }, { '0', (3, 1) }, { 'A', (3, 2) }
        };
        static readonly Dictionary<char, (int x, int y)> DirectionalKeypad = new Dictionary<char, (int x, int y)>
        {
            { ' ', (0, 0) }, { '^', (0, 1) }, { 'A', (0, 2) },
            { '<', (1, 0) }, { 'v', (1, 1) }, { '>', (1, 2) }
        };

        public void Part1(object input, bool test, ref object solution)
        {
            string inputText = (string)input;
            List<string> codes = new List<string>();
            Dictionary<string, int> NumericParts = new Dictionary<string, int>();
            foreach (string line in inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None))
            {
                if (!string.IsNullOrEmpty(line))
                {
                    codes.Add(line);
                    NumericParts.Add(line, int.Parse(line.Trim('A')));
                }
            }

            int totalComplexity = 0;

            foreach (string code in codes)
            {
                List<string> NumericRobotSequences = new List<string>();
                List<string> DirectionalFirstRobotSequences = new List<string>();
                List<string> DirectionalSecondRobotSequences = new List<string>();

                Console.WriteLine($"Code: {code}");
                NumericRobotSequences = CalculateNumericSequences(code);

                foreach (var n in NumericRobotSequences)
                {
                    Console.WriteLine($"Numeric Robot: {n}");
                    DirectionalFirstRobotSequences.AddRange(CalculateDirectionalSequences(n));
                }

                foreach (var d in DirectionalFirstRobotSequences)
                {
                    Console.WriteLine($"Directional Robot 1: {d}");
                    DirectionalSecondRobotSequences.AddRange(CalculateDirectionalSequences(d));
                }

                int complexity = DirectionalSecondRobotSequences.OrderBy(s => s.Length).First().Length * NumericParts[code];
                var seq = DirectionalSecondRobotSequences.OrderBy(s => s.Length).First();
                var seqnum = NumericRobotSequences.OrderBy(s => s.Length).First();
                var seqfirst = DirectionalFirstRobotSequences.OrderBy(s => s.Length).First();
                totalComplexity += complexity;

                Console.WriteLine($"Complexity: {DirectionalSecondRobotSequences.OrderBy(s => s.Length).First().Length}*{NumericParts[code]} {complexity}\n");
            }

            solution = totalComplexity;
        }
        static List<string> CalculateDirectionalSequences(string command)
        {
            var sequences = new List<string>();
            sequences.Add("");
            string type = "D";
            (int x, int y) currentPos = DirectionalKeypad['A'];

            for (int i = 0; i < command.Length; i++)
            {
                string trySequence = "";
                char key = command[i];
                (int x, int y) targetPos = DirectionalKeypad[key];
                trySequence = GenerateMovement(currentPos, (targetPos.x, targetPos.y), type);

                if (trySequence.Length > 1 && trySequence.Distinct().Count() > 1)
                {
                    var permutations = Combinatorial.GetPermutationsWithRept(trySequence.ToList());
                    var newSequences = new List<string>();
                    foreach (var p in permutations)
                    {
                        if (CheckPath(p))
                        {
                            foreach (var sequence in sequences)
                            {
                                newSequences.Add(sequence + string.Join("", p));
                            }
                        }
                    }
                    sequences = newSequences;
                }
                else
                {
                    sequences = sequences.Select(s => s + trySequence).ToList();
                }

                sequences = sequences.Select(s => s + "A").ToList();
                currentPos = (targetPos.x, targetPos.y);

            }


            List<string> ret = new List<string>();
            foreach (var s in sequences)
            {
                ret.Add(string.Join("", s));
            }
            return ret;

            bool CheckPath(List<char> permutationSequence)
            {
                int x = currentPos.x;
                int y = currentPos.y;
                foreach (var step in permutationSequence)
                {
                    switch (step)
                    {
                        case '^': x--; break;
                        case 'v': x++; break;
                        case '<': y--; break;
                        case '>': y++; break;
                    }
                    if (x == 0 && y == 0) return false;
                }
                return true;
            }

            void AddSequence(List<char> permutationSequence)
            {
                sequences.Append(string.Join("", permutationSequence));
            }

        }
        static List<string> CalculateNumericSequences(string code)
        {
            var sequences = new List<string>();
            sequences.Add("");
            string type = "N";
            (int x, int y) currentPos = NumericKeypad['A']; // Posizione iniziale

            for (int i = 0; i < code.Length; i++)
            {
                string trySequence = "";
                char key = code[i];
                (int x, int y) targetPos = NumericKeypad[key];
                trySequence = GenerateMovement(currentPos, (targetPos.x, targetPos.y), type);

                if (trySequence.Length > 1 && trySequence.Distinct().Count() > 1)
                {
                    var permutations = Combinatorial.GetPermutationsWithRept(trySequence.ToList());
                    var newSequences = new List<string>();
                    foreach (var p in permutations)
                    {
                        if (CheckPath(p))
                        {
                            foreach (var sequence in sequences)
                            {
                                newSequences.Add(sequence + string.Join("", p));
                            }
                        }
                    }
                    sequences = newSequences;
                }
                else
                {
                    sequences = sequences.Select(s => s + trySequence).ToList();
                }

                sequences = sequences.Select(s => s + "A").ToList();
                currentPos = (targetPos.x, targetPos.y);
            }
            List<string> ret = new List<string>();
            foreach (var s in sequences)
            {
                ret.Add(string.Join("", s));
            }
            return ret;

            bool CheckPath(List<char> permutationSequence)
            {
                int x = currentPos.x;
                int y = currentPos.y;
                foreach (var step in permutationSequence)
                {
                    switch (step)
                    {
                        case '^': x--; break;
                        case 'v': x++; break;
                        case '<': y--; break;
                        case '>': y++; break;
                    }
                    if (x == 3 && y == 0) return false;
                }
                return true;
            }

            void AddSequence(List<char> permutationSequence)
            {
                sequences.Append(string.Join("", permutationSequence));
            }
        }
        static string GenerateMovement((int x, int y) start, (int x, int y) target, string type)
        {
            var movements = new List<char>();

            if (type == "N")
            {
                while (start.x > target.x) { movements.Add('^'); start.x--; }
                while (start.y < target.y) { movements.Add('>'); start.y++; }
                while (start.y > target.y) { movements.Add('<'); start.y--; }
                while (start.x < target.x) { movements.Add('v'); start.x++; }
            }
            else if (type == "D")
            {
                while (start.y < target.y) { movements.Add('>'); start.y++; }
                while (start.x < target.x) { movements.Add('v'); start.x++; }
                while (start.x > target.x) { movements.Add('^'); start.x--; }
                while (start.y > target.y) { movements.Add('<'); start.y--; }
            }
            return string.Join("", movements);
        }

        public void Part2(object input, bool test, ref object solution)
        {
            string inputText = (string)input;
            List<string> codes = new List<string>();
            Dictionary<string, long> NumericParts = new Dictionary<string, long>();
            foreach (string line in inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None))
            {
                if (!string.IsNullOrEmpty(line))
                {
                    codes.Add(line);
                    NumericParts.Add(line, int.Parse(line.Trim('A')));
                }
            }
            int nRobot = test ? 2 : 25;


            // Inizializzazione dei dati
            var cache = new Dictionary<(char a, char b, int robots), long>();
            var totalComplexity = 0L;

            foreach (var code in codes)
            {
                var minCost = long.MaxValue;

                // Genera tutti i possibili movimenti per digitare il codice
                var possibleMoves = NumpadRobotMoves(code, 'A');
                foreach (var moves in possibleMoves)
                {
                    // Calcola il costo totale della sequenza corrente
                    var pairs = moves.Select((c, i) => (a: i == 0 ? 'A' : moves[i - 1], b: c)).ToArray();
                    var totalCost = pairs.Select(pair => Cost(pair, 25, cache)).Sum();

                    if (totalCost < minCost)
                        minCost = totalCost;
                }

                // Aggiorna la complessità totale
                totalComplexity += minCost * int.Parse(code.Substring(0, code.Length - 1)); // Rimuove l'ultimo carattere (A) dal codice
            }
            solution = totalComplexity.ToString();
        }
        private (char c, int x, int y)[] numKeypad = new (char, int, int)[]
{
    ('7', 0, 0), ('8', 1, 0), ('9', 2, 0),
    ('4', 0, 1), ('5', 1, 1), ('6', 2, 1),
    ('1', 0, 2), ('2', 1, 2), ('3', 2, 2),
    ('0', 1, 3), ('A', 2, 3)
};
        private (char c, int x, int y)[] dirKeypad = new (char, int, int)[]
{
    ('^', 1, 0), // Pulsante "su" (^)
    ('A', 2, 0), // Pulsante "azione" (A)
    ('<', 0, 1), // Pulsante "sinistra" (<)
    ('v', 1, 1), // Pulsante "giù" (v)
    ('>', 2, 1)  // Pulsante "destra" (>)
};


        IEnumerable<string> Moves(char start, char end, (char c, int x, int y)[] keypad)
        {
            var result = new List<string>();
            var startPoint = keypad.First(p => p.c == start);
            var endPoint = keypad.First(p => p.c == end);

            var stack = new Stack<(int x, int y, List<char> path)>();
            stack.Push((startPoint.x, startPoint.y, new List<char>()));

            while (stack.Count > 0)
            {
                var (x, y, path) = stack.Pop();

                if (x == endPoint.x && y == endPoint.y)
                {
                    yield return new string(path.ToArray());
                    continue;
                }

                if (!keypad.Any(p => p.x == x && p.y == y))
                    continue;

                if (x < endPoint.x) stack.Push((x + 1, y, path.Append('>').ToList()));
                if (x > endPoint.x) stack.Push((x - 1, y, path.Append('<').ToList()));
                if (y < endPoint.y) stack.Push((x, y + 1, path.Append('v').ToList()));
                if (y > endPoint.y) stack.Push((x, y - 1, path.Append('^').ToList()));
            }
        }


        private long Cost((char a, char b) pair, int robots, Dictionary<(char a, char b, int robots), long> cache)
        {
            if (cache.TryGetValue((pair.a, pair.b, robots), out var cachedCost))
                return cachedCost;

            var minCost = long.MaxValue;
            var moves = Moves(pair.a, pair.b, dirKeypad);

            foreach (var move in moves)
            {
                var pairs = move.Append('A').Select((c, i) => (a: i == 0 ? 'A' : move[i - 1], b: c)).ToArray();
                var cost = pairs.Select(p => robots == 1 ? 1 : Cost(p, robots - 1, cache)).Sum();
                if (cost < minCost)
                    minCost = cost;
            }

            cache[(pair.a, pair.b, robots)] = minCost;
            return minCost;
        }

        IEnumerable<string> NumpadRobotMoves(string code, char startPosition)
        {
            var end = code[0];
            var moves = Moves(startPosition, end, numKeypad);
            foreach (var move in moves)
            {
                if (code.Length == 1)
                {
                    yield return move + 'A';
                }
                else
                {
                    var nextMoves = NumpadRobotMoves(code.Length > 1 ? code.Substring(1) : string.Empty, end);
                    foreach (var nextMove in nextMoves)
                    {
                        yield return move + 'A' + nextMove;
                    }
                }
            }
        }


        /*public void Part2(object input, bool test, ref object solution)
        {
            string inputText = (string)input;
            List<string> codes = new List<string>();
            Dictionary<string, long> NumericParts = new Dictionary<string, long>();
            foreach (string line in inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None))
            {
                if (!string.IsNullOrEmpty(line))
                {
                    codes.Add(line);
                    NumericParts.Add(line, int.Parse(line.Trim('A')));
                }
            }

            long totalComplexity = 0;
            foreach (string code in codes)
            {
                string NumericRobotSequence;
                List<string> NumericRobotSequenceList = new List<string>();
                List<string> DirectionalPrecedentRobotSequence = new List<string>();
                List<string> DirectionalNewRobotSequence = new List<string>();
                Console.WriteLine($"Code: {code}");
                NumericRobotSequence = CalculateNumericSequenceExtended(code);
                NumericRobotSequenceList = SequenceList(NumericRobotSequence);
                Console.WriteLine($"Numeric Robot: {NumericRobotSequence}");
                ShowMovements(NumericRobotSequenceList, "N");
                CalculateDirectionalSequenceExtended(NumericRobotSequenceList, ref DirectionalPrecedentRobotSequence);
                int nR = test ? 2 : 25; //numero di robot direzionali frapposti tra me e il robot numerico
                Console.WriteLine($"Directional Robot {nR}°: {string.Join("", DirectionalPrecedentRobotSequence)}");
                //ShowMovements(DirectionalPrecedentRobotSequence, "D");
                int r = nR - 1; //-1 perché un robot direzionale è stato già elaborato

                while (r > 0)
                {
                    CalculateDirectionalSequenceExtended(DirectionalPrecedentRobotSequence, ref DirectionalNewRobotSequence);
                    Console.WriteLine($"Directional Robot {r}°: {string.Join("", DirectionalNewRobotSequence)}");
                    //ShowMovements(DirectionalNewRobotSequence, "D");
                    r--;
                }
                long complexity = DirectionalNewRobotSequence.Sum(s => s.Length) * NumericParts[code];
                Console.WriteLine($"{DirectionalNewRobotSequence.Sum(s => s.Length)}*{NumericParts[code]} {complexity}");
                totalComplexity += complexity;

                var commands = DirectionalNewRobotSequence;
                int robot = nR;
                while (robot > 0)
                {
                    string robseq = "";
                    var sequences = CalculateKeyPressSequence(commands);
                    foreach (var sequence in sequences)
                    {
                        robseq += sequence;
                    }
                    commands.Clear();
                    commands = SequenceList(robseq);
                    Console.WriteLine($"robot: {robot}, sequenza: {robseq}");
                    robot--;
                }
                List<string> numericsequence = CalculateNumericKeyPressSequence(commands);
                Console.WriteLine($"Numeric Robot: {string.Join("", numericsequence)}");
            }
            solution = totalComplexity;
            //4167648 too low
            //4341300 too low

            //per la part 1 dovrebbe essere 164960, non 173652
        }


        public static void ShowMovements(List<string> Commands, string type)
        {
            if (type.Equals("N"))
            {
                // Tastierino numerico
                char[,] keypad =
                {
                    { '7', '8', '9' },
                    { '4', '5', '6' },
                    { '1', '2', '3' },
                    { ' ', '0', 'A' }
                };

                int x = 3, y = 2; // Posizione iniziale (A)

                foreach (var command in Commands)
                {
                    bool[,] visited = new bool[4, 3];
                    visited[x, y] = true;
                    foreach (var c in command)
                    {
                        int newX = x, newY = y;

                        switch (c)
                        {
                            case '^': newX = Math.Max(0, x - 1); break; // Su
                            case 'v': newX = Math.Min(3, x + 1); break; // Giù
                            case '<': newY = Math.Max(0, y - 1); break; // Sinistra
                            case '>': newY = Math.Min(2, y + 1); break; // Destra
                        }

                        // Controlla se la nuova posizione è uno spazio vuoto
                        if (keypad[newX, newY] == ' ')
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Errore: movimento non valido verso uno spazio vuoto.");
                            Console.ResetColor();
                            throw new InvalidOperationException($"Movimento non valido verso uno spazio vuoto alla posizione ({newX}, {newY}).");
                        }
                        else
                        {
                            x = newX;
                            y = newY;
                            visited[x, y] = true;
                        }
                    }
                    // Stampa il tastierino
                    Console.WriteLine("Percorso sul tastierino numerico:");
                    for (int i = 0; i < 4; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            if (visited[i, j])
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                            }
                            Console.Write(keypad[i, j] + " ");
                            Console.ResetColor();
                        }
                        Console.WriteLine();
                    }
                    Thread.Sleep(2000);

                }


                Console.WriteLine("Premo la lettera A alla fine.");
            }
            else if (type.Equals("D"))
            {
                // Tastierino direzionale
                char[,] keypad =
                {
                { ' ', '^', 'A' },
                { '<', 'v', '>' }
            };

                int x = 0, y = 2; // Posizione iniziale (A)

                foreach (var command in Commands)
                {
                    bool[,] visited = new bool[2, 3];
                    visited[x, y] = true;

                    foreach (var c in command)
                    {
                        int newX = x, newY = y;

                        switch (c)
                        {
                            case '^': newX = Math.Max(0, x - 1); break; // Su
                            case 'v': newX = Math.Min(1, x + 1); break; // Giù
                            case '<': newY = Math.Max(0, y - 1); break; // Sinistra
                            case '>': newY = Math.Min(2, y + 1); break; // Destra
                        }

                        // Controlla se la nuova posizione è uno spazio vuoto
                        if (keypad[newX, newY] == ' ')
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Errore: movimento non valido verso uno spazio vuoto.");
                            Console.ResetColor();
                            throw new InvalidOperationException($"Movimento non valido verso uno spazio vuoto alla posizione ({newX}, {newY}).");
                        }
                        else
                        {
                            x = newX;
                            y = newY;
                            visited[x, y] = true;
                        }
                    }
                    // Stampa il tastierino
                    Console.WriteLine("Percorso sul tastierino direzionale:");
                    for (int i = 0; i < 2; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            if (visited[i, j])
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                            }
                            Console.Write(keypad[i, j] + " ");
                            Console.ResetColor();
                        }
                        Console.WriteLine();
                    }
                    Thread.Sleep(2000);
                }

                Console.WriteLine("Premo la lettera A alla fine.");
            }
            else
            {
                Console.WriteLine("Tipo di tastierino non supportato.");
            }
        }

        public List<string> CalculateKeyPressSequence(List<string> commands)
        {
            // Definizione del tastierino
            string[,] keypad = new string[,]
            {
                { " ", "^", "A" },
                { "<", "v", ">" }
            };

            // Posizione iniziale (0,2)
            int currentX = 0;
            int currentY = 2;

            // Lista per memorizzare le sequenze di tasti premuti
            List<string> keyPressSequences = new List<string>();

            foreach (string command in commands)
            {
                StringBuilder keyPressSequence = new StringBuilder();

                foreach (char move in command)
                {
                    int newX = currentX, newY = currentY;

                    // Aggiorna la posizione in base al comando
                    switch (move)
                    {
                        case '^': newX--; break; // Su
                        case 'v': newX++; break; // Giù
                        case '<': newY--; break; // Sinistra
                        case '>': newY++; break; // Destra
                        case 'A':
                            keyPressSequence.Append(keypad[currentX, currentY]);
                            continue; // Premere il tasto attuale
                    }

                    // Verifica se il movimento è valido
                    if (newX >= 0 && newX < 2 && newY >= 0 && newY < 3 && !(newX == 0 && newY == 0))
                    {
                        // Aggiorna la posizione solo se valida
                        currentX = newX;
                        currentY = newY;
                    }
                    else
                    {
                        throw new Exception("Movimento non ammesso");
                    }
                }

                // Aggiunge la sequenza dei tasti premuti per questo comando
                keyPressSequences.Add(keyPressSequence.ToString());
            }

            return keyPressSequences;
        }
        public List<string> CalculateNumericKeyPressSequence(List<string> commands)
        {
            // Definizione del tastierino
            string[,] keypad = new string[,]
            {
        { "7", "8", "9" },
        { "4", "5", "6" },
        { "1", "2", "3" },
        { " ", "0", "A" },
            };

            // Posizione iniziale (3,2)
            int currentX = 3;
            int currentY = 2;

            // Lista per memorizzare le sequenze di tasti premuti
            List<string> keyPressSequences = new List<string>();

            foreach (string command in commands)
            {
                StringBuilder keyPressSequence = new StringBuilder();

                foreach (char move in command)
                {
                    int newX = currentX, newY = currentY;

                    // Aggiorna la posizione in base al comando
                    switch (move)
                    {
                        case '^': newX--; break; // Su
                        case 'v': newX++; break; // Giù
                        case '<': newY--; break; // Sinistra
                        case '>': newY++; break; // Destra
                        case 'A':
                            keyPressSequence.Append(keypad[currentX, currentY]);
                            continue; // Premere il tasto attuale
                    }

                    // Verifica se il movimento è valido
                    if (newX >= 0 && newX < 4 && newY >= 0 && newY < 3 && !(newX == 3 && newY == 0))
                    {
                        // Aggiorna la posizione solo se valida
                        currentX = newX;
                        currentY = newY;
                    }
                    else
                    {
                        throw new Exception("Movimento non ammesso");
                    }
                }

                // Aggiunge la sequenza dei tasti premuti per questo comando
                keyPressSequences.Add(keyPressSequence.ToString());
            }

            return keyPressSequences;
        }


        public static List<string> SequenceList(string sequenceString)
        {
            List<string> ret = new List<string>();
            string sequenceToProcess = "";
            for (int l = 0; l < sequenceString.Length; l++)
            {
                if (sequenceString[l] != 'A')
                {
                    sequenceToProcess += sequenceString[l];
                }
                else
                {
                    sequenceToProcess += sequenceString[l];
                    while (l < sequenceString.Length - 1 && sequenceString[l + 1] == 'A')
                    {
                        sequenceToProcess += sequenceString[l + 1];
                        l++;
                    }

                    ret.Add(sequenceToProcess);
                    sequenceToProcess = "";
                }
            }

            return ret;
        }

        // Calcola la sequenza di movimenti per il tastierino numerico
        static string CalculateNumericSequenceExtended(string code)
        {
            string type = "N";
            var sequence = new List<string>();
            (int x, int y) currentPos = NumericKeypad['A']; // Posizione iniziale

            for (int i = 0; i < code.Length; i++)
            {
                char key = code[i];
                (int targetX, int targetY) = NumericKeypad[key];

                // Aggiungi il movimento verso il tasto
                sequence.Add(GenerateMovementExtended(currentPos, (targetX, targetY), type));
                if (sequence.Last() == "") sequence.RemoveAt(sequence.Count - 1);
                sequence.Add("A"); // Premere il tasto

                // Aggiorna la posizione corrente
                currentPos = (targetX, targetY);
            }
            return string.Join("", sequence);
        }


        public static MemoryCache cacheSequence = MemoryCache.Default;
        static void CalculateDirectionalSequenceExtended(List<string> inputSequence, ref List<string> outputSequence)
        {
            string type = "D"; //Directional
            (int x, int y) currentPos = DirectionalKeypad['A']; // Posizione iniziale

            foreach (var s in inputSequence)
            {
                string cacheKey = $"{currentPos.x},{currentPos.y},{s}";
                if (cacheSequence.Contains(cacheKey))
                {
                    var cachedResult = (Tuple<string, (int x, int y)>)cacheSequence.Get(cacheKey);
                    outputSequence.AddRange(SequenceList(cachedResult.Item1));
                    currentPos = cachedResult.Item2;
                }
                else
                {
                    var blockSequence = new StringBuilder();
                    foreach (char command in s)
                    {
                        (int targetX, int targetY) = DirectionalKeypad[command];
                        blockSequence.Append($"{GenerateMovementExtended(currentPos, (targetX, targetY), type)}A");
                        currentPos = (targetX, targetY);
                    }

                    string blockSequenceStr = blockSequence.ToString();
                    outputSequence.AddRange(SequenceList(blockSequenceStr));
                    cacheSequence.Add(cacheKey, Tuple.Create(blockSequenceStr, currentPos), DateTimeOffset.UtcNow.AddMinutes(10)); //Token, sequenza di uscita, posizione di uscita
                }
            }
        }
        public static MemoryCache cache = MemoryCache.Default;

        static string GenerateMovementExtended((int x, int y) start, (int x, int y) target, string type)
        {
            //La chiave è formata da start.x:start.y:target.x:target.y:type
            string cacheKey = $"{start.x}:{start.y}:{target.x}:{target.y}:{type}";

            // Verifica ed eventuale recupero di un elemento dalla cache
            string cachedValue = cache.Get(cacheKey) as string;
            if (cachedValue != null)
            {
                //Console.WriteLine("Valore recuperato dalla cache: " + cachedValue);
                return cachedValue;
            }
            else
            {
                //Console.WriteLine("Valore non trovato nella cache.");
            }

            var movements = new List<char>();

            if (type == "N")
            {
                bool leri = start.y != target.y;
                bool updo = start.x != target.x;
                bool down = target.x > start.x;
                bool up = target.x < start.x;
                bool left = target.y < start.y;
                bool right = target.y > start.y;
                bool bottomRow = start.x == 3;
                bool farLeft = start.y == 0;
                if (bottomRow && left && up)
                {
                    while (start.x > target.x) { movements.Add('^'); start.x--; }
                    while (start.x < target.x) { movements.Add('v'); start.x++; }
                    while (start.y < target.y) { movements.Add('>'); start.y++; }
                    while (start.y > target.y) { movements.Add('<'); start.y--; }
                }
                else if (farLeft && down)
                {
                    while (start.y < target.y) { movements.Add('>'); start.y++; }
                    while (start.y > target.y) { movements.Add('<'); start.y--; }
                    while (start.x > target.x) { movements.Add('^'); start.x--; }
                    while (start.x < target.x) { movements.Add('v'); start.x++; }
                }
                else if (up && left)
                {
                    while (start.y < target.y) { movements.Add('>'); start.y++; }
                    while (start.y > target.y) { movements.Add('<'); start.y--; }
                    while (start.x < target.x) { movements.Add('v'); start.x++; }
                    while (start.x > target.x) { movements.Add('^'); start.x--; }
                }
                else if (down && left)
                {
                    while (start.y < target.y) { movements.Add('>'); start.y++; }
                    while (start.y > target.y) { movements.Add('<'); start.y--; }
                    while (start.x < target.x) { movements.Add('v'); start.x++; }
                    while (start.x > target.x) { movements.Add('^'); start.x--; }

                }
                else if (down && right)
                {
                    while (start.x < target.x) { movements.Add('v'); start.x++; }
                    while (start.x > target.x) { movements.Add('^'); start.x--; }
                    while (start.y < target.y) { movements.Add('>'); start.y++; }
                    while (start.y > target.y) { movements.Add('<'); start.y--; }

                }
                else if (up && right)
                {
                    while (start.x < target.x) { movements.Add('v'); start.x++; }
                    while (start.x > target.x) { movements.Add('^'); start.x--; }
                    while (start.y < target.y) { movements.Add('>'); start.y++; }
                    while (start.y > target.y) { movements.Add('<'); start.y--; }
                }

                else
                {
                    while (start.x != target.x || start.y != target.y)
                    {
                        while (start.y < target.y) { movements.Add('>'); start.y++; }
                        while (start.y > target.y && !(start.x == 3 && start.y - 1 == 0)) { movements.Add('<'); start.y--; }
                        while (start.x < target.x && !(start.y == 0 && start.x + 1 == 3)) { movements.Add('v'); start.x++; }
                        while (start.x > target.x) { movements.Add('^'); start.x--; }

                    }
                }
            }
            else if (type == "D")
            {
                bool leri = start.y != target.y;
                bool updo = start.x != target.x;
                bool down = target.x > start.x;
                bool up = target.x < start.x;
                bool left = target.y < start.y;
                bool right = target.y > start.y;
                bool bottomRow = start.x == 3;
                bool farthestLeft = start.y == 0 && start.x == 1;
                bool TofarthestLeft = target.y == 0 && target.x == 1;
                if (farthestLeft)
                {
                    while (start.y < target.y) { movements.Add('>'); start.y++; }
                    while (start.y > target.y) { movements.Add('<'); start.y--; }
                    while (start.x < target.x) { movements.Add('v'); start.x++; }
                    while (start.x > target.x) { movements.Add('^'); start.x--; }
                }
                else if (up && left)
                {
                    while (start.y < target.y) { movements.Add('>'); start.y++; }
                    while (start.y > target.y) { movements.Add('<'); start.y--; }
                    while (start.x < target.x) { movements.Add('v'); start.x++; }
                    while (start.x > target.x) { movements.Add('^'); start.x--; }
                }
                else if (up && right)
                {
                    while (start.x < target.x) { movements.Add('v'); start.x++; }
                    while (start.x > target.x) { movements.Add('^'); start.x--; }
                    while (start.y < target.y) { movements.Add('>'); start.y++; }
                    while (start.y > target.y) { movements.Add('<'); start.y--; }
                }

                else if (TofarthestLeft)
                {
                    while (start.x < target.x) { movements.Add('v'); start.x++; }
                    while (start.x > target.x) { movements.Add('^'); start.x--; }
                    while (start.y < target.y) { movements.Add('>'); start.y++; }
                    while (start.y > target.y) { movements.Add('<'); start.y--; }
                }
                else if (down && left)
                {
                    while (start.y < target.y) { movements.Add('>'); start.y++; }
                    while (start.y > target.y) { movements.Add('<'); start.y--; }
                    while (start.x < target.x) { movements.Add('v'); start.x++; }
                    while (start.x > target.x) { movements.Add('^'); start.x--; }

                }
                else if (down && right)
                {
                    while (start.x < target.x) { movements.Add('v'); start.x++; }
                    while (start.x > target.x) { movements.Add('^'); start.x--; }
                    while (start.y < target.y) { movements.Add('>'); start.y++; }
                    while (start.y > target.y) { movements.Add('<'); start.y--; }

                }
                else
                {
                    while (start.x != target.x || start.y != target.y)
                    {
                        while (start.y < target.y) { movements.Add('>'); start.y++; }
                        while (start.y > target.y && !(start.x == 0 && start.y - 1 == 0)) { movements.Add('<'); start.y--; }
                        while (start.x < target.x) { movements.Add('v'); start.x++; }
                        while (start.x > target.x && !(start.y == 0 && start.x - 1 == 0)) { movements.Add('^'); start.x--; }
                    }
                }
            }

            string ret = string.Join("", movements);

            string cacheValue = ret;
            CacheItemPolicy policy = new CacheItemPolicy
            {
                AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(5) // Scadenza dopo 5 minuti
            };
            cache.Add(cacheKey, cacheValue, policy);

            return ret;
        }
        /*Da REDDIT: 
        So, it turns out a greedy-ish algorithm completely works on Day 21 (on both parts, but since you don't really need to worry about that for Part 1, I labeled this as part 2).
        By "greedy-ish", however, we can't be short sighted. We don't want to be greedy from n to n+1, we actually need to be greedy from n to n+4. Here's how this goes down.
        Basically, think of every movement between buttons as going from "From" (the button you are starting at) to the button "To" (the button you are going to), we can define our greedy algorithm as follows.
        Every direction is made up of an *updo* and a *leri* string.
        *Updo:* Either an up or a down, "\^\^", "v"- this is "down" if *from* is on a "higher" row and *to*
        *Leri:* Either a left or a right: "<", ">>>", etc.  - this is "left" if *from* is to the \\*right\\* of *to*
        Note that updo/leri can be empty when *from and to* are on the same row/column respectively
        So, for instance, if you are on the number pad going from "A" to "5", your *updo* "\^\^" and your leri is "<"
        We never want to "mix" our updos and our leris ("<\^\^<" is always worse than "<<\^\^"), it's always better to concatenate them. *The question is which comes first, the updo or the leri?*
        If either updo or leri is empty, our job is easy: just return the other one.
        *NUMPAD EXCLUSIVE RULE*
        If you are on the bottom row *and* going to the left column -> *updo + leri*
        If you are in the far-left column *and* travelling to the bottom row -> *leri + updo*
        This is necessary to avoid cutting the corner.
        *DIRECTION PAD EXCLUSIVE RULE*
        If you are starting on the farthest left column (meaning you are starting on the "<" key) -> *leri + updo*
        If you are traveling to the farthest left column (meaning you are starting on the "<" key) -> *updo + leri*
        *GENERAL CASE RULES*
        From here, we have to dig a little deeper. We can categorize are updo as either an "Up" and "Down" and our leri as either a "Left" or a "Right". But at this point a pattern emerges.
        Let's consider the combination of an *Up* updo and a *Left* leri - i.e., which is better, *"\^<" or "<\^"*
        It turns out, when possible, *Left + Up* is always equal to or better \\*when possible\\* (specifically, when it doesn't conflict with the "don't enter the empty square" rule. This difference grows the further down the depth you go. This is also true for all quantities of \^ and < we could see (which is at most 3 ups and 2 lefts on the numberpad and 1 up and 2 lefts on the direction pad.
        Using this as a model, we can create our preference for diagonal paths.
        |Path|Updo|Leri|Best Order|
        |:-|:-|:-|:-|
        |UP-LEFT|Up|Left|leri + updo|
        |DOWN-LEFT|Down|Left|leri + updo|
        |DOWN-RIGHT|Down|Right|updo + leri|
        |UP-RIGHT|Up|Right|updo + leri|
        Now, let me tell you. UP-RIGHT is a right old jerk. UP-RIGHT will make you think "Oh, it doesn't matter, it's the same". It lulls you in, promising a Part 1 completion. In fact, either "updo + leri" or "leri+updo" for Up-right will work on Part 1, at 2 levels of robots.
        It will even work at 3 levels of robots.
        But at level 4, finally, they start to diverge. Updo+leri ends up with a lower cost than leri + updo
        And that's it. This greedy algorithm works! No need for searching! Well, kinda. You still cannot actually store the total instructions, so you still have to do a depth-first-search, and you \\*definitely\\* need memoization here. But this greedy algorithm is, to me, much easier to implement than a search, and much faster.
        Yes, it's more code because you have to handle special cases, but on my computer using kotlin, my runtime for part 1 and 2 combined was just 54ms, which is pretty dogone fast.*/

        /*static void WriteOutputToFile(string output, string outputfilepath)
        {
            // Scrive l'output progressivo su file per evitare il sovraccarico di memoria
            using (StreamWriter writer = new StreamWriter(outputfilepath, append: true))
            {
                writer.Write(output);
                writer.Flush();
            }
        }
        static string ReadOutputFromFile(string outputFilePath)
        {
            // Legge il contenuto completo del file
            using (StreamReader reader = new StreamReader(outputFilePath))
            {
                return reader.ReadToEnd();
            }
        }*/



    }
}

