 
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using static AOC2015.Day13;
using static AOC2015.Day7;
using static System.Net.Mime.MediaTypeNames;

namespace AOC.Utilities.Math
{
    internal class Combinatorial
    {
        #region Combinations
        #region Number
        /// <summary>
        /// This function gets the total number of unique combinations based upon N and K. Also called Binomial Coefficient.
        /// N is the total number of items.
        /// K is the size of the group.
        /// Total number of unique combinations = N! / ( K! (N - K)! ).
        /// C_N,K
        /// This function is less efficient, but is more likely to not overflow when N and K are large.
        /// N > K
        /// Taken from:  http://blog.plover.com/math/choose.html
        /// </summary>
        /// <param name="N"></param>
        /// <param name="K"></param>
        /// <returns></returns>
        public static long NumberCombinationsWithoutRept(long N, long K)
        {
            long r = 1;
            long d;
            if (K > N) return 0;
            for (d = 1; d <= K; d++)
            {
                r *= N--;
                r /= d;
            }
            return r;
        }
        /// <summary>
        /// Cr_N,K = ( N + K - 1)! / (( K! ) ( N - 1 )!)
        /// </summary>
        /// <param name="N"></param>
        /// <param name="K"></param>
        /// <returns></returns>
        public static long NumberCombinationsWithRept(long N, long K)
        {
            long num = NumberPermutationsWithoutRept(N + K - 1);
            long den = NumberPermutationsWithoutRept(K);
            long den2 = NumberPermutationsWithoutRept(N - 1);
            return num / den / den2;
        }

        #endregion

        public static List<List<T>> GetAllCombinations<T>(List<T> elements)
        {
            List<List<T>> result = new List<List<T>>();
            GetAllCombinationsHelper(elements, 0, new List<T>(), result);
            return result;
        }

        public static void GetAllCombinationsHelper<T>(List<T> elements, int currentIndex, List<T> currentCombination, List<List<T>> result)
        {
            if (currentIndex == elements.Count)
            {
                result.Add(new List<T>(currentCombination));
                return;
            }

            // Include the current element in the combination
            currentCombination.Add(elements[currentIndex]);
            GetAllCombinationsHelper(elements, currentIndex + 1, currentCombination, result);
            currentCombination.RemoveAt(currentCombination.Count - 1); // Backtrack

            // Exclude the current element from the combination
            GetAllCombinationsHelper(elements, currentIndex + 1, currentCombination, result);
        }


        /// <summary>
        /// This function gets all groups of unique combinations based upon N and K.
        /// N is the total number of items.
        /// K is the size of the group.
        /// Total number of unique combinations C_N,K = N! / ( K! (N - K)! ).
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static IEnumerable<IEnumerable<T>> GetCombinationsWithoutRept<T>(IEnumerable<T> list, int length)
        {
            int ind_digit = 0;
            int num_digit = 2;
            IEnumerable<IEnumerable<T>> a = list.Select(t => new T[] { t });
            for (int i = 0; i < length - 1; i++)
            {
                a = a.SelectMany((t) => list, (t1, t2) => t1.Where(tt => !tt.Equals(t2)).Concat(new T[] { t2 }));
                a = a.Where(t => t.Count() == i + 2);
                a = a.ToList();
                /*a = a.SelectMany(combination => list.Where(element => !combination.Contains(element)),
                      (combination, element) => combination.Concat(new T[] { element }))
                    .Where(combination => combination.Sum(e => Convert.ToInt32(e)) == target);*/
                var b = new List<IEnumerable<T>>();
                int ind_col = 0;
                int ind_row = 0;
                int num_cols = list.Count() - num_digit + 1;

                int[] ind_indexes = new int[num_digit - 2];
                int[] num_indexes = new int[num_digit - 2];

                for (int ii = 0; ii < ind_indexes.Length; ii++)
                {
                    num_indexes[ii] = list.Count() - ind_digit;
                }
                foreach (var item in a)
                {
                    if (ind_row <= ind_col - ind_indexes.Sum()) b.Add(item);
                    int num_rows = list.Count() - ind_digit - ind_indexes.Sum();
                    ind_col++;
                    if (ind_col == num_cols)
                    {
                        ind_row++;
                        if (ind_row == num_rows)
                        {
                            ind_row = 0;
                            SetIndexesCombWithoutRept(ind_indexes, num_indexes);
                            num_rows = list.Count() - ind_digit - ind_indexes.Sum();
                        }
                        if (num_rows > 0) ind_row = ind_row % (num_rows);
                        ind_col = 0;
                    }
                }
                ind_digit++;
                num_digit++;
                a = b;
            }
            return a;
        }

