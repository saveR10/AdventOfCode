using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;

namespace AOC.DataStructures.InnerSource
{
    internal class HashSet
    {
        static void Example()
        {
            //Descrizione: Una collezione di elementi univoci senza un ordine garantito.
            //Caratteristiche:
            //  Supporta operazioni di unione, intersezione e differenza.
            //  Accesso rapido(O(1) in media).
            //Uso consigliato: Per garantire l'unicità degli elementi.

            // Creazione di un HashSet di interi
            HashSet<int> numbers = new HashSet<int>();

            // Aggiungere elementi
            numbers.Add(1);
            numbers.Add(2);
            numbers.Add(3);


            // Prova ad aggiungere un elemento duplicato
            bool added = numbers.Add(2); // Restituisce false perché 2 è già presente
            Console.WriteLine($"Elemento aggiunto? {added}");

            // Verifica se un elemento esiste
            Console.WriteLine($"Contiene 3? {numbers.Contains(3)}"); // True
            Console.WriteLine($"Contiene 5? {numbers.Contains(5)}"); // False

            // Rimuovi un elemento
            numbers.Remove(2);

            // Iterare sugli elementi
            Console.WriteLine("Elementi nel HashSet:");
            foreach (int number in numbers)
            {
                Console.WriteLine(number);
            }


            HashSet<int> set1 = new HashSet<int> { 1, 2, 3 };
            HashSet<int> set2 = new HashSet<int> { 3, 4, 5 };

            set1.UnionWith(set2); // set1 diventa { 1, 2, 3, 4, 5 }
            set1.IntersectWith(set2); // set1 diventa { 3 }
            set1.ExceptWith(set2); // Rimuove gli elementi di set2 da set1
            set1.SymmetricExceptWith(set2);

            //Gestione delle Stringhe
            HashSet<string> names = new HashSet<string> { "Alice", "Bob", "Charlie" };

            names.Add("Alice"); // Non viene aggiunto perché è già presente
            names.Add("alice"); // Viene aggiunto perché è case-sensitive

            foreach (string name in names)
            {
                Console.WriteLine(name);
            }

            HashSet<string> names_NonCaseSensitive = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            names_NonCaseSensitive.Add("Alice");
            names_NonCaseSensitive.Add("alice"); // Non viene aggiunto perché il confronto è case-insensitive

            HashSet<int> uniqueNumbers = new HashSet<int> { 1, 2, 3 };
            uniqueNumbers.Add(3); // Non viene aggiunto perché è già presente.

        }
    }
}

