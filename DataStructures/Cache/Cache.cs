using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;

namespace AOC.DataStructures.Cache
{
    public static class Cache
    {

        public static void Example()
        {
            // Creazione della cache
            MemoryCache cache = MemoryCache.Default;

            // Aggiunta di un elemento alla cache
            string cacheKey = "exampleKey";
            string cacheValue = "exampleValue";
            CacheItemPolicy policy = new CacheItemPolicy
            {
                AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(5) // Scadenza dopo 5 minuti
            };
            cache.Add(cacheKey, cacheValue, policy);

            // Recupero di un elemento dalla cache
            string cachedValue = cache.Get(cacheKey) as string;
            if (cachedValue != null)
            {
                Console.WriteLine("Valore recuperato dalla cache: " + cachedValue);
            }
            else
            {
                Console.WriteLine("Valore non trovato nella cache.");
            }




            MemoryCache ComplexCache = MemoryCache.Default;

            // Creazione della chiave complessa
            var key = new CompositeKey("User1", "SessionA");

            // Usa ToString() per generare una chiave stringa
            string Key = key.ToString();

            // Aggiunta alla cache
            cache.Add(Key, "CachedValue", DateTimeOffset.Now.AddMinutes(5));

            // Recupero dalla cache
            string value = cache.Get(Key) as string;
            if (value != null)
            {
                Console.WriteLine($"Valore recuperato: {value}");
            }
            else
            {
                Console.WriteLine("Chiave non trovata nella cache.");
            }

        }
        public class CompositeKey
        {
            public string Key1 { get; }
            public string Key2 { get; }

            public CompositeKey(string key1, string key2)
            {
                Key1 = key1;
                Key2 = key2;
            }

            // Implementazione di Equals
            public override bool Equals(object obj)
            {
                if (obj is CompositeKey other)
                {
                    return Key1 == other.Key1 && Key2 == other.Key2;
                }
                return false;
            }

            // Implementazione di GetHashCode
            public override int GetHashCode()
            {
                unchecked // Consente il wrapping del risultato in caso di overflow
                {
                    int hash = 17;
                    hash = hash * 31 + (Key1 != null ? Key1.GetHashCode() : 0);
                    hash = hash * 31 + (Key2 != null ? Key2.GetHashCode() : 0);
                    return hash;
                }
            }

            // Metodo per convertire la chiave in una stringa
            public override string ToString()
            {
                return $"{Key1}:{Key2}";
            }
        }

        //public static MemoryCache cacheSequence = MemoryCache.Default;

