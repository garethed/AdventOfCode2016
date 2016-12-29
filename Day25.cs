using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2016
{
    class Day25 : Day
    {
        public dynamic Input
        {
            get
            {
                return
@"cpy a d
cpy 4 c
cpy 643 b
inc d
dec b
jnz b -2
dec c
jnz c -5
cpy d a
jnz 0 0
cpy a b
cpy 0 a
cpy 2 c
jnz b 2
jnz 1 6
dec b
dec c
jnz c -4
inc a
jnz 1 -7
cpy 2 b
jnz c 2
jnz 1 4
dec b
dec c
jnz 1 -4
jnz 0 0
out b
jnz a -19
jnz 1 -21";
            }
        }

        public string Part1(dynamic input)
        {
            var program = new Day12.Program((string)input);
            int i = 1;

            while (true)
            {
                if (test(i, program))
                {
                    return i.ToString();
                }

                Utils.WriteTransient(i.ToString());
                i++;
            }
        }

        bool test(int seed, Day12.Program program)
        {
            program.Reset(seed);
            for (int j = 0; j < 10; j++)
            {
                if (program.ReadNextOutput() != j % 2)
                {
                    return false;
                }

            }

            return true;
        }

        public string Part2(dynamic input)
        {
            throw new NotImplementedException();
        }

        public void Test()
        {
        }
    }
}
