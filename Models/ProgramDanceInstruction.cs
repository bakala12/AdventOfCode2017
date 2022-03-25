namespace AdventOfCode2017.Models
{
    public abstract class ProgramDanceInstruction
    {
        public abstract void Execute(char[] programs);
    }

    public class SpinDanceInstruction : ProgramDanceInstruction
    {
        private readonly int _value;

        public SpinDanceInstruction(int value)
        {
            _value = value;
        }

        public override void Execute(char[] programs)
        {
            char[] tmp = new char[_value];
            Array.Copy(programs, programs.Length-_value, tmp, 0, _value);
            Array.Copy(programs, 0, programs, _value, programs.Length-_value);
            Array.Copy(tmp, programs, _value);
        }
    }

    public class ExchangeDanceInstruction : ProgramDanceInstruction
    {
        private readonly int _from;
        private readonly int _to;

        public ExchangeDanceInstruction(int from, int to)
        {
            _from = from;
            _to = to;
        }

        public override void Execute(char[] programs)
        {
            char t = programs[_from];
            programs[_from] = programs[_to];
            programs[_to] = t;
        }
    }

    public class PartnerDanceInstruction : ProgramDanceInstruction
    {
        public readonly char _first;
        public readonly char _second;

        public PartnerDanceInstruction(char first, char second)
        {
            _first = first;
            _second = second; 
        }

        public override void Execute(char[] programs)
        {
            var fi = IndexOf(programs, _first);
            var si = IndexOf(programs, _second);
            char t = programs[fi];
            programs[fi] = programs[si];
            programs[si] = t;
        }

        private static int IndexOf<T>(T[] array, T item)
        {
            for(int i = 0; i < array.Length; i++)
                if(array[i]!.Equals(item))
                    return i;
            return -1;
        }
    }
}