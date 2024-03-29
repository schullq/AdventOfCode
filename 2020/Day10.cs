using System;
using System.Collections;
using AdventOfCode.Commons;

namespace AdventOfCode._2020
{
    public partial class Day10 : AbstractDay
    {
        #region Init

        private static List<int> _input = new List<int>
#if TEST
        {
            28,
            33,
            18,
            42,
            31,
            14,
            46,
            20,
            48,
            47,
            24,
            23,
            49,
            45,
            19,
            38,
            39,
            11,
            1,
            32,
            25,
            35,
            8,
            17,
            7,
            9,
            4,
            2,
            34,
            10,
            3,
        };
#else
            ();
#endif

        public Day10() : base(10)
        {
        }

        #endregion

        public void ExecuteDay()
        {
#if !TEST
            _input = Parser.ParseInt();
#endif

            _input.Add(0);
            _input.Sort();

            Dictionary<int, int> totals = new Dictionary<int, int>
            {
                { 1, 0 }, { 2, 0 }, { 3, 1 }
            };
            for (int i = 0; i < _input.Count - 1; i++)
            {
                totals[Math.Abs(_input[i + 1] - _input[i])]++;
            }

            this.PartA = totals[1] * totals[3];

            _input.Reverse();

            var totalRoutes = new Dictionary<int, long> { { _input.Max() + 3, 1 } };
            foreach (var a in _input)
            {
                totalRoutes.TryGetValue(a + 1, out long x);
                totalRoutes.TryGetValue(a + 2, out long y);
                totalRoutes.TryGetValue(a + 3, out long z);
                totalRoutes[a] = x + y + z;
            }

            this.PartB = totalRoutes[0];
        }
    }
}