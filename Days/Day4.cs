using AdventOfCode2017.Input;

namespace AdventOfCode2017.Days
{
    public class Day4 : AocDay<string[][]>
    {
        public Day4(IInputParser<string[][]> inputParser) : base(inputParser)
        {
        }

        protected override void Part1(string[][] input)
        {
            Console.WriteLine(input.Count(l => l.Length == l.Distinct().Count()));
        }

        protected override void Part2(string[][] input)
        {
            Console.WriteLine(input.Count(IsLineValid));
        }

        private static bool IsLineValid(string[] passphrase)
        {
            for(int i = 0; i < passphrase.Length; i++)
                for(int j = i+1; j < passphrase.Length; j++)
                    if(CanRearange(passphrase[i], passphrase[j]))
                        return false;
            return true;
        }

        private static bool CanRearange(string from, string into)
        {
            if(from.Length != into.Length)
                return false;
            bool[] used = new bool[into.Length];
            for(int i = 0; i < from.Length; i++)
            {
                int ind = -1;
                for(int j = 0; j < into.Length; j++)
                {
                    if(from[i] == into[j] && !used[j])
                    {
                        ind = j;
                        break;
                    }
                }
                if(ind < 0)
                    return false;
                used[ind] = true;                
            }
            return true;
        }
    }
}