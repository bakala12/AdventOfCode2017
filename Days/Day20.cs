using AdventOfCode2017.Input;
using AdventOfCode2017.Models;

namespace AdventOfCode2017.Days
{
    public class Day20 : AocDay<Particle[]>
    {
        public Day20(IInputParser<Particle[]> inputParser) : base(inputParser)
        {
        }

        protected override void Part1(Particle[] input)
        {
            Console.WriteLine(input.MinBy(p => GetPosition(p, 100000).ManhattanDistance).Id);
        }

        protected override void Part2(Particle[] input)
        {
            var destroyed = new bool[input.Length];
            var collisions = FindAllCollisions(input);
            VerifyCollisions(collisions);
            foreach(var colisionsInMoment in collisions.GroupBy(col => col.Time))
            {
                foreach(var col in colisionsInMoment.Where(c => !destroyed[c.P1.Id] && !destroyed[c.P2.Id]).ToList())
                {
                    destroyed[col.P1.Id] = destroyed[col.P2.Id] = true;
                }
            }
            Console.WriteLine(destroyed.Count(d => !d));
        }

        private static void VerifyCollisions(List<Collision> collisions)
        {
            foreach(var c in collisions)
            {
                if(GetPosition(c.P1, c.Time) != GetPosition(c.P2, c.Time))
                    throw new Exception($"Invalid collision {c.P1.Id} and {c.P2.Id} at {c.Time}");
            }
        }

        private static Position GetPosition(Particle p, long time)
        {
            return (time * (time+1) / 2) * p.Acceleration + time * p.Velocity + p.Position;
        }

        private readonly record struct Collision(Particle P1, Particle P2, long Time);

        private static List<Collision> FindAllCollisions(Particle[] input)
        {
            var collisions = new List<Collision>();
            for(int i = 0; i < input.Length; i++)
                for(int j = i+1; j < input.Length; j++)
                {
                    var time = WillCollide(input[i], input[j]);
                    if(time > 0)
                        collisions.Add(new Collision(input[i], input[j], time));
                }
            return collisions;
        }

        // p(t) = p(0) + (v(0)+0.5a)*t + 0.5at^2
        private static long WillCollide(Particle p1, Particle p2)
        {
            var a = p1.Acceleration - p2.Acceleration;
            var b = 2 * (p1.Velocity - p2.Velocity) + a; 
            var c = 2 * (p1.Position - p2.Position);
            return QuadraticEquationHelper.GetTimeSolution(a, b, c);
        }

        static class QuadraticEquationHelper
        {
            internal static (long, long)? GetSolutions(long a, long b, long c, out bool allSolutions)
            {
                allSolutions = false;
                if(a == 0)
                {
                    if(b != 0)
                    {
                        var t = AsIntegerSolution(-c / (double)b);
                        return (t > 0 ? t : -1, -1);
                    }
                    else 
                        allSolutions = true;
                    return null;
                }
                var delta = b * b - 4 * a * c;
                if(((long)delta) >= 0)
                {
                    var t1 = AsIntegerSolution((-b - Math.Sqrt(delta))/(2 * a));
                    var t2 = AsIntegerSolution((-b + Math.Sqrt(delta))/(2 * a));
                    return (t1, t2);
                }
                return null;
            }

            internal static long GetTimeSolution(Position a, Position b, Position c)
            {
                var xSol = GetSolutions(a.X, b.X, c.X, out bool allX);
                if(!allX && !xSol.HasValue) return -1;
                var ySol = GetSolutions(a.Y, b.Y, c.Y, out bool allY);
                if(!allY && !ySol.HasValue) return -1;
                var zSol = GetSolutions(a.Z, b.Z, c.Z, out bool allZ);
                if(!allZ && !zSol.HasValue) return -1;
                if(!allX && xSol.HasValue)
                {
                    var (t1, t2) = xSol.Value;
                    if(t1 > 0 && (allY || (ySol.HasValue && (ySol.Value.Item1 == t1 || ySol.Value.Item2 == t1))) &&
                        (allZ || (zSol.HasValue && (zSol.Value.Item1 == t1 || zSol.Value.Item2 == t1))))
                        return t1;
                    if(t2 > 0 && (allY || (ySol.HasValue && (ySol.Value.Item1 == t2 || ySol.Value.Item2 == t2))) &&
                        (allZ || (zSol.HasValue && (zSol.Value.Item1 == t2 || zSol.Value.Item2 == t2))))
                        return t2;
                }
                else if(!allY && ySol.HasValue)
                {
                    var (t1, t2) = ySol.Value;
                    if(t1 > 0 && 
                        (allZ || (zSol.HasValue && (zSol.Value.Item1 == t1 || zSol.Value.Item2 == t1))))
                        return t1;
                    if(t2 > 0 &&
                        (allZ || (zSol.HasValue && (zSol.Value.Item1 == t2 || zSol.Value.Item2 == t2))))
                        return t2;
                }
                else if(!allZ && zSol.HasValue)
                {
                    var (t1, t2) = zSol.Value;
                    if(t1 > 0 && t2 > 0)
                        return Math.Min(t1, t2);
                    else if(t1 > 0)
                        return t1;
                    else if(t2 > 0)
                        return t2;
                }
                return -1;
            }
        
            private static long AsIntegerSolution(double sol)
            {
                if((long)Math.Floor(sol) == (long)Math.Ceiling(sol))
                    return (long)Math.Floor(sol);
                return -1;
            }
        }
    }
}