        public static void SetIndexesCombWithoutRept(int[] ind_indexes, int[] num_indexes)
        {
            if (ind_indexes.Length > 0)
            {
                if (ind_indexes?.First() < num_indexes?.First()) ind_indexes[0] += 1;

                if (ind_indexes?.First() == num_indexes?.First())
                {
                    SetIndexesHelperCombWithoutRept(ind_indexes, num_indexes, 0);
                }
            }
        }
        public static void SetIndexesHelperCombWithoutRept(int[] ind_indexes, int[] num_indexes, int i)
        {
            if (ind_indexes[i] == num_indexes[i])
            {
                ind_indexes[i] = 0;
                if (i < ind_indexes.Length - 1)
                {
                    ind_indexes[i + 1] += 1;
                    SetIndexesHelperCombWithoutRept(ind_indexes, num_indexes, i + 1);
                }
                num_indexes[i] = num_indexes[i] - 1;
                if (num_indexes[i] == 0)
                {
                    if (i < ind_indexes.Length - 1)
                    {
                        num_indexes[i] = num_indexes[i + 1];
                    }
                }
            }
        }

        /// <summary>
        /// This function gets all groups of combinations with elements repetition based upon N and K.
        /// N is the total number of items.
        /// K is the size of the group.
        /// Number of combinations C'_N,K = (N + K - 1)! / ( K! (N - 1)! ).
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static IEnumerable<IEnumerable<T>> GetCombinationsWithRept<T>(IEnumerable<T> list, int length)
        {
            int num_digit = 2;
            IEnumerable<IEnumerable<T>> a = list.Select(t => new T[] { t });
            for (int i = 0; i < length - 1; i++)
            {
                a = a.SelectMany((t) => list, (t1, t2) => t1.Concat(new T[] { t2 }));
                a = a.Where(t => t.Count() == i + 2);
                a = a.ToList();
                var b = new List<IEnumerable<T>>();
                int ind_col = 0;
                int ind_row = 0;
                int num_cols = list.Count();

                int[] ind_indexes = new int[num_digit - 2];
                int[] num_indexes = new int[num_digit - 2];

                for (int ii = 0; ii < ind_indexes.Length; ii++)
                {
                    num_indexes[ii] = list.Count();
                }
                foreach (var item in a)
                {
                    if (ind_row <= ind_col - ind_indexes.Sum()) b.Add(item);
                    int num_rows = list.Count() - ind_indexes.Sum();
                    ind_col++;
                    if (ind_col == num_cols)
                    {
                        ind_row++;
                        if (ind_row == num_rows)
                        {
                            ind_row = 0;
                            SetIndexesCombWithRept(ind_indexes, num_indexes);
                            num_rows = list.Count() - ind_indexes.Sum();
                        }
                        if (num_rows > 0) ind_row = ind_row % (num_rows);
                        ind_col = 0;
                    }
                }
                num_digit++;
                a = b;
            }
            return a;
        }
        public static void SetIndexesCombWithRept(int[] ind_indexes, int[] num_indexes)
        {
            if (ind_indexes.Length > 0)
            {
                if (ind_indexes?.First() < num_indexes?.First()) ind_indexes[0] += 1;

                if (ind_indexes?.First() == num_indexes?.First())
                {
                    SetIndexesHelperCombWithRept(ind_indexes, num_indexes, 0);
                }
            }
        }
        public static void SetIndexesHelperCombWithRept(int[] ind_indexes, int[] num_indexes, int i)
        {
            if (ind_indexes[i] == num_indexes[i])
            {
                ind_indexes[i] = 0;
                if (i < ind_indexes.Length - 1)
                {
                    ind_indexes[i + 1] += 1;
                    SetIndexesHelperCombWithRept(ind_indexes, num_indexes, i + 1);
                }
                num_indexes[i] = num_indexes[i] - 1;
                if (num_indexes[i] == 0)
                {
                    if (i < ind_indexes.Length - 1)
                    {
                        num_indexes[i] = num_indexes[i + 1];
                    }
                }
            }
        }

