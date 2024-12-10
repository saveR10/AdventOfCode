using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;

namespace AOC.DataStructures.Cache
{
    internal class Cache
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
        private class CompositeKey
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
    }
}
