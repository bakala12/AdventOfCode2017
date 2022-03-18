namespace AdventOfCode2017.Input;

public interface IInputParser<TParsedInput>
{
    TParsedInput ParseInput(string input);
}
