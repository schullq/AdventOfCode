using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Commons;
using MoreLinq;

namespace AdventOfCode._2021
{
    public class Day09 : AbstractDay
    {
        #region Init

        private static List<string> _input = new List<string>
#if TEST
        {
            "2199943210",
            "3987894921",
            "9856789892",
            "8767896789",
            "9899965678"
        };
#else
            ();
#endif

        public Day09() : base(9)
        {
        }

        #endregion

        public void ExecuteDay()
        {
#if !TEST
            _input = this.Parser.Parse();
#endif
            var aInput = _input
                .Select(x => x.ToCharArray().Select(x => (int)x - '0').ToArray())
                .ToArray();
            List<(int x, int y)> lowPoints = new List<(int x, int y)>();
            int answer = 0;
            for (int i = 0; i < _input.Count; i++)
            {
                for (int j = 0; j < _input[i].Length; j++)
                {
                    var c = aInput[i][j];
                    if (i > 0 && c >= aInput[i - 1][j])
                        continue;
                    if (j > 0 && c >= aInput[i][j - 1])
                        continue;
                    if (j < aInput[i].Length - 1 && c >= aInput[i][j + 1])
                        continue;
                    if (i < aInput.Length - 1 && c >= aInput[i + 1][j])
                        continue;
                    lowPoints.Add((j, i));
                    answer += aInput[i][j] + 1;
                    // neighbor cannot be lower so we jump it
                    j++;
                }
            }

            this.PartA = answer;

            var sizes = new List<int>();
            foreach (var (x, y) in lowPoints)
            {
                var s = 0;
                var seen = new HashSet<(int x, int y)>();

                IEnumerable<(int x, int y)> traverse((int x, int y) v)
                {
                    var (x, y) = v;

                    if (aInput[y][x] == 9) yield break;
                    if (seen.Contains((x, y))) yield break;
                    seen.Add((x, y));

                    s++;

                    if (y > 0)
                        yield return (x, y - 1);
                    if (y < aInput.Length - 1)
                        yield return (x, y + 1);
                    if (x > 0)
                        yield return (x - 1, y);
                    if (x < aInput[y].Length - 1)
                        yield return (x + 1, y);
                }

                MoreEnumerable
                    .TraverseBreadthFirst((x, y), traverse)
                    .ToList();

                sizes.Add(s);
            }

            this.PartB = sizes
                .OrderByDescending(c => c)
                .Take(3)
                .Aggregate((a, b) => a * b);
        }
    }
}