using System;
using System.Collections.Generic;
using AdventOfCode.Commons;

namespace AdventOfCode._2021
{
    public class Day10 : AbstractDay
    {
        #region Init

        private static List<string> _input = new List<string>
#if TEST
        {

        };
#else
            ();
#endif

        public Day10() : base(10)
        { }

        #endregion



        public void ExecuteDay()
        {
#if !TEST
            _input = this.Parser.Parse();
#endif

            this.PartA = 0;

            this.PartB = 0;
        }

    }
}
