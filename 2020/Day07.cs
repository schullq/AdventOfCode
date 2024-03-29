using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using AdventOfCode.Commons;

namespace AdventOfCode._2020
{
    public class Day07 : AbstractDay
    {
        #region Init

        private static List<string> _input = new List<string>
#if TEST
        {
            "light red bags contain 1 bright white bag, 2 muted yellow bags.",
            "dark orange bags contain 3 bright white bags, 4 muted yellow bags.",
            "bright white bags contain 1 shiny gold bag.",
            "muted yellow bags contain 2 shiny gold bags, 9 faded blue bags.",
            "shiny gold bags contain 1 dark olive bag, 2 vibrant plum bags.",
            "dark olive bags contain 3 faded blue bags, 4 dotted black bags.",
            "vibrant plum bags contain 5 faded blue bags, 6 dotted black bags.",
            "faded blue bags contain no other bags.",
            "dotted black bags contain no other bags."
        };
#else
            ();
#endif

        public Day07() : base(07)
        { }

        #endregion

        public void ExecuteDay()
        {
#if !TEST
            _input = this.Parser.Parse();
#endif
            Regex regex =
                new Regex(
                    @"^(?<bag>[\w ]+) bags contain (?:no other bags|((?<content>[\w ]+) bags?(?:, )?)+)\.$"
                );

            var rules = _input.Select(l => regex.Match(l))
               .ToDictionary(
                    m => m.Groups["bag"].Value, 
                    m => m.Groups["content"].Captures
                       .Select(c => Regex.Match(c.Value, @"^(\d+) (.*)$"))
                       .Select(m => (
                            size: int.Parse(m.Groups[1].Value), 
                            colour: m.Groups[2].Value))
                       .ToList());

            var reverse = rules.SelectMany(x => x.Value, (x, c) => (from: x.Key, c.colour))
               .ToLookup(x => x.colour, x => x.from);

            var visited = new HashSet<string>();
            void visitReverse(string color)
            {
                if (visited.Contains(color))
                    return;
                visited.Add(color);
                foreach (var c in reverse[color])
                    visitReverse(c);
            }

            visitReverse("shiny gold");
            this.PartA = visited.Count - 1;

            int CountBags(string colour)
            {
                return 1 + rules[colour].Sum(x => x.size * CountBags(x.colour));
            }

            this.PartB = CountBags("shiny gold") - 1;
        }
    }
}
