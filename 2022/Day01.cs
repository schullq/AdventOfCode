using AdventOfCode.Commons;

namespace AdventOfCode._2022
{
    public partial class Day01 : AbstractDay
    {
        #region Init

        private static List<string> _input = new List<string>
#if TEST
        {
            "1000",
            "2000",
            "3000",
            "",
            "4000",
            "",
            "5000",
            "6000",
            "",
            "7000",
            "8000",
            "9000",
            "",
            "10000",
        };
#else
            ();
#endif

        public Day01() : base(01)
        { }

        #endregion

        public void ExecuteDay()
        {
#if !TEST
            _input = Parser.Parse(keepEmptyLines: true);
#endif
            List<int> elves = new List<int>();
            int currentElf = 0;
            foreach (var food in _input)
            {
                if (string.IsNullOrWhiteSpace(food))
                {
                    elves.Add(currentElf);
                    currentElf = 0;
                }
                else
                    currentElf += int.Parse(food);
            }

            this.PartA = elves.Max();

            elves.Sort();
            this.PartB = elves.TakeLast(3).Sum();
        }
    }
}
