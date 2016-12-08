using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace AdventOfCode2016
{
    class Day8
    {
        public static string Part1()
        {
            var screen = new bool[width, height];

            foreach (var line in Utils.splitLines(input))
            {
                var command = line.Replace("  ", " ");
                command = command.Replace(" =", "=");
                command = command.Replace("= ", "=");

                if (command.StartsWith("rect"))
                {
                    var parts = command.Split(' ', 'x');
                    rect(screen, int.Parse(parts[1]), int.Parse(parts[2]));
                }
                else
                {
                    var parts = command.Split(' ', '=');
                    var index = int.Parse(parts[3]);
                    var delta = int.Parse(parts[5]);
                    if (parts[1] == "row")
                    {
                        rotateRow(screen, index, delta);
                    }
                    else
                    {
                        rotateCol(screen, index, delta);
                    }                    
                }

                //printScreen(screen);
            }

            printScreen(screen);
            return screen.Cast<bool>().Count(b => b).ToString();


        }

        private static void printScreen(bool[,] screen)
        {
            Console.WriteLine();

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Console.Write(screen[x, y] ? "#" : ".");
                }
                Console.WriteLine();
            }
        }

        private static void rotateRow(bool[,] screen, int index, int delta)
        {
            bool[] copy = new bool[width];
                        
            for (int x = 0; x < width; x++)
            {
                copy[x] = screen[(x - delta + width) % width, index];

            }

            for (int x = 0; x < width; x++)
            {
                screen[x, index] = copy[x];
            }
        }

        private static void rotateCol(bool[,] screen, int index, int delta)
        {
            bool[] copy = new bool[height];

            for (int y = 0; y < height; y++)
            {
                copy[y] = screen[index, (y - delta + height) % height];

            }

            for (int y = 0; y < height; y++)
            {
                screen[index, y] = copy[y];
            }
        }

        private static void rect(bool[,] screen, int width, int height)
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    screen[x, y] = true;
                }
            }
        }

        static int width = 50;
        static int height = 6;


