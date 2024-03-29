using System;
using System.Collections.Generic;
using AdventOfCode.Commons;

namespace AdventOfCode._2020
{
    public class Day08 : AbstractDay
    {
        #region Init

        private static List<string> _input = new List<string>
#if TEST
        {
"nop +0",
"acc +1",
"jmp +4",
"acc +3",
"jmp -3",
"acc -99",
"acc +1",
"jmp -4",
"acc +6"
        };
#else
            ();
#endif

        public Day08() : base(08)
        { }

        #endregion

        public void ExecuteDay()
        {
#if !TEST
            _input = Parser.Parse();
#endif
            var instructions = _input.Select(l => l.Split(' '))
               .Select(l => (type: l[0], value: int.Parse(l[1])))
               .ToList();
               
            this.PartA = Run(instructions).value;


            for (int i = 0; i < instructions.Count; i++)
            {
                if (instructions[i].type == "acc")
                    continue;

                var save = instructions[i];
                instructions[i] = save.type == "nop"
                    ? ("jmp", save.value)
                    : ("nop", save.value);

                var ret = Run(instructions);

                if (!ret.Item1)
                {
                    this.PartB = ret.value;
                    return;
                }
                else
                {
                    instructions[i] = save;
                }

            }
        }

        private (bool, int value) Run(List<(string type, int value)> instructions)
        {
            List<int> visited = new List<int>();

            int i = 0, accumulator = 0;
            do
            {
                if (visited.Contains(i))
                    return (true, accumulator);
                visited.Add(i);
                switch (instructions[i].type)
                {
                    case "jmp":
                        i += instructions[i].value;
                        break;
                    case "acc":
                        accumulator += instructions[i].value;
                        i++;
                        break;
                    case "nop":
                    default:
                        i++;
                        break;
                }
            } while (i < instructions.Count && visited.Count != instructions.Count);

            return (false, accumulator); ;
        }
    }
}
