using AdventOfCode2017.Models;

namespace AdventOfCode2017.Input
{
    public class TuringMachineInputParser : IInputParser<(TuringMachine, long)>
    {
        public (TuringMachine, long) ParseInput(string input)
        {
            var states = new Dictionary<string, TuringMachineState>();
            var lines = input.Split(Environment.NewLine);
            string startState = lines[0].Split()[3].Trim('.');
            long checkSumSteps = long.Parse(lines[1].Split()[5]);
            int pos = 3;
            while(pos < lines.Length)
            {
                var state = ParseState(lines, pos);
                states.Add(state.Name, state);
                pos += 10;
            }
            return (new TuringMachine(startState, states), checkSumSteps);
        }

        private TuringMachineState ParseState(string[] lines, int pos)
        {
            var name = lines[pos].Split()[2].Trim(':');
            var on0 = ParseAction(lines, pos+1);
            var on1 = ParseAction(lines, pos+5);
            return new TuringMachineState(name, on0, on1);
        }

        private TuringMachineAction ParseAction(string[] lines, int pos)
        {
            var symbol = int.Parse(lines[pos].Split(' ', StringSplitOptions.RemoveEmptyEntries)[5].Trim(':'));
            var write = int.Parse(lines[pos+1].Split(' ', StringSplitOptions.RemoveEmptyEntries)[4].Trim('.'));
            var move = lines[pos+2].Split(' ', StringSplitOptions.RemoveEmptyEntries)[6].Trim('.') == "left" ? TuringMachineMove.Left : TuringMachineMove.Right;
            var next = lines[pos+3].Split(' ', StringSplitOptions.RemoveEmptyEntries)[4].Trim('.');
            return new TuringMachineAction(symbol, write, move, next);
        }
    }
}