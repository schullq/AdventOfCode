using System;
using System.Collections.Generic;
using AdventOfCode.Commons;
using MoreLinq;

namespace AdventOfCode._2021
{
    public class Day14 : AbstractDay
    {
        #region Init

        private static List<string> _input = new List<string>
#if TEST
        {
            "NNCB",
            "CH -> B",
            "HH -> N",
            "CB -> H",
            "NH -> C",
            "HB -> C",
            "HC -> B",
            "HN -> C",
            "NN -> C",
            "BH -> H",
            "NC -> B",
            "NB -> B",
            "BN -> B",
            "BB -> N",
            "BC -> B",
            "CC -> N",
            "CN -> C"
        };
#else
            ();
#endif

        public Day14() : base(14)
        { }

        #endregion

        public void ExecuteDay()
        {
#if !TEST
            _input = this.Parser.Parse();
#endif
            string input = _input[0];
            _input.RemoveAt(0);

            Dictionary<string, string> rules = _input.Select(x => x.Split(" -> ")).ToDictionary(x => x[0], y => y[1]);

            Dictionary<string, long> pairs = new Dictionary<string, long>();

            foreach (var window in input.Window(2).Select(x => string.Join("", x)))
            {
                if (!pairs.ContainsKey(window))
                    pairs[window] = 0;
                pairs[window]++;
            }

            static long processPairs(int steps,
                string input,
                Dictionary<string, long> pairs,
                Dictionary<string, string> rules
            )
            {
                for (int i = 0; i < steps; i++)
                {
                    var pairsClone = new Dictionary<string, long>(pairs);
                    foreach (var pair in pairsClone)
                    {
                        pairs[pair.Key] -= pair.Value;
                        if (!pairs.ContainsKey(pair.Key[0] + rules[pair.Key]))
                            pairs[pair.Key[0] + rules[pair.Key]] = 0;
                        pairs[pair.Key[0] + rules[pair.Key]] += pair.Value;
                        if (!pairs.ContainsKey(rules[pair.Key] + pair.Key[1]))
                            pairs[rules[pair.Key] + pair.Key[1]] = 0;
                        pairs[rules[pair.Key] + pair.Key[1]] += pair.Value;
                    }
                }

                var charCount = new Dictionary<char, long> { [input[0]] = 1 };
                foreach (var pair in pairs)
                {
                    if (!charCount.ContainsKey(pair.Key[1]))
                        charCount[pair.Key[1]] = 0;
                    charCount[pair.Key[1]] += pair.Value;
                }

                return charCount.Values.Max() - charCount.Values.Min();
            }

            this.PartA = processPairs(10,
                input,
                new Dictionary<string, long>(pairs),
                rules
            );

            this.PartB = processPairs(40,
                input,
                pairs,
                rules
            );
        }
    }
}
