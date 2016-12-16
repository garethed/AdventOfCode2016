﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2016
{
    static class Utils
    {
        public static IEnumerable<string> splitLines(string input)
        {
            return input.Replace("\r", "").Split('\n').Select(l => l.Trim());
        }

        public static void Test(Func<dynamic, string> method, dynamic input, string output)
        {
            Test(method, new dynamic[] { input }, new string[] { output });
        }

        public static void Test(Func<dynamic, string> method, dynamic[] inputs, string[] outputs)
        {
            for (int i = 0; i < inputs.Length; i++)
            {
                var actual = method(inputs[i]);
                Console.Write(inputs[i] + " -> " + actual);
                if (actual == outputs[i])
                {
                    WriteLine(" : OK", ConsoleColor.Green);
                }
                else
                {
                    WriteLine(" : WRONG. Should be " + outputs[i], ConsoleColor.Red);
                }
            }
        }

        public static void WriteLine(string msg, ConsoleColor color)
        {
            Console.WriteLine();
            var old = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(msg);
            Console.ForegroundColor = old;
        }

        public static void ClearLine()
        {
            Console.Write(new string(' ', Console.WindowWidth));
            Console.CursorLeft = 0;
        }
    }
}