        #region Examples
        /// <summary>
        /// Un negoziante vuole esporre in una piccola vetrina 4 paia di scarpe scelte tra 10 modelli diversi.        
        /// In quanti modi si possono esporre le scarpe all'interno della vetrina?
        /// n=10, gli oggetti sono i 10 modelli di scarpe.
        /// k=4, i posti sono le 4 paia di scarpe da esporre.
        /// Per l'esposizione NON conta l'ordine.
        /// I modelli sono tutti distinti, quindi NON c'è ripetizione di oggetti.
        /// Si applica la formula delle combinazioni senza ripetizione di oggetti.
        /// Ci sono 210 modi diversi per esporre in vetrina 4 paia di scarpe scelte tra 10 modelli.
        ///  <see cref="http://www.matematica.it"/>
        /// </summary>
        public static void ExampleCombinationsWithousRept1()
        {
            //var n2 = AOC.Utilities.Math.Combinatorial.NumberCombinationsWithoutRept(10, 2);
            //var n3 = AOC.Utilities.Math.Combinatorial.NumberCombinationsWithoutRept(10, 3);
            var n4 = AOC.Utilities.Math.Combinatorial.NumberCombinationsWithoutRept(10, 4);
            //var n5 = AOC.Utilities.Math.Combinatorial.NumberCombinationsWithoutRept(10, 5);
            //var n6 = AOC.Utilities.Math.Combinatorial.NumberCombinationsWithoutRept(10, 6);
            //var n7 = AOC.Utilities.Math.Combinatorial.NumberCombinationsWithoutRept(10, 7);
            //var n8 = AOC.Utilities.Math.Combinatorial.NumberCombinationsWithoutRept(10, 8);
            //var n9 = AOC.Utilities.Math.Combinatorial.NumberCombinationsWithoutRept(10, 9);
            //var n10 = AOC.Utilities.Math.Combinatorial.NumberCombinationsWithoutRept(10, 10);

            IEnumerable<int> list = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            //var c2 = AOC.Utilities.Math.Combinatorial.GetCombinationsWithoutRept(list, 2);
            //var c3 = AOC.Utilities.Math.Combinatorial.GetCombinationsWithoutRept(list, 3);
            var c4 = AOC.Utilities.Math.Combinatorial.GetCombinationsWithoutRept(list, 4);
            //var c5 = AOC.Utilities.Math.Combinatorial.GetCombinationsWithoutRept(list, 5);
            //var c6 = AOC.Utilities.Math.Combinatorial.GetCombinationsWithoutRept(list, 6);
            //var c7 = AOC.Utilities.Math.Combinatorial.GetCombinationsWithoutRept(list, 7);
            //var c8 = AOC.Utilities.Math.Combinatorial.GetCombinationsWithoutRept(list, 8);
            //var c9 = AOC.Utilities.Math.Combinatorial.GetCombinationsWithoutRept(list, 9);
            //var c10 = AOC.Utilities.Math.Combinatorial.GetCombinationsWithoutRept(list, 10);

            //var coun2 = c2.Count();
            //var coun3 = c3.Count();
            var coun4 = c4.Count();
            //var coun5 = c5.Count();
            //var coun6 = c6.Count();
            //var coun7 = c7.Count();
            //var coun8 = c8.Count();
            //var coun9 = c9.Count();
            ViewResult(c4);
        }


