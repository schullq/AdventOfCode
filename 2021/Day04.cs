using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Commons;

namespace AdventOfCode._2021
{
    public class Day04 : AbstractDay
    {
        private static List<string> _input = new List<string>
#if TEST
        {
            "7,4,9,5,11,17,23,2,0,14,21,24,10,16,13,6,15,25,12,22,18,20,8,19,3,26,1",
            "22 13 17 11  0",
            " 8  2 23  4 24",
            "21  9 14 16  7",
            " 6 10  3 18  5",
            " 1 12 20 15 19",
            " 3 15  0  2 22",
            " 9 18 13 17  5",
            "19  8  7 25 23",
            "20 11 10 24  4",
            "14 21 16 12  6",
            "14 21 17 24  4",
            "10 16 15  9 19",
            "18  8 23 26 20",
            "22 11 13  6  5",
            " 2  0 12  3  7"
        };
#else
            ();
#endif

        private static List<Dictionary<int, bool>> _grids = new List<Dictionary<int, bool>>();
        private static List<int> numbers;

        public Day04() : base(4)
        {
        }

        public int PhaseOne()
        {
            _input = Parser.Parse();
            ParseBingo();

            foreach (var n in numbers)
            {
                foreach (var grid in _grids)
                {
                    if (grid.ContainsKey(n))
                    {
                        grid[n] = true;
                    }

                    var res = CheckVictory(grid);
                    if (res != 0)
                    {
                        return res * n;
                    }
                }
            }

            return 0;
        }

        public int PhaseTwo()
        {
            if (_input == null || _input.Count == 0)
            {
                _input = Parser.Parse();
                ParseBingo();
            }

            int answer = 0;
            List<Dictionary<int, bool>> toRemove = new List<Dictionary<int, bool>>();
            foreach (var n in numbers)
            {
                foreach (var grid in _grids)
                {
                    if (grid.ContainsKey(n))
                    {
                        grid[n] = true;
                    }

                    var res = CheckVictory(grid);
                    if (res != 0)
                    {
                        answer = res * n;
                        toRemove.Add(grid);
                    }
                }

                toRemove.ForEach(x => _grids.Remove(x));
                toRemove.Clear();
            }

            return answer;
        }

        private static int CheckVictory(Dictionary<int, bool> grid)
        {
            for (int x = 0; x <= 20; x += 5)
            {
                if (grid.ElementAt(x).Value && grid.ElementAt(x + 1).Value
                                            && grid.ElementAt(x + 2).Value
                                            && grid.ElementAt(x + 3).Value
                                            && grid.ElementAt(x + 4).Value)
                {
                    return grid.Where(pair => !pair.Value).Select(pair => pair.Key).Sum();
                }
            }

            for (int y = 0; y < 5; y++)
            {
                if (grid.ElementAt(y).Value && grid.ElementAt(y + 5).Value
                                            && grid.ElementAt(y + 10).Value
                                            && grid.ElementAt(y + 15).Value
                                            && grid.ElementAt(y + 20).Value)
                {
                    return grid.Where(x => !x.Value).Select(x => x.Key).Sum();
                }
            }

            return 0;
        }

        private static void ParseBingo(int size = 5)
        {
            numbers = _input[0].Split(',').Select(int.Parse).ToList();

            _input.RemoveAt(0);

            _grids.Add(new Dictionary<int, bool>());
            Dictionary<int, bool> currentGrid = _grids.Last();

            int i = size;

            foreach (var line in _input)
            {
                if (i == 0)
                {
                    _grids.Add(new Dictionary<int, bool>());
                    currentGrid = _grids.Last();
                    i = size;
                }

                var values = line.Split(' ').Where(x => x.Length > 0).Select(int.Parse).ToList();
                values.ForEach(x => currentGrid.Add(x, false));
                i--;
            }

            _grids.RemoveAll(x => x.Count == 0);
        }
    }
}