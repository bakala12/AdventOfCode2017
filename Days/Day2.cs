using AdventOfCode2017.Input;

namespace AdventOfCode2017.Days
{
    public class Day2 : AocDay<int[][]>
    {
        public Day2(IInputParser<int[][]> inputParser) : base(inputParser)
        {
        }

        protected override void Part1(int[][] input)
        {
            Console.WriteLine(input.Sum(l => l.Max() - l.Min()));
        }

        protected override void Part2(int[][] input)
        {
            int sum = 0;
            for(int i = 0; i < input.Length; i++)
                sum += FindDividers(input[i]);
            Console.WriteLine(sum);
        }

        private static int FindDividers(int[] tab)
        {
            for(int i = 0; i < tab.Length; i++)
                for(int j = 0; j < tab.Length; j++)
                    if(i != j && tab[i] > tab[j] && tab[i] % tab[j] == 0)
                        return tab[i] / tab[j];
            return 0;
        }
    }
}