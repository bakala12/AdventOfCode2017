namespace AdventOfCode2017.Models
{
    public readonly record struct ImageTransformRule(int Size, bool[,] Pattern, bool[,] Result);
}