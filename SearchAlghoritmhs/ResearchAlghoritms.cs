using AOC.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AOC.SearchAlghoritmhs
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class ResearchAlgorithmsAttribute : Attribute
    {
        public ResearchAlgorithmsAttribute(TypologyEnum typology, ResolutionEnum resolution = ResolutionEnum.None)
        {
            Typology = typology;
            Resolution = resolution;
        }

        public ResearchAlgorithmsAttribute(ResolutionEnum resolution)
        {
            Resolution = resolution;
        }

        public TypologyEnum Typology { get; }
        public ResolutionEnum Resolution { get; }

        [Flags]
        public enum TypologyEnum
        {
            None = 0,
            Hashing = 1 << 0,             // Algoritmi di criptazione/decriptazione
            BinaryOperation = 1 << 1,     // Operazioni sui valori di tipo binario
            Game = 1 << 2,                // Rifacimento di giochi classici
            JSON = 1 << 3,                // Gestione di oggetti di tipo JSON
            Map = 1 << 4,                 // Problemi basati su mappe o grafi
            Escaping = 1 << 5,            // Gestione dei caratteri di escape
            TextRules = 1 << 6,           // Gestione delle regole di formattazione del testo
            Cronometers = 1 << 7,         // Calcoli temporali
            Ingredients = 1 << 8,         // Problemi legati a ingredienti o oggetti
            Combinatorial = 1 << 9,       // Problemi combinatori, raggruppamenti
            Decompressing = 1 << 10,      // Algoritmi di decompressione
            Regex = 1 << 11,              // Problemi legati a espressioni regolari
            Reduction = 1 << 12,          // Problemi di riduzione o semplificazione
            Recursive = 1 << 13,          // Problemi risolvibili ricorsivamente
            MachineInstructions = 1<< 14, // Simula un sistema che esegue una serie di istruzioni su uno o più registri. Ogni istruzione modifica i registri tramite operazioni aritmetiche (come incremento, divisione, moltiplicazione) o condiziona il flusso di esecuzione (come salti condizionati in base a valori o proprietà dei registri). La simulazione prosegue eseguendo le istruzioni fino al completamento, applicando logiche di controllo basate sullo stato dei registri
            Keypad = 1 << 15,             // Simula l'utilizzo di un keypad
            Triangles = 1 << 16,          // Trova i triangoli
            Gate = 1 << 17,               // Gate da cui provengono input e che producono output
        }

        [Flags]
        public enum ResolutionEnum
        {
            None = 0,
            AlphaStar = 1 << 0,
            Dijkstra = 1 << 1,
            Drawing = 1 << 2,             // Schematizzazione visiva
            DFS = 1 << 3,                 // Depth-First Search (DFS) DFS esplora i nodi in profondità, seguendo un percorso fino al suo termine prima di tornare indietro ed esplorare altri percorsi. Opzione con BackTracking (opzionale del BackTracking è il Pruning)
            BFS = 1 << 4,                 // Breadth-First Search (BFS) BFS esplora i nodi di un grafo o albero livello per livello, iniziando dal nodo di partenza e visitando tutti i suoi vicini prima di passare ai vicini di livello successivo
            SystemLinearEquations = 1 << 5, // Equazioni lineari
            Cache = 1 << 6,               // Ottimizzazione tramite cache
            Overflow = 1 << 7,             // Problemi di overflow numerico, richiedono considerazioni matematiche o utilizzo di strutture dati adatte
            NumberTheory = 1 << 8         //Problemi basati su proprietà matematiche dei numeri, inclusi divisori, multipli, fattorizzazione, teoria dei resti, congruenze e relazioni numeriche che richiedono ottimizzazioni o calcoli specifici legati alla struttura dei numeri stessi
        }

        public override string ToString()
        {
            return $"Typology: {Typology}, Resolution: {Resolution}";
        }

        public static List<string> SearchFolder(TypologyEnum? typology = null, ResolutionEnum? resolution = null)
        {
            var interestedClasses = new List<string>();
            var baseFolder = "C:\\Users\\s.cecere\\source\\repos\\AOC\\AOC\\Sources\\";

            try
            {
                // Ottieni le directory per ogni anno
                var yearFolders = Directory.GetDirectories(baseFolder);

                foreach (var year in yearFolders)
                {
                    var yearName = Path.GetFileName(year);
                    var files = Directory.GetFiles(year);

                    foreach (var file in files)
                    {
                        var fileName = Path.GetFileNameWithoutExtension(file);

                        // Crea dinamicamente l'istanza del tipo
                        var fullTypeName = $"AOC{yearName}.{fileName}";
                        var type = Type.GetType(fullTypeName);

                        if (type == null || !typeof(IDay).IsAssignableFrom(type))
                            continue;

                        // Ottieni gli attributi della classe
                        var attributes = (ResearchAlgorithmsAttribute[])Attribute.GetCustomAttributes(type, typeof(ResearchAlgorithmsAttribute));

                        if (attributes.Length > 0)
                        {
                            foreach (var attribute in attributes)
                            {
                                // Filtra in base ai parametri forniti
                                if ((typology == null || attribute.Typology == typology) &&
                                    (resolution == null || attribute.Resolution == resolution))
                                {
                                    interestedClasses.Add($"{yearName}_{ fileName}");
                                    break; // Basta aggiungere una volta la classe
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Errore durante la ricerca: {ex.Message}");
            }

            return interestedClasses;
        }
    }
}
