using System;
using System.Collections.Generic;
using AdventOfCode.Commons;

namespace AdventOfCode._2022
{
    public partial class Day12 : AbstractDay
    {
        #region Init

        private static List<string> _input = new List<string>
#if TEST
        {

        };
#else
            ();
#endif

        public Day12() : base(12)
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
