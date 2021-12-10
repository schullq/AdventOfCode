using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Commons;
using MathNet.Numerics.Statistics;

namespace AdventOfCode._2021
{
    public class Day07 : AbstractDay
    {

        #region Init

        private static List<int> _input = new List<int>
#if TEST
        {
            16,1,2,0,5,2,7,1,2,14
        };
#else
            ();
#endif

        public Day07() : base(7)
        { }

        #endregion



        public int PhaseOne()
        {
#if !TEST
            _input = Parser.ParseInt(',');
#endif
            int median = (int)Math.Round(_input.Select(x => (double)x).Median());
            int answer = 0;

            foreach (var crab in _input)
            {
                answer += Math.Abs(crab - median);
            }

            return answer;
        }

        public int PhaseTwo()
        {
#if !TEST
            if (_input == null || _input.Count == 0)
                _input = Parser.ParseInt(',');
#endif
            int median = (int)Math.Round(_input.Select(x => (double)x).Median());
            int average = _input.Average() < (double) median
                ? (int) Math.Round(_input.Average())
                : (int) Math.Truncate(_input.Average());
            
            int answer = 0;

            foreach (var crab in _input)
            {
                int max = Math.Abs(crab - average);
                int cost = 0;
                for (int i = 1; i <= max; i++)
                {
                    cost += i;
                }

                answer += cost;
            }

            return answer;
        }
    }
}