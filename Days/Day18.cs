using System.Collections.Concurrent;
using AdventOfCode2017.Input;

namespace AdventOfCode2017.Days
{
    public class Day18 : AocDay<string[]>
    {
        public Day18(IInputParser<string[]> inputParser) : base(inputParser)
        {
        }

        protected override void Part1(string[] input)
        {
            var registers = new DefaultDictionary<char, long>();
            int pos = 0;
            long lastSound = -1;
            long GetValue(string arg) => long.TryParse(arg, out long val) ? val : registers![arg[0]];
            while(pos >= 0 && pos < input.Length)
            {
                var s = input[pos].Split();
                switch(s[0])
                {
                    case "snd":
                        lastSound = GetValue(s[1]);
                        break;
                    case "set":
                        registers[s[1][0]] = GetValue(s[2]);
                        break;
                    case "add":
                        registers[s[1][0]] += GetValue(s[2]);
                        break;
                    case "mul":
                        registers[s[1][0]] *= GetValue(s[2]);
                        break;
                    case "mod":
                        registers[s[1][0]] %= GetValue(s[2]);
                        break;
                    case "rcv":
                        if(GetValue(s[1]) != 0)
                        {
                            Console.WriteLine(lastSound);
                            return;
                        }
                        break;
                    case "jgz":
                        if(GetValue(s[1]) > 0)
                            pos += (int)GetValue(s[2])-1;
                        break;
                }
                pos++;
            }
        }

        protected override void Part2(string[] input)
        {
            var input1 = new BlockingCollection<long>();
            var input2 = new BlockingCollection<long>();
            var program1 = new ProgramExecutor(0, input, input1, input2);
            var program2 = new ProgramExecutor(1, input, input2, input1);
            var task1 = Task.Run(() => program1.Execute());
            var task2 = Task.Run(() => program2.Execute());
            var deadlockMonitor = Task.Run(() => 
            {
                while(input1.Any() || input2.Any() || !program1.IsWaiting || !program2.IsWaiting) 
                    Task.Delay(100);
            });
            var programs = Task.WhenAll(task1, task2);
            var main = Task.WhenAny(programs, deadlockMonitor);
            main.Wait();
            Console.WriteLine(program2.SendCount);
        }

        internal class DefaultDictionary<TKey, TValue> : Dictionary<TKey, TValue>
            where TKey : notnull
            where TValue : struct
        {
            public new TValue this[TKey key]
            {
                get => TryGetValue(key, out TValue value) ? value : default(TValue);
                set => base[key] = value;
            }
        }

        internal class ProgramExecutor
        {
            private readonly string[] _data;
            private readonly BlockingCollection<long> _input;
            private readonly BlockingCollection<long> _output;
            private readonly DefaultDictionary<char, long> _registers;

            public bool IsWaiting { get; private set; }
            public int SendCount { get; private set; } = 0;

            public ProgramExecutor(int programId, string[] data, BlockingCollection<long> input, BlockingCollection<long> output)
            {
                _data = data;
                _input = input;
                _output = output;
                _registers = new DefaultDictionary<char, long>();
                _registers['p'] = programId;
            }

            public void Execute()
            {
                int pos = 0;
                long GetValue(string arg) => long.TryParse(arg, out long val) ? val : _registers![arg[0]];
                while(pos >= 0 && pos < _data.Length)
                {
                    var s = _data[pos].Split();
                    switch(s[0])
                    {
                        case "snd":
                            _output.Add(GetValue(s[1]));
                            SendCount++;
                            break;
                        case "set":
                            _registers[s[1][0]] = GetValue(s[2]);
                            break;
                        case "add":
                            _registers[s[1][0]] += GetValue(s[2]);
                            break;
                        case "mul":
                            _registers[s[1][0]] *= GetValue(s[2]);
                            break;
                        case "mod":
                            _registers[s[1][0]] %= GetValue(s[2]);
                            break;
                        case "rcv":
                            IsWaiting = true;
                            _registers[s[1][0]] = _input.Take();
                            IsWaiting = false;
                            break;
                        case "jgz":
                            if(GetValue(s[1]) > 0)
                                pos += (int)GetValue(s[2])-1;
                            break;
                    }
                    pos++;
                }
            }
        }
    }
}