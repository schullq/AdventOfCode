using System;
using System.Collections.Generic;
using AdventOfCode.Commons;

namespace AdventOfCode._2022
{
    public partial class Day03 : AbstractDay
    {
        #region Init

        private static List<string> _input = new List<string>
#if TEST
        {
            "vJrwpWtwJgWrhcsFMMfFFhFp",
            "jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL",
            "PmmdzqPrVvPwwTWBwg",
            "wMqvLMZHhHMvwLHjbvcjnnSBnvTQFn",
            "ttgJtRGJQctTZtZT",
            "CrZsJsPPZsGzwwsLwLmpwMDw",
        };
#else
            ();
#endif

        public Day03() : base(03)
        {
        }

        #endregion

        public void ExecuteDay()
        {
#if !TEST
            _input = Parser.Parse();
#endif
            List<int> prio = new List<int>();

            foreach (var rucksack in _input)
            {
                string a = rucksack.Substring(0, rucksack.Length / 2);
                string b = rucksack.Substring(rucksack.Length / 2, rucksack.Length / 2);

                IEnumerable<char> common = a.Intersect(b);

                if (common.Count() == 1)
                {
                    char l = common.First();

                    if (l is >= 'A' and <= 'Z')
                        prio.Add((int) l - (int) 'A' + 27);
                    else
                        prio.Add((int)l - (int)'a' + 1);
                }
            }
            
            this.PartA = prio.Sum();

            prio.Clear();

            for (int i = 0; i + 2 < _input.Count; i += 3)
            {
                string elfA = _input[i];
                string elfB = _input[i+1];
                string elfC = _input[i+2];

                var intersectA = elfA.Intersect(elfB);
                var intersectB = intersectA.Intersect(elfC);

                if (intersectB.Count() == 1)
                {
                    char l = intersectB.First();

                    if (l is >= 'A' and <= 'Z')
                        prio.Add((int)l - (int)'A' + 27);
                    else
                        prio.Add((int)l - (int)'a' + 1);
                }
            }

            this.PartB = prio.Sum();
        }
    }
}