using MoreLinq;

namespace AdventOfCode._2022
{
    public partial class Day03
    {
        public void ExecuteDayLinq()
        {
#if !TEST
            _input = Parser.Parse();
#endif
            this.PartA = _input.Select(x => x
                    .Substring(0, x.Length / 2)
                    .Intersect(x.Substring(x.Length / 2, x.Length / 2))
                    .Select(y => y switch
                    {
                        >= 'a' and <= 'z' => y - 'a' + 1,
                        >= 'A' and <= 'Z' => y - 'A' + 27,
                    })
                    .First())
                .Sum();

            this.PartB = _input
                .Chunk(3)
                .Select(x => x[0]
                    .Intersect(x[1])
                    .Intersect(x[2])
                    .Select(y => y switch
                    {
                        >= 'a' and <= 'z' => y - 'a' + 1,
                        >= 'A' and <= 'Z' => y - 'A' + 27,
                    })
                    .First())
                .Sum();
        }
    }
}