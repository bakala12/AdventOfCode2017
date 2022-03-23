using AdventOfCode2017.Input;
using AdventOfCode2017.Models;

namespace AdventOfCode2017.Days
{
    public class Day11 : AocDay<HexDirection[]>
    {
        public Day11(IInputParser<HexDirection[]> inputParser) : base(inputParser)
        {
        }

        protected override void Part1(HexDirection[] input)
        {
            var location = FindLocation(input, new List<(int, int)>());
            Console.WriteLine(FindDistance(location));
        }

        protected override void Part2(HexDirection[] input)
        {
            var locations = new List<(int, int)>();
            FindLocation(input, locations);
            Console.WriteLine(locations.Max(l => FindDistance(l)));
        }

        private static (int,int) FindLocation(HexDirection[] input, List<(int,int)>locations)
        {
            var (x,y) = (0,0);
            locations.Add((0,0));
            foreach(var hexDir in input)
            {
                switch(hexDir)
                {
                    case HexDirection.n:
                        y-=2;
                        break;
                    case HexDirection.ne:
                        x++;
                        y--;
                        break;
                    case HexDirection.nw:
                        x--;
                        y--;
                        break;
                    case HexDirection.s:
                        y+=2;
                        break;
                    case HexDirection.se:
                        x++;
                        y++;
                        break;
                    case HexDirection.sw:
                        x--;
                        y++;
                        break;
                }
                locations.Add((x,y));
            }
            return (x,y);
        }
    
        private static int FindDistance((int,int) loc)
        {
            var (x,y) = loc;
            return Math.Abs(Math.Abs(y)-Math.Abs(x)) / 2 + Math.Abs(x);
        }
    }
}