using AdventOfCode2017.Models;

namespace AdventOfCode2017.Input
{
    public class HexDirecionInputParser : IInputParser<HexDirection[]>
    {
        public HexDirection[] ParseInput(string input)
        {
            return input.Split(',').Select(s => (HexDirection)Enum.Parse(typeof(HexDirection), s)).ToArray();
        }
    }
}