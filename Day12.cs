using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2016
{
    class Day12 : Day
    {
        public dynamic Input
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

        Dictionary<string, Action<Value, Value, Value, List<Statement>>> instructions;

        public Day12()
        {
            instructions = new Dictionary<string, Action<Value, Value, Value, List<Statement>>>()
            {
                { "cpy", (x, y, p, r) => { y.value = x.value; p.value = p.value + 1; } },
                { "inc", (x, y, p, r) => { x.value = x.value + 1; p.value = p.value + 1; } },
                { "dec", (x, y, p, r) => { x.value = x.value - 1; p.value = p.value + 1; } },
                { "jnz", (x, y, p, r) => p.value = p.value + (x.value == 0 ? 1 : y.value) },
                { "tgl", (x, y, p, r) => {  toggle(x, p, r); p.value = p.value + 1; } },
                { "add", (x, y, p, r) => { x.value = x.value + y.value; p.value = p.value + 1; } }
            };
        }

        public string Part1(dynamic input)
        {

            var registers = new Value[5]; // p, a, b, c, d
            for (int i = 0; i < registers.Length; i++)
            {
                registers[i] = new Value();
            }

            var program = ParseProgram(input, instructions, registers);
            var pc = registers[0];

            while (pc.value < program.Count)
            {
                program[pc.value].Execute(pc, program);
                //Utils.WriteTransient(printRegisters(registers));
            }

            Console.WriteLine("Should be 12480");
            return registers[1].value.ToString();
            
        }

        private string printRegisters(Value[] registers)
        {
            return String.Format("{0:10} {1:10} {2:10} {3:10}", registers[1].value, registers[2].value, registers[3].value, registers[4].value);
        }

        private void toggle(Value offset, Value pc, List<Statement> program)
        {
            int value = offset.value + pc.value;
            if (value < program.Count)
            {
                var statement = program[value];

                switch (statement.type)
                {
                    case "inc":
                        statement.type = "dec";
                        break;
                    case "dec":
                    case "tgl":
                        statement.type = "inc";
                        break;
                    case "jnz":
                        statement.type = "cpy";
                        break;
                    case "cpy":
                        statement.type = "jnz";
                        break;
                }

                statement.instruction = instructions[statement.type];
            }
        }

        private List<Statement> ParseProgram(string input, Dictionary<string, Action<Value, Value, Value, List<Statement>>> instructions, Value[] registers)
        {
            var ret = new List<Statement>();

            foreach (var line in Utils.splitLines(input))
            {
                var elements = line.Split(' ');
                var statement = new Statement();
                statement.instruction = instructions[elements[0]];
                statement.LValue = ParseValue(elements[1], registers);
                statement.type = elements[0];
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

        public string Part2(dynamic input)
        {
            return Part1("cpy 1 c\n" + input);
        }        

        private class Value
        {
            public int value
            {
                get { return _value; }
                set { if (mutable) { _value = value; } }
            }

            public Value(int value)
            {
                this._value = value;
            }

            public Value() { mutable = true; }

            private static Value NoValue = new Value(int.MinValue);
            private int _value;
            private bool mutable;

            public override string ToString()
            {
                return value.ToString();
            }
        }

        private class Statement
        {
            public Action<Value, Value, Value, List<Statement>> instruction;
            public Value LValue;
            public Value RValue;
            public string type;

            public void Execute(Value p, List<Statement> program)
            {
                instruction(LValue, RValue, p, program);
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
