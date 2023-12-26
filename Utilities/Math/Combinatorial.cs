using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC.Utilities.Math
{
    internal class Combinatorial
    {
        static IEnumerable<IEnumerable<T>>
    GetPermutationsWithRept<T>(IEnumerable<T> list, int length)
        {
            if (length == 1) return list.Select(t => new T[] { t });
            return GetPermutationsWithRept(list, length - 1)
                .SelectMany(t => list,
                    (t1, t2) => t1.Concat(new T[] { t2 }));
        }

        /// <summary>
        /// This function gets the total number of unique combinations based upon N and K.
        /// N is the total number of items.
        /// K is the size of the group.
        /// Total number of unique combinations = N! / ( K! (N - K)! ).
        /// This function is less efficient, but is more likely to not overflow when N and K are large.
        /// Taken from:  http://blog.plover.com/math/choose.html
        /// </summary>
        /// <param name="N"></param>
        /// <param name="K"></param>
        /// <returns></returns>
        public static long GetBinCoeff(long N, long K)
        {
            // 
            long r = 1;
            long d;
            if (K > N) return 0;
            for (d = 1; d <= K; d++)
            {
                r *= N--;
                r /= d;
            }
            return r;
        }

    }
}
