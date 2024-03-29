using System;
using System.Collections.Generic;
using AdventOfCode.Commons;

namespace AdventOfCode._2020
{
    public partial class Day11 : AbstractDay
    {
        #region Init

        private static List<string> _input = new List<string>
#if TEST
        {
            "L.LL.LL.LL",
            "LLLLLLL.LL",
            "L.L.L..L..",
            "LLLL.LL.LL",
            "L.LL.LL.LL",
            "L.LLLLL.LL",
            "..L.L.....",
            "LLLLLLLLLL",
            "L.LLLLLL.L",
            "L.LLLLL.LL",
        };
#else
            ();
#endif

        public Day11() : base(011)
        {
        }

        #endregion

        public void ExecuteDay()
        {
#if !TEST
            _input = Parser.Parse();
#endif
            var seats = _input.Select(x => x.ToCharArray()).ToList();

            int totalA = 0;
            int totalB = 0;
            do
            {
                bool hasChanged = false;
                var seatsSnapshot = seats.Select(x => (char[]) x.Clone()).ToList();
                for (int y = 0; y < seatsSnapshot.Count; y++)
                {
                    for (int x = 0; x < seatsSnapshot[y].Length; x++)
                    {
                        if (seatsSnapshot[y][x] == '.')
                            continue;

                        int occupied = 0;
                        foreach (var d in (x, y).GetLinearAdjacent(seatsSnapshot).Where(e => e.Value.Any()))
                        {
                            foreach (var p in d.Value)
                            {
                                if (seatsSnapshot[p.y][p.x] == '#')
                                {
                                    occupied++;
                                    break;
                                }
                            }
                        }

                        switch (seatsSnapshot[y][x])
                        {
                            case '#':
                                if (occupied >= 5)
                                {
                                    seats[y][x] = 'L';
                                    hasChanged = true;
                                }

                                break;
                            case 'L':
                                if (occupied == 0)
                                {
                                    seats[y][x] = '#';
                                    hasChanged = true;
                                }

                                break;
                        }
                    }
                }

                PrintMap();
                Console.WriteLine();

                totalA++;
                if (!hasChanged)
                    break;
            } while (true);

            this.PartA = 0;
            this.PartB = seats.SelectMany(x => x).Count(x => x == '#');

            void PrintMap()
            {
                foreach (var range in seats)
                {
                    foreach (var seat in range)
                        Console.Write(seat);
                    Console.WriteLine();
                }
            }
        }
    }
}