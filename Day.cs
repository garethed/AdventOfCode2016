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

        string Part1(dynamic input);
        string Part2(dynamic input);

        dynamic Input { get; }
    }
}
