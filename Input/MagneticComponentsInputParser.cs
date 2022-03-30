using AdventOfCode2017.Models;

namespace AdventOfCode2017.Input
{
    public class MagneticComponentsInputParser : IInputParser<MagneticComponent[]>
    {
        public MagneticComponent[] ParseInput(string input)
        {
            var list = new List<MagneticComponent>();
            foreach(var line in input.Split(Environment.NewLine))
            {
                var s = line.Split('/');
                list.Add(new MagneticComponent(int.Parse(s[0]), int.Parse(s[1])));
            }
            return list.ToArray();
        }
    }
}