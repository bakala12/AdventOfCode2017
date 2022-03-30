using AdventOfCode2017.Input;

namespace AdventOfCode2017.Days
{
    public class Day23 : AocDay<string[]>
    {
        public Day23(IInputParser<string[]> inputParser) : base(inputParser)
        {
        }

        protected override void Part1(string[] input)
        {
            Console.WriteLine(ExecuteProgram(input));
        }

        protected override void Part2(string[] input)
        {
            long b = long.Parse(input[0].Split()[2]) * 100+100000;
            int h = 0;
            for(int i = 0; i <= 1000; i++)
            {
                if(IsComposite(b))
                    h++;
                b += 17;
            }
            Console.WriteLine(h);
        }

        private static bool IsComposite(long b)
        {
            for(int d = 2; d < b; d++)
                if(b % d == 0)
                    return true;
            return false;
        }

        private static int ExecuteProgram(string[] input)
        {
            var registers = new Dictionary<char, long>()
            {
                { 'a', 0 }, 
                { 'b', 0 }, 
                { 'c', 0 }, 
                { 'd', 0 }, 
                { 'e', 0 }, 
                { 'f', 0 }, 
                { 'g', 0 }, 
                { 'h', 0 }, 
            };

            long GetValue(string s) => long.TryParse(s, out long val) ? val : registers![s[0]];
            int muls = 0;
            int pos = 0;
            while(pos >= 0 && pos < input.Length)
            {
                var s = input[pos].Split();
                switch(s[0])
                {
                    case "set":
                        registers[s[1][0]] = GetValue(s[2]);
                        break;
                    case "sub":
                        registers[s[1][0]] -= GetValue(s[2]);
                        break;
                    case "mul":
                        registers[s[1][0]] *= GetValue(s[2]);
                        muls++;
                        break;
                    case "jnz":
                        if(GetValue(s[1]) != 0)
                            pos += (int)(GetValue(s[2]) - 1);
                        break;
                }
                pos++;
            }

            return muls;
        }
    }
}