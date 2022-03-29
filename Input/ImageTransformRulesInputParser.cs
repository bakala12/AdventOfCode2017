using AdventOfCode2017.Models;

namespace AdventOfCode2017.Input
{
    public class ImageTransformRulesInputParser : IInputParser<List<ImageTransformRule>>
    {
        public List<ImageTransformRule> ParseInput(string input)
        {
            var list = new List<ImageTransformRule>();
            foreach(var line in input.Split(Environment.NewLine))
            {
                var l = line.Split(" => ");
                var size = l[0].IndexOf('/');
                var pattern = new bool[size,size];
                var result = new bool[size+1, size+1];
                WriteSquares(pattern, size, l[0]);
                WriteSquares(result, size+1, l[1]);
                list.Add(new ImageTransformRule(size, pattern, result));
            }
            return list;
        }

        private static void WriteSquares(bool[,] tab, int size, string str)
        {
            var split = str.Split('/');
            if(split.Length != size)
                throw new Exception("Invalid input");
            for(int row = 0; row < split.Length; row++)
            {
                if(split[row].Length != size)
                    throw new Exception("Invalid row");
                for(int column = 0; column < split[row].Length; column++)
                    tab[row, column] = split[row][column] == '#';
            }
        }
    }
}