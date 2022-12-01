using System;
using System.Collections.Generic;
using System.Xml.Linq;
using AdventOfCode.Commons;

namespace AdventOfCode._2021
{
    public class Day25 : AbstractDay
    {
        #region Init

        private static List<char[]> _input = new List<char[]>
#if TEST
        {
            "v...>>.vv>".ToCharArray(),
            ".vv>>.vv..".ToCharArray(),
            ">>.>v>...v".ToCharArray(),
            ">>v>>.>.v.".ToCharArray(),
            "v>v.vv.v..".ToCharArray(),
            ">.>>..v...".ToCharArray(),
            ".vv..>.>v.".ToCharArray(),
            "v.v..>>v.v".ToCharArray(),
            "....v..v.>".ToCharArray()
        };
#else
            ();
#endif

        public Day25() : base(25)
        { }

        #endregion

        public void ExecuteDay()
        {
#if !TEST
            _input = Parser.ParseCharArray();
#endif

            static void Swap((int x, int y) a, (int x, int y) b)
            {
                (_input[a.y][a.x], _input[b.y][b.x]) = (_input[b.y][b.x], _input[a.y][a.x]);
            }

            for (int y = 0; y < _input.Count; y++)
            {
                for (int t = 0; t < 2; t++)
                {
                    for (int x = 0; x < _input[y].Length; x++)
                    {
                        if (t == 0 && _input[y][x] == '>' && x < _input[y].Length - 1 && _input[y][x + 1] == '.')
                            Swap((x, y), (x + 1, y));
                        else if (t == 1 && _input[y][x] == 'V' && y < _input.Count - 1 && _input[y + 1][x] == '.')
                            Swap((x, y), (x, y + 1));
                    }
                }
            }

            this.PartA = 0;
            this.PartB = 0;
        }

    }
}
