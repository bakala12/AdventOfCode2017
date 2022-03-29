using AdventOfCode2017.Input;

namespace AdventOfCode2017.Days
{
    public class Day22 : AocDay<bool[,]>
    {
        public Day22(IInputParser<bool[,]> inputParser) : base(inputParser)
        {
        }

        protected override void Part1(bool[,] input)
        {
            var infectedNodes = new HashSet<(int,int)>();
            var middle = input.GetLength(0) / 2;
            for(int i = 0; i < input.GetLength(0); i++)
                for(int j = 0; j < input.GetLength(1); j++)
                    if(input[i,j])
                        infectedNodes.Add((i - middle, j - middle));
            var loc = (0,0);
            var dir = Direction.Up;
            int infections = 0;
            for(int burst = 0; burst < 10000; burst++)
            {
                var left = !infectedNodes.Contains(loc);
                dir = Turn(dir, left);
                if(left)
                {
                    infectedNodes.Add(loc);
                    infections++;
                }
                else 
                    infectedNodes.Remove(loc);
                loc = Move(loc, dir);
            }
            Console.WriteLine(infections);
        }

        protected override void Part2(bool[,] input)
        {
            var nodes = new Dictionary<(int,int), NodeState>(10000000);
            var middle = input.GetLength(0) / 2;
            for(int i = 0; i < input.GetLength(0); i++)
                for(int j = 0; j < input.GetLength(1); j++)
                    nodes.Add((i - middle, j - middle), input[i,j] ? NodeState.Infected : NodeState.Clean);
            var loc = (0,0);
            var dir = Direction.Up;
            int infections = 0;
            for(int burst = 0; burst < 10000000; burst++)
            {
                if(!nodes.TryGetValue(loc, out NodeState state))
                {
                    nodes.Add(loc, NodeState.Clean);
                    state = NodeState.Clean;
                }
                dir = Turn2(state, dir);
                var next = NextState(state);
                nodes[loc] = next;
                if(next == NodeState.Infected)
                    infections++;
                loc = Move(loc, dir);
            }
            Console.WriteLine(infections);
        }

        enum Direction { Down, Up, Left, Right }

        enum NodeState { Clean, Weakened, Infected, Flagged}

        private static (int,int) Move((int,int) pos, Direction d)
        {
            var (y,x) = pos;
            return d switch 
            {
                Direction.Up => (y-1, x),
                Direction.Down => (y+1, x),
                Direction.Left => (y, x-1),
                Direction.Right => (y, x+1),
                _ => pos
            };
        }

        private static Direction Turn(Direction dir, bool left)
        {
            return dir switch 
            {
                Direction.Up => left ? Direction.Left : Direction.Right,
                Direction.Down => left ? Direction.Right : Direction.Left,
                Direction.Left => left ? Direction.Down : Direction.Up,
                Direction.Right => left ? Direction.Up : Direction.Down,
                _ => throw new Exception()
            };
        }

        private static Direction Turn2(NodeState state, Direction dir)
        {
            return state switch
            {
                NodeState.Clean => Turn(dir, true),
                NodeState.Weakened => dir,
                NodeState.Infected => Turn(dir, false),
                NodeState.Flagged => OppositeDirection(dir),
                _ => throw new Exception()
            };
        }

        private static Direction OppositeDirection(Direction dir)
        {
            return dir switch
            {
                Direction.Up => Direction.Down,
                Direction.Down => Direction.Up,
                Direction.Left => Direction.Right,
                Direction.Right => Direction.Left,
                _ => throw new Exception()
            };
        }

        private static NodeState NextState(NodeState state) => (NodeState)(((int)state + 1) % 4);
    }
}