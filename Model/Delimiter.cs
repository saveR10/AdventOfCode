using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC.Model
{
    internal static class Delimiter
    {
        public static String[] delimiters = { "\r\n", " " };
        public static String[] delimiter_space = { " " };
        public static String[] delimiter_line = { "\r\n","\n" };
        public static String[] delimiter_equals = { "=" };
        public static String[] delimiter_parentesi = { "(", ")"};
        public static String[] delimiter_comma = { "," };
        public static String[] delimiter_parentesi_graffe = { "{", "}" };
        public static String[] delimiter_signs = { "=", "-" };
        public static String[] delimiter_parentesi_inizio = { "{" };
        public static String[] delimiter_parentesi_fine = { "}" };
        public static String[] delimiter_puntovirgola = { ";" };
        public static String[] delimiter_operator = { "<",">","=="};
        public static String[] delimiter_tilde = { "~"};
        public static String[] Backslash = { "\\" };
        public static String[] Dot = { "." };
        public static String[] DoubleDot = { ":" };
        public static String[] delimiter_SquareBrackets= { "[","]" };
        public static String[] delimiter_dash = { "-"};
        public static String[] delimiter_x = { "x" };

    }
}
