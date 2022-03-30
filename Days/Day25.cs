using AdventOfCode2017.Input;
using AdventOfCode2017.Models;

namespace AdventOfCode2017.Days
{
    public class Day25 : AocDay<(TuringMachine, long)>
    {
        public Day25(IInputParser<(TuringMachine, long)> inputParser) : base(inputParser)
        {
        }

        protected override void Part1((TuringMachine, long) input)
        {
            var (machine, checkSumSteps) = input;
            int cursor = 0;
            var tape = new TuringMachineTape();
            TuringMachineState state = machine.States[machine.StartState];
            for(int s = 0; s < checkSumSteps; s++)
            {
                var v = tape[cursor];
                var action = v == 0 ? state.ActionOn0 : state.ActionOn1;
                tape[cursor] = action.Write;
                state = machine.States[action.NextStateName];
                cursor += action.Move == TuringMachineMove.Left ? -1 : 1;
            }
            Console.WriteLine(tape.CountOnes());
        }

        protected override void Part2((TuringMachine, long) input)
        {
            Console.WriteLine("AoC 2017 DONE!");
        }

        private class TuringMachineTape
        {
            private Dictionary<int, int> _values;

            public TuringMachineTape()
            {
                _values = new Dictionary<int, int>();
            }

            public int this[int cursor]
            {
                get
                {
                    return _values.TryGetValue(cursor, out int val) ? val : 0;
                }
                set
                {
                    if(!_values.TryGetValue(cursor, out _))
                        _values.Add(cursor, value);
                    else
                        _values[cursor] = value;
                }
            }
        
            public long CountOnes() => _values.LongCount(v => v.Value == 1);
        }
    }
}