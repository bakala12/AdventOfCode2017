using AdventOfCode2017.Helpers;
using AdventOfCode2017.Input;

namespace AdventOfCode2017.Days
{
    public class Day14 : AocDay<string>
    {
        public Day14(IInputParser<string> inputParser) : base(inputParser)
        {
        }

        protected override void Part1(string input)
        {
            int c = 0;
            for(int i = 0; i < 128; i++)
                c += BitCount(KnotHashHelper.CalculateKnotHash($"{input}-{i}"));
            Console.WriteLine(c);
        }

        protected override void Part2(string input)
        {
            bool[,] grid = new bool[128,128];
            for(int i = 0; i < 128; i++)
            {
                var hash = KnotHashHelper.CalculateKnotHash($"{input}-{i}");
                for(int j = 0; j < 128; j++)
                    grid[i,j] = BitSet(j, hash);
            }
            Console.WriteLine(FindRegions(grid));
        }

        private static int BitCount(byte[] hash)
        {
            return Enumerable.Range(0,128).Count(i => BitSet(i, hash)); 
        }

        private static bool BitSet(int ind, byte[] hash)
        {
            return ((1 << (7 - ind % 8)) & hash[ind/8]) != 0;
        }

        private static int FindRegions(bool[,] grid)
        {
            bool[,] visited = new bool[128,128];
            int regions = 0;
            for(int i = 0; i < 128; i++)
                for(int j = 0; j < 128; j++)
                    if(!visited[i,j] && grid[i,j])
                    {
                        var queue = new Queue<(int,int)>();
                        queue.Enqueue((i,j));
                        visited[i,j] = true;
                        while(queue.Count > 0)
                        {
                            var (ii,jj) = queue.Dequeue();
                            foreach(var (ni, nj) in GetNeighbours(ii, jj))
                            {
                                if(ni >= 0 && ni < 128 && nj >= 0 && nj < 128 && grid[ni,nj] && !visited[ni,nj])
                                {
                                    visited[ni,nj] = true;
                                    queue.Enqueue((ni,nj));
                                }
                            }
                        }
                        regions++;
                    }
                
            return regions;
        }

        private static IEnumerable<(int,int)> GetNeighbours(int i, int j)
        {
            yield return (i-1,j);
            yield return (i+1,j);
            yield return (i,j-1);
            yield return (i,j+1);
        }
    }
}