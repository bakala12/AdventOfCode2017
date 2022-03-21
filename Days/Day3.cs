using AdventOfCode2017.Input;

namespace AdventOfCode2017.Days
{
    public class Day3 : AocDay<int>
    {
        public Day3(IInputParser<int> inputParser) : base(inputParser)
        {
        }

        protected override void Part1(int input)
        {
            int spiral = (int)Math.Ceiling(Math.Sqrt(input)) / 2;
            int spiralSize = 2*spiral;
            int maxNumber = (2*spiral+1)*(2*spiral+1);
            int v3 = maxNumber - spiralSize;
            int v2 = v3 - spiralSize;
            int v1 = v2 - spiralSize;
            int c;
            if(v1 >= input)
                c = v1 - spiral;
            else if(v2 >= input)
                c = v2 - spiral;
            else if(v3 >= input)
                c = v3 - spiral;
            else
                c = maxNumber - spiral;
            Console.WriteLine(Math.Abs(input - c) + spiral);            
        }

        protected override void Part2(int input)
        {
            Console.WriteLine(SquareValues().First(s => s > input));
        }

        private IEnumerable<int> SquareValues()
        {
            yield return 1;
            var list = new Dictionary<(int,int),int>() { { (0,0),1} };
            int spiral = 1;
            while(true)
            {
                foreach(var s in OnSpiral(spiral))
                {
                    int sum = 0;
                    foreach(var n in GetNeighbours(s))
                    {
                        if(list.TryGetValue(n, out int v))
                            sum += v;
                    }
                    list.Add(s, sum);
                    yield return sum;
                }
                spiral++;
            }
        }

        private IEnumerable<(int, int)> OnSpiral(int spiral)
        {
            int spiralSize = 2*spiral;
            int x = spiral, y = spiral-1;
            for(int c = 0; c < spiralSize - 1; c++)
                yield return (x, y--);
            for(int c = 0; c < spiralSize; c++)
                yield return (x--, y);
            for(int c = 0; c < spiralSize; c++)
                yield return (x, y++);
            for(int c = 0; c <= spiralSize; c++)
                yield return (x++, y);
        }

        private IEnumerable<(int,int)> GetNeighbours((int,int) s)
        {
            var (x,y) = s;
            for(int xx = -1; xx <= 1; xx++)
                for(int yy = -1; yy <= 1; yy++)
                    if(xx != 0 || yy != 0)
                        yield return (x+xx,y+yy);
        }
    }
}