        /// <summary>
        /// 24 amici, ex compagni di liceo, si rivedono dopo qualche anno e organizzano una cena.A fine serata si salutano e ognuno stringe la mano a tutti gli altri. Quante strette di mano ci saranno?
        /// Il numero totale di elementi N è 24.
        /// Il numero di elementi con cui si forma un raggruppamento K è 2, in quanto una stretta di mano avviene fra due persone.
        /// L'ordine NON ha importanza: ognuno può stringere la mano a chi vuole e in che ordine vuole. Quindi si parla di combinazioni.
        /// Uno stesso elemento, all'interno di un raggruppamento NON può essere ripetuto! Una persona non può stringersi la mano da sola. Quindi si tratta di combinazioni semplici.
        /// Ci saranno 276 strette di mano tra 24 persone.
        /// </summary>
        ///  <see cref="https://www.mcurie.edu.it/files/nicosia.salvatore/PROBLEMI_DI_CALCOLO_COMBINATORIO_prof._Nicosia.pdf"/>
        public static void ExampleCombinationsWithousRept2()
        {
            var n = AOC.Utilities.Math.Combinatorial.NumberCombinationsWithoutRept(24, 2);
            IEnumerable<int> list = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24 };
            var c = AOC.Utilities.Math.Combinatorial.GetCombinationsWithoutRept(list, 2);
            ViewResult(c);
        }
        /// <summary>
        /// Date le tre lettere a, b, c, quante combinazioni, con ripetizione, di classe 4 possono formarsi?
        /// Se ne potranno formare 15.
        /// </summary>
        public static void ExampleCombinationsWithRept1()
        {
            //var n2 = AOC.Utilities.Math.Combinatorial.NumberCombinationsWithRept(3, 2);
            //var n3 = AOC.Utilities.Math.Combinatorial.NumberCombinationsWithRept(3, 3);
            var n4 = AOC.Utilities.Math.Combinatorial.NumberCombinationsWithRept(3, 4);

            IEnumerable<char> list = new List<char>() { 'a', 'b', 'c' };
            //var c2 = AOC.Utilities.Math.Combinatorial.GetCombinationsWithRept(list, 2);
            //var c3 = AOC.Utilities.Math.Combinatorial.GetCombinationsWithRept(list, 3);
            var c4 = AOC.Utilities.Math.Combinatorial.GetCombinationsWithRept(list, 4);
            ViewResult(c4);
        }



        //long a = AOC.Utilities.Math.Combinatorial.GetBinCoeff(90, 6); NumberCombinationsWithoutRept

        #endregion
        #endregion

        #region Dispositions

        #region Number
        /// <summary>
        /// DR_N,K = N^K
        /// </summary>
        /// <param name="N"></param>
        /// <param name="K"></param>
        /// <returns></returns>
        public static double NumberDispositionsWithRept(long N, long K)
        {
            return System.Math.Pow(N, K);
        }

        /// <summary>
        /// D_N,K = N!/(N-K)!
        /// Con N > K
        /// </summary>
        /// <param name="N"></param>
        /// <param name="K"></param>
        /// <returns></returns>
        public static double NumberDispositionsWithoutRept(long N, long K)
        {
            long num = NumberPermutationsWithoutRept(N);
            long den = NumberPermutationsWithoutRept(N - K);
            return num / den;
        }
        #endregion

        public static IEnumerable<IEnumerable<T>> GetDispositionsWithRept<T>(IEnumerable<T> list, int length)
        {
            if (length == 1) return list.Select(t => new T[] { t });
            return GetDispositionsWithRept(list, length - 1).SelectMany(t => list, (t1, t2) => t1.Concat(new T[] { t2 }));
        }

        static List<List<T>> GenerateDispositionsWithoutRepetition<T>(List<T> elements, int seats)
        {
            List<List<T>> result = new List<List<T>>();
            GenerateDispositionsWithoutRepetitionRecursive(elements, seats, new List<T>(), result);
            return result;
        }

