using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using AdventOfCode.Commons;

namespace AdventOfCode._2021
{
    public class Day22 : AbstractDay
    {
        #region Init

        private static List<string> _input = new List<string>
#if TEST
        {
"on x=10..12,y=10..12,z=10..12",
"on x=11..13,y=11..13,z=11..13",
"off x=9..11,y=9..11,z=9..11",
"on x=10..10,y=10..10,z=10..10",
//"on x=-20..26,y=-36..17,z=-47..7",
//"on x=-20..33,y=-21..23,z=-26..28",
//"on x=-22..28,y=-29..23,z=-38..16",
//"on x=-46..7,y=-6..46,z=-50..-1",
//"on x=-49..1,y=-3..46,z=-24..28",
//"on x=2..47,y=-22..22,z=-23..27",
//"on x=-27..23,y=-28..26,z=-21..29",
//"on x=-39..5,y=-6..47,z=-3..44",
//"on x=-30..21,y=-8..43,z=-13..34",
//"on x=-22..26,y=-27..20,z=-29..19",
//"off x=-48..-32,y=26..41,z=-47..-37",
//"on x=-12..35,y=6..50,z=-50..-2",
//"off x=-48..-32,y=-32..-16,z=-15..-5",
//"on x=-18..26,y=-33..15,z=-7..46",
//"off x=-40..-22,y=-38..-28,z=23..41",
//"on x=-16..35,y=-41..10,z=-47..6",
//"off x=-32..-23,y=11..30,z=-14..3",
//"on x=-49..-5,y=-3..45,z=-29..18",
//"off x=18..30,y=-20..-8,z=-3..13",
//"on x=-41..9,y=-7..43,z=-33..15",
//"on x=-54112..-39298,y=-85059..-49293,z=-27449..7877",
//"on x=967..23432,y=45373..81175,z=27513..53682"
        };
#else
            ();
#endif

        public Day22() : base(22)
        { }

        #endregion

        private Dictionary<(long x, long y, long z), bool> _cubes = new Dictionary<(long x, long y, long z), bool>();

        public void ExecuteDay()
        {
#if !TEST
            _input = Parser.Parse();
#endif
            Regex regex = new Regex(@"(?<switch>on |off )x=(?<x>\-?\d+\.\.\-?\d+),y=(?<y>\-?\d+\.\.\-?\d+),z=(?<z>\-?\d+\.\.\-?\d+)");

            List<(bool s, List<long> x, List<long> y, List<long> z)> lines = _input.Select(l => regex.Match(l))
               .Select(m => (
                 s: m.Groups["switch"].Value.Trim() == "on" ? true : false, 
                 x: m.Groups["x"].Value.Split("..").Select(long.Parse).ToList(),
                 y: m.Groups["y"].Value.Split("..").Select(long.Parse).ToList(),
                 z: m.Groups["z"].Value.Split("..").Select(long.Parse).ToList()))
               .ToList();

            this.PartB = 0;
        }

        private void PartTwo(List<(bool s, List<long> x, List<long> y, List<long> z)> lines)
        {
            long count = 0;
            for (int i = 0; i < lines.Count; i++)
            {
                if (!lines[i].s)
                    continue;

            }
        }

        //private long CountRest((bool s, List<long> x, List<long> y, List<long> z) current,
        //    List<(bool s, List<long> x, List<long> y, List<long> z)> linesRest
        //)
        //{
        //    List<(bool s, List<long> x, List<long> y, List<long> z)> overlaped =
        //        new List<(bool s, List<long> x, List<long> y, List<long> z)>();

        //    foreach (var l in linesRest)
        //    {
        //        var cx = CutRange((l.x[0], l.x[1]), (int) current.x[0], (int) current.x[^1]);
        //        var cy = CutRange((l.y[0], l.y[1]), (int) current.y[0], (int) current.y[^1]);
        //        var cz = CutRange((l.z[0], l.z[1]), (int) current.z[0], (int) current.z[^1]);

        //        if (!cx.Any() || !cy.Any() || !cz.Any())
        //            continue;

        //        overlaped.Add((current.s), );
        //    }
        //}

        private void PartOne(List<(bool s, List<long> x, List<long> y, List<long> z)> lines)
        {
            foreach (var l in lines.Where(x => x.x[0] >= -50 && x.x[1] <= 50 && x.y[0] >= -50 && x.y[1] <= 50 && x.z[0] >= -50 && x.z[1] <= 50))
            {
                foreach (var x in CutRange((l.x[0], l.x[1]), -50, 50))
                    foreach (var y in CutRange((l.y[0], l.y[1]), -50, 50))
                        foreach (var z in CutRange((l.z[0], l.z[1]), -50, 50))
                        {
                            this._cubes[(x, y, z)] = l.s;
                        }

            }
            
            this.PartA = this._cubes.Values.Count(x => x);
        }

        private IEnumerable<int> CutRange((long a, long b) range,
            int min,
            int max
        )
        {
            if (range.b < min)
                return null;
            if (range.a > max)
                return null;

            range = (Math.Min(Math.Max(range.a, min), max), Math.Min(Math.Max(range.b, min), max));

            return Enumerable.Range((int) range.a, (int) (range.b - range.a) + 1);
        }
    }
}
