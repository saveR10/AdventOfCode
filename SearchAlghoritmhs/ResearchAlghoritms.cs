using AOC.Documents.LINQ;
using AOC.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Security.Policy;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace AOC.SearchAlghoritmhs
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class ResearchAlgorithmsAttribute : Attribute
    {
        public ResearchAlgorithmsAttribute(TypologyEnum typology, ResolutionEnum resolution = ResolutionEnum.None, DifficultEnum difficult = default)
        {
            Typology = typology;
            Resolution = resolution;
            Difficult = difficult;
        }
        public ResearchAlgorithmsAttribute(DifficultEnum difficult = default)
        {
            Difficult = difficult;
        }

        public ResearchAlgorithmsAttribute(ResolutionEnum resolution)
        {
            Resolution = resolution;
        }

        public TypologyEnum Typology { get; }
        public ResolutionEnum Resolution { get; }
        public DifficultEnum Difficult { get; }

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
            /// <summary>
            /// AlphaStar è un approccio euristico che, pur non essendo un algoritmo specifico, è ispirato a metodi avanzati di ricerca e ottimizzazione, in particolare a tecniche come quelle utilizzate da AlphaZero per giochi complessi come il Go. AlphaStar si basa su una combinazione di metodi di ricerca approssimativa e intelligente, come la ricerca A* o tecniche simili, che permettono di esplorare soluzioni in modo più rapido, ma non necessariamente ottimale. Caratteristiche principali di AlphaStar (basato su A):* Usa euristiche per guidare la ricerca in modo più intelligente. Non garantisce sempre la soluzione ottimale, ma può trovare soluzioni quasi ottimali in un tempo significativamente inferiore, specialmente in ambienti complessi o in spazi di ricerca vasti. È spesso utilizzato quando è necessario bilanciare il tempo di calcolo con la qualità della soluzione (ad esempio, nei giochi complessi o nei problemi di ricerca in spazi molto ampi).
            /// </summary>
            AlphaStar = 1 << 0,           
            /// <summary>
            /// L'algoritmo di Dijkstra è un algoritmo di ricerca per determinare il cammino più breve da un nodo di partenza a tutti gli altri nodi di un grafo, utilizzando un approccio greedy (avidità). Dijkstra funziona così: Ogni nodo viene visitato esattamente una volta. I nodi vengono esplorati in ordine crescente di distanza (costo) dal nodo di partenza. È garantito che, al momento in cui un nodo viene visitato, il cammino per quel nodo è il più corto possibile. Caratteristiche principali di Dijkstra: Funziona con grafi a pesatura non negativa (cioè, i pesi degli archi devono essere ≥ 0). È ottimale e trova sempre la soluzione migliore(il cammino più breve). Viene usato per risolvere problemi di percorso in situazioni dove l'ottimalità è importante. La differenza principale tra AlphaStar e Dijkstra sta nell'approccio che adottano per risolvere un problema di ricerca, in particolare nei contesti di algoritmi di percorso o ottimizzazione.       La principale differenza: Dijkstra garantisce sempre la soluzione ottimale (cammino più breve), ma può essere lento per grafi di grandi dimensioni. AlphaStar, invece, è un approccio approssimativo che può ridurre il tempo di calcolo sfruttando euristiche intelligenti per trovare soluzioni "buone" o "quasi ottimali", ma non garantisce sempre la soluzione perfetta. In breve: Dijkstra è un algoritmo preciso e ottimale per trovare il percorso più breve. AlphaStar è un algoritmo approssimativo che cerca di esplorare soluzioni buone in tempi più rapidi, utilizzando strategie basate su euristiche.
            /// </summary>
            Dijkstra = 1 << 1,
            /// <summary>
            /// Schematizzazione visiva
            /// </summary>
            Drawing = 1 << 2,             
            DFS = 1 << 3,                 // Depth-First Search (DFS) DFS esplora i nodi in profondità, seguendo un percorso fino al suo termine prima di tornare indietro ed esplorare altri percorsi. Opzione con BackTracking (opzionale del BackTracking è il Pruning)
            BFS = 1 << 4,                 // Breadth-First Search (BFS) BFS esplora i nodi di un grafo o albero livello per livello, iniziando dal nodo di partenza e visitando tutti i suoi vicini prima di passare ai vicini di livello successivo
            SystemLinearEquations = 1 << 5, // Equazioni lineari
            Cache = 1 << 6,               // Ottimizzazione tramite cache
            Overflow = 1 << 7,             // Problemi di overflow numerico, richiedono considerazioni matematiche o utilizzo di strutture dati adatte
            NumberTheory = 1 << 8         //Problemi basati su proprietà matematiche dei numeri, inclusi divisori, multipli, fattorizzazione, teoria dei resti, congruenze e relazioni numeriche che richiedono ottimizzazioni o calcoli specifici legati alla struttura dei numeri stessi
        }
        [Flags]
        public enum DifficultEnum
        {
            None = 0,
            VeryEasy = 1 << 0,
            Easy = 1 << 1,
            Medium = 1 <<2,
            Hard = 1 << 3,
            VeryHard = 1 << 4,
            Legend = 1 << 5,
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
