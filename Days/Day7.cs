using AdventOfCode2017.Input;
using AdventOfCode2017.Models;

namespace AdventOfCode2017.Days
{
    public class Day7 : AocDay<Dictionary<string, ProgramTreeNode>>
    {
        private string? root;

        public Day7(IInputParser<Dictionary<string, ProgramTreeNode>> inputParser) : base(inputParser)
        {
        }

        protected override void Part1(Dictionary<string, ProgramTreeNode> input)
        {
            root = input.Single(s => !input.Any(ss => ss.Value.HoldPrograms.Contains(s.Key))).Key;
            Console.WriteLine(root);
        }

        protected override void Part2(Dictionary<string, ProgramTreeNode> input)
        {
            var subTowers = input.ToDictionary(s => s.Key, s => FindWeightOfSubTower(s.Key, input));
            var start = root!;
            int goodWeight = subTowers[start];
            int weightCorrected = -1;
            while(true)
            {
                var node = input[start];
                var neighboursCount = node.HoldPrograms.ToDictionary(p => p, p => node.HoldPrograms.Count(pp => subTowers[pp] == subTowers[p]));
                if(neighboursCount.Values.Min() > 1) //given node not balanced
                {
                    weightCorrected = node.Weight + goodWeight - subTowers[start];
                    break;
                }
                else //subnode not balanced
                {
                    start = neighboursCount.Single(n => n.Value == 1).Key;
                    goodWeight = subTowers[neighboursCount.MaxBy(v => v.Value).Key];
                }
            }
            Console.WriteLine(weightCorrected);
        }

        private static int FindWeightOfSubTower(string node, Dictionary<string, ProgramTreeNode> graph)
        {
            var n = graph[node];
            return n.Weight + n.HoldPrograms.Sum(p => FindWeightOfSubTower(p, graph));
        }
    }
}