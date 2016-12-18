using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2016
{
    class Day17 : Day
    {
        public dynamic Input
        {
            get
            {
                return "dmypynyp";
            }
        }

        public string Part1(dynamic input)
        {
            var seed = (string)input;
            var next = new Queue<Pos>();
            next.Enqueue(new Pos());

            while (next.Count > 0)
            {
                var result = GenerateMoves(next, seed);
                if (result != null)
                {
                    return result.Path;
                }
            }

            return null;

        }

        private Pos GenerateMoves(Queue<Pos> next, string seed)
        {
            var last = next.Dequeue();
            if (last.x == 3 && last.y == 3)
            {
                return last;
            }

            string hash = Utils.MD5(seed + last.Path);
            var steps = "UDLR";

            for (int i=0; i < 4; i++)
            {
                if ("bcdef".Contains(hash[i]))
                {
                    var nextpos = last.Move(steps[i]);
                    if (nextpos != null)
                    {
                        next.Enqueue(nextpos);
                    }
                }
            }

            return null;
        }

        public string Part2(dynamic input)
        {
            var seed = (string)input;
            var next = new Queue<Pos>();
            next.Enqueue(new Pos());

            var max = 0;

            while (next.Count > 0)
            {
                var result = GenerateMoves(next, seed);
                if (result != null)
                {
                    max = result.Path.Length;
                }
            }

            return max.ToString();
        }

        public void Test()
        {
            Utils.Test(Part1, new[] { "ihgpwlah", "kglvqrro", "ulqzkmiv" }, new[] { "DDRRRD", "DDUDRLRRUDRD", "DRURDRUDDLLDLUURRDULRLDUUDDDRR" });
            Utils.Test(Part2, new[] { "ihgpwlah", "kglvqrro", "ulqzkmiv" }, new[] { "370", "492", "830" });
        }

        class Pos
        {
            public int x;
            public int y;
            public string Path;

            internal Pos Move(char v)
            {
                var next = new Pos() { x = this.x, y = this.y, Path = this.Path + v };

                switch (v)
                {
                    case 'U':
                        if (x == 0) return null;
                        next.x--;
                        break;
                    case 'D':
                        if (x == 3) return null;
                        next.x++;
                        break;
                    case 'L':
                        if (y == 0) return null;
                        next.y--;
                        break;
                    case 'R':
                        if (y == 3) return null;
                        next.y++;
                        break;
                }

                return next;
            }
        }
    }
}
