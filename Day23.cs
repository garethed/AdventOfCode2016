using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2016
{
    class Day23 : Day
    {
        public dynamic Input
        {
            get
            {
                return
@"cpy a b
dec b
cpy a d
cpy 0 a
cpy b c
inc a
dec c
jnz c -2
dec d
jnz d -5
dec b
cpy b c
cpy c d
dec d
inc c
jnz d -2
tgl c
cpy -16 c
jnz 1 c
cpy 93 c
jnz 80 d
inc a
inc d
jnz d -2
inc c
jnz c -5";

            }
        }

        public string Part1(dynamic input)
        {
            return new Day12().Part1("cpy 7 a\n" + input);
        }

        public string Part2(dynamic input)
        {
            return new Day12().Part1("cpy 12 a\n" + input);
        }

        public void Test()
        {
            Utils.Test(Part1,
@"cpy 2 a
tgl a
tgl a
tgl a
cpy 1 a
dec a
dec a", "3");
        }
    }
}
