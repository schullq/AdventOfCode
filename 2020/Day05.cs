using System;
using System.Collections.Generic;
using AdventOfCode.Commons;
using MoreLinq;

namespace AdventOfCode._2020
{
    public class Day05 : AbstractDay
    {
        #region Init

        private static List<string> _input = new List<string>
#if TEST
        {
"FBFBBFFRLR"
        };
#else
            ();
#endif

        public Day05() : base(05)
        { }

        #endregion



        public void ExecuteDay()
        {
#if !TEST
            _input = this.Parser.Parse();
#endif
            int maxId = -1;
            List<int> seats = new List<int>();
            foreach (var seat in _input)
            {
                (int front, int back) row = (0, 127);
                (int left, int right) col = (0, 7);
                foreach (var c in seat)
                {
                    switch (c)
                    {
                        case 'F':
                            row = (row.front, (int)Math.Truncate(Convert.ToDecimal(row.back + row.front) / 2));
                            break;
                        case 'B':
                            row = ((int)Math.Round(Convert.ToDecimal(row.back + row.front) / 2, MidpointRounding.AwayFromZero), row.back);
                            break;
                        case 'L':
                            col = (col.left, (int)Math.Truncate(Convert.ToDecimal(col.right + col.left) / 2));
                            break;
                        case 'R':
                            col = ((int)Math.Round(Convert.ToDecimal(col.left + col.right) / 2, MidpointRounding.AwayFromZero), col.right);
                            break;
                    }
                }

                seats.Add(row.front * 8 + col.left);
                maxId = maxId < (row.front * 8 + col.left) ? (row.front * 8 + col.left) : maxId;
            }

            seats = seats.OrderBy(x => x).ToList();
            this.PartA = maxId;

            this.PartB = seats.Window(2).Where(x => x[1] - x[0] != 1).ToList()[0][0] + 1;
        }

    }
}
