using System;
using System.Collections.Generic;
using AdventOfCode.Commons;

namespace AdventOfCode._{0}
{
    public class Day{1} : AbstractDay
    {
        #region Init

        private static List<string> _input = new List<string>
#if TEST
        {

        };
#else
            ();
#endif

        public Day{1}() : base({1})
        { }

        #endregion

        public void ExecuteDay()
        {
#if !TEST
            _input = Parser.Parse();
#endif

            this.PartA = 0;

            this.PartB = 0;
        }

    }
}