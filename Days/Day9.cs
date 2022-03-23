using AdventOfCode2017.Input;
using AdventOfCode2017.Models;

namespace AdventOfCode2017.Days
{
    public class Day9 : AocDay<string>
    {
        public Day9(IInputParser<string> inputParser) : base(inputParser)
        {
        }

        protected override void Part1(string input)
        {
            var group = Parser.ParseGroup(new Queue<char>(input));
            Console.WriteLine(group.GetScore());
        }

        protected override void Part2(string input)
        {
            var group = Parser.ParseGroup(new Queue<char>(input));
            Console.WriteLine(group.CountCharacters());
        }
    }
}