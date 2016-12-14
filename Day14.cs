using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2016
{
    class Day14 : Day
    {
        public string Input
        {
            get
            {
                return "yjdafjpo";
            }
        }

        public string Part1(string input)
        {
            return FindKeys(input, 1);
        }
        public string Part2(string input)
        {
            return FindKeys(input, 2017);
        }

        struct Candidate
        {
            public char character;
            public int index;
            public string hash; 
        }

        public string FindKeys(string input, int md5rounds)
        {
            int i = 0;
            int found = 0;
            int upperbound = int.MaxValue;

            var candidates = new HashSet<Candidate>();
            var results = new SortedList<int, int>();

            while (i < upperbound)
            {
                var seed = input + i;
                var md5 = System.Security.Cryptography.MD5.Create();

                var hash = seed;

                for (int j = 0; j < md5rounds; j++)
                {
                    hash = BitConverter.ToString(md5.ComputeHash(Encoding.ASCII.GetBytes(hash))).Replace("-", "").ToLower();
                }

                char seq = '#';
                int len = 0;
                bool foundtriplet = false;
                foreach (var c in hash)
                {
                    if (c == seq)
                    {
                        len++;
                        if (len == 3 && !foundtriplet)
                        {
                            candidates.Add(new Candidate() { character = c, index = i, hash = hash });
                            foundtriplet = true;
                        }
                        if (len == 5)
                        {
                            foreach (var candidate in new List<Candidate>(candidates))
                            {
                                if (candidate.index < i - 1000)
                                {
                                    candidates.Remove(candidate);
                                }
                                else if (candidate.character == c && candidate.index != i)
                                {
                                    found++;
                                    Console.Write(found + ": " + candidate.index + "-" + candidate.hash + " / " + i + "-" + hash);
                                    Console.CursorLeft = 0;
                                    candidates.Remove(candidate);
                                    results.Add(candidate.index, candidate.index);

                                    if (results.Count > 64)
                                    {
                                        upperbound = results.Keys[63] + 1000;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        seq = c;
                        len = 1;
                    }
                }

                i++;

            }

            Utils.ClearLine();

            return results.Keys[63].ToString();
        }

        public void Test()
        {
            Utils.Test(Part1, "abc", "22728");
            Utils.Test(Part2, "abc", "22551");
        }
    }
}