        static void GenerateDispositionsWithoutRepetitionRecursive<T>(
            List<T> elements, int seats, List<T> currentDisposition, List<List<T>> result)
        {
            if (seats == 0)
            {
                result.Add(new List<T>(currentDisposition));
                return;
            }

            for (int i = 0; i < elements.Count; i++)
            {
                if (!currentDisposition.Contains(elements[i]))
                {
                    currentDisposition.Add(elements[i]);
                    GenerateDispositionsWithoutRepetitionRecursive(elements, seats - 1, currentDisposition, result);
                    currentDisposition.RemoveAt(currentDisposition.Count - 1); // Backtrack
                }
            }
        }

            #region Examples
            /// Utilizzando le cifre 1,2,3,4 quanti numeri di 4 cifre si possono formare?
            /// N=3, gli oggetti sono le 3 cifre
            /// K=4, i posti sono le 4 cifre
            /// Conta 'ordine: le cifre hanno posizioni ben precise, quindi conta l'ordine con cui i numeri 1,2,3 si dispongono.
            /// R=4, ciascuna cifra 1,2,3 può ripetersi fino a 4 volte per formare il numero a 4 cifre, quindi c'è ripetizione di oggetti.
            /// DR_N,K=N^K, si applica la formula delle disposizioni con ripetizioni di oggetti.
            /// DR_3,4 = 3^4=81, si possono formare 81 numeri di 4 cifre usando le cifre 1,2,3
            /// 1111  1112  1113        2111  2112  2113        3111  3112  3113
            /// 1121  1122  1123        2121  2122  2123        3121  3122  3123
            /// 1131  1132  1133        2131  2132  2133        3131  3132  3133
            /// 1211  1212  1213        2211  2212  2213        3211  3212  3213
            /// 1221  1222  1223        2221  2222  2223        3221  3222  3223
            /// 1231  1232  1233        2231  2232  2233        3231  3232  3233
            /// 1311  1312  1313        2311  2312  2313        3311  3312  3313
            /// 1321  1322  1323        2321  2322  2323        3321  3322  3323
            /// 1331  1332  1333        2331  2332  2333        3331  3332  3333
            public static void ExampleDispositionsWithRept1()
        {
            var n = NumberDispositionsWithRept(3, 4);
            IEnumerable<int> list = new List<int>() { 1, 2, 3 };
            var d = AOC.Utilities.Math.Combinatorial.GetDispositionsWithRept(list, 4);
            ViewResult(d);
        }

