using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2016
{
    class Day19 : Day
    {
        public dynamic Input
        {
            get
            {
                return 3017957;
            }
        }

        public string Part1(dynamic input)
        {
            int count = input;
            var elves = new BitArray(count);

            int pos = 0;

            for (int i = 0; i < count - 1; i++)
            {
                // find elf to steal from
                pos = (pos + 1) % count;

                while (elves[pos])
                {
                    pos = (pos + 1) % count;
                }

                elves[pos] = true;

                // find next elf to do stealing
                while (elves[pos])
                {
                    pos = (pos + 1) % count;
                }
            }

            for (int i = 0; i < count; i++)
            {
                if (!elves[i])
                {
                    return (i + 1).ToString();
                }
            }

            return "uh-oh";
        }

        public string Part2(dynamic input)
        {
            int count = input;
            var elves = new BitArray(count);

            int opp = (count) / 2;
            // first steal
            elves[opp] = true;

            for (int i = 1; i < count - 1; i++)
            {
                // find elf to steal from
                var delta = 1 + (count - i + 1) % 2;
                var initial_delta = delta;

                while (delta > 0)
                {
                    opp = (opp + 1) % count;
                    if (!elves[opp])
                    {
                        delta--;
                    }
                }

                elves[opp] = true;
            }

            for (int i = 0; i < count; i++)
            {
                if (!elves[i])
                {
                    return (i + 1).ToString();
                }
            }

            return "uh-oh";
        }

        public void Test()
        {
            Utils.Test(Part1, 5, "3");
            Utils.Test(Part2, 5, "2");
            Utils.Test(Part2, 20, "13");
        }
    }
}
