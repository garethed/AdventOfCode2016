using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2016
{
    class Day16 : Day
    {
        public dynamic Input
        {
            get
            {
                return new { start = "10010000000110000", length = 272 };
            }
        }

        public string Part1(dynamic input)
        {
            StringBuilder databuilder = new StringBuilder();
            databuilder.Append((string)input.start);
            int targetlength = input.length;

            while (databuilder.Length < targetlength)
            {
                var startingLength = databuilder.Length;

                databuilder.Append("0");

                for (int i = 0; i < startingLength; i++)
                {
                    databuilder.Append(reverseChar(databuilder[startingLength - i - 1]));
                }
            }

            var data = databuilder.ToString().Substring(0, targetlength);

            var checksum = new StringBuilder();

            while (checksum.Length % 2 == 0)
            {
                checksum.Clear();

                for (int i = 0; i < data.Length / 2; i++)
                {
                    checksum.Append(data[2 * i] == data[2 * i + 1] ? "1" : "0");
                }

                data = checksum.ToString();
            }
            
            return checksum.ToString();
        }

        private char reverseChar(char v)
        {
            return v == '1' ? '0' : '1';
        }

        public string Part2(dynamic input)
        {
            return Part1(new { start = "10010000000110000", length = 35651584 });
        }

        public void Test()
        {
            Utils.Test(Part1, new { start = "10000", length = 20 }, "01100");
        }
    }
}
