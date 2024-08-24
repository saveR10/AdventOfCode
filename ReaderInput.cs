using AOC.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AOC
{
    public class ReaderInput
    {
        public string InputText(int year, int day) => Input(File.ReadAllText, year, day);
        public IEnumerable<string> InputLines(int year, int day) => Input(File.ReadLines, year, day);
        public T Input<T>(Func<string, T> f, int year, int day)
        {
            string sessionId = "53616c7465645f5f7e1c8a6f3c55abdd3ed4d582ae5b047a22a66496cfdb2d10c191ca590510a65f50f0f1dc8c899ec577bb39f463012274a59a37e9d463fd7d";
            string input;
            var fileName = Path.Combine("C:\\Users\\s.cecere\\source\\repos\\AOC\\AOC\\Input\\", $"{year}\\day{day}.txt");
            if (!File.Exists(fileName))
            {
                Uri uri = new Uri($"https://adventofcode.com/{year}/day/{day}/input");
                var handler = new HttpClientHandler() { CookieContainer = new CookieContainer() };
                var client = new HttpClient(handler);
                handler.CookieContainer.Add(new Cookie("session", sessionId, "/", ".adventofcode.com"));
                input = client.GetStringAsync(uri).Result;
                File.WriteAllText(fileName, input);
            }
            else
            {
                return f(fileName);
            }
            return f(fileName);
        }



        public string TestText(int year, int day)
        {
            var fileName = Path.Combine("C:\\Users\\s.cecere\\source\\repos\\AOC\\AOC\\Test\\", $"{year}\\day{day}.txt");

            if (File.Exists(fileName))
            {
                return File.ReadAllText(fileName);
            }
            else
            {
                File.WriteAllText(fileName, test);
                return test;
            }
        }

        public IEnumerable<string> TestLines(int year, int day)
        {
            var fileName = Path.Combine("C:\\Users\\s.cecere\\source\\repos\\AOC\\AOC\\Test\\", $"{year}\\day{day}.txt");

            if (File.Exists(fileName))
            {
                return File.ReadAllText(fileName).Split(Delimiter.delimiter_line, StringSplitOptions.None);
            }
            else
            {
                File.WriteAllText(fileName, test);
                return (test).Split(Delimiter.delimiter_line, StringSplitOptions.None);
            }

        }
        public string test = @"London to Dublin = 464
London to Belfast = 518
Dublin to Belfast = 141";

    }
}
