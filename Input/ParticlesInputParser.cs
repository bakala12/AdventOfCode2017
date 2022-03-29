using AdventOfCode2017.Models;

namespace AdventOfCode2017.Input
{
    public class ParticlesInputParser : IInputParser<Particle[]>
    {
        public Particle[] ParseInput(string input)
        {
            var lines = input.Split(Environment.NewLine);
            var particles = new Particle[lines.Length];
            for(int i = 0; i < lines.Length; i++)
            {
                var s = lines[i].Split(", ");
                particles[i] = new Particle(i, ParsePosition(s[0]), ParsePosition(s[1]), ParsePosition(s[2]));
            }
            return particles;
        }

        private Position ParsePosition(string str)
        {
            var s = str.Split(new char[] {'=', '<', ',','>'}, StringSplitOptions.RemoveEmptyEntries);
            return new Position(int.Parse(s[1]), int.Parse(s[2]), int.Parse(s[3]));
        }
    }
}