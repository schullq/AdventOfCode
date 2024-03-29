using System;
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
            "..##.......",
            "#...#...#..",
            ".#....#..#.",
            "..#.#...#.#",
            ".#...##..#.",
            "..#.##.....",
            ".#.#.#....#",
            ".#........#",
            "#.##...#...",
            "#...##....#",
            ".#..#...#.#"
        };
#else
            ();
#endif

        public Day03() : base(03)
        { }

        #endregion

        public void ExecuteDay()
        {
#if !TEST
            _input = this.Parser.Parse();
#endif

            static long CountTrees((int x, int y) slope)
            {
                long count = 0;

                int x, y;
                x = y = 0;
                int width = _input[0].Length;

                while (y < _input.Count)
                {
                    count += _input[y][x % width] == '#' ? 1 : 0;
                    x += slope.x;
                    y += slope.y;
                }

                return count;
            }

            this.PartA = CountTrees((3, 1));

            this.PartB = CountTrees((1, 1)) *
                         CountTrees((3, 1)) *
                         CountTrees((5, 1)) *
                         CountTrees((7, 1)) *
                         CountTrees((1, 2));
        }
    }
}
