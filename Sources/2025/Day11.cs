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

namespace AOC2025
{
    [ResearchAlgorithmsAttribute(ResolutionEnum.DFS)]
    [ResearchAlgorithmsAttribute(ResolutionEnum.Cache)]
    [ResearchAlgorithmsAttribute(TypologyEnum.Recursive)]
    [ResearchAlgorithmsAttribute(DifficultEnum.Medium)]
    public class Day11 : Solver, IDay
    {
        public void Part1(object input, bool test, ref object solution)
        {
            string inputText = (string)input;

            // Parsing
            var graph = new Dictionary<string, List<string>>();

            foreach (var line in inputText.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries))
            {
                var parts = line.Split(':');
                var name = parts[0].Trim();
                var outs = parts[1].Trim().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();

                graph[name] = outs;
            }

            // Memo cache
            var memo = new Dictionary<string, long>();

            long DFS(string node)
            {
                if (node == "out")
                    return 1;

                if (memo.TryGetValue(node, out long cached))
                    return cached;

                long total = 0;

                if (graph.ContainsKey(node))
                {
                    foreach (var nxt in graph[node])
                        total += DFS(nxt);
                }

                memo[node] = total;
                return total;
            }

            solution = DFS("you");
        }


        public void Part2(object input, bool test, ref object solution)
        {
            if (test)
            {
                input = @"svr: aaa bbb
aaa: fft
fft: ccc
bbb: tty
tty: ccc
ccc: ddd eee
ddd: hub
hub: fff
eee: dac
dac: fff
fff: ggg hhh
ggg: out
hhh: out";
            }
                string inputText = (string)input;

                // Parse graph
                var graph = new Dictionary<string, List<string>>();

                foreach (var line in inputText.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    var parts = line.Split(':');
                    var name = parts[0].Trim();
                    var outs = parts[1].Trim().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();

                    graph[name] = outs;
                }

                // Memo: (node, seenDAC, seenFFT) -> count
                var memo = new Dictionary<(string node, bool seenDAC, bool seenFFT), long>();

                long DFS(string node, bool seenDAC, bool seenFFT)
                {
                    // Se passo da dac/fft aggiorno i flag
                    if (node == "dac") seenDAC = true;
                    if (node == "fft") seenFFT = true;

                    // Caso terminale
                    if (node == "out")
                    {
                        return (seenDAC && seenFFT) ? 1 : 0;
                    }

                    var key = (node, seenDAC, seenFFT);
                    if (memo.TryGetValue(key, out long cached))
                        return cached;

                    long total = 0;

                    if (graph.ContainsKey(node))
                    {
                        foreach (var nxt in graph[node])
                            total += DFS(nxt, seenDAC, seenFFT);
                    }

                    memo[key] = total;
                    return total;
                }

                solution = DFS("svr", false, false);
            }

        }
    }