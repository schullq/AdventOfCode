using System;
using System.Collections.Generic;
using AdventOfCode.Commons;
using MoreLinq;

namespace AdventOfCode._2022
{
    public partial class Day06 : AbstractDay
    {
        #region Init

        private static List<char> _input =
#if TEST

            "nppdvjthqldpwncqszvftbrmjlhg".ToCharArray().ToList();
#else
            new List<char>();
#endif

        public Day06() : base(06)
        { }

        #endregion

        public void ExecuteDay()
        {
#if !TEST
            _input = Parser.ParseCharList();
#endif
            int totalA = 4;
            int totalB = 14;

            foreach (var s in _input.Window(4))
            {
                if (s.Distinct().Count() != s.Count())
                    totalA++;
                else
                    break;
            }

            this.PartA = totalA;

            foreach (var s in _input.Window(14))
            {
                if (s.Distinct().Count() != s.Count())
                    totalB++;
                else
                    break;
            }

            this.PartB = totalB;
        }

    }
}
