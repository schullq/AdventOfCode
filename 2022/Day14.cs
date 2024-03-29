using System;
using System.Collections.Generic;
using AdventOfCode.Commons;

namespace AdventOfCode._2022
{
    public partial class Day14 : AbstractDay
    {
        #region Init

        private static List<string> _input = new List<string>
#if TEST
        {
            "498,4 -> 498,6 -> 496,6",
            "503,4 -> 502,4 -> 502,9 -> 494,9",
        };
#else
            ();
#endif

        public Day14() : base(14)
        {
        }

        #endregion

        public void ExecuteDay()
        {
#if !TEST
            _input = Parser.Parse();
#endif
            int totalA = 0;
            int totalB = 0;

            Dictionary<int, Dictionary<int, char>> initMap = new Dictionary<int, Dictionary<int, char>>();

            foreach (var path in _input)
            {
                var points = path.Split(" -> ")
                    .Select(p => (x: int.Parse(p.Split(',')[0]), y: int.Parse(p.Split(',')[1]))).ToList();

                for (int i = 0; i < points.Count; i++)
                {
                    if (!initMap.ContainsKey(points[i].y))
                        initMap[points[i].y] = new Dictionary<int, char>();

                    // Vertical
                    if (i != 0 && points[i - 1].y == points[i].y)
                    {
                        int diff = points[i - 1].x > points[i].x ? -1 : 1;

                        for (int x = points[i - 1].x; x != points[i].x + diff; x += diff)
                        {
                            initMap[points[i].y][x] = '#';
                        }
                    }
                    // Horizontal
                    else if (i != 0 && points[i - 1].x == points[i].x)
                    {
                        int diff = points[i - 1].y > points[i].y ? -1 : 1;

                        for (int y = points[i - 1].y; y != points[i].y + diff; y += diff)
                        {
                            if (!initMap.ContainsKey(y))
                                initMap[y] = new Dictionary<int, char>();
                            initMap[y][points[i].x] = '#';
                        }
                    }
                }
            }

            var mapA = initMap.ToDictionary(y => y.Key, y => y.Value.ToDictionary(x => x.Key, x => x.Value));
            var mapB = initMap.ToDictionary(y => y.Key, y => y.Value.ToDictionary(x => x.Key, x => x.Value));

            (int x, int y) maxVal = (initMap.Values.SelectMany(e => e.Keys).Max(), initMap.Keys.Max() + 2);

            int Process(Dictionary<int, Dictionary<int, char>> map, (int x, int y)? max)
            {
                bool isFinished = false;
                (int x, int y) source = (500, 0);

                do
                {
                    (int x, int y) sand = source;

                    do
                    {
                        (int x, int y) next = (sand.x, sand.y + 1);

                        if (next.y == max?.y)
                        {
                            if (!map.ContainsKey(sand.y))
                                map[sand.y] = new Dictionary<int, char>();
                            map[sand.y][sand.x] = 'o';
                            break;
                        }
                        else if (!map.ContainsKey(next.y) || (!map[next.y].ContainsKey(next.x) &&
                                                              !map[next.y].ContainsKey(next.x + 1) &&
                                                              !map[next.y].ContainsKey(next.x - 1)))
                            sand = next;
                        else if (next.y != max?.y && !map[next.y].ContainsKey(next.x) || map[next.y].ContainsKey(next.x) && map[next.y][next.x] != 'o' && map[next.y][next.x] != '#')
                            sand = next;
                        else if (next.y != max?.y && !map[next.y].ContainsKey(next.x - 1) || map[next.y].ContainsKey(next.x - 1) && map[next.y][next.x - 1] != 'o' && map[next.y][next.x - 1] != '#')
                            sand = (next.x - 1, next.y);
                        else if (next.y != max?.y && !map[next.y].ContainsKey(next.x + 1) || map[next.y].ContainsKey(next.x + 1) && map[next.y][next.x + 1] != 'o' && map[next.y][next.x + 1] != '#')
                            sand = (next.x + 1, next.y);
                        else
                        {

                            if (!map.ContainsKey(sand.y))
                                map[sand.y] = new Dictionary<int, char>();
                            map[sand.y][sand.x] = 'o';
                            if (sand == source)
                                isFinished = !isFinished;
                            break;
                        }
                        if (max == null && map.Keys.Count(e => e > sand.y) == 0)
                        {
                            isFinished = !isFinished;
                            break;
                        }
                    } while (true);
                } while (!isFinished);

                return map.Values.SelectMany(e => e.Values).Count(e => e == 'o');
            }

            this.PartA = Process(mapA, null);
            this.PartB = Process(mapB, maxVal);
        }

        public static void PrintMap(Dictionary<int, Dictionary<int, char>> map)
        {
            (int x, int y) max = (map.Values.SelectMany(e => e.Keys).Max(), map.Keys.Max());
            (int x, int y) min = (map.Values.SelectMany(e => e.Keys).Min(), map.Keys.Min());

            for (int y = min.y; y <= max.y; y++)
            {
                for (int x = min.x; x <= max.x; x++)
                {
                    if (map.ContainsKey(y) && map[y].ContainsKey(x))
                        Console.Write(map[y][x]);
                    else
                        Console.Write('.');
                }

                Console.WriteLine();
            }
        }
    }
}