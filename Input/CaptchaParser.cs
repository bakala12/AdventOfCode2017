using AdventOfCode2017.Models;

namespace AdventOfCode2017.Input
{
    public class CaptchaParser : IInputParser<Captcha>
    {
        public Captcha ParseInput(string input)
        {
            return new Captcha(input.Select(c => c - '0').ToArray());
        }
    }
}