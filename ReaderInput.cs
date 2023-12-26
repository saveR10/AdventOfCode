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
            string sessionId = "53616c7465645f5fb7277cc7747627ee8beeed829b7d1784203cd8f83d77623a6ee7c2774a72f8bce9f51d340a8d65cd8246ea4ea43d92e9dfbd1b7db8249804";
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
        public string test = @""""" 
""abc"" 
""aaa\""aaa"" 
""\x27""""";

    }
}
