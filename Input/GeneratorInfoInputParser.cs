namespace AdventOfCode2017.Input
{
    public class GeneratorInfoInputParser : IInputParser<(int, int)>
    {
        public (int, int) ParseInput(string input)
        {
            var s = input.Split(Environment.NewLine).Select(l => l.Split()).ToArray();
            return (int.Parse(s[0][4]), int.Parse(s[1][4]));
        }
    }
}