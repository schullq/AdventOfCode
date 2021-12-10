using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using AdventOfCode.Commons;

namespace AdventOfCode._2021
{
    public class Day02 : AbstractDay
    {
        private static List<string> _input = new List<string>
#if TEST
        {
            "forward 5",
            "down 5",
            "forward 8",
            "up 3",
            "down 8",
            "forward 2"
        };
#else
            ();
#endif

        public Day02() : base(2)
        { }

        public int PhaseOne()
        {
#if !TEST
            _input = Parser.Parse();
#endif
            int h = 0;
            int v = 0;

            foreach (string[] r in _input.Select(i => i.Split(' ')))
            {
                switch (r[0])
                {
                    case "forward":
                        v += int.Parse(r[1]);
                        break;
                    case "down":
                        h += int.Parse(r[1]);
                        break;
                    case "up" when h - int.Parse(r[1]) < 0:
                        break;
                    case "up":
                        h -= int.Parse(r[1]);
                        break;
                }
            }

            return h * v;
        }

        public int PhaseTwo()
        {
#if !TEST
            _input = Parser.Parse();
#endif
            int h = 0;
            int v = 0;
            int aim = 0;

            foreach (string[] r in _input.Select(i => i.Split(' ')))
            {
                switch (r[0])
                {
                    case "forward":
                        {
                            h += int.Parse(r[1]);
                            if (aim != 0)
                                v += aim * int.Parse(r[1]);
                            break;
                        }
                    case "down":
                        aim += int.Parse(r[1]);
                        break;
                    case "up" when aim - int.Parse(r[1]) < 0:
                        break;
                    case "up":
                        aim -= int.Parse(r[1]);
                        break;
                }
            }

            return h * v;
        }

        public int PhaseOneLinq()
        {
#if !TEST
            _input = Parser.Parse();
#endif
            var regex = new Regex(@"(?<dir>forward|down|up) (?<n>\d+)");
            var (depth, hor, aim) = _input
                .Select(l => regex.Match(l))
                .Select(m => (
                    i: m.Groups["dir"].Value,
                    n: int.Parse(m.Groups["n"].Value)))
                .Aggregate((depth: 0, hor: 0, aim: 0), (p, m) =>
                    m.i switch
                    {
                        "forward" => (p.depth + (p.aim * m.n), p.hor + m.n, p.aim),
                        "down" => (p.depth, p.hor, p.aim + m.n),
                        "up" => (p.depth, p.hor, p.aim - m.n)
                    });

            return hor * aim;
        }

        public int PhaseTwoLinq()
        {
#if !TEST
            _input = Parser.Parse();
#endif
            var regex = new Regex(@"(?<dir>forward|down|up) (?<n>\d+)");
            var (depth, hor, aim) = _input
                .Select(l => regex.Match(l))
                .Select(m => (
                    i: m.Groups["dir"].Value,
                    n: int.Parse(m.Groups["n"].Value)))
                .Aggregate((depth: 0, hor: 0, aim: 0), (p, m) =>
                    m.i switch
                    {
                        "forward" => (p.depth + (p.aim * m.n), p.hor + m.n, p.aim),
                        "down" => (p.depth, p.hor, p.aim + m.n),
                        "up" => (p.depth, p.hor, p.aim - m.n)
                    });

            return hor * depth;
        }
    }
}
