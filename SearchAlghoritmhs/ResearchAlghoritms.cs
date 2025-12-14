using AOC.Documents.LINQ;
using AOC.Model;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace AOC.SearchAlghoritmhs
{
    /// <summary>
    /// Attributo utilizzato per classificare ogni puzzle con:
    /// - Tipologia del problema (es: mappe, hashing, JSON, clustering, ecc.)
    /// - Algoritmo / tecnica di risoluzione utilizzata (es: BFS, DFS, Dijkstra, DP, ecc.)
    /// - Difficoltà percepita
    /// 
    /// Può essere applicato sia a classi che a metodi.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    #region ==== COSTRUTTORI ====

    public class ResearchAlgorithmsAttribute : Attribute
    {
        /// <summary>
        /// Costruttore canonico completo: permette di specificare titolo, tipologia, algoritmo di risoluzione, difficoltà ed eventuali note.
        /// </summary>
        public ResearchAlgorithmsAttribute(string title, TypologyEnum typology, ResolutionEnum resolution = ResolutionEnum.None, DifficultEnum difficult = default, string note = null)
        {
            Title = title;
            Typology = typology;
            Resolution = resolution;
            Difficult = difficult;
            Note = note;
        }
        /// <summary>
        /// Costruttore completo: permette di specificare tipologia, algoritmo di risoluzione e difficoltà.
        /// </summary>
        public ResearchAlgorithmsAttribute(TypologyEnum typology, ResolutionEnum resolution = ResolutionEnum.None, DifficultEnum difficult = default)
        {
            Typology = typology;
            Resolution = resolution;
            Difficult = difficult;
        }
        /// <summary>
        /// Costruttore rapido: permette di specificare solo la difficoltà.
        /// </summary>
        public ResearchAlgorithmsAttribute(DifficultEnum difficult = default)
        {
            Difficult = difficult;
        }

        public ResearchAlgorithmsAttribute(ResolutionEnum resolution)
        {
            Resolution = resolution;
        }
        #endregion

        #region ==== PROPRIETÀ ====

        /// <summary>
        /// Tipologia del problema (strutture dati, mappe, clustering, giochi, compressione, ecc.)
        /// </summary>
        public TypologyEnum Typology { get; }
        /// <summary>
        /// Tecnica o algoritmo utilizzato per risolvere il problema (BFS, DFS, A*, Dijkstra, DP, ecc.).
        /// </summary>
        public ResolutionEnum Resolution { get; }
        /// <summary>
        /// Stima della difficoltà percepita nel risolvere il puzzle.
        /// </summary>
        public DifficultEnum Difficult { get; }
        /// <summary>
        /// Titolo del puzzle
        /// </summary>
        public string Title { get; }
        /// <summary>
        /// Note sul puzzle
        /// </summary>
        public string Note { get; }
        #endregion

        #region ==== ENUM: TIPOLOGIA ====

        /// <summary>
        /// Categorizza il tipo di puzzle o problema.
        /// </summary>
        [Flags]
        public enum TypologyEnum
        {
            None = 0,
            /// <summary>
            /// Algoritmi di criptazione/decriptazione
            /// </summary>
            Hashing = 1 << 0,             
            BinaryOperation = 1 << 1,     // Operazioni bitwise o confronti binari
            /// <summary>
            /// Simulazioni o ricostruzioni di giochi
            /// </summary>
            Game = 1 << 2,
            JSON = 1 << 3,                // Parsing o gestione di formati JSON
            Map = 1 << 4,                 // Problemi basati su mappe, griglie o grafi
            Escaping = 1 << 5,            // Gestione dei caratteri di escape
            /// <summary>
            /// Riconoscimento e applicazione di regole testuali
            /// </summary>
            TextRules = 1 << 6,           
            /// <summary>
            /// Problemi temporali o sincronizzazioni
            /// </summary>
            Cronometers = 1 << 7,
            /// <summary>
            /// Liste di ingredienti, oggetti, inventari
            /// </summary>
            Ingredients = 1 << 8,
            /// <summary>
            /// Permutazioni, combinazioni, insiemi, grouping
            /// </summary>
            Combinatorial = 1 << 9,
            /// <summary>
            /// Algoritmi di decompressione
            /// </summary>
            Decompressing = 1 << 10,      
            /// <summary>
            /// Problemi risolvibili per ricorsione
            /// </summary>
            Recursive = 1 << 11,
            /// <summary>
            /// Simula un sistema che esegue una serie di istruzioni su uno o più registri. 
            /// Ogni istruzione modifica i registri tramite operazioni aritmetiche (come incremento, divisione, moltiplicazione) o condiziona il flusso di esecuzione (come salti condizionati in base a valori o proprietà dei registri). 
            /// La simulazione prosegue eseguendo le istruzioni fino al completamento, applicando logiche di controllo basate sullo stato dei registri
            /// </summary>
            MachineInstructions = 1 << 12,
            /// <summary>
            /// Simulazione tastierini/keypad. Molto simile a MachineInstructions.
            /// </summary>
            Keypad = 1 << 13,
            /// <summary>
            /// Calcoli geometrici legati a triangoli
            /// </summary>
            Triangles = 1 << 14,          
            Gate = 1 << 15,               // Simulazione logica di gate in->out
            /// <summary>
            /// Problemi di overflow numerico, richiedono considerazioni matematiche o utilizzo di strutture dati adatte
            /// </summary>
            Overflow = 1 << 16,           
            /// <summary>
            /// Clustering è una famiglia di algoritmi che raggruppano elementi in insiemi (cluster)
            /// sulla base della loro vicinanza o similitudine. Nel contesto del problema Day 8,
            /// si calcolano tutte le distanze tra coppie di punti nello spazio e si uniscono iterativamente
            /// i due punti più vicini usando una struttura Union-Find. Questo processo è equivalente a
            /// costruire una Minimum Spanning Forest parziale ordinando gli archi per distanza crescente.
            /// Caratteristiche principali:
            /// - Usa distanze metriche (euclidee nel problema).
            /// - Ordina gli archi in base alla lunghezza (approccio tipo Kruskal).
            /// - Unisce i nodi tramite Union-Find per formare cluster sempre più grandi.
            /// - Non cerca percorsi ottimali come Dijkstra, né usa euristiche come AlphaStar:
            ///   è puro clustering gerarchico agglomerativo basato su distanze.
            /// </summary>
            Clustering = 1 << 17,
            /// <summary>
            /// Problemi che sono apparentemente fuori scala di risoluzione! E per risolverli c'è un modo banale e immediato.
            /// </summary>
            Trolling = 1 << 18,
            /// <summary>
            /// Problemi basati su grafi generali: nodi connessi da archi pesati o non pesati, esplorazione dei percorsi, cammini minimi, alberi di copertura, ecc.
            /// </summary>
            Graph = 1 << 19,
        }
        #endregion

        #region ==== ENUM: RISOLUZIONE ====

        /// <summary>
        /// Categorizza l'algoritmo utilizzato per risolvere il puzzle.
        /// </summary>
        [Flags]
        public enum ResolutionEnum
        {
            None = 0,
            /// <summary>
            /// AlphaStar è un approccio euristico che, pur non essendo un algoritmo specifico, è ispirato a metodi avanzati di ricerca e ottimizzazione, in particolare a tecniche come quelle utilizzate da AlphaZero per giochi complessi come il Go. 
            /// AlphaStar si basa su una combinazione di metodi di ricerca approssimativa e intelligente, come la ricerca A* o tecniche simili, che permettono di esplorare soluzioni in modo più rapido, ma non necessariamente ottimale. 
            /// Caratteristiche principali di AlphaStar (basato su A):
            /// * Usa euristiche per guidare la ricerca in modo più intelligente. 
            /// Non garantisce sempre la soluzione ottimale, ma può trovare soluzioni quasi ottimali in un tempo significativamente inferiore, specialmente in ambienti complessi o in spazi di ricerca vasti. 
            /// È spesso utilizzato quando è necessario bilanciare il tempo di calcolo con la qualità della soluzione (ad esempio, nei giochi complessi o nei problemi di ricerca in spazi molto ampi).
            /// </summary>
            AlphaStar = 1 << 0,
            /// <summary>
            /// L'algoritmo di Dijkstra è un algoritmo di ricerca per determinare il cammino più breve da un nodo di partenza a tutti gli altri nodi di un grafo, utilizzando un approccio greedy (avidità). 
            /// Dijkstra funziona così: 
            ///     Ogni nodo viene visitato esattamente una volta. 
            ///     I nodi vengono esplorati in ordine crescente di distanza (costo) dal nodo di partenza. 
            ///     È garantito che, al momento in cui un nodo viene visitato, il cammino per quel nodo è il più corto possibile. 
            /// Caratteristiche principali di Dijkstra: 
            ///     Funziona con grafi a pesatura non negativa (cioè, i pesi degli archi devono essere ≥ 0). 
            ///     È ottimale e trova sempre la soluzione migliore(il cammino più breve). 
            ///     Viene usato per risolvere problemi di percorso in situazioni dove l'ottimalità è importante. 
            /// La differenza principale tra AlphaStar e Dijkstra sta nell'approccio che adottano per risolvere un problema di ricerca, in particolare nei contesti di algoritmi di percorso o ottimizzazione.       
            /// La principale differenza: Dijkstra garantisce sempre la soluzione ottimale (cammino più breve), ma può essere lento per grafi di grandi dimensioni. AlphaStar, invece, è un approccio approssimativo che può ridurre il tempo di calcolo sfruttando euristiche intelligenti per trovare soluzioni "buone" o "quasi ottimali", ma non garantisce sempre la soluzione perfetta. 
            /// In breve: 
            ///     Dijkstra è un algoritmo preciso e ottimale per trovare il percorso più breve. 
            ///     AlphaStar è un algoritmo approssimativo che cerca di esplorare soluzioni buone in tempi più rapidi, utilizzando strategie basate su euristiche.
            /// </summary>
            Dijkstra = 1 << 1,
            /// <summary>
            /// Produzione di schemi grafici o rappresentazioni visuali del problema.            
            /// </summary>
            Drawing = 1 << 2,
            /// <summary>
            /// Depth-First Search (DFS) DFS esplora i nodi in profondità, seguendo un percorso fino al suo termine prima di tornare indietro ed esplorare altri percorsi. Opzione con BackTracking (opzionale del BackTracking è il Pruning).
            /// </summary>
            DFS = 1 << 3,
            /// <summary>
            /// Breadth-First Search (BFS) BFS esplora i nodi di un grafo o albero livello per livello, iniziando dal nodo di partenza e visitando tutti i suoi vicini prima di passare ai vicini di livello successivo.
            /// </summary>
            BFS = 1 << 4,
            /// <summary>
            /// Sistemi lineari
            /// </summary>
            SystemLinearEquations = 1 << 5,
            /// <summary>
            /// Memorizzazione e memoization
            /// </summary>
            Cache = 1 << 6,
            /// <summary>
            /// Problemi basati su proprietà matematiche dei numeri, inclusi divisori, multipli, fattorizzazione, teoria dei resti, congruenze e relazioni numeriche che richiedono ottimizzazioni o calcoli specifici legati alla struttura dei numeri stessi.
            /// </summary>
            NumberTheory = 1 << 7,
            /// <summary>
            /// Uso di Reflection per accesso dinamico.
            /// </summary>
            Reflection = 1 << 8,
            /// <summary>
            /// Parsing tramite espressioni regolari
            /// </summary>
            Regex = 1 << 9,
            /// <summary>
            /// Modular arithmetic involves operations on numbers where the results "wrap around" upon reaching a certain value, called the modulus. It is often referred to informally as "clock arithmetic" because of its similarity to the way hours wrap around on a clock.
            /// </summary>
            ModularArithmetic = 1 << 10,
            /// <summary>
            /// Dynamic Programming: nella programmazione dinamica calcolo semplicemente un valore in un determinato stato. Non mi serve aggiungere la complessità deò DFS o BFS.
            /// </summary>
            DP = 1 << 11,
            /// <summary>
            /// SMT / Constraint Solver (es. Z3).
            /// 
            /// Risoluzione del problema tramite un solver di vincoli logici e matematici
            /// (Satisfiability Modulo Theories).
            /// 
            /// Il problema viene modellato dichiarando:
            /// - variabili (intere, booleane, reali, ecc.)
            /// - vincoli (uguaglianze, disuguaglianze, somme, limiti)
            /// - una funzione obiettivo da minimizzare o massimizzare
            /// 
            /// Il solver si occupa di:
            /// - trovare una soluzione valida se esiste
            /// - garantire l'ottimalità se richiesto (Optimize)
            /// 
            /// Tipicamente usato per:
            /// - sistemi lineari o interi
            /// - problemi combinatori
            /// - ottimizzazione vincolata
            /// - ricerca globale senza esplorazione esplicita dello spazio degli stati
            /// 
            /// Esempi di solver:
            /// - Z3
            /// - CVC5
            /// - OR-Tools (CP-SAT)
            /// </summary>
            SMTSolver = 1 << 12,
            /// <summary>
            /// Semantica di riduzione / normalizzazione
            /// </summary>
            Reduction = 1 << 13,
            /// <summary>
            /// Brute Force: esplorazione esaustiva di tutte le possibili soluzioni per trovare quella ottimale.
            /// Caratteristiche principali:
            /// - Garantisce di trovare la soluzione ottimale se lo spazio delle soluzioni è finito e completamente esplorato.
            /// - Inefficiente su spazi di ricerca grandi, poiché esplora ogni combinazione possibile.
            /// - Spesso utilizzato per problemi combinatori di piccola/medio scala, come permutazioni, combinazioni o conteggi esaustivi.
            /// </summary>
            BruteForce = 1 << 14,
        }
        #endregion

        #region ==== ENUM: DIFFICOLTÀ ====

        /// <summary>
        /// Livello di difficoltà del puzzle.
        /// </summary>
        [Flags]
        public enum DifficultEnum
        {
            None = 0,
            WarmUp = 1 << 0,
            Easy = 1 << 1,
            Medium = 1 << 2,
            Hard = 1 << 3,
            VeryHard = 1 << 4,
            Legend = 1 << 5
        }
        #endregion

        #region ==== METODI ====

        /// <summary>
        /// Ritorna descrizione breve dell'attributo (tipologia + tecnica).
        /// </summary>
        public override string ToString()
        {
            return $"Typology: {Typology}, Resolution: {Resolution}";
        }
        /// <summary>
        /// Cerca all'interno della struttura delle cartelle tutte le classi 
        /// che implementano IDay e che possiedono un attributo ResearchAlgorithmsAttribute
        /// corrispondente ai filtri specificati.
        /// </summary>
        /// <param name="typology">Filtra per tipologia (opzionale)</param>
        /// <param name="resolution">Filtra per tecnica di risoluzione (opzionale)</param>
        /// <returns>Lista dei nomi delle classi identificate</returns>
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
                                    interestedClasses.Add($"{yearName}_{fileName}");
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
        #endregion
    }
}
