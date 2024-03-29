using System;
using System.Collections.Generic;
using AdventOfCode.Commons;

namespace AdventOfCode._2022
{
    public partial class Day02 : AbstractDay
    {
        #region Init

        private static List<string> _input = new List<string>
#if TEST
        {
            "A Y",
            "B X",
            "C Z"
        };
#else
            ();
#endif

        public Day02() : base(02)
        {
        }

        #endregion

        public void ExecuteDay()
        {
#if !TEST
            _input = Parser.Parse();
#endif
            Dictionary<char, Dictionary<char, int>> scoreGuideA = new Dictionary<char, Dictionary<char, int>>
            {
                { 'A', new Dictionary<char, int> { { 'X', 3 + 1 }, { 'Y', 6 + 2 }, { 'Z', 0 + 3 } } },
                { 'B', new Dictionary<char, int> { { 'X', 0 + 1 }, { 'Y', 3 + 2 }, { 'Z', 6 + 3 } } },
                { 'C', new Dictionary<char, int> { { 'X', 6 + 1 }, { 'Y', 0 + 2 }, { 'Z', 3 + 3 } } },
            };

            Dictionary<char, Dictionary<char, int>> scoreGuideB = new Dictionary<char, Dictionary<char, int>>
            {
                { 'A', new Dictionary<char, int> { { 'X', 0 + 3 }, { 'Y', 3 + 1 }, { 'Z', 6 + 2 } } },
                { 'B', new Dictionary<char, int> { { 'X', 0 + 1 }, { 'Y', 3 + 2 }, { 'Z', 6 + 3 } } },
                { 'C', new Dictionary<char, int> { { 'X', 0 + 2 }, { 'Y', 3 + 3 }, { 'Z', 6 + 1 } } },
            };

            int scoreA = 0;
            int scoreB = 0;

            foreach (var round in _input)
            {
                char a = round.Split(' ')[0].ToCharArray()[0];
                char b = round.Split(' ')[1].ToCharArray()[0];

                scoreA += scoreGuideA[a][b];
                scoreB += scoreGuideB[a][b];
            }

            this.PartA = scoreA;

            this.PartB = scoreB;
        }
    }
}