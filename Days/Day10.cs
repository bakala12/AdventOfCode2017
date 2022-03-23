using System.Text;
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
            int tabSize = 256;
            var tab = Enumerable.Range(0, tabSize).ToArray();
            int pos = 0, skip = 0;
            foreach(var length in input)
            {
                int end = (pos + length - 1) % tabSize;
                int p = pos;
                int l = length;
                while(l > 1)
                {
                    int t = tab[p];
                    tab[p] = tab[end];
                    tab[end] = t;
                    p++;
                    end += tabSize - 1;
                    p %= tabSize;
                    end %= tabSize;
                    l -= 2;
                }
                pos += length + skip;
                pos %= tabSize;
                skip++;
            }
            Console.WriteLine(tab[0] * tab[1]);
        }

        protected override void Part2(int[] input)
        {
            var bytes = PrepareInput(input);
            int tabSize = 256;
            var tab = Enumerable.Range(0, tabSize).ToArray();
            int pos = 0, skip = 0;
            for(int round = 0; round < 64; round++)
            {
                foreach(var length in bytes)
                {
                    int end = (pos + length - 1) % tabSize;
                    int p = pos;
                    int l = length;
                    while(l > 1)
                    {
                        int t = tab[p];
                        tab[p] = tab[end];
                        tab[end] = t;
                        p++;
                        end += tabSize - 1;
                        p %= tabSize;
                        end %= tabSize;
                        l -= 2;
                    }
                    pos += length + skip;
                    pos %= tabSize;
                    skip++;
                }
            }
            Console.WriteLine(PrepareHash(tab));
        }

        private byte[] PrepareInput(int[] input)
        {
            var s = string.Join(',', input);
            var bytes = new byte[s.Length + 5];
            for(int i = 0; i < s.Length; i++)
                bytes[i] = (byte)s[i];
            var additional = new byte[] { 17, 31, 73, 47, 23 };
            Array.Copy(additional, 0, bytes, s.Length, additional.Length);
            return bytes;
        }

        private string PrepareHash(int[] tab)
        {
            byte[] denseHash = new byte[16];
            for(int i = 0; i < 16; i++)
            {
                denseHash[i] = (byte)tab[16*i];
                for(int j = 1; j < 16; j++)
                    denseHash[i] ^= (byte)(tab[16*i+j]);
            }
            var sb = new StringBuilder();
            for(int i = 0; i < 16; i++)
                sb.Append((char)(denseHash[i]));
            return sb.ToString().Trim();
        }
    }
}