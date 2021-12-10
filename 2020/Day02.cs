using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using AdventOfCode.Commons;

namespace AdventOfCode._2020
{
    public class Day02 : AbstractDay
    {
        #region Init

        private static List<string> _input = new List<string>
#if TEST
        {
"1-3 a: abcde",
"1-3 b: cdefg",
"2-9 c: ccccccccc"
        };
#else
            ();
#endif

        public Day02() : base(2, 2020)
        { }

        #endregion



        public void ExecuteDay()
        {
#if !TEST
            _input = Parser.Parse();
#endif
            var regex = new Regex(@"(?<from>\d+)-(?<to>\d+) (?<l>\w): (?<pass>\w+)");
            PartA = _input
                .Select(l => regex.Match(l))
                .Select(r => (
                    f: int.Parse(r.Groups["from"].Value),
                    t: int.Parse(r.Groups["to"].Value),
                    l: r.Groups["l"].Value.ToCharArray()[0],
                    p: r.Groups["pass"].Value)
                ).Count(x =>
                {
                    var count = x.p.ToCharArray().Count(y => y == x.l);
                    return count >= x.f && count <= x.t;
                });
            PartB = _input
                .Select(l => regex.Match(l))
                .Select(r => (
                    p1: int.Parse(r.Groups["from"].Value) - 1,
                    p2: int.Parse(r.Groups["to"].Value) - 1,
                    l: r.Groups["l"].Value.ToCharArray()[0],
                    p: r.Groups["pass"].Value.ToCharArray())
                ).Count(x => x.p[x.p1] == x.l ^ x.p[x.p2] == x.l);
        }
    }
}