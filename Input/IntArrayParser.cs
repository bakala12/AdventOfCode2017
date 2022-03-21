namespace AdventOfCode2017.Input
{
    public class IntArrayParser : IInputParser<int[]>
    {
        public int[] ParseInput(string input)
        {
            return input.Split(Environment.NewLine).Select(int.Parse).ToArray();
        }
    }
}