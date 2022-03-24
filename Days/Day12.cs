using AdventOfCode2017.Input;
using AdventOfCode2017.Models;

namespace AdventOfCode2017.Days
{
    public class Day12 : AocDay<ProgramGraphVertex[]>
    {
        public Day12(IInputParser<ProgramGraphVertex[]> inputParser) : base(inputParser)
        {
        }

        protected override void Part1(ProgramGraphVertex[] input)
        {
            bool[] visited = new bool[input.Length];
            visited[0] = true;
            var queue = new Queue<int>();
            queue.Enqueue(0);
            while(queue.Count > 0)
            {
                var v = queue.Dequeue();
                foreach(var nv in input[v].CommunicateWith)
                    if(!visited[nv])
                    {
                        queue.Enqueue(nv);
                        visited[nv] = true;
                    }
            }
            Console.WriteLine(visited.Count(v => v));
        }

        protected override void Part2(ProgramGraphVertex[] input)
        {
            bool[] visited = new bool[input.Length];
            var queue = new Queue<int>();
            int groupCount = 0;
            while(visited.Any(v => !v))
            {
                var first = 0;
                while(visited[first]) first++;
                queue.Enqueue(first);              
                while(queue.Count > 0)
                {
                    var v = queue.Dequeue();
                    foreach(var nv in input[v].CommunicateWith)
                        if(!visited[nv])
                        {
                            queue.Enqueue(nv);
                            visited[nv] = true;
                        }
                }
                groupCount++;
            }
            Console.WriteLine(groupCount);
        }
    }
}