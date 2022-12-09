using System;
using System.Collections.Generic;
using AdventOfCode.Commons;

namespace AdventOfCode._{0}
{
    public partial class Day{1} : AbstractDay
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
            int totalA = 0;
            int totalB = 0;


            this.PartA = totalA;
            this.PartB = totalB;
        }

    }
}