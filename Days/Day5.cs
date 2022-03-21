using AdventOfCode2017.Input;

namespace AdventOfCode2017.Days
{
    public class Day5 : AocDay<int[]>
    {
        public Day5(IInputParser<int[]> inputParser) : base(inputParser)
        {
        }

        protected override void Part1(int[] input)
        {
            var data = new int[input.Length];
            Array.Copy(input, data, input.Length);
            int pos = 0;
            int step = 0;
            while(pos >= 0 && pos < data.Length)
            {
                int jump = data[pos];
                data[pos]++;
                pos += jump;
                step++;
            }
            Console.WriteLine(step);
        }

        protected override void Part2(int[] input)
        {
            var data = new int[input.Length];
            Array.Copy(input, data, input.Length);
            int pos = 0;
            int step = 0;
            while(pos >= 0 && pos < data.Length)
            {
                int jump = data[pos];
                if(jump < 3)
                    data[pos]++;
                else
                    data[pos]--;
                pos += jump;
                step++;
            }
            Console.WriteLine(step);
        }
    }
}