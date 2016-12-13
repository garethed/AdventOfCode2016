using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2016
{
    class Day12 : Day
    {
        public string Input
        {
            get
            {
                return @"cpy 1 a
cpy 1 b
cpy 26 d
jnz c 2
jnz 1 5
cpy 7 c
inc d
dec c
jnz c -2
cpy a c
inc a
dec b
jnz b -2
cpy c b
dec d
jnz d -6
cpy 13 c
cpy 14 d
inc a
dec d
jnz d -2
dec c
jnz c -5";
            }
        }

        public string Part1(string input)
        {
            var instructions = new Dictionary<string, Action<Value, Value, Value>>()
            {
                { "cpy", (x, y, p) => { y.value = x.value; p.value = p.value + 1; } },
                { "inc", (x, y, p) => { x.value = x.value + 1; p.value = p.value + 1; } },
                { "dec", (x, y, p) => { x.value = x.value - 1; p.value = p.value + 1; } },
                { "jnz", (x, y, p) => p.value = p.value + (x.value == 0 ? 1 : y.value) }
            };

            var registers = new Value[5]; // p, a, b, c, d
            for (int i = 0; i < registers.Length; i++)
            {
                registers[i] = new Value();
            }

            var program = ParseProgram(input, instructions, registers);
            var pc = registers[0];

            while (pc.value < program.Count)
            {
                program[pc.value].Execute(pc);
            }

            return registers[1].value.ToString();
            
        }

        private List<Statement> ParseProgram(string input, Dictionary<string, Action<Value, Value, Value>> instructions, Value[] registers)
        {
            var ret = new List<Statement>();

            foreach (var line in Utils.splitLines(input))
            {
                var elements = line.Split(' ');
                var statement = new Statement();
                statement.instruction = instructions[elements[0]];
                statement.LValue = ParseValue(elements[1], registers);
                if (elements.Length > 2)
                {
                    statement.RValue = ParseValue(elements[2], registers);
                }

                ret.Add(statement);
            }

            return ret;
        }

        private Value ParseValue(string v, Value[] registers)
        {
            if (v.Length == 1 && char.IsLetter(v[0]))
            {
                var index = (int)v[0] - (int)'a';
                return registers[index + 1];
            }
            return new Value(int.Parse(v));            
        }

        public string Part2(string input)
        {
            return Part1("cpy 1 c\n" + input);
        }

        

        private class Value
        {
            public int value;

            public Value(int value)
            {
                this.value = value;
            }

            public Value() { }

            private static Value NoValue = new Value(int.MinValue);
        }

        private class Statement
        {
            public Action<Value, Value, Value> instruction;
            public Value LValue;
            public Value RValue;

            public void Execute(Value p)
            {
                instruction(LValue, RValue, p);
            }
        }

        public void Test()
        {
            Utils.Test(
                Part1,
                new string[]
                {
@"cpy 41 a
inc a
inc a
dec a
jnz a 2
dec a"
                },
                new string[] { "42" }
                );
        }
    }
}
