using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2016
{
    class Day1 : Day
    {
        public dynamic Input
        {
            get
            {
                return "L5, R1, R4, L5, L4, R3, R1, L1, R4, R5, L1, L3, R4, L2, L4, R2, L4, L1, R3, R1, R1, L1, R1, L5, R5, R2, L5, R2, R1, L2, L4, L4, R191, R2, R5, R1, L1, L2, R5, L2, L3, R4, L1, L1, R1, R50, L1, R1, R76, R5, R4, R2, L5, L3, L5, R2, R1, L1, R2, L3, R4, R2, L1, L1, R4, L1, L1, R185, R1, L5, L4, L5, L3, R2, R3, R1, L5, R1, L3, L2, L2, R5, L1, L1, L3, R1, R4, L2, L1, L1, L3, L4, R5, L2, R3, R5, R1, L4, R5, L3, R3, R3, R1, R1, R5, R2, L2, R5, L5, L4, R4, R3, R5, R1, L3, R1, L2, L2, R3, R4, L1, R4, L1, R4, R3, L1, L4, L1, L5, L2, R2, L1, R1, L5, L3, R4, L1, R5, L5, L5, L1, L3, R1, R5, L2, L4, L5, L1, L1, L2, R5, R5, L4, R3, L2, L1, L3, L4, L5, L5, L2, R4, R3, L5, R4, R2, R1, L5";
            }
        }

        public void Test()
        {
            Debug.Assert(Part1("R5, L5, R5, R3") == "12");
            Debug.Assert(Part2("R8, R4, R4, R8") == "4");
        }

        public string Part2(dynamic steps)
        {

            var visited = new List<string>();

            var x = 0;
            var y = 0;
            var dx = 0;
            var dy = 1;

            foreach (var step in steps.Replace(",", "").Split(' '))
            {
                var direction = step[0];
                var distance = int.Parse(step.Substring(1));

                var tmp = dx;
                dx = dy;
                dy = tmp;

                if (direction == 'R')
                {
                    dy = -dy;
                }
                else
                {
                    dx = -dx;
                }

                for (int i = 0; i < distance; i++)
                {

                    x += dx;
                    y += dy;

                    var coords = x + ":" + y;

                    if (visited.Contains(coords))
                    {
                        return (x + y).ToString();
                    }

                    visited.Add(coords);
                }
            }

            return "";
        }

        public string Part1(dynamic steps)
        {
            var x = 0;
            var y = 0;
            var dx = 0;
            var dy = 1;

            foreach (var step in steps.Replace(",", "").Split(' '))
            {
                var direction = step[0];
                var distance = int.Parse(step.Substring(1));

                var tmp = dx;
                dx = dy;
                dy = tmp;

                if (direction == 'R')
                {
                    dy = -dy;
                }
                else
                {
                    dx = -dx;
                }


                x += dx * distance;
                y += dy * distance;

                var coords = x + ":" + y;


            }

            return (x + y).ToString();
        }
    }
}
