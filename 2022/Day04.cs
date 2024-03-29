using System;
using System.Collections.Generic;
using AdventOfCode.Commons;

namespace AdventOfCode._2022
{
    public partial class Day04 : AbstractDay
    {
        #region Init

        private static List<string> _input = new List<string>
#if TEST
        {
            "2-4,6-8",
            "2-3,4-5",
            "5-7,7-9",
            "2-8,3-7",
            "6-6,4-6",
            "2-6,4-8"
        };
#else
            ();
#endif

        public Day04() : base(04)
        {
        }

        #endregion

        public void ExecuteDay()
        {
#if !TEST
            _input = Parser.Parse();
#endif
            int total = 0;
            foreach (var pair in _input)
            {
                string[] elves = pair.Split(',');

                (int s, int e) a = (int.Parse(elves[0].Split('-')[0]), int.Parse(elves[0].Split('-')[1]));
                (int s, int e) b = (int.Parse(elves[1].Split('-')[0]), int.Parse(elves[1].Split('-')[1]));

                if (a.s >= b.s && a.e <= b.e || b.s >= a.s && b.e <= a.e)
                    total++;
            }
            
            this.PartA = total;

            total = 0;
            foreach (var pair in _input)
            {
                string[] elves = pair.Split(',');

                (int s, int e) a = (int.Parse(elves[0].Split('-')[0]), int.Parse(elves[0].Split('-')[1]));
                (int s, int e) b = (int.Parse(elves[1].Split('-')[0]), int.Parse(elves[1].Split('-')[1]));

                if (a.s >= b.s && a.e <= b.e || 
                    b.s >= a.s && b.e <= a.e || 
                    a.e >= b.s && a.s <= b.e ||
                    b.e >= a.s && b.s <= a.e)
                    total++;
            }

            this.PartB = total;
        }
    }
}