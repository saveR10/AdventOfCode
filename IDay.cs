using NUnit.Framework.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AOC
{
    internal interface IDay
    {
        void Part1(object input, bool test, ref object solution);
        void Part2(object input, bool test, ref object solution);
    }
}
