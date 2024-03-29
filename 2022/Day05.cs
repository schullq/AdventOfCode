using AdventOfCode.Commons;

namespace AdventOfCode._2022
{
    public partial class Day05 : AbstractDay
    {
        #region Init

        private static List<string> _input = new List<string>
#if TEST
        {
            "    [D]    ",
            "[N] [C]    ",
            "[Z] [M] [P]",
            " 1   2   3 ",
            "",
            "move 1 from 2 to 1",
            "move 3 from 1 to 3",
            "move 2 from 2 to 1",
            "move 1 from 1 to 2",
            "",
        };
#else
            ();
#endif

        public new string PartA { get; set; }
        public new string PartB { get; set; }

        public Day05() : base(05)
        {
        }

        #endregion

        public void ExecuteDay()
        {
#if !TEST
            _input = Parser.Parse(keepEmptyLines: true);
#endif
            string totalA = "";
            string totalB = "";

            List<List<char>> stacksA = new List<List<char>>
            {
                new List<char>(),
                new List<char>(),
                new List<char>(),
#if !TEST
                new List<char>(),
                new List<char>(),
                new List<char>(),
                new List<char>(),
                new List<char>(),
                new List<char>()
#endif
            };

            for (int i = 0; _input[i + 1].Length != 0; i++)
            {
                for (int j = 1; j < _input[i].Length; j += 4)
                {
                    if (_input[i][j] != ' ') 
                        stacksA[j / 4].Insert(0, _input[i][j]);
                }
            }

            List<List<char>> stacksB = new List<List<char>>
            {
                new List<char>(stacksA[0]),
                new List<char>(stacksA[1]),
                new List<char>(stacksA[2]),
#if !TEST
                new List<char>(stacksA[3]),
                new List<char>(stacksA[4]),
                new List<char>(stacksA[5]),
                new List<char>(stacksA[6]),
                new List<char>(stacksA[7]),
                new List<char>(stacksA[8])
#endif
            };

#if !TEST
            List<string> instructions = _input.Skip(10).ToList();
#else
            List<string> instructions = _input.Skip(5).ToList();
#endif
            foreach (var instruction in instructions.SkipLast(1))
            {
                int nbr = int.Parse(instruction.Split(' ')[1]);
                int from = int.Parse(instruction.Split(' ')[3]);
                int to = int.Parse(instruction.Split(' ')[5]);

                while (nbr-- > 0)
                {
                    stacksA[to - 1].Add(stacksA[from - 1].Last());
                    stacksA[from - 1].RemoveAt(stacksA[from - 1].Count - 1);
                }
            }

            totalA = string.Join("", stacksA.Where(x => x.Count > 0).Select(x => x.Last()));

            this.PartA = totalA;

            foreach (var instruction in instructions.SkipLast(1))
            {
                int nbr = int.Parse(instruction.Split(' ')[1]);
                int from = int.Parse(instruction.Split(' ')[3]);
                int to = int.Parse(instruction.Split(' ')[5]);

                stacksB[to - 1].AddRange(stacksB[from - 1].TakeLast(nbr));
                stacksB[from - 1].RemoveRange(stacksB[from - 1].Count - nbr, nbr);
            }

            totalB = string.Join("", stacksB.Where(x => x.Count > 0).Select(x => x.Last()));

            this.PartB = totalB;
        }
    }
}