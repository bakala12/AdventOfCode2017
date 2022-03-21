using AdventOfCode2017.Input;
using AdventOfCode2017.Models;

namespace AdventOfCode2017.Days
{
    public class Day1 : AocDay<Captcha>
    {
        public Day1(IInputParser<Captcha> inputParser) : base(inputParser)
        {
        }

        protected override void Part1(Captcha input)
        {
            int c = 0;
            for(int i = 1; i < input.Length; i++)
                if(input[i] == input[i-1])
                    c += input[i];
            if(input[input.Length-1] == input[0])
                c += input[0];
            Console.WriteLine(c);
        }

        protected override void Part2(Captcha input)
        {
            int c = 0;
            int m = input.Length / 2;
            for(int i = 0; i < input.Length; i++)
                if(input[i] == input[(i+m) % input.Length])
                    c += input[i];
            Console.WriteLine(c);
        }
    }
}