using AOC;
using AOC.Model;
using AOC.SearchAlghoritmhs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using static AOC.SearchAlghoritmhs.ResearchAlgorithmsAttribute;

namespace AOC2016
{
    [ResearchAlgorithms(TypologyEnum.Hashing)]
    [ResearchAlgorithms(ResolutionEnum.Regex)]
    [ResearchAlgorithms(ResolutionEnum.Cache)]
    [ResearchAlgorithms(DifficultEnum.Hard)]
    public class Day14 : Solver, IDay
    {
        public static int keysToFind = 64;
        public static int keysFinded = 0;
        public static long targetIndex = 0;

        public void Part1(object input, bool test, ref object solution)
        {
            string inputString = (string)input;
            Salt = inputString.Split(Delimiter.delimiter_line, StringSplitOptions.None)[0];
            bool cacheSolution = false;
            if (!cacheSolution)
            {
                //L'approccio che ho utilizzato è scorretto! Fai conto che la 64esima key è all'indice 20000 e la conferma ce l'hai a 21000. Se a 20900 hai la conferma di un'altra chiave, che non sia quella all'indice 20000, la risposta è errata.
                //Nella Parte1, la conferma della 64esima chiave ce l'ho tra le prime 64, quindi mi è bastato ordinare gli indici e prendere il 64esimo.
                //Nella Parte2, ho ottenuto la conferma di chiavi oltre la 64esima! Quindi, trovata la 64chiave (errata), a partire da quell'indice, devo cercare per i prossimi 1000, prendere tutti gli indici delle chiavi trovate (più di 64), ordinarli, e selezionare il 64esimo.
                for (int i = 0; keysToFind > 0; i++)
                {
                    string md5 = CreateMD5($"{Salt}{i}");
                    ClearCharToFind(i);
                    //CheckRepetition(md5, 3, i);
                    //CheckRepetition(md5, 5, i);
                    CheckRepetitionWithRegex(md5, 3, i);
                    CheckRepetitionWithRegex(md5, 5, i);
                }
                targetIndex = KeyIndex.Max(k => k);
                solution = targetIndex;
            }
            else
            {
                solution = Find64thKeyIndex(Salt);
            }
        }
        public static string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                //return  HexadecimalEncoding.ToHexString(hashBytes); // .NET 5 +

