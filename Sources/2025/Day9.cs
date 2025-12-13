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
using System.Linq;
using AOC.DataStructures.ParsingInput;
using System.Runtime.Remoting.Messaging;
using AOC.DataStructures.Cache;
using System.Runtime.Caching.Hosting;   // <- importante
using System.Runtime.Caching;

namespace AOC2025
{
    [ResearchAlgorithmsAttribute(ResolutionEnum.Cache)]
    [ResearchAlgorithmsAttribute(DifficultEnum.Hard)]
    public class Day9 : Solver, IDay
    {
        public void Part1(object input, bool test, ref object solution)
        {
            string inputText = (string)input;

            // Parsing delle coordinate (colonna, riga)
            var lines = inputText.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            var points = new List<(int x, int y)>();

            foreach (var line in lines)
            {
                var parts = line.Split(',');
                int x = int.Parse(parts[0]); // colonna
                int y = int.Parse(parts[1]); // riga
                points.Add((x, y));
            }

            long maxArea = 0;

            for (int i = 0; i < points.Count; i++)
            {
                for (int j = i + 1; j < points.Count; j++)
                {
                    var (x1, y1) = points[i];
                    var (x2, y2) = points[j];

                    long width = Math.Abs(x1 - x2) + 1;  // differenza colonne
                    long height = Math.Abs(y1 - y2) + 1;  // differenza righe

                    long area = width * height;

                    if (area > maxArea)
                        maxArea = area;
                }
            }

            solution = maxArea;
        }

        public long maxXBounder;
        public long maxYBounder;
        public long minXBounder;
        public long minYBounder;
        MemoryCache MemoryPoints = MemoryCache.Default;
        public void Part2(object input, bool test, ref object solution)
        {
            string inputText = (string)input;

            // Leggi punti
            var points = inputText
                .Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(l =>
                {
                    var p = l.Split(',');
                    return (x: long.Parse(p[0]), y: long.Parse(p[1]));
                })
                .ToList();

            int n = points.Count;

            // Costruisci poligono
            var poly = BuildPolygonVertices(points);
            var edge = BuildEdge(poly);
            long best = 0;

            maxXBounder = poly.Max(p=>p.x);
            maxYBounder = poly.Max(p => p.y);
            minXBounder = poly.Min(p => p.x);
            minYBounder = poly.Min(p => p.y);

            // Prova tutte le coppie di punti
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine(i);
                for (int j = i + 1; j < n; j++)
                {
                    long x1 = points[i].x;
                    long x2 = points[j].x;
                    long y1 = points[i].y;
                    long y2 = points[j].y;

                    long minX = Math.Min(x1, x2);
                    long maxX = Math.Max(x1, x2);
                    long minY = Math.Min(y1, y2);
                    long maxY = Math.Max(y1, y2);

                    long width = maxX - minX + 1;
                    long height = maxY - minY + 1;

                    //if (width <= 1 || height <= 1)
                    //    continue;

                    long area = width * height;
                    if (area <= best)
                        continue;

                    if (!RectangleInsideFast(minX, minY, maxX, maxY, poly, edge))
                        continue;

                    best = area;
                }
            }

            solution = best;
        }
        bool PointOnEdge(long x, long y,
                 List<Tuple<(long x, long y), (long x, long y)>> edges)
        {
            foreach (var e in edges)
            {
                var (x1, y1) = e.Item1;
                var (x2, y2) = e.Item2;

                // spigolo verticale
                if (x1 == x2)
                {
                    if (x == x1 && y >= Math.Min(y1, y2) && y <= Math.Max(y1, y2))
                        return true;
                }
                // spigolo orizzontale
                else if (y1 == y2)
                {
                    if (y == y1 && 
                        x >= Math.Min(x1, x2) && 
                        x <= Math.Max(x1, x2))
                        return true;
                }
            }

            return false;
        }

        public List<Tuple<(long x, long y), (long x, long y)>> BuildEdge(List<(long x, long y)> poly)
        {
            List<Tuple<(long x, long y), (long x, long y)>> edge = new List<Tuple<(long x, long y), (long x, long y)>>();
            int n= poly.Count;
            for(int i=0;i<poly.Count;i++)
            {
                edge.Add(new Tuple<(long x, long y), (long x, long y)> ((poly[i % n]),(poly[(i + 1) % n])));
            }

            return edge;
        }
        bool RectangleInside(long minX, long minY, long maxX, long maxY,
                     List<(long x, long y)> poly,
                     List<Tuple<(long x, long y), (long x, long y)>> edge)
        {
            for (long x = minX; x <= maxX; x++)
            {
                // lato alto, verso destra
                if (!PointInside(x, minY, poly, edge)) return false;
                // lato basso, verso destra
                if (!PointInside(x, maxY, poly, edge)) return false;
            }

            for (long y = minY; y <= maxY; y++)
            {
                //lato sinistro, verso giù
                if (!PointInside(minX, y, poly, edge)) return false;
                //lato destro, verso giù
                if (!PointInside(maxX, y, poly, edge)) return false;
            }

            return true;
        }
        bool RectangleInsideFast(long minX, long minY, long maxX, long maxY,
                           List<(long x, long y)> poly,
                           List<Tuple<(long x, long y), (long x, long y)>> edges)
        {
            // 1) controlla i 4 angoli
            if (!PointInside(minX, minY, poly, edges)) return false;
            if (!PointInside(minX, maxY, poly, edges)) return false;
            if (!PointInside(maxX, minY, poly, edges)) return false;
            if (!PointInside(maxX, maxY, poly, edges)) return false;

            for (long x = minX+1; x <= maxX-1; x++)
            {
                // lato alto, verso destra
                if (!EdgePointInside(x, minY, poly, edges)) return false;
                // lato basso, verso destra
                if (!EdgePointInside(x, maxY, poly, edges)) return false;
            }

            for (long y = minY+1; y <= maxY-1; y++)
            {
                //lato sinistro, verso giù
                if (!EdgePointInside(minX, y, poly, edges)) return false;
                //lato destro, verso giù
                if (!EdgePointInside(maxX, y, poly, edges)) return false;
            }



            return true;
        }
        public bool EdgePointInside(long x, long y,
         List<(long x, long y)> poly,
         List<Tuple<(long x, long y), (long x, long y)>> edges)
        {
            var key = (x, y); // puoi usare direttamente la tupla come chiave

            if (!MemoryPoints.Contains(key.ToString()))
            {
                bool value = PointInside(x, y, poly, edges);
                MemoryPoints.Add(key.ToString(), value, DateTimeOffset.Now.AddMinutes(10));
            }

            return (bool)MemoryPoints.Get(key.ToString());
        }