static string input = @" rect 1x1
rotate row y = 0 by 7
rect 1x1
rotate row y = 0 by 5
rect 1x1
rotate row y = 0 by 5
rect 1x1
rotate row y = 0 by 2
rect 1x1
rotate row y = 0 by 3
rect 1x1
rotate row y = 0 by 5
rect 1x1
rotate row y = 0 by 3
rect 1x1
rotate row y = 0 by 2
rect 1x1
rotate row y = 0 by 3
rect 2x1
rotate row y = 0 by 7
rect 6x1
rotate row y = 0 by 3
rect 2x1
rotate row y = 0 by 2
rect 1x2
rotate row y = 1 by 10
rotate row y=0 by 3
rotate column x=0 by 1
rect 2x1
rotate column x = 20 by 1
rotate column x=15 by 1
rotate column x=5 by 1
rotate row y=1 by 5
rotate row y=0 by 2
rect 1x2
rotate row y = 0 by 5
rotate column x=0 by 1
rect 4x1
rotate row y = 2 by 15
rotate row y=0 by 5
rotate column x=0 by 1
rect 4x1
rotate row y = 2 by 5
rotate row y=0 by 5
rotate column x=0 by 1
rect 4x1
rotate row y = 2 by 10
rotate row y=0 by 10
rotate column x=8 by 1
rotate column x=5 by 1
rotate column x=0 by 1
rect 9x1
rotate column x = 27 by 1
rotate row y=0 by 5
rotate column x=0 by 1
rect 4x1
rotate column x = 42 by 1
rotate column x=40 by 1
rotate column x=22 by 1
rotate column x=17 by 1
rotate column x=12 by 1
rotate column x=7 by 1
rotate column x=2 by 1
rotate row y=3 by 10
rotate row y=2 by 5
rotate row y=1 by 3
rotate row y=0 by 10
rect 1x4
rotate column x = 37 by 2
rotate row y=3 by 18
rotate row y=2 by 30
rotate row y=1 by 7
rotate row y=0 by 2
rotate column x=13 by 3
rotate column x=12 by 1
rotate column x=10 by 1
rotate column x=7 by 1
rotate column x=6 by 3
rotate column x=5 by 1
rotate column x=3 by 3
rotate column x=2 by 1
rotate column x=0 by 1
rect 14x1
rotate column x = 38 by 3
rotate row y=3 by 12
rotate row y=2 by 10
rotate row y=0 by 10
rotate column x=7 by 1
rotate column x=5 by 1
rotate column x=2 by 1
rotate column x=0 by 1
rect 9x1
rotate row y = 4 by 20
rotate row y=3 by 25
rotate row y=2 by 10
rotate row y=0 by 15
rotate column x=12 by 1
rotate column x=10 by 1
rotate column x=8 by 3
rotate column x=7 by 1
rotate column x=5 by 1
rotate column x=3 by 3
rotate column x=2 by 1
rotate column x=0 by 1
rect 14x1
rotate column x = 34 by 1
rotate row y=1 by 45
rotate column x=47 by 1
rotate column x=42 by 1
rotate column x=19 by 1
rotate column x=9 by 2
rotate row y=4 by 7
rotate row y=3 by 20
rotate row y=0 by 7
rotate column x=5 by 1
rotate column x=3 by 1
rotate column x=2 by 1
rotate column x=0 by 1
rect 6x1
rotate row y = 4 by 8
rotate row y=3 by 5
rotate row y=1 by 5
rotate column x=5 by 1
rotate column x=4 by 1
rotate column x=3 by 2
rotate column x=2 by 1
rotate column x=1 by 3
rotate column x=0 by 1
rect 6x1
rotate column x = 36 by 3
rotate column x=25 by 3
rotate column x=18 by 3
rotate column x=11 by 3
rotate column x=3 by 4
rotate row y=4 by 5
rotate row y=3 by 5
rotate row y=2 by 8
rotate row y=1 by 8
rotate row y=0 by 3
rotate column x=3 by 4
rotate column x=0 by 4
rect 4x4
rotate row y = 4 by 10
rotate row y=3 by 20
rotate row y=1 by 10
rotate row y=0 by 10
rotate column x=8 by 1
rotate column x=7 by 1
rotate column x=6 by 1
rotate column x=5 by 1
rotate column x=3 by 1
rotate column x=2 by 1
rotate column x=1 by 1
rotate column x=0 by 1
rect 9x1
rotate row y = 0 by 40
rotate column x=44 by 1
rotate column x=35 by 5
rotate column x=18 by 5
rotate column x=15 by 3
rotate column x=10 by 5
rotate row y=5 by 15
rotate row y=4 by 10
rotate row y=3 by 40
rotate row y=2 by 20
rotate row y=1 by 45
rotate row y=0 by 35
rotate column x=48 by 1
rotate column x=47 by 5
rotate column x=46 by 5
rotate column x=45 by 1
rotate column x=43 by 1
rotate column x=40 by 1
rotate column x=38 by 2
rotate column x=37 by 3
rotate column x=36 by 2
rotate column x=32 by 2
rotate column x=31 by 2
rotate column x=28 by 1
rotate column x=23 by 3
rotate column x=22 by 3
rotate column x=21 by 5
rotate column x=20 by 1
rotate column x=18 by 1
rotate column x=17 by 3
rotate column x=13 by 1
rotate column x=10 by 1
rotate column x=8 by 1
rotate column x=7 by 5
rotate column x=6 by 5
rotate column x=5 by 1
rotate column x=3 by 5
rotate column x=2 by 5
rotate column x=1 by 5";


        static string testinput =
@"rect 3x2
rotate column x=1 by 1
rotate row y=0 by 4
rotate column x=1 by 1";
    }
}
