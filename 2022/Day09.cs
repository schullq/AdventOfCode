using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using AdventOfCode.Commons;

namespace AdventOfCode._2022
{
    public partial class Day09 : AbstractDay
    {
        #region Init

        private static List<string> _input = new List<string>
#if TEST
        {
"R 5",
"U 8",
"L 8",
"D 3",
"R 17",
"D 10",
"L 25",
"U 20",
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
            List<(int x, int y)> knots = new List<(int x, int y)>
            {
                new(0, 0),
                new(0, 0),
                new(0, 0),
                new(0, 0),
                new(0, 0),
                new(0, 0),
                new(0, 0),
                new(0, 0),
                new(0, 0),
                new(0, 0)
            };

            List<(int x, int y)> visitedA = new List<(int x, int y)> { knots[1] };
            List<(int x, int y)> visitedB = new List<(int x, int y)> { knots[9] };

            foreach (var inst in _input)
            {
                char dir = inst.Split(' ')[0][0];
                int f = int.Parse(inst.Split(' ')[1]);
                while (--f >= 0)
                {
                    switch (dir)
                    {
                        case 'U':
                            knots[0]  = (knots[0] .x, knots[0] .y + 1);
                            break;
                        case 'R':
                            knots[0]  = (knots[0] .x + 1, knots[0] .y);
                            break;
                        case 'D':
                            knots[0]  = (knots[0] .x, knots[0] .y - 1);
                            break;
                        case 'L':
                            knots[0]  = (knots[0] .x - 1, knots[0] .y);
                            break;
                    }

                    for (int i = 1; i < 10; i++)
                    {
                        var pos = knots[i - 1].GetCartesianAdjacent(true)
                            .Where(x => knots[i].GetCartesianAdjacent(true)
                                .Contains(x));

                        if (pos.Contains(knots[i]))
                            continue;

                        var neighbors = knots[i - 1].GetCartesianNeighbors()
                            .Where(x => knots[i].GetCartesianAdjacent()
                                .Contains(x));
                        if (pos.Contains(knots[i - 1]))
                        {
                            knots[9] = knots[i - 1];
                        }
                        else if (neighbors.Any())
                        {
                            knots[i] = neighbors.First();
                        }
                        else
                        {
                            knots[i] = pos.First();
                        }
                    }

                    visitedA.Add(knots[1]);
                    visitedB.Add(knots[9]);
                }
            }

            //PrintVisited(visited);

            this.PartA = visitedA.Distinct().Count();
            this.PartB = visitedB.Distinct().Count();
        }

        public void PrintVisited(List<(int x, int y)> visited)
        {
            int height = visited.Max(v => v.y);
            int width = visited.Max(v => v.x);

            for (int i = height; i >= 0; i--)
            {
                for (int j = 0; j <= width; j++)
                {
                    Console.Write(visited.Contains((j, i)) ? '#' : '.');
                }
                Console.WriteLine();
            }
        }

    }
}
