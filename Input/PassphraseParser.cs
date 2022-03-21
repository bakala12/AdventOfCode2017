namespace AdventOfCode2017.Input
{
    public class PassphraseParser : IInputParser<string[][]>
    {
        public string[][] ParseInput(string input)
        {
            return input.Split(Environment.NewLine).Select(s => s.Split()).ToArray();
        }
    }
}