using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2016
{
    class Day18 : Day
    {
        public dynamic Input
        {
            get
            {
                return new { first = ".^^^.^.^^^.^.......^^.^^^^.^^^^..^^^^^.^.^^^..^^.^.^^..^.^..^^...^.^^.^^^...^^.^.^^^..^^^^.....^....", rows = 40 };
            }
        }

        public string Part1(dynamic input)
        {
            var count = 0;
            var row = ("." + (string)input.first + ".").Select(c => c == '^').ToArray();
            count += row.Count(c => !c) - 2;

            //printRow(row, count);

            for (int i = 1; i < (int)input.rows; i++)
            {
                var next = new bool[row.Length];
                for (int j = 1; j < next.Length - 1; j++)
                {
                    next[j] = isTrap(row[j - 1], row[j], row[j + 1]);
                    if (!next[j])
                    {
                        count++;
                    }
                }
                row = next;
                //printRow(row, count);
            }

            return count.ToString();
        }

        private void printRow(bool[] row, int count)
        {
            foreach (var cell in row)
            {
                Console.Write(cell ? "^" : ".");
            }
            Console.WriteLine(" " + count);
        }

        private bool isTrap(bool v1, bool v2, bool v3)
        {
            return (v1 && v2 && !v3) || (!v1 && v2 && v3) || (v1 && !v2 && !v3) || (!v1 && !v2 && v3);
        }

        public string Part2(dynamic input)
        {
            return Part1(new { first = Input.first, rows = 400000 });
        }

        public void Test()
        {
            Utils.Test(Part1, new { first = ".^^.^.^^^^", rows = 10 }, "38");
        }
    }
}
