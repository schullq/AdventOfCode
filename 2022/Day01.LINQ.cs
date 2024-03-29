using MoreLinq;

namespace AdventOfCode._2022
{
    public partial class Day01
    {
        public void ExecuteDayLinq()
        {
#if !TEST
            _input = Parser.Parse(keepEmptyLines: true);
#endif
            this.PartA = _input.Split(x => x == "")
                .Select(x => x
                    .Select(int.Parse)
                    .Sum())
                .Max();

            this.PartB = _input.Split(x => x == "")
                .Select(x => x
                    .Select(int.Parse)
                    .Sum())
                .PartialSort(3, OrderByDirection.Descending)
                .Sum();
        }
    }
}
