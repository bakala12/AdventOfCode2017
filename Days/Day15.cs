using AdventOfCode2017.Input;

namespace AdventOfCode2017.Days
{
    public class Day15 : AocDay<(int, int)>
    {
        public Day15(IInputParser<(int, int)> inputParser) : base(inputParser)
        {
        }

        protected override void Part1((int, int) input)
        {
            int factorA = 16807;
            int factorB = 48271;
            var (a,b) = input;
            int c = 0;
            for(int i = 0; i < 40000000; i++)
            {
                a = NextValue(a, factorA);
                b = NextValue(b, factorB);
                if((a & 0xffff) == (b & 0xffff))
                    c++;
            }
            Console.WriteLine(c);
        }

        protected override void Part2((int, int) input)
        {
            int factorA = 16807;
            int factorB = 48271;
            var (a,b) = input;
            var aValues = GetValues(a, factorA, 0x3);
            var bValues = GetValues(b, factorB, 0x7);
            Console.Write(aValues.Zip(bValues).Take(5000000).Count(p => (p.First & 0xffff) == (p.Second & 0xffff)));
        }

        private static int NextValue(int previous, int factor)
        {
            long val = (long)previous * (long)factor;
            return (int)(val % 2147483647);
        }

        private static IEnumerable<int> GetValues(int start, int factor, int mask)
        {
            while(true)
            {
                start = NextValue(start, factor);
                if((start & mask) == 0)
                    yield return start;
            }
        }
    }
}