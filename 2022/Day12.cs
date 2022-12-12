using System;
using System.Collections.Generic;
using AdventOfCode.Commons;
using MoreLinq;

namespace AdventOfCode._2022
{
    public partial class Day12 : AbstractDay
    {
        #region Init

        private static List<string> _input = new List<string>
#if TEST
        {
            "Sabqponm",
            "abcryxxl",
            "accszExk",
            "acctuvwj",
            "abdefghi",
        };
#else
            ();
#endif

        public Day12() : base(12)
        {
        }

        #endregion

        public void ExecuteDay()
        {
#if !TEST
            _input = Parser.Parse();
#endif
            int totalA = 0;
            int totalB = 0;

            var graph = _input.Select(x => x.Select(c => c switch
                {
                    'S' => 0,
                    'E' => 26,
                    _ => c - 'a'
                }
            ).ToList()).ToList();

            (int x, int y) start = _input.SelectMany(l => l).Select((c, i) => (c, i)).Where(e => e.c == 'S')
                .Select(e => (e.i % _input[0].Length, e.i / _input[0].Length)).Single();
            (int x, int y) end = _input.SelectMany(l => l).Select((c, i) => (c, i)).Where(e => e.c == 'E')
                .Select(e => (e.i % _input[0].Length, e.i / _input[0].Length)).Single();
            totalA = Helpers.BFS(graph, start, end);

            this.PartA = totalA;

            var possibleStarts = graph.SelectMany(l => l).Select((c, i) => (c, i)).Where(e => e.c == 0).Select(e => (e.i % _input[0].Length, e.i / _input[0].Length));

            var t = possibleStarts.Select(x => Helpers.BFS(graph, x, end)).ToList();
            totalB = possibleStarts.Select(x => Helpers.BFS(graph, x, end)).Where(x => x != 0).Min();
            this.PartB = totalB;
        }
    }
}