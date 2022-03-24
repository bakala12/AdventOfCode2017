namespace AdventOfCode2017.Helpers
{
    public static class KnotHashHelper
    {
        public static int[] CalculateKnotHashArray(IEnumerable<int> lengths, int rounds)
        {
            int tabSize = 256;
            var tab = Enumerable.Range(0, tabSize).ToArray();
            int pos = 0, skip = 0;
            for(int round = 0; round < rounds; round++)
            {
                foreach(var length in lengths)
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
            return tab;
        }

        public static byte[] CalculateKnotHash(IEnumerable<int> lengths, int rounds = 64)
        {
            var tab = CalculateKnotHashArray(lengths, rounds);
            byte[] denseHash = new byte[16];
            for(int i = 0; i < 16; i++)
            {
                denseHash[i] = (byte)tab[16*i];
                for(int j = 1; j < 16; j++)
                    denseHash[i] ^= (byte)(tab[16*i+j]);
            }
            return denseHash;
        }

        public static byte[] CalculateKnotHash(string input)
        {
            var lengths = input.Select(c => (int)c).Concat(new int[] { 17, 31, 73, 47, 23 });
            return CalculateKnotHash(lengths);
        }
    }
}