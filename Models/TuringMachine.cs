namespace AdventOfCode2017.Models
{
    public enum TuringMachineMove { Left, Right }
    public readonly record struct TuringMachineAction(int Symbol, int Write, TuringMachineMove Move, string NextStateName);
    public readonly record struct TuringMachineState(string Name, TuringMachineAction ActionOn0, TuringMachineAction ActionOn1);
    public readonly record struct TuringMachine(string StartState, Dictionary<string, TuringMachineState> States);
}