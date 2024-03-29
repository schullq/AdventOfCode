using System;
using System.Collections.Generic;
using AdventOfCode.Commons;

namespace AdventOfCode._2021
{
    public class Day17 : AbstractDay
    {
        #region Init

        private string _input =
#if TEST
            "target area: x=201..230, y=-99..-65";
            //"target area: x=20..30, y=-10..-5";
#else
            "";
#endif

        public Day17() : base(17)
        { }

        #endregion



        public void ExecuteDay()
        {
#if !TEST
            //_input = Parser.Parse();
#endif
            var (x1, x2) = (int.Parse(this._input.Split(" ")[2].Split("=")[1].Split("..")[0]),
                int.Parse(this._input.Split(" ")[2].Split("=")[1].Split("..")[1].TrimEnd(',')));
            var (y1, y2) = (int.Parse(this._input.Split(" ")[3].Split("=")[1].Split("..")[0]),
                int.Parse(this._input.Split(" ")[3].Split("=")[1].Split("..")[1]));

            List<(int x, int y)> validVelocities = new List<(int x, int y)>();

            foreach (var x in Enumerable.Range(1, x2))
            {
                foreach (var y in Enumerable.Range(y1, 2000))
                {
                    if (this.IsValid((x, y), (x1, x2, y1, y2)))
                        validVelocities.Add((x, y));
                }
            }

            int lowY = (y1 > y2) ? y2 : y1;

            this.PartA = lowY > 0 
                ? lowY * (lowY - 1) / 2
                : lowY * (lowY + 1) / 2;

            this.PartB = validVelocities.Count;
        }

        private bool IsValid(
            (int x, int y) v,
            (int x1, int x2, int y1, int y2) area
        )
        {
            int x = 0;
            int y = 0;
            do
            {
                y += v.y;
                x += v.x;
                v.y--;
                v.x = (v.x > 0) ? v.x - 1 : (v.x < 0) ? v.x + 1 : v.x;
                if (x >= area.x1 && x <= area.x2 && y >= area.y1 && y <= area.y2)
                    return true;
                
            } while (y >= area.y1);

            return false;
        }
    }
}
