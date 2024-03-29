using System;
using System.Collections.Generic;
using AdventOfCode.Commons;

namespace AdventOfCode._2020
{
    public class Day09 : AbstractDay
    {
        #region Init

        private static List<string> _input = new List<string>
#if TEST
        {
            "35",
            "20",
            "15",
            "25",
            "47",
            "40",
            "62",
            "55",
            "65",
            "95",
            "102",
            "117",
            "150",
            "182",
            "127",
            "219",
            "299",
            "277",
            "309",
            "576"
        };
#else
            ();
#endif

        public Day09() : base(09)
        { }

        #endregion

        public void ExecuteDay()
        {
#if !TEST
            _input = Parser.Parse();
#endif
            var list = _input.Select(long.Parse).ToList();
            const int preambleSize = 25;

            for (int i = preambleSize; i < list.Count; i++)
            {
                long current = list[i];
                bool valid = false;
                for (int j = i - preambleSize; j < i; j++)
                {
                    for (int k = j + 1; k < i; k++)
                    {
                        if (list[k] + list[j] != current)
                            continue;
                        valid = true;
                        break;
                    }

                    if (valid) break;
                }

                if (valid)
                    continue;
                this.PartA = current;
                break;
            }

            var subList = list.TakeWhile(x => x < this.PartA).ToList();

            for (int i = 0; i < subList.Count; i++)
            {
                var (sum, min, max) = (subList[i], subList[i], subList[i]);
                for (int j = i + 1; j < subList.Count; j++)
                {
                    sum += subList[j];
                    min = subList[j] < min ? subList[j] : min;
                    max = subList[j] > max ? subList[j] : max;
                    if (sum == this.PartA)
                    {
                        this.PartB = min + max;
                        return;
                    }

                    if (sum > this.PartA)
                        break;
                }
            }
        }
    }
}
