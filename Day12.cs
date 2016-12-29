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


        public Day12()
        {

        }

        public string Part1(dynamic input)
        {
            var program = new Program(input);
            program.Execute();

            Console.WriteLine("Should be 317993");
            return program.registers[0].value.ToString();
            
        }

        private string printRegisters(Value[] registers)
        {
            return String.Format("{0:10} {1:10} {2:10} {3:10}", registers[1].value, registers[2].value, registers[3].value, registers[4].value);
        }

        public string Part2(dynamic input)
        {
            Console.WriteLine("Should be 9227647");
            return Part1("cpy 1 c\n" + input);
        }        

        public class Program
        {
            Dictionary<string, Action<Value, Value>> instructions;
            List<Statement> statements;
            public Value[] registers;
            Action<int> output;
            int pc;
            bool stop;
            int outputdata;

            public Program(string program)
            {
                registers = new Value[4];
                for (int i = 0; i < registers.Length; i++)
                {
                    registers[i] = new Value();
                }

                instructions = new Dictionary<string, Action<Value, Value>>()
                {
                    { "cpy", (x, y) => { y.value = x.value; pc++; } },
                    { "inc", (x, y) => { x.value = x.value + 1; pc++; } },
                    { "dec", (x, y) => { x.value = x.value - 1; pc++; } },
                    { "jnz", (x, y) => pc +=  (x.value == 0 ? 1 : y.value) },
                    { "tgl", (x, y) => {  toggle(x); pc++; } },
                    { "add", (x, y) => { x.value = x.value + y.value; pc++; } },
                    { "out", (x, y) => { output(x.value); pc++; } },
                };

                statements = ParseProgram(program);
            }

            public void Reset(params int[] registervalues)
            {
                pc = 0;
                for (int i = 0; i < registers.Length; i++)
                {
                    if (i < registervalues.Length)
                    {
                        registers[i].value = registervalues[i];
                    }
                    else
                    {
                        registers[i].value = 0;
                    }

                }
            }

            public void Execute()
            {
                while (pc < statements.Count && !stop)
                {
                    statements[pc].Execute();
                    //Utils.WriteTransient(printRegisters(registers));
                }
                stop = false;
            }

            public int ReadNextOutput()
            {
                output = v => { stop = true; outputdata = v; };
                Execute();
                return outputdata;
            }

            private List<Statement> ParseProgram(string input)
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
                    return registers[index];
                }
                return new Value(int.Parse(v));
            }

            private void toggle(Value offset)
            {
                int value = offset.value + pc;
                if (value < statements.Count)
                {
                    var statement = statements[value];

                    switch (statement.type)
                    {
                        case "inc":
                            statement.type = "dec";
                            break;
                        case "dec":
                        case "tgl":
                        case "out":
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
        }

        public class Value
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

        public class Statement
        {
            public Action<Value, Value> instruction;
            public Value LValue;
            public Value RValue;
            public string type;

            public void Execute()
            {
                instruction(LValue, RValue);
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
