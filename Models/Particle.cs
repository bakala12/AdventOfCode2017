namespace AdventOfCode2017.Models
{
    public readonly record struct Position(long X, long Y, long Z)
    {
        public long ManhattanDistance => Math.Abs(X) + Math.Abs(Y) + Math.Abs(Z);

        public static Position operator+(Position p1, Position p2) => new Position(p1.X + p2.X, p1.Y + p2.Y, p1.Z + p2.Z);
        public static Position operator-(Position p1, Position p2) => new Position(p1.X - p2.X, p1.Y - p2.Y, p1.Z - p2.Z);
        public static Position operator*(long k, Position p) => new Position(k * p.X, k * p.Y, k * p.Z);

    }

    public readonly record struct Particle(int Id, Position Position, Position Velocity, Position Acceleration);
}