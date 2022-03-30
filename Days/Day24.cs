using AdventOfCode2017.Input;
using AdventOfCode2017.Models;

namespace AdventOfCode2017.Days
{
    public class Day24 : AocDay<MagneticComponent[]>
    {
        public Day24(IInputParser<MagneticComponent[]> inputParser) : base(inputParser)
        {
        }

        protected override void Part1(MagneticComponent[] input)
        {
            Console.WriteLine(FindStrongestBridge(input, new bool[input.Length], 0));
        }

        protected override void Part2(MagneticComponent[] input)
        {
            Console.WriteLine(FindLongestStrongestBridge(input, new bool[input.Length], 0, 0).Item1);
        }

        private static long FindStrongestBridge(MagneticComponent[] components, bool[] taken, int endWith)
        {
            bool anyMatches = false;
            long max = 0;
            for(int i = 0; i < components.Length; i++)
            {
                if(!taken[i])
                {
                    taken[i] = true;
                    if(endWith == components[i].Port1)
                    {
                        anyMatches = true;
                        max = Math.Max(max, FindStrongestBridge(components, taken, components[i].Port2));
                    }
                    if(endWith == components[i].Port2)
                    {
                        anyMatches = true;
                        max = Math.Max(max, FindStrongestBridge(components, taken, components[i].Port1));
                    }
                    taken[i] = false;
                }
            }
            if(!anyMatches)
                return taken.Select((s,i) => s ? components[i].Port1 + components[i].Port2 : 0).Sum();
            return max;
        }

        private static (long, int) FindLongestStrongestBridge(MagneticComponent[] components, bool[] taken, int endWith, int count)
        {
            bool anyMatches = false;
            long max = 0;
            int longest = 0;
            for(int i = 0; i < components.Length; i++)
            {
                if(!taken[i])
                {
                    taken[i] = true;
                    if(endWith == components[i].Port1)
                    {
                        anyMatches = true;
                        var (m,c) = FindLongestStrongestBridge(components, taken, components[i].Port2, count+1);
                        if(c > longest)
                        {
                            longest = c;
                            max = m;
                        }
                        else if(c == longest && max < m)
                        {
                            max = m;
                        }
                    }
                    if(endWith == components[i].Port2)
                    {
                        anyMatches = true;
                        var (m,c) = FindLongestStrongestBridge(components, taken, components[i].Port1, count+1);
                        if(c > longest)
                        {
                            longest = c;
                            max = m;
                        }
                        else if(c == longest && max < m)
                        {
                            max = m;
                        }
                    }
                    taken[i] = false;
                }
            }
            if(!anyMatches)
                return (taken.Select((s,i) => s ? components[i].Port1 + components[i].Port2 : 0).Sum(), count);
            return (max, longest);
        }
    }
}