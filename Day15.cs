using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2016
{
    class Day15 : Day
    {
        public dynamic Input
        {
            get
            {
                return
@"Disc #1 has 7 positions; at time=0, it is at position 0.
Disc #2 has 13 positions; at time=0, it is at position 0.
Disc #3 has 3 positions; at time=0, it is at position 2.
Disc #4 has 5 positions; at time=0, it is at position 2.
Disc #5 has 17 positions; at time=0, it is at position 0.
Disc #6 has 19 positions; at time=0, it is at position 7.";
            }
        }

        public string Part1(dynamic input)
        {
            var positions = new List<int>();
            var sizes = new List<int>();

            foreach (var line in Utils.splitLines(input))
            {
                var parts = line.Replace(".", "").Split(' ');
                var size = int.Parse(parts[3]);
                var position = int.Parse(parts[parts.Length - 1]);
                positions.Add((position + positions.Count + 1) % size);
                sizes.Add(size);
            }

            int steps = 0;

            while (!positions.All(p => p == 0))
            {
                for (int i = 0; i < positions.Count; i++)
                {
                    positions[i] = (positions[i] + 1) % sizes[i];
                }

                steps++;
            }

            return steps.ToString();            
        }

        public string Part2(dynamic input)
        {
            return Part1(input + "\nDisc #7 has 11 positions; at time=0, it is at position 0.");
        }

        public void Test()
        {
            Utils.Test(Part1,
@"Disc #1 has 5 positions; at time=0, it is at position 4.
Disc #2 has 2 positions; at time=0, it is at position 1.",
"5");
            
        }
    }
}
