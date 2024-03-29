using MoreLinq;

namespace AdventOfCode._2022
{
    public partial class Day02
    {
        public void ExecuteDayLinq()
        {
#if !TEST
            _input = Parser.Parse();
#endif
            this.PartA = _input.Select(x => x.Split(' '))
                .Select(x => (x[1][0] - 'W') +
                             x switch
                             {
                                 ["A", "Z"] or ["B", "X"] or ["C", "Y"] => 0,
                                 ["A", "X"] or ["B", "Y"] or ["C", "Z"] => 3,
                                 ["A", "Y"] or ["B", "Z"] or ["C", "X"] => 6,
                             })
                .Sum();

            this.PartB = _input.Select(x => x.Split(' '))
                .Select(x => ((x[1][0] - 'X') * 3) +
                             x switch
                             {
                                 ["A", "X"] or ["B", "Z"] or ["C", "Y"] => 3,
                                 ["A", "Y"] or ["B", "X"] or ["C", "Z"] => 1,
                                 ["A", "Z"] or ["B", "Y"] or ["C", "X"] => 2,
                             })
                .Sum();
        }
    }
}
