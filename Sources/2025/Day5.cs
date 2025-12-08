using AOC;
using AOC.DataStructures.Clustering;
using AOC.Model;
using AOC.SearchAlghoritmhs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using static AOC.SearchAlghoritmhs.ResearchAlgorithmsAttribute;
using System.Linq;   // <- importante

namespace AOC2025
{
    //[ResearchAlgorithmsAttribute(ResolutionEnum)]
    public class Day5 : Solver, IDay
    {
        public void Part1(object input, bool test, ref object solution)
        {
            string inputText = (string)input;
            var parts = inputText.Split(new[] { "\n\n", "\r\n\r\n" }, StringSplitOptions.None);

            var ranges = parts[0]
                .Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(line =>
                {
                    var x = line.Split('-');
                    return (start: long.Parse(x[0]), end: long.Parse(x[1]));
                })
                .ToList();

            var ids = parts[1]
                .Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(long.Parse)
                .ToList();

            int freshCount = ids.Count(id => IsFresh(id, ranges));

            solution = freshCount;
        }

        static bool IsFresh(long id, List<(long start, long end)> ranges)
        {
            foreach (var (start, end) in ranges)
            {
                if (id >= start && id <= end)
                    return true;
            }
            return false;
        }


        public void Part2(object input, bool test, ref object solution)
        {
            string inputText = (string)input;
            var parts = inputText.Split(new[] { "\n\n", "\r\n\r\n" }, StringSplitOptions.None);

            var ranges = parts[0]
            .Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries)
            .Select(line =>
            {
                var p = line.Split('-');
                return (start: long.Parse(p[0]), end: long.Parse(p[1]));
            })
            .ToList();

            // Sort by starting point
            ranges.Sort((a, b) => a.start.CompareTo(b.start));

            // Merge overlapping ranges
            var merged = new List<(long start, long end)>();
            var current = ranges[0];

            for (int i = 1; i < ranges.Count; i++)
            {
                var next = ranges[i];

                if (next.start <= current.end + 1)
                {
                    // overlap → extend
                    current.end = Math.Max(current.end, next.end);
                }
                else
                {
                    // no overlap → push and move to next
                    merged.Add(current);
                    current = next;
                }
            }
            merged.Add(current);

            // --- Count fresh IDs ---
            long totalFresh = merged.Sum(r => r.end - r.start + 1);

            solution = totalFresh;
        }
    }
}