using AdventOfCode2017.Input;

namespace AdventOfCode2017.Days
{
    public class Day6 : AocDay<int[]>
    {
        private List<int[]>? hashesHistory;
        private int[]? lastHash;

        public Day6(IInputParser<int[]> inputParser) : base(inputParser)
        {
            if(inputParser is IntArrayParser p)
                p.Separator = "\t";
        }

        protected override void Part1(int[] input)
        {
            var data = new int[input.Length];
            Array.Copy(input, data, input.Length);
            var hashes = new List<int[]>();
            int steps = 0;
            while(!hashes.Any(d => Compare(d, data)))
            {
                hashes.Add(data);
                data = Divide(data);
                steps++;
            }
            hashesHistory = hashes;
            lastHash = data;
            Console.WriteLine(steps);
        }

        protected override void Part2(int[] input)
        {
            int lastInd = hashesHistory!.FindIndex(d => Compare(d, lastHash!));
            Console.WriteLine(hashesHistory.Count - lastInd);
        }

        private static bool Compare(int[] tab1, int[] tab2)
        {
            for(int i = 0; i < tab1.Length; i++)
                if(tab1[i] != tab2[i])
                    return false;
            return true;
        }

        private int[] Divide(int[] data)
        {
            int maxInd = 0;
            for(int i = 1; i < data.Length; i++)
            {
                if(data[i] > data[maxInd])
                    maxInd = i;
            }
            var tab = new int[data.Length];
            Array.Copy(data, tab, data.Length);
            var toDistribute = tab[maxInd];
            tab[maxInd] = 0;
            while(toDistribute > 0)
            {
                maxInd++;
                if(maxInd == tab.Length)
                    maxInd = 0;
                tab[maxInd]++;
                toDistribute--;
            }
            return tab;
        }
    }
}