        /// <summary>
        /// Le targhe automobilistiche sono costituite da 2 lettere, seguite da 3 cifre, seguite a loro volta da 2 lettere.
        /// Sapendo che le 2 lettere possono essere scelte fra le 26 dell'alfabeto anglosassone, si calcoli quante automobili si possono immatricolare in questo modo.
        /// Abbiamo K=2 elementi che si possono scegliere tra N=26 elementi distinti (le lettere dell'alfabeto anglosassone).
        /// L'ordine ha importanza! perchè due lettere scambiate di posto generano due targhe distinte. Poichè K è diverso da N abbiamo a che fare con disposizioni.
        /// Uno stesso elemento, all'interno di un raggruppamento, può essere ripetuto, infatti vi sono targhe che iniziano con due lettere uguali. Quindi disposizioni con ripetizione.
        /// </summary>
        public static void ExampleDispositionsWithRept2()
        {
            IEnumerable<char> list = new List<char>() { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
            var d = AOC.Utilities.Math.Combinatorial.GetDispositionsWithRept(list, 2);
            ViewResult(d);

            //Il numero totale è:
            var n = NumberDispositionsWithRept(26, 2) * NumberDispositionsWithRept(10, 3) * NumberDispositionsWithRept(26, 2);
        }

        /// <summary>
        /// In quanti modi diversi 5 alunni si possono sedere su 3 sedie numerate?
        /// Gli elementi sono i 5 alunni, N=5.
        /// I posti sono le 3 sedie, K=3.
        /// Le sedie sono numerate, quindi CONTA L'ORDINE con cui gli alunni si siedono.
        /// I 5 alunni sono persone tutte distinte, quindi NON C'E' RIPETIZIONE di oggetti.
        /// Si applica la formula delle disposizioni senza ripetizioni di oggetti.
        /// Ci sono 60 modi diversi in cui gli alunni si possono sedere.
        /// </summary>
        public static void ExampleDispositionsWithoutRept1()
        {
            var n = NumberDispositionsWithoutRept(5, 3);
            List<char> elements = new List<char> { '1', '2', '3', '4', '5' };
            int seats = 3;
            List<List<char>> dispositions = GenerateDispositionsWithoutRepetition(elements, seats);

            Console.WriteLine($"All dispositions without repetition of {seats} elements:");
            foreach (var disposition in dispositions)
            {
                Console.WriteLine(string.Join(", ", disposition));
            }
            
        }

        #endregion
        #endregion

        #region Permutations
        #region Number
        /// <summary>
        /// PN= N!
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static long NumberPermutationsWithoutRept(long num)
        {
            long n = num;
            for (long i = n - 1; i > 0; i--)
            {
                n *= i;
            }
            num--;
            return n;
        }
        /// <summary>
        /// P_NR= N! / ( R1! * R2! * ... * Rn!) 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <returns></returns>
        public static long NumberPermutationsWithRept<T>(IEnumerable<T> enumerable)
        {
            IEnumerable<T> a = enumerable.AsEnumerable();
            List<T> b = new List<T>();
            Dictionary<T, int> dict = new Dictionary<T, int>();
            foreach (var item in a)
            {
                if (!b.Any(bb => bb.Equals(item)))
                {
                    b.Add(item);
                    dict.Add(item, 1);
                }
                else
                {
                    dict[item] += 1;
                }
            }
            long r = 1;
            dict.ToList().ForEach((ts) => r *= NumberPermutationsWithoutRept(ts.Value));
            return NumberPermutationsWithoutRept(enumerable.Count()) / r;
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static IEnumerable<IEnumerable<T>> GetPermutationsWithoutRept<T>(IEnumerable<T> list, int length)
        {
            int ind_digit = 0;
            int num_digit = 2;
            IEnumerable<IEnumerable<T>> a = list.Select(t => new T[] { t });
            for (int i = 0; i < length - 1; i++)
            {
                a = a.SelectMany((t) => list, (t1, t2) => t1.Where(tt => !tt.Equals(t2)).Concat(new T[] { t2 }));
                a = a.Where(t => t.Count() == i + 2);
                a = a.ToList();
                var b = new List<IEnumerable<T>>();
                int ind_col = 0;
                int ind_row = 0;
                int num_cols = list.Count() - num_digit + 1;

                int[] ind_indexes = new int[num_digit - 2];
                int[] num_indexes = new int[num_digit - 2];

                for (int ii = 0; ii < ind_indexes.Length; ii++)
                {
                    num_indexes[ii] = list.Count() - ind_digit;
                }
                foreach (var item in a)
                {
                    if (i == 3)
                    {
                        Console.WriteLine($"{item.ElementAt(0)}{item.ElementAt(1)}{item.ElementAt(2)}{item.ElementAt(3)}{item.ElementAt(4)} ");
                    }
                    //if (ind_row <= ind_col - ind_indexes.Sum())
                    b.Add(item);
                    int num_rows = list.Count() - ind_digit - ind_indexes.Sum();
                    ind_col++;
                    if (ind_col == num_cols)
                    {
                        ind_row++;
                        if (ind_row == num_rows)
                        {
                            ind_row = 0;
                            SetIndexesCombWithoutRept(ind_indexes, num_indexes);
                            num_rows = list.Count() - ind_digit - ind_indexes.Sum();
                        }
                        if (num_rows > 0) ind_row = ind_row % (num_rows);
                        ind_col = 0;
                    }
                }
                ind_digit++;
                num_digit++;
                a = b;
            }
            return a;
        }


        public static List<List<T>> GetPermutationsWithRept<T>(List<T> elements)
        {
            List<List<T>> result = new List<List<T>>();
            PermutationsWithReptRecursive(elements, 0, result);
            return result;
        }
        static void PermutationsWithReptRecursive<T>(List<T> elements, int index, List<List<T>> result)
        {
            if (index == elements.Count)
            {
                result.Add(new List<T>(elements));

            }
            else
            {
                HashSet<T> swapped = new HashSet<T>();
                for (int i = index; i < elements.Count; i++)
                {
                    if (!swapped.Contains(elements[i]))
                    {
                        swapped.Add(elements[i]);
                        SwapPermutation(elements, index, i);
                        PermutationsWithReptRecursive(elements, index + 1, result);
                        SwapPermutation(elements, index, i); // Backtrack
                    }
                }
            }
        }

        static void SwapPermutation<T>(List<T> list, int index1, int index2)
        {
            T temp = list[index1];
            list[index1] = list[index2];
            list[index2] = temp;
        }

        #region Examples
        /// <summary>
        /// Quanti anagrammi anche senza senso si possono formare con la parola LIBRO?
        /// Gli oggetti sono le 5 lettere della parola LIBRO.
        /// I posti sono le 5 caselle occupate dalle lettere della parola LIBRO.
        /// Per formare un anagramma CONTA L'ORDINE  con cui le lettere si succedono.
        /// Le 5 lettere sono tutte distinte quindi NON C'E' RIPETIZIONE di oggetti.
        /// Si applica la formula della permutazione senza ripetizioni di oggetti.
        ///  <see cref="http://www.matematica.it"/>
        /// </summary>
        public static void ExamplePermutationsWithoutRept1()
        {
            var n = NumberPermutationsWithoutRept(5);
            IEnumerable<char> list = new List<char>() { 'L', 'I', 'B', 'R', 'O' };
            var p = GetPermutationsWithoutRept(list, 5);
            ViewResult(p);
        }

        public static void ExamplePermutationsWithoutRept2()
        {
            var n = NumberPermutationsWithoutRept(4);
            IEnumerable<char> list = new List<char>() { 'R', 'O', 'M', 'A' };
            var p = GetPermutationsWithoutRept(list, 4);
            ViewResult(p);
        }

        /// <summary>
        /// Quanti anagrammi anche senza senso si possono formare con la parola MAMMA?
        /// Gli oggetti sono le 5 lettere della parola MAMMA.
        /// I posti sono le 5 caselle occupate dalle lettere della parola MAMMA.
        /// Per formare un anagramma CONTA L'ORDINE  con cui le lettere si succedono.
        /// Le 5 lettere non sono tutte distinte quindi C'E' RIPETIZIONE di oggetti: M si ripete 3 volte ed A si ripete 2 volte.
        /// Si applica la formula della permutazione con ripetizioni di oggetti.
        ///  <see cref="http://www.matematica.it"/>
        /// </summary>
        public static void ExamplePermutationsWithRept1()
        {
            List<char> elements = new List<char> { 'M', 'A', 'M', 'M', 'A' };
            var n2 = NumberPermutationsWithRept(elements);
            List<List<char>> permutations = Combinatorial.GetPermutationsWithRept(elements);

            Console.WriteLine("All permutations:");
            foreach (var permutation in permutations)
            {
                Console.WriteLine(string.Join(", ", permutation));
            }
        }
        #endregion

        #endregion

        static void ViewResult<T>(IEnumerable<IEnumerable<T>> enumerable)
        {
            List<IEnumerable<T>> list = enumerable.ToList();
            for (int i = 0; i < list.Count(); i++)
            {
                for (int j = 0; j < list[i].Count(); j++)
                {
                    Console.Write(list[i].ElementAt(j));
                    if (j < list[i].Count() - 1) Console.Write("-");
                }
                Console.WriteLine();
            }
        }
    }
}
