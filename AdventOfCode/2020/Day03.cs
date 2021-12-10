using System.Collections.Generic;
using AdventOfCode.Commons;

namespace AdventOfCode._2020
{
    public class Day03 : AbstractDay
    {
        #region Init

        private static List<string> _input = new List<string>
#if TEST
        {

        };
#else
            ();
#endif

        public Day03() : base(3, 2020)
        { }

        #endregion



        public void ExecuteDay()
        {
#if !TEST
            _input = Parser.Parse();
#endif



            PartA = 0;

            PartB = 0;
        }

    }
}