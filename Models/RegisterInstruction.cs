namespace AdventOfCode2017.Models
{
    public enum RegisterInstructionType
    {
        inc, dec
    }

    public readonly record struct RegisterInstructionCondition(string RegisterName, Func<int, int, bool> EvaluateFunction, int Argument)
    {
        public bool EvaluateCondition(Func<string, int> valueProvider) => EvaluateFunction(valueProvider(RegisterName), Argument);
    }

    public readonly record struct RegisterInstruction(string Register, RegisterInstructionType InstructionType, int By, RegisterInstructionCondition Condition)
    {
        public int ChangeValue => InstructionType == RegisterInstructionType.inc ? By : -By;
    }
}