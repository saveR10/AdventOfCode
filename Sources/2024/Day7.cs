using AOC;
using AOC.DataStructures.Clustering;
using AOC.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text.RegularExpressions;

namespace AOC2024
{
    public class Day7 : Solver, IDay
    {
        public void Part1(object input, bool test, ref object solution)
        {
            BigInteger TotalCalibrationResult = 0;
            bool CalibratedEquation = false;
            string inputText = (string)input;
            foreach (string equation in inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None))
            {
                CalibratedEquation = false;
                if (!string.IsNullOrEmpty(equation))
                {

                    BigInteger testValue = BigInteger.Parse(equation.Split(':')[0]);
                    List<BigInteger> numbers = new List<BigInteger>();
                    foreach (var number in equation.Split(':')[1].Trim().Split(' '))
                    {
                        numbers.Add(int.Parse(number));
                    }

                    CalibratedEquation= CanAchieveTestValue(testValue,numbers);

                    if (CalibratedEquation)
                    {
                        TotalCalibrationResult += testValue;
                    }
                }
            }
            solution = TotalCalibrationResult;
        }
        public bool CanAchieveTestValue(BigInteger testValue, List<BigInteger> numbers)
        {
            // Funzione ricorsiva che prova tutte le combinazioni di + e *
            bool Evaluate(int index, BigInteger currentValue)
            {
                // Caso base: abbiamo usato tutti i numeri
                if (index == numbers.Count)
                    return currentValue == testValue;

                // Prova l'operatore +
                if (Evaluate(index + 1, currentValue + numbers[index]))
                    return true;

                // Prova l'operatore *
                if (Evaluate(index + 1, currentValue * numbers[index]))
                    return true;

                // Nessuna combinazione funziona
                return false;
            }

            // Controlla se la lista dei numeri è vuota
            if (numbers == null || numbers.Count == 0)
                return false;

            // Avvia la ricorsione partendo dal primo numero
            return Evaluate(1, numbers[0]);
        }
        public void Part2(object input, bool test, ref object solution)
        {
            BigInteger TotalCalibrationResult = 0;
            bool CalibratedEquation = false;
            string inputText = (string)input;
            foreach (string equation in inputText.Split(Delimiter.delimiter_line, StringSplitOptions.None))
            {
                CalibratedEquation = false;
                if (!string.IsNullOrEmpty(equation))
                {

                    BigInteger testValue = BigInteger.Parse(equation.Split(':')[0]);
                    List<BigInteger> numbers = new List<BigInteger>();
                    foreach (var number in equation.Split(':')[1].Trim().Split(' '))
                    {
                        numbers.Add(int.Parse(number));
                    }

                    CalibratedEquation = CanAchieveTestValue(testValue, numbers);

                    CalibratedEquation = CanAchieveTestValueWithConcatenation(testValue, numbers);

                    if (CalibratedEquation)
                    {
                        TotalCalibrationResult += testValue;
                    }
                }
            }
            solution = TotalCalibrationResult;
        }
        public bool CanAchieveTestValueWithConcatenation(BigInteger testValue, List<BigInteger> numbers)
        {
            // Funzione ricorsiva che prova tutte le combinazioni di +, *, e ||
            bool Evaluate(int index, BigInteger currentValue)
            {
                // Caso base: abbiamo usato tutti i numeri
                if (index == numbers.Count)
                    return currentValue == testValue;

                // Prova l'operatore +
                if (Evaluate(index + 1, currentValue + numbers[index]))
                    return true;

                // Prova l'operatore *
                if (Evaluate(index + 1, currentValue * numbers[index]))
                    return true;

                // Prova l'operatore || (concatenazione)
                if (Evaluate(index + 1, Concatenate(currentValue, numbers[index])))
                    return true;

                // Nessuna combinazione funziona
                return false;
            }

            // Funzione per concatenare due numeri
            BigInteger Concatenate(BigInteger a, BigInteger b)
            {
                string concatenated = a.ToString() + b.ToString();
                return BigInteger.Parse(concatenated);
            }

            // Controlla se la lista dei numeri è vuota
            if (numbers == null || numbers.Count == 0)
                return false;

            // Avvia la ricorsione partendo dal primo numero
            return Evaluate(1, numbers[0]);
        }
    }
}
