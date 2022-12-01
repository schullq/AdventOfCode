using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Commons;
using MoreLinq.Extensions;

namespace AdventOfCode._2021
{
    public class Day01 : AbstractDay
    {
        private static List<int> _input = new List<int>
#if TEST
        {
            199,
            200,
            208,
            210,
            200,
            207,
            240,
            269,
            260,
            263
        };
#else
            ();
#endif
        public Day01() : base(1)
        { }

        public void ExecuteDay()
        {
            _input = this.Parser.ParseInt();
            this.PartA = _input.Window(2).Count(x => x.Last() > x.First());
            this.PartB = _input.Window(4).Count(x => x.Last() > x.First());
        }

        public int PhaseOne()
        {
#if !TEST
            _input = this.Parser.ParseInt();
#endif
            int larger = 0;

            for (int i = 1; i < _input.Count; i++)
                if (_input[i] > _input[i - 1])
                    larger++;

            return larger;
        }

        public int PhaseTwo()
        {
#if !TEST
            _input = this.Parser.ParseInt();
#endif
            int larger = 0;

            for (int i = 3; i < _input.Count; i++)
            {
                int sum1 = _input[i - 3] + _input[i - 2] + _input[i - 1];
                int sum2 = _input[i - 2] + _input[i - 1] + _input[i];

                if (sum2 > sum1) larger++;
            }

            return larger;
        }

        public int PhaseOneLinq()
        {
#if !TEST
            _input = this.Parser.ParseInt();
#endif
            return _input.Window(2).Count(x => x.Last() > x.First());
        }

        public int PhaseTwoLinq()
        {
#if !TEST
            _input = this.Parser.ParseInt();
#endif
            return _input.Window(4).Count(x => x.Last() > x.First());
        }
    }
}