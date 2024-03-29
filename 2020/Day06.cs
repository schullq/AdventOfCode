using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using AdventOfCode.Commons;
using MoreLinq;

namespace AdventOfCode._2020
{
    public class Day06 : AbstractDay
    {
        #region Init

        private static List<string> _input = new List<string>
#if TEST
            { };
#else
            ();
#endif

        public Day06() : base(06)
        { }

        #endregion

        public void ExecuteDay()
        {
#if !TEST
            _input = this.Parser.Parse();
#endif

            var groups = Regex.Split(this.Parser.Input, "\n\n")
               .Select(x => (answers: x.Replace("\n", ""), people: x.Count(c => c == '\n') + 1))
               .ToList();

            this.PartA = groups.Select(x => x.answers.Distinct()).Sum(x => x.Count());

            this.PartB = groups.Select(x => x.answers
                   .Select(c => c)
                   .GroupBy(y => y, (y, _) => _.Count())
                   .Where(y => y == x.people)
                )
               .Sum(x => x.Count());
        }
    }
}
