using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2016
{
    interface Day
    {
        void Test();

        string Part1(string input);
        string Part2(string input);

        string Input { get; }
    }
}
