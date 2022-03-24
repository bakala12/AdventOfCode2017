using AdventOfCode2017.Input;
using AdventOfCode2017.Models;

namespace AdventOfCode2017.Days
{
    public class Day13 : AocDay<List<FirewallConfiguration>>
    {
        public Day13(IInputParser<List<FirewallConfiguration>> inputParser) : base(inputParser)
        {
        }

        protected override void Part1(List<FirewallConfiguration> input)
        {
            Console.WriteLine(input.Sum(c => ScannerPosition(c, c.Depth) == 0 ? c.Depth * c.Range : 0));
        }

        protected override void Part2(List<FirewallConfiguration> input)
        {
            int delay = 0;
            while(CheckIfCaught(input, delay)) delay++;
            Console.WriteLine(delay);
        }

        private static int ScannerPosition(FirewallConfiguration configuration, int time)
        {
            var r = time % (2*configuration.Range-2);
            return r >= configuration.Range ? 2 * configuration.Range - 2 - r : r;
        }

        private static bool CheckIfCaught(List<FirewallConfiguration> configuration, int delay)
        {
            return configuration.Any(c => ScannerPosition(c, c.Depth + delay) == 0);
        }
    }
}