using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AOC.DataStructures.AlghoritmsOptimization
{
    public class TwoStackAlgorithm
    {
        private int n;          // size of the stack
        private Node first;     // top of stack

        // helper linked list class
        private class Node
        {
            public string item;
            public Node next;
        }

        public bool IsEmpty()
        {
            return first == null;
        }

        public int Size()
        {
            return n;
        }

        public void Push(string item)
        {
            Node oldfirst = first;
            first = new Node();
            first.item = item;
            first.next = oldfirst;
            n++;
            Check();
        }

        public string Pop()
        {
            if (IsEmpty()) throw new InvalidOperationException("Stack underflow");
            string item = first.item;
            first = first.next;
            n--;
            Check();
            return item;
        }

        public string Peek()
        {
            if (IsEmpty()) throw new InvalidOperationException("Stack underflow");
            return first.item;
        }

        private bool Check()
        {
            if (n < 0)
            {
                return false;
            }
            if (n == 0)
            {
                if (first != null) return false;
            }
            else if (n == 1)
            {
                if (first == null) return false;
                if (first.next != null) return false;
            }
            else
            {
                if (first == null) return false;
                if (first.next == null) return false;
            }

            int numberOfNodes = 0;
            for (Node x = first; x != null && numberOfNodes <= n; x = x.next)
            {
                numberOfNodes++;
            }
            if (numberOfNodes != n) return false;

            return true;
        }

        public static void Example()
        {
            long startTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            string inputName = "mathExpression.txt";
            string esercizio = "TwoStackAlgorithm";

            using (StreamWriter writer = new StreamWriter($"../../DataStructures/IO/Output/{esercizio}_{inputName}"))
            {
                writer.Write($"Questo è il risultato dell'esercizio {esercizio} con input {inputName} :");
                writer.WriteLine();

                string stringReader = File.ReadAllText($"../../DataStructures/IO/Input/{inputName}");

                string result = "";

                Stack<string> ops = new Stack<string>();
                Stack<double> vals = new Stack<double>();
                for (int i = 0; i < stringReader.Length; i++)
                {
                    if (stringReader[i] == '(')
                    {
                    }
                    else if (stringReader[i] == '+' || stringReader[i] == '-' || stringReader[i] == '*' || stringReader[i] == '/')
                    {
                        ops.Push(stringReader[i].ToString());
                    }
                    else if (stringReader[i] == ')')
                    {
                        string op1 = ops.Pop();
                        double num1 = vals.Pop();
                        double num2 = vals.Pop();
                        double ris = 0.0;
                        if (op1[0] == '+') { ris = num1 + num2; }
                        if (op1[0] == '-') { ris = num2 - num1; }
                        if (op1[0] == '*') { ris = num1 * num2; }
                        if (op1[0] == '/') { ris = num1 / num2; }
                        vals.Push(ris);
                    }
                    else
                    {
                        vals.Push(double.Parse(stringReader[i].ToString()));
                    }
                }

                writer.Write("Il risultato è " + vals.Pop().ToString());
                writer.WriteLine();
            }
        }
    }
}
