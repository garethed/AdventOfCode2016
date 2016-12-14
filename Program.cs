using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2016
{
    class Program
    {
        static void Main(string[] args)
        {
            var day = new Day14();

            try
            {

                Utils.WriteLine("** TESTS **", ConsoleColor.Red);
                day.Test();
                Utils.WriteLine("** PART 1 **", ConsoleColor.Red);
                Utils.WriteLine(day.Part1(day.Input), ConsoleColor.Green);
                Utils.WriteLine("** PART 2 **", ConsoleColor.Red);
                Utils.WriteLine(day.Part2(day.Input), ConsoleColor.Green);

            }
            catch (NotImplementedException)
            {
            }

            Console.ReadLine();

        }
    }
}
