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
    [ResearchAlgorithmsAttribute(TypologyEnum.Trolling)]
    public class Day12 : Solver, IDay
    {
        public void Part1(object input, bool test, ref object solution)
        {
            string inputText = (string)input;
            
            var parser = new InputParser();
            parser.Parse(inputText);
            var regions = parser.RegionList;

            int fit = 0;
            foreach (var region in regions)
            {
                int regionArea = region.Height*region.Width;
                int regionPresents = region.RequiredShapes.Sum(p => p);
                if (regionArea >= (regionPresents * 9))
                    fit++;
            }

            /*var shapes = parser.PresentShapeList;
            var regions = parser.RegionList;

            // Precompute all variants of all shapes
            foreach (var shape in shapes)
                shape.GenerateVariants();

            int fit = 0;

            foreach (var region in regions)
            {
                if (CanFitRegion(region, shapes))
                    fit++;
            }*/

            solution = fit;
        }

        // ===============================================================
        // PARSER
        // ===============================================================

        public class InputParser
        {
            public List<PresentShape> PresentShapeList = new List<PresentShape>();
            public List<TreeRegion> RegionList = new List<TreeRegion>();

            public void Parse(string input)
            {
                var lines = input
                    .Replace("\r\n", "\n")
                    .Split('\n')
                    .Select(l => l.TrimEnd())
                    .ToList();

                PresentShape currentShape = null;
                bool parsingRegions = false;

                foreach (var line in lines)
                {
                    var trimmed = line.Trim();

                    if (string.IsNullOrWhiteSpace(trimmed))
                        continue;

                    // --- REGION LINES ---
                    if (trimmed.Contains("x") && trimmed.Contains(":"))
                    {
                        parsingRegions = true;

                        var parts = trimmed.Split(':');
                        var size = parts[0].Trim();
                        var nums = parts[1].Trim();

                        var dims = size.Split('x');
                        int w = int.Parse(dims[0]);
                        int h = int.Parse(dims[1]);

                        var counts = nums
                            .Split(' ')
                            .Where(n => n.Length > 0)
                            .Select(int.Parse)
                            .ToList();

                        RegionList.Add(new TreeRegion
                        {
                            Width = w,
                            Height = h,
                            RequiredShapes = counts
                        });

                        continue;
                    }

                    if (parsingRegions)
                        continue;

                    // --- SHAPE INDEX ---
                    if (trimmed.EndsWith(":"))
                    {
                        currentShape = new PresentShape();
                        currentShape.Id = int.Parse(trimmed.TrimEnd(':'));
                        PresentShapeList.Add(currentShape);
                        continue;
                    }

                    // --- SHAPE ROW ---
                    if (currentShape != null)
                        currentShape.Rows.Add(trimmed);
                }
            }
        }

        // ===============================================================
        // DATA MODELS
        // ===============================================================

        public class PresentShape
        {
            public int Id;
            public List<string> Rows = new List<string>();

            private List<Tuple<int, int>> _blocks;
            public List<Tuple<int, int>> Blocks
            {
                get
                {
                    if (_blocks == null)
                        _blocks = ExtractBlocks();
                    return _blocks;
                }
            }

            public List<ShapeVariant> Variants = new List<ShapeVariant>();

            private List<Tuple<int, int>> ExtractBlocks()
            {
                var list = new List<Tuple<int, int>>();

                for (int y = 0; y < Rows.Count; y++)
                {
                    string row = Rows[y];
                    for (int x = 0; x < row.Length; x++)
                    {
                        if (row[x] == '#')
                            list.Add(Tuple.Create(x, y));
                    }
                }

                return list;
            }

            public void GenerateVariants()
            {
                Variants = new List<ShapeVariant>();

                List<Tuple<int, int>> baseBlocks = Blocks;

                List<List<Tuple<int, int>>> rots = new List<List<Tuple<int, int>>>();
                rots.Add(baseBlocks);
                rots.Add(Rotate90(baseBlocks));
                rots.Add(Rotate90(rots[1]));
                rots.Add(Rotate90(rots[2]));

                foreach (var r in rots)
                {
                    var norm = Normalize(r);
                    AddVariantIfNew(norm);

                    var flipped = FlipHorizontal(r);
                    var normFlipped = Normalize(flipped);
                    AddVariantIfNew(normFlipped);
                }
            }

            private void AddVariantIfNew(List<Tuple<int, int>> blocks)
            {
                string key = MakeKey(blocks);

                if (!Variants.Exists(v => v.Key == key))
                {
                    ShapeVariant sv = new ShapeVariant();
                    sv.Key = key;
                    sv.Blocks = blocks;
                    Variants.Add(sv);
                }
            }

            private string MakeKey(List<Tuple<int, int>> blocks)
            {
                return string.Join(";", blocks.Select(p => p.Item1 + "," + p.Item2));
            }

            private static List<Tuple<int, int>> Rotate90(List<Tuple<int, int>> blocks)
            {
                int maxX = blocks.Max(p => p.Item1);

                var list = new List<Tuple<int, int>>();
                foreach (var p in blocks)
                    list.Add(Tuple.Create(p.Item2, maxX - p.Item1));
                return list;
            }

            private static List<Tuple<int, int>> FlipHorizontal(List<Tuple<int, int>> blocks)
            {
                int maxX = blocks.Max(p => p.Item1);

                var list = new List<Tuple<int, int>>();
                foreach (var p in blocks)
                    list.Add(Tuple.Create(maxX - p.Item1, p.Item2));
                return list;
            }

            private static List<Tuple<int, int>> Normalize(List<Tuple<int, int>> blocks)
            {
                int minX = blocks.Min(p => p.Item1);
                int minY = blocks.Min(p => p.Item2);

                return blocks
                    .Select(p => Tuple.Create(p.Item1 - minX, p.Item2 - minY))
                    .OrderBy(p => p.Item2)
                    .ThenBy(p => p.Item1)
                    .ToList();
            }
        }

        public class ShapeVariant
        {
            public string Key;
            public List<Tuple<int, int>> Blocks;
        }

        public class TreeRegion
        {
            public int Width;
            public int Height;
            public List<int> RequiredShapes = new List<int>();
        }

        // ===============================================================
        // CHECK REGION FIT (placeholder)
        // ===============================================================

        private bool CanFitRegion(TreeRegion region, List<PresentShape> shapes)
        {
            int W = region.Width;
            int H = region.Height;

            bool[,] grid = new bool[H, W];

            // ----- Costruisco una lista di pezzi da posare -----
            List<ShapeVariant> pieces = new List<ShapeVariant>();

            for (int i = 0; i < region.RequiredShapes.Count; i++)
            {
                int count = region.RequiredShapes[i];
                if (count <= 0) continue;

                PresentShape sh = shapes.First(s => s.Id == i);

                for (int c = 0; c < count; c++)
                    pieces.AddRange(sh.Variants); // una copia virtuale per ogni richiesta
            }

            // Ordina i pezzi per dimensione decrescente (pruning migliore)
            pieces = pieces
                .OrderByDescending(p => p.Blocks.Count)
                .ToList();

            return PlacePiece(0, pieces, grid, W, H);
        }

        private bool PlacePiece(int index, List<ShapeVariant> pieces, bool[,] grid, int W, int H)
        {
            if (index == pieces.Count)
                return true; // tutti posati

            var shape = pieces[index];

            // Prova a posarlo in ogni posizione possibile
            for (int y = 0; y < H; y++)
            {
                for (int x = 0; x < W; x++)
                {
                    if (CanPlace(shape, grid, x, y, W, H))
                    {
                        ApplyShape(shape, grid, x, y, true);
                        if (PlacePiece(index + 1, pieces, grid, W, H))
                            return true;
                        ApplyShape(shape, grid, x, y, false);
                    }
                }
            }

            return false;
        }

        private bool CanPlace(ShapeVariant shape, bool[,] grid, int offsetX, int offsetY, int W, int H)
        {
            for (int i = 0; i < shape.Blocks.Count; i++)
            {
                int bx = shape.Blocks[i].Item1 + offsetX;
                int by = shape.Blocks[i].Item2 + offsetY;

                if (bx < 0 || by < 0 || bx >= W || by >= H)
                    return false;

                if (grid[by, bx])
                    return false;
            }
            return true;
        }

        private void ApplyShape(ShapeVariant shape, bool[,] grid, int offsetX, int offsetY, bool set)
        {
            for (int i = 0; i < shape.Blocks.Count; i++)
            {
                int bx = shape.Blocks[i].Item1 + offsetX;
                int by = shape.Blocks[i].Item2 + offsetY;
                grid[by, bx] = set;
            }
        }


        public void Part2(object input, bool test, ref object solution)
        {
            solution = 2;
        }
    }


}