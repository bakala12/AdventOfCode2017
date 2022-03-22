using AdventOfCode2017.Models;

namespace AdventOfCode2017.Input
{
    public class RegisterInstructionInputParser : IInputParser<RegisterInstruction[]>
    {
        public RegisterInstruction[] ParseInput(string input)
        {
            var split = input.Split(Environment.NewLine);
            var result = new RegisterInstruction[split.Length];
            for(int i = 0; i < split.Length; i++)
            {
                var s = split[i].Split(' ');
                result[i] = new RegisterInstruction(s[0], (RegisterInstructionType)Enum.Parse(typeof(RegisterInstructionType), s[1]), int.Parse(s[2]), 
                    new RegisterInstructionCondition(s[4], ParseFunction(s[5]), int.Parse(s[6])));
            }
            return result;
        }

        private static Func<int, int, bool> ParseFunction(string str)
        {
            return str switch 
            {
                "<" => (x,y) => x < y,
                ">" => (x,y) => x > y,
                "<=" => (x,y) => x <= y,
                ">=" => (x,y) => x >= y,
                "!=" => (x,y) => x != y,
                "==" => (x,y) => x == y,
                _ => (x,y) => false
            };
        }
    }
}