        //Memoization con cache 500 char
        /*int cacheSize500 = 500;
        int fullCache500 = inputSequence.Length / cacheSize500;
        int remaining500 = inputSequence.Length % cacheSize500;
        for (int i = 0; i < fullCache500; i++)
        {
            string block = inputSequence.Substring(i * cacheSize500, cacheSize500);
            string cacheKey = $"{currentPos.x},{currentPos.y}:{block}";
            if (CacheToken.ContainsKey(cacheKey) && cacheSequence.Contains(cacheKey))
            {
                var cachedResult = (Tuple<string, (int x, int y)>)cacheSequence.Get(cacheKey);
                sequence.Append(cachedResult.Item1);
                //WriteOutputToFile(cachedResult.Item1);
                currentPos = cachedResult.Item2;
            }
            else
            {
                var blockSequence = new StringBuilder();
                foreach (char command in block)
                {
                    (int targetX, int targetY) = DirectionalKeypad[command];
                    blockSequence.Append($"{GenerateMovementExtended(currentPos, (targetX, targetY), type)}A"); //dovrei dimezzare il tempo di accesso alla memoria
                                                                                                                //blockSequence.Append("A"); // Premere il tasto
                    currentPos = (targetX, targetY);
                }

                string blockSequenceStr = blockSequence.ToString();
                sequence.Append(blockSequenceStr);
                //WriteOutputToFile(blockSequenceStr);

                cacheSequence.Add(cacheKey, Tuple.Create(blockSequenceStr, currentPos), DateTimeOffset.UtcNow.AddMinutes(10));
            }
            if (sequence.Length > 50000000)
            {
                //WriteOutputToFile(sequence.ToString(), outputFilePath);
                sequence.Clear();
                GC.Collect();
            }
        }
        if (sequence.Length > 0)
        {
            //WriteOutputToFile(sequence.ToString(), outputFilePath);
            sequence.Clear();
        }

        //Memoization con cache 100 char
        int cacheSize100 = 100;
        int fullCache100 = remaining500 / cacheSize100;
        int remaining100 = remaining500 % cacheSize100;
        for (int i = 0; i < fullCache100; i++)
        {
            string block = inputSequence.Substring(fullCache500 * cacheSize500 + i * cacheSize100, cacheSize100);
            string cacheKey = $"{currentPos.x},{currentPos.y}:{block}";

            if (cacheSequence.Contains(cacheKey))
            {
                var cachedResult = (Tuple<string, (int x, int y)>)cacheSequence.Get(cacheKey);
                sequence.Append(cachedResult.Item1);
                //WriteOutputToFile(cachedResult.Item1);
                currentPos = cachedResult.Item2;
            }
            else
            {
                var blockSequence = new StringBuilder();
                foreach (char command in block)
                {
                    (int targetX, int targetY) = DirectionalKeypad[command];
                    blockSequence.Append($"{GenerateMovementExtended(currentPos, (targetX, targetY), type)}A"); //dovrei dimezzare il tempo di accesso alla memoria
                    //blockSequence.Append("A"); // Premere il tasto
                    currentPos = (targetX, targetY);
                }

                string blockSequenceStr = blockSequence.ToString();
                sequence.Append(blockSequenceStr);
                //WriteOutputToFile(blockSequenceStr);

                cacheSequence.Add(cacheKey, Tuple.Create(blockSequenceStr, currentPos), DateTimeOffset.UtcNow.AddMinutes(10));
            }
            if (sequence.Length > 50000000)
            {
                //  WriteOutputToFile(sequence.ToString(), outputFilePath);
                sequence.Clear();
                GC.Collect();
            }
        }
        if (sequence.Length > 0)
        {
            //WriteOutputToFile(sequence.ToString(), outputFilePath);
            sequence.Clear();
        }

        //Memoization con cache 10 char
        int cacheSize10 = 10;
        int fullCache10 = remaining100 / cacheSize10;
        int remaining10 = remaining100 % cacheSize10;
        for (int i = 0; i < fullCache10; i++)
        {
            string block = inputSequence.Substring(fullCache500 * cacheSize500 + fullCache100 * cacheSize100 + (i * cacheSize10), cacheSize10);
            string cacheKey = $"{currentPos.x},{currentPos.y}:{block}";

            if (cacheSequence.Contains(cacheKey))
            {
                var cachedResult = (Tuple<string, (int x, int y)>)cacheSequence.Get(cacheKey);
                sequence.Append(cachedResult.Item1);
                //WriteOutputToFile(cachedResult.Item1);
                currentPos = cachedResult.Item2;
            }
            else
            {
                var blockSequence = new StringBuilder();
                foreach (char command in block)
                {
                    (int targetX, int targetY) = DirectionalKeypad[command];
                    blockSequence.Append($"{GenerateMovementExtended(currentPos, (targetX, targetY), type)}A"); //dovrei dimezzare il tempo di accesso alla memoria
                    //blockSequence.Append("A"); // Premere il tasto
                    currentPos = (targetX, targetY);
                }

                string blockSequenceStr = blockSequence.ToString();
                sequence.Append(blockSequenceStr);
                //WriteOutputToFile(blockSequenceStr);

                cacheSequence.Add(cacheKey, Tuple.Create(blockSequenceStr, currentPos), DateTimeOffset.UtcNow.AddMinutes(10));
            }
            if (sequence.Length > 50000000)
            {
                //  WriteOutputToFile(sequence.ToString(), outputFilePath);
                sequence.Clear();
                GC.Collect();
            }
        }
        if (sequence.Length > 0)
        {
            //WriteOutputToFile(sequence.ToString(), outputFilePath);
            sequence.Clear();
        }

        //Memoization con cache 5 char
        int cacheSize5 = 5;
        int fullCache5 = remaining10 / cacheSize5;
        int remaining5 = remaining10 % cacheSize5;
        for (int i = 0; i < fullCache5; i++)
        {
            string block = inputSequence.Substring(fullCache500 * cacheSize500 + fullCache100 * cacheSize100 + fullCache10 * cacheSize10 + i * (cacheSize5), cacheSize5);
            string cacheKey = $"{currentPos.x},{currentPos.y}:{block}";

            if (cacheSequence.Contains(cacheKey))
            {
                var cachedResult = (Tuple<string, (int x, int y)>)cacheSequence.Get(cacheKey);
                sequence.Append(cachedResult.Item1);
                //WriteOutputToFile(cachedResult.Item1);
                currentPos = cachedResult.Item2;
            }
            else
            {
                var blockSequence = new StringBuilder();
                foreach (char command in block)
                {
                    (int targetX, int targetY) = DirectionalKeypad[command];
                    blockSequence.Append($"{GenerateMovementExtended(currentPos, (targetX, targetY), type)}A"); //dovrei dimezzare il tempo di accesso alla memoria
                    //blockSequence.Append("A"); // Premere il tasto
                    currentPos = (targetX, targetY);
                }

                string blockSequenceStr = blockSequence.ToString();
                sequence.Append(blockSequenceStr);
                //WriteOutputToFile(blockSequenceStr);

                cacheSequence.Add(cacheKey, Tuple.Create(blockSequenceStr, currentPos), DateTimeOffset.UtcNow.AddMinutes(10));
            }
            if (sequence.Length > 50000000)
            {
                //  WriteOutputToFile(sequence.ToString(), outputFilePath);
                sequence.Clear();
                GC.Collect();
            }
        }
        if (sequence.Length > 0)
        {
            //WriteOutputToFile(sequence.ToString(), outputFilePath);
            sequence.Clear();
            GC.WaitForPendingFinalizers();
        }

        //caratteri rimanenti
        for (int i = fullCache100 * cacheSize100 + fullCache10 * cacheSize10 + fullCache5 * cacheSize5; i < remaining5; i++)
        {
            char command = inputSequence[i];
            (int targetX, int targetY) = DirectionalKeypad[command];
            sequence.Append(GenerateMovementExtended(currentPos, (targetX, targetY), type));
            //WriteOutputToFile(GenerateMovementExtended(currentPos, (targetX, targetY), type));
            sequence.Append("A"); // Premere il tasto
            //WriteOutputToFile("A"); // Premere il tasto
            currentPos = (targetX, targetY);
        }
        if (sequence.Length > 0)
        {
            //WriteOutputToFile(sequence.ToString(), outputFilePath);
            sequence.Clear();
        }
        return sequence.ToString();*/


    }
}
