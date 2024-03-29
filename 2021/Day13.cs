using System;
using System.Collections.Generic;
using System.Drawing;
using AdventOfCode.Commons;
using MoreLinq;

namespace AdventOfCode._2021
{
    public class Day13 : AbstractDay
    {
        #region Init

        private static List<string> _input = new List<string>
#if TEST
        {
            "6,10",
            "0,14",
            "9,10",
            "0,3",
            "10,4",
            "4,11",
            "6,0",
            "6,12",
            "4,1",
            "0,13",
            "10,12",
            "3,4",
            "3,0",
            "8,4",
            "1,10",
            "2,14",
            "8,10",
            "9,0",
            "fold along y=7",
            "fold along x=5",
        };
#else
            ();
#endif

        public Day13() : base(13)
        { }

        #endregion

        public void ExecuteDay()
        {
#if !TEST
            _input = this.Parser.Parse();
#endif
            var dots = _input.Where(x => !x.StartsWith("fold"))
               .Select(x => (x.Split(',')))
               .Select(x => (int.Parse(x[0]), int.Parse(x[1])))
               .ToList();

            var folds = _input.Where(x => x.StartsWith("fold"))
               .Select(x => x.Split(' ')[2].Split('='))
               .Select(x => (dir: x[0], pos: int.Parse(x[1])))
               .ToList();

            static List<(int x, int y)> Fold(List<(int x, int y)> dots,
                string dir,
                int pos
            )
            {
                return dir == "x"
                    ? dots.Select(d => (x: pos - Math.Abs(d.x - pos), y: d.y))
                       .Distinct()
                       .ToList()
                    : dots.Select(d => (x: d.x, y: pos - Math.Abs(d.y - pos)))
                       .Distinct()
                       .ToList();
            }

            this.PartA = Fold(dots, folds[0].dir, folds[0].pos).Count;

            foreach (var (dir, pos) in folds)
                dots = Fold(dots, dir, pos);

            for (int y = 0; y < dots.Max(e => e.Item2) + 1; y++)
            {
                for (int x = 0; x < dots.Max(e => e.Item1); x++)
                {
                    Console.Write(dots.Contains((x, y)) ? "#" : ".");
                }
                Console.WriteLine();
            }
        }
    }
}
