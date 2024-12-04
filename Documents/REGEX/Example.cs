using AOC.Documents.LINQ;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AOC.Documents.REGEX
{
    public static class REGEX
    {
        public static void Example()
        {
            string text = ".@23sdA  VBRRT_-abc1xyza223!'ì2mul(1,2)mul(xx)mul(12)mul(123,124)";
            Console.WriteLine($"\n{text}");

            //NUMERI
            // \d   corrisponde a una cifra numerica (0-9)
            string pattern1 = @"\d";
            MatchCollection matches1 = Regex.Matches(text, pattern1);
            ShowMatches(pattern1,matches1);

            // \D   corrisponde a qualsiasi carattere che non sia una cifra
            string pattern2 = @"\D";
            MatchCollection matches2 = Regex.Matches(text, pattern2);
            ShowMatches(pattern2,matches2);


            //LETTERE ALFABETICHE
            // [a-z]    corrisponde a lettere minuscole dalla a alla z
            string pattern3 = @"[a-z]";
            MatchCollection matches3 = Regex.Matches(text, pattern3);
            ShowMatches(pattern3, matches3);

            // [A-Z]    corrisponde a lettere maiuscole dalla A alla Z
            string pattern4 = @"[A-Z]";
            MatchCollection matches4 = Regex.Matches(text, pattern4);
            ShowMatches(pattern4, matches4);

            // [a-zA - Z]: corrisponde a qualsiasi lettera maiuscola o minuscola
            string pattern5 = @"[a-zA-Z]";
            MatchCollection matches5 = Regex.Matches(text, pattern5);
            ShowMatches(pattern5, matches5);

            // [a-zA-Z0-9]
            string pattern13 = @"[a-zA-Z0-9]";
            MatchCollection matches13 = Regex.Matches(text, pattern13);
            ShowMatches(pattern13, matches13);


            //CARATTERI ALFANUMERICI
            // \w       corrisponde a lettere, numeri e il carattere underscore _(equivale a [a-zA - Z0 - 9_])
            string pattern6 = @"\w";
            MatchCollection matches6 = Regex.Matches(text, pattern6);
            ShowMatches(pattern6, matches6);

            // \W       corrisponde a tutto ciò che non è alfanumerico o _
            string pattern7 = @"\W";
            MatchCollection matches7 = Regex.Matches(text, pattern7);
            ShowMatches(pattern7, matches7);

            //SPAZI E CARATTERI DI CONTROLLO
            // \s       corrisponde a qualsiasi spazio bianco, incluso spazio, tabulazione e ritorno a capo.
            string pattern8 = @"\s";
            MatchCollection matches8 = Regex.Matches(text, pattern8);
            ShowMatches(pattern8, matches8);

            //  \S      corrisponde a qualsiasi carattere che non sia uno spazio bianco.
            string pattern9 = @"\S";
            MatchCollection matches9 = Regex.Matches(text, pattern9);
            ShowMatches(pattern9, matches9);


            //CARATTERI GENERICI
            //  .       corrisponde a qualsiasi carattere eccetto il ritorno a capo(\n).
            string pattern10 = @".";
            MatchCollection matches10 = Regex.Matches(text, pattern10);
            ShowMatches(pattern10, matches10);

            //  [xyz]   corrisponde a uno qualsiasi dei caratteri elencati(es., x, y o z).
            string pattern11 = @"[xyz]";
            MatchCollection matches11 = Regex.Matches(text, pattern11);
            ShowMatches(pattern11, matches11);

            //  [^xyz]  corrisponde a qualsiasi carattere escluso x, y o z.
            string pattern12 = @"[^xyz]";
            MatchCollection matches12 = Regex.Matches(text, pattern12);
            ShowMatches(pattern12, matches12);

            //CARATTERI SPECIALI.
            //Per indicare caratteri speciali come "." o "*", è necessario "escaparli" con una barra inversa(\):
            //\.: corrisponde al punto..
            //\*: corrisponde all'asterisco *.

            //matchare la stringa mul(X,Y) dove X e Y sono dei numeri
            string pattern14 = @"mul\((\d,\d)\)";
            MatchCollection matches14 = Regex.Matches(text, pattern14);
            ShowMatches(pattern14, matches14);

            //matchare la stringa mul(X,Y) dove X e Y sono dei numeri ad 1 cifra
            string pattern15 = @"mul\((\d{1},\d{1})\)";
            MatchCollection matches15 = Regex.Matches(text, pattern15);
            ShowMatches(pattern15, matches15);

            //matchare la stringa mul(X,Y) dove X e Y sono dei numeri a 2 cifre
            string pattern16 = @"mul\((\d{2},\d{2})\)";
            MatchCollection matches16 = Regex.Matches(text, pattern16);
            ShowMatches(pattern16, matches16);

            //matchare la stringa mul(X,Y) dove X e Y sono dei numeri a 3 cifre
            string pattern17 = @"mul\((\d{3},\d{3})\)";
            MatchCollection matches17 = Regex.Matches(text, pattern17);
            ShowMatches(pattern17, matches17);

            Console.ReadLine();
        }
        public static void ShowMatches(string pattern, MatchCollection matches)
        {
            Console.Write($"{pattern.PadRight(30)}");
            foreach(Match match in matches)
            {
                Console.Write(match);
            }
            Console.WriteLine();
        }
    }
}
