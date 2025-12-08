using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AOC2025.Day8;

namespace AOC.DataStructures.ParsingInput
{
    public static class Input3DPoint
    {
        public static List<Point3D> Parse(string input)
        {
            List<Point3D> points = new List<Point3D>();

            string[] lines = input.Split(
                new[] { "\r\n", "\n" },
                StringSplitOptions.RemoveEmptyEntries);

            foreach (string line in lines)
            {
                string[] parts = line.Split(',');
                int x = int.Parse(parts[0]);
                int y = int.Parse(parts[1]);
                int z = int.Parse(parts[2]);
                points.Add(new Point3D(x, y, z));
            }

            return points;
        }
    }
}
