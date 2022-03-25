namespace AdventOfCode2017.Input
{
    public class StringLinesParser : IInputParser<string[]>
    {
        public string[] ParseInput(string input)
        {
            return input.Split(Environment.NewLine);
        }
    }
}