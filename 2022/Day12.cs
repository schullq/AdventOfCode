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

            this.PartA = Helpers.Bfs(
                start,
                p => p == end,
                p => p.GetCartesianNeighbors(graph)
                    .Where(n => graph[n.y][n.x] - graph[p.y][p.x] <= 1)
                    .Select(c => (c, 1)));

            var possibleStarts = graph.SelectMany(l => l).Select((c, i) => (c, i)).Where(e => e.c == 0)
                .Select(e => (e.i % _input[0].Length, e.i / _input[0].Length))
                .ToList();
            var reverseGraph = graph.Select(l => l.Select(e => 26 - e).ToList()).ToList();

            this.PartB = Helpers.Bfs(
                end,
                possibleStarts.Contains,
                p => p.GetCartesianNeighbors(reverseGraph)
                    .Where(n => reverseGraph[n.y][n.x] - reverseGraph[p.y][p.x] <= 1)
                    .Select(c => (c, 1)));
        }
    }
}