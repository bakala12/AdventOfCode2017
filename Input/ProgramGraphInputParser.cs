using AdventOfCode2017.Models;

namespace AdventOfCode2017.Input
{
    public class ProgramGraphInputParser : IInputParser<ProgramGraphVertex[]>
    {
        public ProgramGraphVertex[] ParseInput(string input)
        {
            var list = new List<ProgramGraphVertex>();
            foreach(var line in input.Split(Environment.NewLine))
            {
                var s = line.Split(" <-> ");
                list.Add(new ProgramGraphVertex(int.Parse(s[0]), s[1].Split(new char[] { ',', ' '}, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray()));
            }
            return list.ToArray();
        }
    }
}