using AdventOfCode2017.Input;

namespace AdventOfCode2017.Days
{
    public class Day17 : AocDay<int>
    {
        public Day17(IInputParser<int> inputParser) : base(inputParser)
        {
        }

        protected override void Part1(int input)
        {
            var list = new List<int>() { 0 };
            int pos = 0;
            for(int iter = 1; iter <= 2017; iter++)
            {
                pos = (pos + input) % iter + 1;
                list.Insert(pos, iter);
            }
            Console.WriteLine(list[(pos+1) % list.Count]);
        }

        protected override void Part2(int input)
        {
            int pos = 0;
            int item = -1;
            for(int iter = 1; iter <= 50_000_000; iter++)
            {
                pos = (pos + input) % iter + 1;
                if(pos == 1)
                    item = iter;
            }
            Console.WriteLine(item);
        }
    }
}