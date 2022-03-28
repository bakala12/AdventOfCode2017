using System.Text;
using AdventOfCode2017.Input;

namespace AdventOfCode2017.Days
{
    public class Day19 : AocDay<string[]>
    {
        public Day19(IInputParser<string[]> inputParser) : base(inputParser)
        {
        }

        private int _steps;

        protected override void Part1(string[] input)
        {
            var (y,x) = (0, input[0].IndexOf('|'));
            var currentDirection = Direction.Down;
            var sb = new StringBuilder();
            int steps = 0;
            while(y >= 0 && y < input.Length && x >= 0 && x < input[y].Length && input[y][x] != ' ')
            {
                if(input[y][x] == '+')
                    currentDirection = Turn((y,x), input, currentDirection);
                else if(char.IsLetter(input[y][x]))
                    sb.Append(input[y][x]);
                (y,x) = Move((y,x), currentDirection);
                steps++;
            }
            Console.WriteLine(sb.ToString());
            _steps = steps;
        }

        protected override void Part2(string[] input)
        {
            Console.WriteLine(_steps);
        }

        enum Direction { Down, Up, Left, Right }

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
    
        private static Direction Turn((int, int) pos, string[] input, Direction currentDirection)
        {
            try {
            return Enum.GetValues(typeof(Direction)).OfType<Direction>().Where(d => OpositeDirections(d, currentDirection)).Single(d => {
                var (yy,xx) = Move(pos, d);
                return yy >= 0 && yy < input.Length && xx >= 0 && xx < input[yy].Length && (
                    ((d == Direction.Up || d == Direction.Down) && (input[yy][xx] == '|' || char.IsLetter(input[yy][xx]))) ||
                    ((d == Direction.Left || d == Direction.Right) && (input[yy][xx] == '-' || char.IsLetter(input[yy][xx])))
                );
            });
            } catch(Exception) { Console.WriteLine($"at {pos}"); throw; }
        }

        private static bool OpositeDirections(Direction d1, Direction d2)
        {
            return d1 switch 
            {
                Direction.Up => d2 == Direction.Left || d2 == Direction.Right,
                Direction.Down => d2 == Direction.Left || d2 == Direction.Right,
                Direction.Left => d2 == Direction.Up || d2 == Direction.Down,
                Direction.Right => d2 == Direction.Up || d2 == Direction.Down,
                _ => false
            };
        }
    }
}