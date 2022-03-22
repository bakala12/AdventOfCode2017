using AdventOfCode2017.Models;

namespace AdventOfCode2017.Input
{
    public class ProgramTreeNodeInputParser : IInputParser<Dictionary<string, ProgramTreeNode>>
    {
        public Dictionary<string, ProgramTreeNode> ParseInput(string input)
        {
            var graph = new Dictionary<string, ProgramTreeNode>();
            foreach(var line in input.Split(Environment.NewLine))
            {
                var s = line.Split(new char[] { ' ', '(', ')', '-', '>', ','}, StringSplitOptions.RemoveEmptyEntries);
                var node = new ProgramTreeNode(s[0], int.Parse(s[1]), s.Skip(2).ToArray());
                graph[node.Name] = node;
            }
            return graph;
        }
    }
}