        bool SegmentsIntersect(
         (long x, long y) a1, (long x, long y) a2,
         (long x, long y) b1, (long x, long y) b2)
        {
            // stesso segmento (o invertito)
            if ((a1 == b1 && a2 == b2) || (a1 == b2 && a2 == b1))
                return true;

            // segmento A verticale
            if (a1.x == a2.x)
            {
                long x = a1.x;
                long ay1 = Math.Min(a1.y, a2.y);
                long ay2 = Math.Max(a1.y, a2.y);

                // segmento B verticale
                if (b1.x == b2.x)
                {
                    if (x != b1.x) return false;

                    long by1 = Math.Min(b1.y, b2.y);
                    long by2 = Math.Max(b1.y, b2.y);

                    return ay1 <= by2 && ay2 >= by1;
                }
                // segmento B orizzontale
                else
                {
                    long bx1 = Math.Min(b1.x, b2.x);
                    long bx2 = Math.Max(b1.x, b2.x);
                    long y = b1.y;

                    return x >= bx1 && x <= bx2 &&
                           y >= ay1 && y <= ay2;
                }
            }
            // segmento A orizzontale
            else
            {
                long y = a1.y;
                long ax1 = Math.Min(a1.x, a2.x);
                long ax2 = Math.Max(a1.x, a2.x);

                // segmento B verticale
                if (b1.x == b2.x)
                {
                    long x = b1.x;
                    long by1 = Math.Min(b1.y, b2.y);
                    long by2 = Math.Max(b1.y, b2.y);

                    return x >= ax1 && x <= ax2 &&
                           y >= by1 && y <= by2;
                }
                // segmento B orizzontale
                else
                {
                    if (y != b1.y) return false;

                    long bx1 = Math.Min(b1.x, b2.x);
                    long bx2 = Math.Max(b1.x, b2.x);

                    return ax1 <= bx2 && ax2 >= bx1;
                }
            }
        }


        long Direction((long x, long y) a, (long x, long y) b, (long x, long y) c)
        {
            return (c.x - a.x) * (b.y - a.y) -
                   (c.y - a.y) * (b.x - a.x);
        }

        bool OnSegment((long x, long y) a, (long x, long y) b, (long x, long y) c)
        {
            return Math.Min(a.x, b.x) <= c.x && c.x <= Math.Max(a.x, b.x) &&
                   Math.Min(a.y, b.y) <= c.y && c.y <= Math.Max(a.y, b.y);
        }



        bool PointInside(long x, long y,
    List<(long x, long y)> poly,
    List<Tuple<(long x, long y), (long x, long y)>> edges)
        {
            // vertice
            for (int i = 0; i < poly.Count; i++)
                if (poly[i].x == x && poly[i].y == y)
                    return true;

            // spigolo
            if (PointOnEdge(x, y, edges)) return true;

            bool inside = false;

            foreach (var e in edges)
            {
                var (x1, y1) = e.Item1;
                var (x2, y2) = e.Item2;

                if ((y1 > y) != (y2 > y))
                {
                    double xi = x1 + (double)(y - y1) * (x2 - x1) / (y2 - y1);
                    if (xi > x)
                        inside = !inside;
                }
            }

            return inside;
        }


        private List<(long x, long y)> BuildPolygonVertices(List<(long x, long y)> pts)
        {
            var poly = new List<(long, long)>();
            int n = pts.Count;

            long px = pts[0].x;
            long py = pts[0].y;
            long dxPrev = 0, dyPrev = 0;



            for (int i = 1; i <= n; i++)
            {
                var (nx, ny) = pts[i % n];

                long dx = Math.Sign(nx - px);
                long dy = Math.Sign(ny - py);

                if (dx != dxPrev || dy != dyPrev)
                    poly.Add((px, py));

                dxPrev = dx;
                dyPrev = dy;
                px = nx;
                py = ny;
            }

            return poly;
        }


    }
}