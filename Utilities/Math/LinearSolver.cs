using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC.Utilities.Math
{
    //recupera alcuni utilities script interessanti da qui
    //https://github.com/tmbarker/advent-of-code/blob/main/Utilities/Collections/CircularBuffer.cs
    public static class LinearSolver
    {
        /// <summary>
        ///     Solve a linear system of equations specified by an augmented coefficient matrix.
        /// </summary>
        /// <param name="a">
        ///     The augmented coefficient matrix. Coefficients should be of the form:
        ///     <para>
        ///         <b>a₀x + a₁y + a₂z = a₃</b>
        ///     </para>
        /// </param>
        /// <param name="epsilon">
        ///     The smallest value to consider non-zero for the purpose of determining if the system
        ///     is consistent
        /// </param>
        /// <returns>A vector containing a solution to the system of equations</returns>
        /// <exception cref="InvalidOperationException">The system of equation has no real solution</exception>
        public static double[] Solve(double[,] a, double epsilon = 1e-10)
        {
            var n = a.GetLength(0);
            var x = new double[n];
            PartialPivot(a, n, epsilon);
            BackSubstitute(a, n, x);
            return x;
        }

        private static void PartialPivot(double[,] a, int n, double epsilon)
        {
            for (var i = 0; i < n; i++)
            {
                //  Partial pivoting
                var pivotRow = i;
                for (var j = i + 1; j < n; j++)
                {
                    if (System.Math.Abs(a[j, i]) > System.Math.Abs(a[pivotRow, i]))
                    {
                        pivotRow = j;
                    }
                }

                //  Swap rows if necessary
                if (pivotRow != i)
                {
                    for (var j = i; j <= n; j++)
                    {
                        var temp = a[i, j];
                        a[i, j] = a[pivotRow, j];
                        a[pivotRow, j] = temp;
                    }
                }

                //  Check for a zero-value pivot, with a non-zero b-value, which indicates an inconsistent system
                if (System.Math.Abs(a[i, i]) < epsilon && System.Math.Abs(a[i, n]) > epsilon)
                {
                    throw new InvalidOperationException("No solution; system is inconsistent.");
                }

                //  Perform elimination below the pivot
                for (var j = i + 1; j < n; j++)
                {
                    var factor = a[j, i] / a[i, i];
                    for (var k = i; k <= n; k++)
                    {
                        a[j, k] -= factor * a[i, k];
                    }
                }
            }
        }

        private static void BackSubstitute(double[,] a, int n, double[] x)
        {
            for (var i = n - 1; i >= 0; i--)
            {
                var sum = 0.0;
                for (var j = i + 1; j < n; j++)
                {
                    sum += a[i, j] * x[j];
                }

                x[i] = (a[i, n] - sum) / a[i, i];
            }
        }
    }
}