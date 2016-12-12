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
            var previousState = new Dictionary<string, int>();
            var movequeue = new Queue<int[]>();
            var depthqueue = new Queue<int>();

            movequeue.Enqueue(state);
            depthqueue.Enqueue(0);

            int count = 0;

            while (movequeue.Count > 0 && minMoves == int.MaxValue)
            {
                count++;

                var nextstate = movequeue.Dequeue();
                var nextDepth = depthqueue.Dequeue() + 1;

                tryMoves(previousState, nextstate, nextDepth, movequeue, depthqueue);
            }

            return minMoves.ToString();
        }

        private int iterations = 0;
        private List<string> previous = new List<string>();

        private string StateString(int[] state)
        {
            return string.Join("", state.Select(s => s.ToString()).ToArray());
        }

        private void tryMoves(Dictionary<string, int> previousState, int[] state, int moves, Queue<int[]> nextStates, Queue<int> nextDepth)
        {
            if (iterations++ % 100 == 0)
            {
                // printState(state);
            }

            int[] possibleDeltas = new int[] { 1, -1 };
            if (state[0] == 3)
            {
                possibleDeltas = new int[] { -1 };
            }
            if (state[0] == 0)
            {
                possibleDeltas = new int[] { 1 };
            }

            foreach (var delta in possibleDeltas)
            {
                foreach (int[] itemsToMove in enumeratePossibleMoves(state))
                {
                    int[] newState = doMove(state, delta, itemsToMove);
                    var stateString = StateString(newState);
                    if (!previousState.ContainsKey(stateString))
                    {
                        previousState[stateString] = moves;

                        if (isValidState(newState))
                        {
                            if (isFinalState(newState))
                            {
                                minMoves = moves;
                                Console.Write(minMoves);
                                Console.CursorLeft = 0;
                            }
                            else
                            {
                                nextStates.Enqueue(newState);
                                nextDepth.Enqueue(moves);
                            }
                        }
                        else
                        {
                            previousState[stateString] = 0;
                        }
                    }
                }
            }
            
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

        private bool isFinalState(int[] newState)
        {
            for (int i = 1; i < newState.Length; i++)
            {
                if (newState[i] != 3)
                {
                    return false;
                }
            }

            return true;
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
