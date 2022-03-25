using AdventOfCode2017.Input;
using AdventOfCode2017.Models;

namespace AdventOfCode2017.Days
{
    public class Day16 : AocDay<List<ProgramDanceInstruction>>
    {
        public Day16(IInputParser<List<ProgramDanceInstruction>> inputParser) : base(inputParser)
        {
        }

        protected override void Part1(List<ProgramDanceInstruction> input)
        {
            var tab = Enumerable.Range((int)'a', 16).Select(p => (char)p).ToArray();
            foreach(var instr in input)
                instr.Execute(tab);
            foreach(var t in tab)
                Console.Write(t);
            Console.WriteLine();
        }

        protected override void Part2(List<ProgramDanceInstruction> input)
        {
            var iterations = 1_000_000_000;
            var tab = Enumerable.Range((int)'a', 16).Select(p => (char)p).ToArray();
            int cycleIteration = -1;
            for(int i = 0; i < iterations; i++)
            {
                foreach(var instr in input)
                {
                    instr.Execute(tab);
                }
                if(tab[0] == 'a' && IsStartPoint(tab))
                {
                    cycleIteration = i;
                    break;
                }
            }
            for(int i = 0; i < iterations % (1+cycleIteration); i++)
                foreach(var instr in input)
                    instr.Execute(tab);
            foreach(var t in tab)
                Console.Write(t);
            Console.WriteLine();
        }

        private static bool IsStartPoint(char[] tab)
        {
            for(int i = 1; i < tab.Length; i++)
                if(tab[i] < tab[i-1])
                    return false;
            return true;
        }
    }
}