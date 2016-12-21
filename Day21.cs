using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2016
{
    class Day21 : Day
    {
        public dynamic Input
        {
            get
            {
                return new
                {
                    start = "abcdefgh",
                    part2start = "fbgdceah",
                    operations =
@"swap position 7 with position 1
swap letter e with letter d
swap position 7 with position 6
move position 4 to position 0
move position 1 to position 4
move position 6 to position 5
rotate right 1 step
swap letter e with letter b
reverse positions 3 through 7
swap position 2 with position 6
reverse positions 2 through 4
reverse positions 1 through 4
reverse positions 5 through 7
rotate left 2 steps
swap letter g with letter f
rotate based on position of letter a
swap letter b with letter h
swap position 0 with position 3
move position 4 to position 7
rotate based on position of letter g
swap letter f with letter e
move position 1 to position 5
swap letter d with letter e
move position 5 to position 2
move position 6 to position 5
rotate right 6 steps
rotate left 4 steps
reverse positions 0 through 3
swap letter g with letter c
swap letter f with letter e
reverse positions 6 through 7
move position 6 to position 1
rotate left 2 steps
rotate left 5 steps
swap position 3 with position 6
reverse positions 1 through 5
rotate right 6 steps
swap letter a with letter b
reverse positions 3 through 4
rotate based on position of letter f
swap position 2 with position 6
reverse positions 5 through 6
swap letter h with letter e
reverse positions 0 through 4
rotate based on position of letter g
rotate based on position of letter d
rotate based on position of letter b
swap position 5 with position 1
rotate based on position of letter f
move position 1 to position 5
rotate right 0 steps
rotate based on position of letter e
move position 0 to position 1
swap position 7 with position 2
rotate left 3 steps
reverse positions 0 through 1
rotate right 7 steps
rotate right 5 steps
swap position 2 with position 0
swap letter g with letter a
rotate left 0 steps
rotate based on position of letter f
swap position 5 with position 1
rotate right 0 steps
rotate left 5 steps
swap letter e with letter a
swap position 5 with position 4
reverse positions 2 through 5
swap letter e with letter a
swap position 3 with position 7
reverse positions 0 through 2
swap letter a with letter b
swap position 7 with position 1
move position 1 to position 6
rotate right 1 step
reverse positions 2 through 6
rotate based on position of letter b
move position 1 to position 0
swap position 7 with position 3
move position 6 to position 5
rotate right 4 steps
reverse positions 2 through 7
reverse positions 3 through 4
reverse positions 4 through 5
rotate based on position of letter f
reverse positions 0 through 5
reverse positions 3 through 4
move position 1 to position 2
rotate left 4 steps
swap position 7 with position 6
rotate right 1 step
move position 5 to position 2
rotate right 5 steps
swap position 7 with position 4
swap letter a with letter e
rotate based on position of letter e
swap position 7 with position 1
swap position 7 with position 3
move position 7 to position 1
swap position 7 with position 4"
                };
            }
        }

        public string Part1(dynamic input)
        {
            char[] chars = ((string)input.start).ToCharArray();
            foreach (var op in Utils.splitLines((string)input.operations))
            {
                var parts = op.Split(' ');
                switch (parts[0])
                {
                    case "swap":
                        if (parts[1] == "position")
                        {
                            chars = swapPosition(chars, int.Parse(parts[2]), int.Parse(parts[5]));
                        }
                        else
                        {
                            chars = swapLetters(chars, parts[2][0], parts[5][0]);
                        }
                        break;
                    case "reverse":
                        chars = reverse(chars, int.Parse(parts[2]), int.Parse(parts[4]));
                        break;
                    case "rotate":
                        if (parts[1] != "based")
                        {
                            chars = rotate(chars, (parts[1] == "left" ? -1 : 1) * int.Parse(parts[2]));
                        } 
                        else
                        {
                            chars = rotateByChar(chars, parts[6][0]);
                        }
                        break;
                    case "move":
                        chars = move(chars, int.Parse(parts[2]), int.Parse(parts[5]));
                        break;
                }

//                 Console.WriteLine(op + " -> " + new string(chars));
            }
            return new string(chars);
        }

        private char[] rotateByChar(char[] chars, char v)
        {
            var index = Array.IndexOf(chars, v);

            return rotate(chars, 1 + index + (index >= 4 ? 1 : 0));
        }

        private char[] move(char[] chars, int from, int to)
        {
            var ret = (char[])chars.Clone();

            for (int i = 0; i < chars.Length; i++)
            {
                if (i == to)
                {
                    ret[i] = chars[from];
                }
                if (i >= from && i < to)
                {
                    ret[i] = chars[i + 1];
                }

                if (i > to && i <= from)
                {
                    ret[i] = chars[i - 1];
                }
            }

            return ret;

        }

        private char[] rotate(char[] chars, int v)
        {
            var ret = (char[])chars.Clone();
            for (int i = 0; i < ret.Length; i++)
            {
                ret[(i + v + ret.Length) % ret.Length] = chars[i];
            }

            return ret;
        }

        private char[] reverse(char[] chars, int v1, int v2)
        {
            var ret = (char[])chars.Clone();
            for (int i = v1; i <= v2; i++)
            {
                ret[i] = chars[v1 + v2 - i];
            }

            return ret;
        }

        private char[] swapLetters(char[] chars, char v1, char v2)
        {
            var ret = (char[])chars.Clone();
            for (int i = 0; i < ret.Length; i++)
            {
                if (chars[i] == v1)
                {
                    ret[i] = v2;
                }
                if (chars[i] == v2)
                {
                    ret[i] = v1;
                }
            }

            return ret;


        }

        private char[] swapPosition(char[] chars, int v1, int v2)
        {
            var ret = (char[])chars.Clone();
            for (int i = 0; i < ret.Length; i++)
            {
                if (i == v1)
                {
                    ret[i] = chars[v2];
                }
                if (i == v2)
                {
                    ret[i] = chars[v1];
                }
            }

            return ret;
        }

        public string Part2(dynamic input)
        {
            char[] chars = ((string)input.part2start).ToCharArray();
            foreach (var op in Utils.splitLines((string)input.operations).Reverse())
            {
                var before = (char[])chars.Clone();
                var parts = op.Split(' ');
                switch (parts[0])
                {
                    case "swap":
                        if (parts[1] == "position")
                        {
                            chars = swapPosition(chars, int.Parse(parts[2]), int.Parse(parts[5]));
                        }
                        else
                        {
                            chars = swapLetters(chars, parts[2][0], parts[5][0]);
                        }
                        break;
                    case "reverse":
                        chars = reverse(chars, int.Parse(parts[2]), int.Parse(parts[4]));
                        break;
                    case "rotate":
                        if (parts[1] != "based")
                        {
                            chars = rotate(chars, (parts[1] == "left" ? 1 : -1) * int.Parse(parts[2]));
                        }
                        else
                        {
                            chars = rotateByCharReverse(chars, parts[6][0]);
                        }
                        break;
                    case "move":
                        chars = move(chars, int.Parse(parts[5]), int.Parse(parts[2]));
                        break;
                }
                Debug.Assert(Part1(new { start = new string(chars), operations = op }) == new string(before));

  //              Console.WriteLine(op + " -> " + new string(chars));
            }
            return new string(chars);
        }

        private char[] rotateByCharReverse(char[] chars, char v)
        {
            for (int i = 0; i < chars.Length; i++)
            {
                int rotation = 1 + i + (i > 3 ? 1 : 0);
                var rotated = rotate(chars, -rotation);
                if (chars.SequenceEqual(rotateByChar(rotated, v)))
                {
                    return rotated;
                }
            }

            throw new Exception("ach");
        }

        public void Test()
        {
            var operations = @"swap position 4 with position 0
swap letter d with letter b
reverse positions 0 through 4
rotate left 1 step
move position 1 to position 4
move position 3 to position 0
rotate based on position of letter b
rotate based on position of letter d";
            Utils.Test(Part1, new { start = "abcde", operations = operations }, "decab");
            Utils.Test(Part1, new { start = "deabc", operations = operations }, "decab");
            Utils.Test(Part2, new { part2start = "decab", operations = operations }, "abcde");
            var decode = Part2(new { part2start = "egafbchd", operations = Input.operations });
            Utils.Test(Part1, new { start = decode, operations = Input.operations }, "egafbchd");

        }
    }
}
