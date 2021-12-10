using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using AdventOfCode.Commons;

namespace AdventOfCode._2021
{
    public class Day05 : AbstractDay
    {
        private static List<string> _input = new List<string>
#if TEST
        {
            "0,9 -> 5,9",
            "8,0 -> 0,8",
            "9,4 -> 3,4",
            "2,2 -> 2,1",
            "7,0 -> 7,4",
            "6,4 -> 2,0",
            "0,9 -> 2,9",
            "3,4 -> 1,4",
            "0,0 -> 8,8",
            "5,5 -> 8,2"
        };
#else
            ();
#endif
        private static List<Tuple<Point, Point>> _clouds;
        
        public Day05() : base(5)
        { }

        public int PhaseOne()
        {
#if !TEST
            _input = Parser.Parse();
#endif
            ParseCoordinate();

            int answer = 0;
            int ySize = Math.Max(_clouds.Max(x => x.Item2.X), _clouds.Max(x => x.Item2.Y));
            int xSize = Math.Max(_clouds.Max(x => x.Item2.X), _clouds.Max(x => x.Item2.Y));
            List<List<int>> map = new List<List<int>>();
            while (ySize >= 0)
            {
                List<int> xLine = new List<int>();
                for (int i = 0; i <= xSize; i++)
                {
                    xLine.Add(0);
                }

                map.Add(xLine);
                ySize--;
            }

            foreach (var cloud in _clouds)
            {
#if TEST
                map.ForEach(y =>
                {
                    y.ForEach(x => Console.Write($"{x} "));
                    Console.WriteLine();
                });
                Console.WriteLine("----------------");
#endif
                if (cloud.Item1.X != cloud.Item2.X && cloud.Item1.Y != cloud.Item2.Y)
                    continue;

                int currentX = cloud.Item1.X;
                int currentY = cloud.Item1.Y;

                bool moveHorizontaly = cloud.Item1.X != cloud.Item2.X;

                do
                {
                    map[currentY][currentX]++;

                    if (moveHorizontaly)
                    {
                        if (cloud.Item1.X > cloud.Item2.X)
                            currentX--;
                        else
                            currentX++;
                    }
                    else
                    {
                        if (cloud.Item1.Y > cloud.Item2.Y)
                            currentY--;
                        else
                            currentY++;
                    }
                } while (currentX != cloud.Item2.X || currentY != cloud.Item2.Y);

                map[cloud.Item2.Y][cloud.Item2.X]++;
            }

            answer = map.Select(y => y.Count(x => x > 1)).Sum();

            return answer;
        }

        public int PhaseTwo()
        {
#if !TEST

            if (_input == null || _input.Count == 0)
                _input = Parser.Parse();
#endif
            ParseCoordinate();

            int answer = 0;
            int ySize = Math.Max(_clouds.Max(x => x.Item2.X), _clouds.Max(x => x.Item2.Y));
            int xSize = Math.Max(_clouds.Max(x => x.Item2.X), _clouds.Max(x => x.Item2.Y));
            List<List<int>> map = new List<List<int>>();
            while (ySize >= 0)
            {
                List<int> xLine = new List<int>();
                for (int i = 0; i <= xSize; i++)
                {
                    xLine.Add(0);
                }

                map.Add(xLine);
                ySize--;
            }

            foreach (var cloud in _clouds)
            {
#if TEST
                map.ForEach(y =>
                {
                    y.ForEach(x => Console.Write($"{x} "));
                    Console.WriteLine();
                });
                Console.WriteLine("----------------");
#endif
                int currentX = cloud.Item1.X;
                int currentY = cloud.Item1.Y;

                do
                {
                    map[currentY][currentX]++;

                    if (cloud.Item1.X != cloud.Item2.X && cloud.Item1.Y != cloud.Item2.Y)
                    {
                        if (cloud.Item1.X > cloud.Item2.X && cloud.Item1.Y > cloud.Item2.Y)
                        {
                            currentX--;
                            currentY--;
                        }
                        else if (cloud.Item1.X > cloud.Item2.X && cloud.Item1.Y < cloud.Item2.Y)
                        {
                            currentX--;
                            currentY++;
                        }
                        else if (cloud.Item1.X < cloud.Item2.X && cloud.Item1.Y > cloud.Item2.Y)
                        {
                            currentX++;
                            currentY--;
                        }
                        else
                        {
                            currentX++;
                            currentY++;
                        }
                    }
                    else if (cloud.Item1.X != cloud.Item2.X)
                    {
                        if (cloud.Item1.X > cloud.Item2.X)
                            currentX--;
                        else
                            currentX++;
                    }
                    else
                    {
                        if (cloud.Item1.Y > cloud.Item2.Y)
                            currentY--;
                        else
                            currentY++;
                    }
                } while (currentX != cloud.Item2.X || currentY != cloud.Item2.Y);

                map[cloud.Item2.Y][cloud.Item2.X]++;
            }
            
            answer = map.Select(y => y.Count(x => x > 1)).Sum();

            return answer;
        }

        private static void ParseCoordinate()
        {
            _clouds = new List<Tuple<Point, Point>>();

            foreach (var line in _input)
            {
                if (line.Length == 0) continue;

                var left = line.Split("->")[0].Trim();
                var right = line.Split("->")[1].Trim();

                var leftPoint = new Point(int.Parse(left.Split(',')[0]), int.Parse(left.Split(',')[1]));
                var rightPoint = new Point(int.Parse(right.Split(',')[0]), int.Parse(right.Split(',')[1]));

                _clouds.Add(new Tuple<Point, Point>(leftPoint, rightPoint));
            }
        }
    }
}