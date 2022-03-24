using System.Text;
using AdventOfCode2017.Helpers;
using AdventOfCode2017.Input;

namespace AdventOfCode2017.Days
{
    public class Day10 : AocDay<int[]>
    {
        public Day10(IInputParser<int[]> inputParser) : base(inputParser)
        {
            ((IntArrayParser)inputParser).Separator = ",";
        }

        protected override void Part1(int[] input)
        {
            var tab = KnotHashHelper.CalculateKnotHashArray(input, 1);
            Console.WriteLine(tab[0] * tab[1]);
        }

        protected override void Part2(int[] input)
        {
            var bytes = PrepareInput(input);
            var hash = KnotHashHelper.CalculateKnotHash(bytes, 64);
            Console.WriteLine(DisplayHash(hash));
        }

        private int[] PrepareInput(int[] input)
        {
            var s = string.Join(',', input);
            var bytes = new int[s.Length + 5];
            for(int i = 0; i < s.Length; i++)
                bytes[i] = (byte)s[i];
            var additional = new byte[] { 17, 31, 73, 47, 23 };
            Array.Copy(additional, 0, bytes, s.Length, additional.Length);
            return bytes;
        }

        private string DisplayHash(byte[] denseHash)
        {
            var sb = new StringBuilder();
            for(int i = 0; i < denseHash.Length; i++)
                sb.Append(denseHash[i].ToString("X2"));
            return sb.ToString().Trim();
        }
    }
}