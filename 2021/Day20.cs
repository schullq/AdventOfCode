using System;
using System.Collections.Generic;
using AdventOfCode.Commons;

namespace AdventOfCode._2021
{
    public class Day20 : AbstractDay
    {
        #region Init

        private static List<string> _input = new List<string>
#if TEST || PRINT_TEST
        {
            "..#.#..#####.#.#.#.###.##.....###.##.#..###.####..#####..#....#..#..##..###..######.###...####..#..#####..##..#.#####...##.#.#..#.##..#.#......#.###.######.###.####...#.##.##..#..#..#####.....#.#....###..#.##......#.....#..#..#..##..#...##.######.####.####.#.#...#.......#..#.#.#...####.##.#......#..#...##.#.##..#...##.#.##..###.#......#.#.......#.#.#.####.###.##...#.....####.#..#..#.##.#....##..#.####....##...##..#...#......#.#.......#.......##..####..#...#.#.#...##..#.#..###..#####........#..####......#..#",
            "#..#.",
            "#....",
            "##..#",
            "..#..",
            "..###"
        };
#else
            ();
#endif

        public Day20() : base(20)
        { }

        #endregion

        public void ExecuteDay()
        {
#if !TEST && !PRINT_TEST
            _input = this.Parser.Parse();
#endif
            var imageEnhancement = _input[0].Select(x => x == '#').ToList();
            List<List<bool>> map = _input.Skip(1).Select(x => x.ToCharArray().Select(y => (bool) (y == '#')).ToList()).ToList();

            static List<List<bool>> Process(int times, List<List<bool>> image, List<bool> enhancementAlgo)
            {
                for (int t = 0; t < times; t++)
                {
                    List<List<bool>> newImage = new List<List<bool>>();

                    for (int y = -1; y <= image.Count; y++)
                    {
                        List<bool> newImageRow = new List<bool>();

                        for (int x = -1; x <= image.Count; x++)
                        {
                            var pixels = new List<bool>();

                            foreach (var adj in (x, y).GetCartesianAdjacent(true))
                            {
                                if (adj.x < 0 || adj.x >= image.Count || adj.y < 0 || adj.y >= image.Count)
                                    pixels.Add(enhancementAlgo[0] & Convert.ToBoolean(t % 2));
                                else
                                    pixels.Add(image[adj.y][adj.x]);
                            }

                            newImageRow.Add(enhancementAlgo[Convert.ToInt32(string.Concat(pixels.Select(p => p ? '1' : '0')), 2)]);
                        }

                        newImage.Add(newImageRow);
                    }

                    image = new List<List<bool>>(newImage);
                }

                return image;
            }

            this.PartA = Process(2, map, imageEnhancement).SelectMany(x => x).Count(x => x);

            this.PartB = Process(50, map, imageEnhancement).SelectMany(x => x).Count(x => x); ;
        }
    }
}
