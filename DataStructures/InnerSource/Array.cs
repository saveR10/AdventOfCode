using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace AOC.DataStructures.InnerSource
{
    internal class Array
    {
        public static void Example()
        {
            //Descrizione: Una collezione di elementi dello stesso tipo, con dimensioni fisse.
            //Caratteristiche:
            //  Accesso diretto tramite indice(O(1)).
            //  Dimensioni immutabili dopo l'inizializzazione.
            //Uso consigliato: Per collezioni di dimensioni note in anticipo.
            int[] numbers = new int[5];
            numbers[0] = 10;

            //Contengono un numero fisso di elementi dello stesso tipo.
            int[] array = { 1, 2, 3, 4 };
            Console.WriteLine(array[0]); // Output: 1



            // []: Array a singola dimensione(monodimensionale)
            //Rappresenta un array lineare, cioè una sequenza di elementi di uno stesso tipo.
            //La dimensione è specificata con un solo indice.
            int[] _array = new int[5]; // Array di 5 elementi
            _array[0] = 1;            // Assegna il valore 1 al primo elemento
            Console.WriteLine(_array[0]); // Output: 1
            
            // [,]: Array multidimensionale(rettangolare)
            //Rappresenta una matrice o un array rettangolare, dove ogni riga ha lo stesso numero di colonne.
            //Gli elementi sono indicizzati da più indici, separati da virgole.
            int[,] matrix = new int[2, 3]; // 2 righe e 3 colonne
            matrix[0, 0] = 1;             // Assegna il valore 1 alla prima cella
            Console.WriteLine(matrix[0, 0]); // Output: 1
            
            // [][]: Array di array(jagged array)
            //Un array "dentellato" o "irregolare", dove ogni elemento è un array separato.
            //Le righe possono avere lunghezze diverse, a differenza degli array rettangolari.
            int[][] jaggedArray = new int[2][]; // Array di 2 righe
            jaggedArray[0] = new int[3];       // Prima riga ha 3 colonne
            jaggedArray[1] = new int[2];       // Seconda riga ha 2 colonne
            jaggedArray[0][0] = 1;             // Assegna 1 al primo elemento
            Console.WriteLine(jaggedArray[0][0]); // Output: 1
            
            //Quando usare cosa
            //  []: Usalo per sequenze lineari di dati.
            //      Esempio: Array di numeri, nomi, ecc.
            //  [,]: Usalo per dati tabulari con dimensioni note e fisse.
            //      Esempio: Tabelle, griglie 2D con dimensioni predeterminate.
            //  [][]: Usalo per strutture irregolari o sparse.
            //      Esempio: Tabelle in cui ogni riga ha un numero diverso di colonne.
        }
    }
}
