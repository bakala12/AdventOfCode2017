namespace AdventOfCode2017.Input
{
    public class SingleIntParser : IInputParser<int>
    {
        public int ParseInput(string input)
        {
            return int.Parse(input);
        }
    }
}