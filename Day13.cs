using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2016
{
    class Day13 : Day
    {
        public dynamic Input
        {
            get
            {
                return "1358,31,39";
            }
        }

        public string Part1(dynamic input)
        {
            var inputs = ((string)input).Split(',').Select(i => int.Parse(i)).ToArray();
            var target = encode(inputs[1], inputs[2]);
            var seed = inputs[0];

            var map = new Dictionary<long, int>();
            var queue = new Queue<long>();

            map[encode(1, 1)] = 0;
            queue.Enqueue(encode(1,1));

            while (queue.Count > 0)
            {
                long value = queue.Dequeue();
                if (expand(map, queue, value, target, seed, int.MaxValue))
                {
                    return (map[value] + 1).ToString();
                }
            }

            return "";
        }

        private bool expand(Dictionary<long, int> map, Queue<long> queue, long value, long target, int seed, int maxdepth)
        {
            int x, y;
            decode(value, out x, out y);
            int distance = map[value];

            if (distance == maxdepth)
            {
                return false;
            }

            foreach (var adjacent in getadjacentcells(x, y).Where(a => !map.ContainsKey(a)))
            {
                if (adjacent == target)
                {
                    return true;
                }

                if (isSpace(adjacent, seed))
                {
                    map[adjacent] = distance + 1;
                    queue.Enqueue(adjacent);
                }
            }

            return false;
        }

        private bool isSpace(long location, int seed)
        {
            int x, y;
            decode(location, out x, out y);

            int value = x * x + 3 * x + 2 * x * y + y + y * y + seed;

            bool space = true;
            while (value > 0)
            {
                if (value % 2 == 1)
                {
                    space = !space;
                }
                value = value >> 1;
            }

            return space;
        }

        private IEnumerable<long> getadjacentcells(int x, int y)
        {
            if (x > 0)
            {
                yield return encode(x - 1, y);
            }
            if (y > 0)
            {
                yield return encode(x, y - 1);
            }

            yield return encode(x + 1, y);
            yield return encode(x, y + 1);
        }

        private void decode(long value, out int x, out int y)
        {
            x = (int) value / 10000;
            y = (int) value % 10000;
        }

        public string Part2(dynamic input)
        {
            var inputs = ((string)input).Split(',').Select(i => int.Parse(i)).ToArray();
            var maxdepth = 50; //inputs[1];
            var seed = inputs[0];

            var map = new Dictionary<long, int>();
            var queue = new Queue<long>();

            map[encode(1, 1)] = 0;
            queue.Enqueue(encode(1, 1));

            while (queue.Count > 0)
            {
                long value = queue.Dequeue();
                expand(map, queue, value, int.MinValue, seed, maxdepth);
            }

            return map.Count.ToString();
        }

        public long encode(int x, int y)
        {
            return x * 10000 + y;
        }

        public void Test()
        {
            Utils.Test(Part1, "10,7,4", "11");
            /*Utils.Test(Part2, "10,1", "3");
            Utils.Test(Part2, "10,5", "11");*/
        }
    }
}
