namespace AdventOfCode2017.Input
{
    public class BooleanArrayInputParser : IInputParser<bool[,]>
    {
        public bool[,] ParseInput(string input)
        {
            var lines = input.Split('\n');
            var result = new bool[lines.Length, lines[0].Length];
            for(int i = 0; i < lines.Length; i++)
                for(int j = 0; j < lines[i].Length; j++)
                    result[i,j] = lines[i][j] == '#';
            return result;
        }
    }
}