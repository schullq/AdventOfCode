using System;
using System.Collections.Generic;
using System.Xml.Linq;
using AdventOfCode.Commons;
using MoreLinq;

namespace AdventOfCode._2021
{
    public class Day15 : AbstractDay
    {
        #region Init

        private static List<string> _input = new List<string>
#if TEST
        {
"1163751742",
"1381373672",
"2136511328",
"3694931569",
"7463417111",
"1319128137",
"1359912421",
"3125421639",
"1293138521",
"2311944581"
        };
#else
            ();
#endif

        public Day15() : base(15)
        { }

        #endregion

        private List<List<int>> _caves;
        private List<(int x, int y)> _path;

        public void ExecuteDay()
        {
#if !TEST
            _input = this.Parser.Parse();
#endif
            this._caves = _input.Select(x => x.Select(y => (int)(y - '0')).ToList()).ToList();
            var destination = (x: this._caves.Count - 1, y: this._caves.Count - 1);

            var (_, _, risk) = Helpers.Dijkstra(
                (0, 0),
                p => p == destination,
                p => p.GetCartesianNeighbors(this._caves)
                   .Select(q => (q, this._caves[q.y][q.x]))
                );

            this.PartA = risk;

            destination = (x: this._caves.Count * 5 - 1, y: this._caves.Count * 5 - 1);

            int findRisk(int x, int y)
            {
                var increase = y / this._caves.Count + x / this._caves.Count;
                (x, y) = (x % this._caves.Count, y % this._caves.Count);
                return (((this._caves[y][x] - 1) + increase) % 9) + 1;
            }

            (_, _, risk) = Helpers.Dijkstra(
                (0, 0),
                p => p == destination,
                p => p.GetCartesianNeighbors()
                   .Where(q => q.y >= 0 && q.y <= destination.y
                                        && q.x >= 0 && q.x <= destination.x)
                   .Select(q => (q, findRisk(q.y, q.x))));

            this.PartB = risk;
        }
    }
}
