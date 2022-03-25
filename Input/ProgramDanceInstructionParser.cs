using AdventOfCode2017.Models;

namespace AdventOfCode2017.Input
{
    public class ProgramDanceInstructionParser : IInputParser<List<ProgramDanceInstruction>>
    {
        public List<ProgramDanceInstruction> ParseInput(string input)
        {
            var list = new List<ProgramDanceInstruction>();
            foreach(var line in input.Split(','))
            {
                ProgramDanceInstruction? instr = null;
                switch(line[0])
                {
                    case 's':
                        instr = new SpinDanceInstruction(int.Parse(line.Substring(1)));
                        break;
                    case 'x':
                        var s = line.Substring(1).Split('/');
                        instr = new ExchangeDanceInstruction(int.Parse(s[0]), int.Parse(s[1]));
                        break;
                    case 'p':
                        var s1 = line.Substring(1).Split('/');
                        instr = new PartnerDanceInstruction(s1[0][0], s1[1][0]);
                        break;
                }
                if(instr != null)
                    list.Add(instr);
            }
            return list;
        }
    }
}