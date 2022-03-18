namespace AdventOfCode2017.Input
{
    public class SpreadSheetParser : IInputParser<int[][]>
    {
        public int[][] ParseInput(string input)
        {
            return input.Split(Environment.NewLine).Select(l => l.Split().Select(int.Parse).ToArray()).ToArray();
        }
    }
}