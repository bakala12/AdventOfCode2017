namespace AdventOfCode2017.Input
{
    public class CaptchaParser : IInputParser<int[]>
    {
        public int[] ParseInput(string input)
        {
            return input.Select(c => c - '0').ToArray();
        }
    }
}