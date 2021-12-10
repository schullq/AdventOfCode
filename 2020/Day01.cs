using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Commons;
using MoreLinq;

namespace AdventOfCode._2020
{
    public class Day01 : AbstractDay
    {
        #region Init

        private static List<string> _input = new List<string>
#if TEST
        {
            "1721",
            "979",
            "366",
            "299",
            "675",
            "1456"
        };
#else
            ();
#endif

        public Day01() : base(1, 2020)
        {
        }

        #endregion


        public int PhaseOne()
        {
#if !TEST
            _input = Parser.Parse();
#endif
            return _input
                .Select(int.Parse)
                .Subsets(2)
                .First(n => n.Sum() == 2020)
                .Aggregate((x, y) => x * y);
        }

        public int PhaseTwo()
        {
#if !TEST
            if (_input == null || _input.Count == 0)
                _input = Parser.Parse();
#endif
            return _input
                .Select(int.Parse)
                .Subsets(3)
                .First(n => n.Sum() == 2020)
                .ToList()
                .Aggregate((x, y) => x * y);
        }
    }
}