                // Convert the byte array to hexadecimal string prior to .NET 5
                StringBuilder sb = new System.Text.StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                }
                return sb.ToString();
            }
        }
        public void ClearCharToFind(int index)
        {
            var toRemove = CharToFind.Where(c => c.Value.Item2 + 1000 < index).Select(c => c.Key).ToList();
            foreach (var key in toRemove.ToList())
            {
                CharToFind.Remove(key);
            }
        }
        public static Dictionary<string, Tuple<char, int>> CharToFind = new Dictionary<string, Tuple<char, int>>();
        public static List<string> HashKey = new List<string>();
        public bool CheckRepetitionWithRegex(string MD5Hash, int repetitionToFind, int index)
        {
            string pattern = "";
            if (repetitionToFind == 3) pattern = @"(.)\1{2}";
            else pattern = @"(.)\1{4}";
            bool ret = false;
            MatchCollection matches = Regex.Matches(MD5Hash, pattern);
            if (matches.Count > 0)
            {
                if (repetitionToFind == 3)
                {
                    CharToFind.Add(MD5Hash, new Tuple<char, int>(Char.Parse(matches[0].Value.Substring(0, 1)), index));
                    return true;
                }
                else if (repetitionToFind == 5)
                {
                    foreach (var m in matches)
                    {
                        var toEvaluate = CharToFind.Where(c => c.Value.Item1.Equals(Char.Parse(m.ToString().Substring(0, 1))) && c.Value.Item2 < index);
                        if (toEvaluate.Count() > 0)
                        {
                            foreach (var e in toEvaluate.OrderBy(c => c.Value.Item2).ToList())
                            {
                                HashKey.Add(e.Key);
                                keysToFind -= 1;
                                keysFinded++;
                                KeyIndex.Add(e.Value.Item2);
                                Console.WriteLine($"Found key {keysFinded} at index {e.Value.Item2}: {e.Key}");
                                CharToFind.Remove(e.Key);
                                if (keysToFind == 0)
                                {
                                    targetIndex = index; //Quando arrivo alla 64esima chiave, vado avanti per altri 1000 indici
                                    //targetIndex = KeyIndex.Max(k => k);
                                    //targetIndex = e.Value.Item2; //Non va bene questo approccio!
                                    //break;
                                }
                            }
                            //return true;
                        }
                    }
                }
            }
            return ret;
        }
        public bool CheckRepetition(string MD5Hash, int repetitionToFind, int index)
        {
            bool ret = false;
            int count = 0;
            char last = Char.Parse(MD5Hash.Substring(0, 1));
            for (int i = 1; i < MD5Hash.Length; i++)
            {
                if (Char.Parse(MD5Hash.Substring(i, 1)).Equals(last))
                    count++;
                else
                {
                    last = Char.Parse(MD5Hash.Substring(i, 1));
                    count = 0;
                }
                if (count == repetitionToFind - 1)
                {
                    if (repetitionToFind == 3)
                    {
                        CharToFind.Add(MD5Hash, new Tuple<char, int>(last, index));
                        return true;
                    }
                    else if (repetitionToFind == 5)
                    {
                        var toEvaluate = CharToFind.Where(c => c.Value.Item1.Equals(last) && c.Value.Item2 < index);
                        if (toEvaluate.Count() > 0)
                        {
                            foreach (var e in toEvaluate.OrderBy(c => c.Value.Item2).ToList())
                            {
                                HashKey.Add(e.Key);
                                keysToFind -= 1;
                                CharToFind.Remove(e.Key);
                                if (keysToFind == 0)
                                {
                                    targetIndex = e.Value.Item2;
                                    break;
                                }
                            }
                            //return true;
                        }
                    }
                }
            }
            return ret;
        }

        #region CacheApproach
        private static string CalculateMd5Hash(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                var sb = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    sb.Append(b.ToString("x2"));
                }
                return sb.ToString();
            }
        }

        public static string Salt = "";
        
        private static readonly Regex TripleRegex = new Regex(@"(.)\1{2}");
        private const int KeysToFind = 64;
        private const int HashWindow = 1000; // Controllo dei successivi 1000 hash
        public static List<string> hashCache = new List<string>();

        private readonly List<int> KeyIndex = new List<int>();

        public int Find64thKeyIndex(string Salt)
        {
            int keyCount = 0;
            int index = -1;
            while (keyCount < KeysToFind)
            {
                index++;

                string hash = GetHash(index);

                // Cerca una tripletta
                Match tripleMatch = TripleRegex.Match(hash);
                if (!tripleMatch.Success) continue;

                // Se trovi una tripletta, verifica quintuple nei successivi 1000 hash
                char tripletChar = tripleMatch.Value[0];
                string quintuple = new string(tripletChar, 5);

                if (ContainsQuintupleWithin(index, quintuple))
                {
                    keyCount++;
                    Console.WriteLine($"Found key {keyCount} at index {index}: {hash}");
                }
            }
            return index;
        }

        private string GetHash(int index)
        {
            if (index == 2697)
            {

            }
            // Recupera o calcola l'hash corrispondente all'indice
            if (index >= hashCache.Count)
            {
                string hashInput = $"{Salt}{index}";
                string hash = CalculateMd5Hash(hashInput);
                hashCache.Add(hash);
            }
            return hashCache[index];
        }

        private bool ContainsQuintupleWithin(int currentIndex, string quintuple)
        {
            for (int offset = 1; offset <= HashWindow; offset++)
            {
                string nextHash = GetHash(currentIndex + offset);
                if (nextHash.Contains(quintuple))
                {
                    return true;
                }
            }
            return false;
        }
        #endregion
        
        public void Part2(object input, bool test, ref object solution)
        {
            string inputString = (string)input;
            Salt = inputString.Split(Delimiter.delimiter_line, StringSplitOptions.None)[0];
            bool cacheSolution = false;
            targetIndex = long.MaxValue;
            if (!cacheSolution)
            {
                for (int i = 0; keysToFind > 0 || i < targetIndex + 1000; i++)
                {
                    string md5 = CreateMD5Part2($"{Salt}{i}");
                    ClearCharToFind(i);
                    //CheckRepetition(md5, 3, i);
                    //CheckRepetition(md5, 5, i);
                    CheckRepetitionWithRegex(md5, 3, i);
                    CheckRepetitionWithRegex(md5, 5, i);
                }
                solution = KeyIndex.OrderBy(k => k).ElementAt(63);
            }
            else
            {
                solution = Find64thKeyIndexPart2(Salt);
            }
        }

        public static string CreateMD5Part2(string input)
        {
            string hash = input;
            // Itera l'hash 2017 volte
            for (int i = 0; i < 2017; i++)
            {
                hash = CalculateMd5Hash(hash);
            }
            return hash;
        }

        #region CacheApproach
        public int Find64thKeyIndexPart2(string Salt)
        {
            int keyCount = 0;
            int index = -1;

            while (keyCount < KeysToFind)
            {
                index++;

                // Calcola l'hash iterato 2017 volte
                string hash = GetStretchedHash(index);

                // Cerca una tripletta
                Match tripleMatch = TripleRegex.Match(hash);
                if (!tripleMatch.Success) continue;

                // Se trovi una tripletta, verifica quintuple nei successivi 1000 hash
                char tripletChar = tripleMatch.Value[0];
                string quintuple = new string(tripletChar, 5);

                if (ContainsQuintupleWithinPart2(index, quintuple))
                {
                    keyCount++;
                    Console.WriteLine($"Found key {keyCount} at index {index}: {hash}");
                }
            }
            return index;
        }

        private string GetStretchedHash(int index)
        {
            // Recupera o calcola l'hash iterato corrispondente all'indice
            if (index >= hashCache.Count)
            {
                string hashInput = $"{Salt}{index}";
                string hash = hashInput;

                // Itera l'hash 2017 volte
                for (int i = 0; i < 2017; i++)
                {
                    hash = CalculateMd5Hash(hash);
                }

                hashCache.Add(hash);
            }
            return hashCache[index];
        }

        private bool ContainsQuintupleWithinPart2(int currentIndex, string quintuple)
        {
            // Verifica quintuple nei successivi 1000 hash
            for (int offset = 1; offset <= HashWindow; offset++)
            {
                string nextHash = GetStretchedHash(currentIndex + offset);
                if (nextHash.Contains(quintuple))
                {
                    return true;
                }
            }
            return false;
        }
        #endregion

    }
}