using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC.Utilities.Drawing
{
    public class MatrixToImage
    {
        public static void MatrixToJpeg(string[,] matrix, string filePath)
        {
            int cellSize = 10; // Dimensione di ogni cella in pixel
            int n = matrix.GetLength(0);
            int m = matrix.GetLength(1);

            using (Bitmap bitmap = new Bitmap(m * cellSize, n * cellSize))
            {
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.Clear(Color.White); // Sfondo bianco

                    for (int i = 0; i < n; i++)
                    {
                        for (int j = 0; j < m; j++)
                        {
                            Color color = GetColor(matrix[i, j]);
                            using (Brush brush = new SolidBrush(color))
                            {
                                g.FillRectangle(brush, j * cellSize, i * cellSize, cellSize, cellSize);
                            }
                        }
                    }
                }

                // Salva l'immagine come JPEG
                bitmap.Save(filePath, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
        }

        private static Color GetColor(string value)
        {
            switch (value)
            {
                case "#": return Color.Black;  // Muri
                case ".": return Color.White;  // Spazio vuoto
                case "@": return Color.Blue;   // Robot
                case "[": return Color.Green;  // Box parte sinistra
                case "]": return Color.Yellow;    // Box parte destra
                default: return Color.Gray;      // Default
            }
        }
    }

}
