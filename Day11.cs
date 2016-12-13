using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2016
{
    class Day11 : Day
    {
        public string Input
        {
            get
            {
                //return "ThG ThM PlG StG;PlM StM;PrG PrM RuG RuM;;";                
                return "00112200022";
            }
        }

        static int minMoves = int.MaxValue;

        public string Part1(string input)
        {
            minMoves = int.MaxValue;

            int[] state = ParseInput(input);

            var states = new Dictionary<int, int>();
            var next = new Queue<int>();

            next.Enqueue(EncodeState(state));
            states.Add(EncodeState(state), 0);


            int[] target = new int[state.Length];
            for (int j= 0; j < target.Length; j++)
            {
                target[j] = 3;
            }
            int encodedTarget = EncodeState(target);


            var moves = 0;
            var movesIncreasesAt = 0;

            int i = 0;

            while (next.Count > 0)
            {

                var nextstate = next.Dequeue();

                if (tryMoves(states, next, nextstate, encodedTarget, state.Length))
                {
                    return (moves + 1).ToString();
                }

                if (movesIncreasesAt == i)
                {
                    moves++;
                    movesIncreasesAt = states.Count - 1;
                }

                i++;
            }

            return "";
        }

        private int EncodeState(int[] state)
        {
            int ret = 0;

            for (int i = 0; i < state.Length; i++)
            {
                ret = ret << 2;
                ret += state[i];
            }

            return ret;

        }

        private int[] DecodeState(int encoded, int count)
        {
            var ret = new int[count];

            for (int i = 0; i < count; i++)
            {
                ret[count - i - 1] = encoded % 4;
                encoded /= 4;
            }

            return ret;

        }

        private bool tryMoves(Dictionary<int, int> previousState, Queue<int> nextStates, int state, int target, int statesize)
        {

            int[] decodedState = DecodeState(state, statesize);

            int[] possibleDeltas = new int[] { 1, -1 };

            if (decodedState[0] == 3)
            {
                possibleDeltas = new int[] { -1 };
            }

            if (decodedState[0] == 0)
            {
                possibleDeltas = new int[] { 1 };

            }


            foreach (var delta in possibleDeltas)
            {
                foreach (int[] itemsToMove in enumeratePossibleMoves(decodedState))
                {
                    int[] newState = doMove(decodedState, delta, itemsToMove);
                    int encoded = EncodeState(newState);
                    if (!previousState.ContainsKey(encoded))
                    {
                        if (isValidState(newState))
                        {

                            if (encoded == target)
                            {                                
                                Console.Write(minMoves);
                                Console.CursorLeft = 0;
                                return true;
                            }
                            else
                            {
                                previousState.Add(encoded, 0);
                                nextStates.Enqueue(encoded);
                            }
                        }
                    }
                }
            }

            return false;       
        }

        private void printState(int[] state)
        {
            var top = Console.CursorTop;
            for (int i = 3; i >= 0; i--)
            {
                for (int j = 0; j < state.Length; j++)
                {
                    if (state[j] == i)
                    {
                        Console.Write(j);
                    }
                    else
                    {
                        Console.Write('.');                    
                    }
                }

                Console.WriteLine();
            }

            Console.CursorTop = top;
        }

        private bool isValidState(int[] state)
        {
            int count = (state.Length - 1) / 2;

            for (int chip = 0; chip < count; chip++)
            {
                if (state[chip + 1] == state[chip + count + 1])
                {
                    // this chip is powered / shielded
                    continue;
                }

                for (int gen = 0; gen < count; gen++)
                {
                    if (gen != chip && state[gen + count + 1] == state[chip + 1])
                    {
                        return false;
                    }

                }

            }

            return true;

        }

        private int[] doMove(int[] state, int delta, int[] itemsToMove)
        {
            var ret = (int[])state.Clone();
            foreach (int i in itemsToMove)
            {
                ret[i] += delta;
            }

            ret[0] += delta;

            return ret;
        }

        private IEnumerable<int[]> enumeratePossibleMoves(int[] state)
        {
            var floor = state[0];

            for (int i = 1; i < state.Length; i++)
            {
                if (state[i] == floor)
                {
                    yield return new int[] { i };

                    for (int j = i + 1; j < state.Length; j++)
                    {
                        if (state[j] == floor)
                        {
                            yield return new int[] { i, j };
                        }
                    }

                }
            }            
        }

        private int[] ParseInput(string input)
        {
            return input.Select(c => int.Parse(c.ToString())).ToArray();
        }

        public string Part2(string input)
        {
            return Part1("001122000002200");
        }

        public void Test()
        {
            // "HM LM;HG;LG;;"
            Utils.Test(Part1, new string[] { "00012" }, new string[] { "11" });
        }


    }
}
