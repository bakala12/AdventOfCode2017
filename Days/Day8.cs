using AdventOfCode2017.Input;
using AdventOfCode2017.Models;

namespace AdventOfCode2017.Days
{
    public class Day8 : AocDay<RegisterInstruction[]>
    {
        public Day8(IInputParser<RegisterInstruction[]> inputParser) : base(inputParser)
        {
        }

        protected override void Part1(RegisterInstruction[] input)
        {
            var registers = new Dictionary<string, int>();

            var valueProvider = new Func<string, int>(register =>
            {
                if(!registers!.ContainsKey(register))
                    registers[register] = 0;
                return registers[register];
            });

            foreach(var instr in input)
            {
                if(instr.Condition.EvaluateCondition(valueProvider))
                    registers[instr.Register] = valueProvider(instr.Register) + instr.ChangeValue;
            }

            Console.WriteLine(registers.Values.Max());
        }

        protected override void Part2(RegisterInstruction[] input)
        {
            var registers = new Dictionary<string, int>();
            int highestEver = 0;

            var valueProvider = new Func<string, int>(register =>
            {
                if(!registers!.ContainsKey(register))
                    registers[register] = 0;
                return registers[register];
            });

            var changeValue = new Action<string, int>((str, val) => {
                if(!registers.ContainsKey(str))
                    registers[str] = val;
                else
                    registers[str] += val;
                if(registers[str] > highestEver)
                    highestEver = registers[str];
            });

            foreach(var instr in input)
            {
                if(instr.Condition.EvaluateCondition(valueProvider))
                    changeValue(instr.Register, instr.ChangeValue);
            }

            Console.WriteLine(highestEver);
        }
    }
}