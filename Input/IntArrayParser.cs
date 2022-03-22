namespace AdventOfCode2017.Input
{
    public class IntArrayParser : IInputParser<int[]>
    {
        public string Separator { get; set; } = Environment.NewLine;

        public int[] ParseInput(string input)
        {
            return input.Split(Separator).Select(int.Parse).ToArray();
        }
    }
}