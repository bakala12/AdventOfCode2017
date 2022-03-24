using AdventOfCode2017.Models;

namespace AdventOfCode2017.Input
{
    public class FirewallInputParser : IInputParser<List<FirewallConfiguration>>
    {
        public List<FirewallConfiguration> ParseInput(string input)
        {
            var list = new List<FirewallConfiguration>();
            foreach(var line in input.Split(Environment.NewLine))
            {
                var s = line.Split(": ");
                list.Add(new FirewallConfiguration(int.Parse(s[0]), int.Parse(s[1])));
            }
            return list;
        }
    }
}