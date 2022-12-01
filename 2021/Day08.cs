using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Commons;

namespace AdventOfCode._2021
{
    public class Day08 : AbstractDay
    {
        #region Init

        private static List<string> _input = new List<string>
#if TEST
        {
            "be cfbegad cbdgef fgaecd cgeb fdcge agebfd fecdb fabcd edb | fdgacbe cefdb cefbgd gcbe",
            "edbfga begcd cbg gc gcadebf fbgde acbgfd abcde gfcbed gfec | fcgedb cgb dgebacf gc",
            "fgaebd cg bdaec gdafb agbcfd gdcbef bgcad gfac gcb cdgabef | cg cg fdcagb cbg",
            "fbegcd cbd adcefb dageb afcb bc aefdc ecdab fgdeca fcdbega | efabcd cedba gadfec cb",
            "aecbfdg fbg gf bafeg dbefa fcge gcbea fcaegb dgceab fcbdga | gecf egdcabf bgf bfgea",
            "fgeab ca afcebg bdacfeg cfaedg gcfdb baec bfadeg bafgc acf | gebdcfa ecba ca fadegcb",
            "dbcfg fgd bdegcaf fgec aegbdf ecdfab fbedc dacgb gdcebf gf | cefg dcbef fcge gbcadfe",
            "bdfegc cbegaf gecbf dfcage bdacg ed bedf ced adcbefg gebcd | ed bcgafe cdgba cbgef",
            "egadfb cdbfeg cegd fecab cgb gbdefca cg fgcdab egfdb bfceg | gbdfcae bgc cg cgb",
            "gcafb gcf dcaebfg ecagb gf abcdeg gaef cafbge fdbac fegbdc | fgae cfgab fg bagce"
        };
#else
            ();
#endif

        public Day08() : base(8)
        {
        }

        #endregion

        private Dictionary<int, string> _digits = new Dictionary<int, string>
        {
            {0, "abcefg"},
            {1, "cf"},
            {2, "acdeg"},
            {3, "acdfg"},
            {4, "bcdf"},
            {5, "abdfg"},
            {6, "abdefg"},
            {7, "acf"},
            {8, "abcdefg"},
            {9, "abcdfg"}
        };

        private List<string> _correspondingDigits = new List<string>
        {
            {""},
            {""},
            {""},
            {""},
            {""},
            {""},
            {""},
            {""},
            {""},
            {""}
        };

        private List<List<string>> _digitsInput = new List<List<string>>();

        public int PhaseOne()
        {
#if !TEST
            _input = this.Parser.Parse();
#endif
            this.PrepareDigits();
            int answer = 0;

            List<int> uniqueDigits = this._digits.Where(x => this._digits.Values.Count(y => y.Length == x.Value.Length) == 1)
                .Select(x => x.Key)
                .ToList();
            var uniqueDigitsValueSize = this._digits.Where(x => uniqueDigits.Contains(x.Key))
                .ToDictionary(x => x.Value.Length, y => y.Key);

            foreach (var line in this._digitsInput)
            {
                var subline = line.GetRange(line.Count - 4, 4);
                foreach (var d in subline)
                {
                    if (uniqueDigitsValueSize.ContainsKey(d.Length))
                        answer++;
                }
            }

            return answer;
        }

        public int PhaseTwo()
        {
#if !TEST
            if (_input == null || _input.Count == 0)
                _input = this.Parser.Parse();
#endif
            this.PrepareDigits();

            return this._digitsInput.Sum(x => this.BruteForce(x.GetRange(0, 10), x.GetRange(10, 4)));
        }

        private int BruteForce(List<string> patterns, List<string> output)
        {

            var one = patterns.First(x => x.Length == 2);
            var four = patterns.First(x => x.Length == 4);
            var seven = patterns.First(x => x.Length == 3);
            var eight = patterns.First(x => x.Length == 7);
            var three = patterns.First(x => x.Length == 5 
                                            && one.All(x.Contains));
            var five = patterns.First(x => x.Length == 5 
                                           && four.Except(one).All(x.Contains));
            var two = patterns.First(x => x.Length == 5 
                                          && !one.All(x.Contains) 
                                          && !four.Except(one).All(x.Contains));
            var nine = patterns.First(x => x.Length == 6 
                                           && four.All(x.Contains) 
                                           && five.All(x.Contains));
            var zero = patterns.First(x => x.Length == 6 
                                           && !five.All(x.Contains) 
                                           && !four.All(x.Contains));
            var six = patterns.First(x => x.Length == 6 
                                          && five.All(x.Contains) 
                                          && !four.All(x.Contains));

            var cipher = new List<string> { zero, one, two, three, four, five, six, seven, eight, nine };
            var digits = output
                .Where(x => x.Length > 0)
                .Select(x => cipher.IndexOf(cipher.First(y => y.All(x.Contains) && x.Length == y.Length)))
                .ToList();

            return int.Parse(string.Join("", digits));
        }

        private void PrepareDigits()
        {
            foreach (var line in _input)
            {
                this._digitsInput.Add(line.Split(new[] {' ', '|'}, StringSplitOptions.RemoveEmptyEntries).ToList());
            }
        }
    }
}