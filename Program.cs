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
            var day = new Day16();

            try
            {
                Checkpoint();
                Utils.WriteLine("** TESTS **", ConsoleColor.Red);
                day.Test();
                Checkpoint();
                Utils.WriteLine("** PART 1 **", ConsoleColor.Red);
                Utils.WriteLine(day.Part1(day.Input), ConsoleColor.Green);
                Checkpoint();
                Utils.WriteLine("** PART 2 **", ConsoleColor.Red);
                Utils.WriteLine(day.Part2(day.Input), ConsoleColor.Green);
                Checkpoint();

            }
            catch (NotImplementedException)
            {
            }

            Utils.WriteLine("** FINISHED **", ConsoleColor.Red);
            Console.ReadLine();

        }

        static DateTime timer = DateTime.MaxValue;

        private static void Checkpoint()
        {
            if (timer != DateTime.MaxValue)
            {
                Utils.WriteLine("Completed in " + (DateTime.Now - timer).TotalSeconds.ToString("0.000s"), ConsoleColor.Gray);
            }
            timer = DateTime.Now;
        }
    }
}
