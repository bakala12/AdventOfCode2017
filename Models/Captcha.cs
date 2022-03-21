namespace AdventOfCode2017.Models
{
    public readonly record struct Captcha(int[] Digits)
    {
        public int Length => Digits.Length;
        public int this[int index] => Digits[index];
    }
}