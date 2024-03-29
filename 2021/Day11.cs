using System;
using System.Collections.Generic;
using AdventOfCode.Commons;
using MoreLinq;

namespace AdventOfCode._2021
{
    public class Day11 : AbstractDay
    {
        #region Init

        private static List<List<(int, bool)>> _input = new List<List<(int, bool)>>
#if TEST
        {
            "11111".Select(x => ((int)(x - '0'), false)).ToList(),
            "19991".Select(x => ((int)(x - '0'), false)).ToList(),
            "19191".Select(x => ((int)(x - '0'), false)).ToList(),
            "19991".Select(x => ((int)(x - '0'), false)).ToList(),
            "11111".Select(x => ((int)(x - '0'), false)).ToList(),
//"".Select(x => (int)(x - '0')).ToList(),
//"".Select(x => (int)(x - '0')).ToList(),
//"".Select(x => (int)(x - '0')).ToList(),
//"".Select(x => (int)(x - '0')).ToList(),
//"".Select(x => (int)(x - '0')).ToList()
        };
#else
            ();
#endif

        public Day11() : base(11)
        {
        }

        #endregion


        public void ExecuteDay()
        {
#if !TEST
            _input = this.Parser.Parse()
                .Select(y => y
                    .Select(x => ((int)(x - '0'), false))
                    .ToList())
                .ToList();
#endif
            int flashes = 0;

            _input.ForEach(x =>
            {
                x.ForEach(y =>
                {
                    if (y.Item1 == 9)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(y.Item1);
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                    {
                        Console.Write(y.Item1);
                    }
                });
                Console.WriteLine();
            });
            Console.WriteLine();

            for (int n = 0; n < 100; n++)
            {
                Console.WriteLine($" --- Step {n + 1} --- ");
                for (int i = 0; i < _input.Count; i++)
                {
                    for (int j = 0; j < _input[i].Count; j++)
                    {
                        _input[i][j] = (_input[i][j].Item1 + 1, _input[i][j].Item2);
                        if (_input[i][j].Item1 >= 10)
                        {
                            flashes = this.MakeItFlash(++flashes, j, i);
                        }
                    }
                }

                _input.ForEach(x =>
                {
                    x.ForEach(y =>
                    {
                        if (y.Item1 == 9)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write(y.Item1);
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        else
                        {
                            Console.Write(y.Item1);
                        }
                    });
                    Console.WriteLine();
                });
                Console.WriteLine();
            }

            this.PartA = flashes;

            this.PartB = 0;
        }

        private int MakeItFlash(int flashes, int x, int y)
        {
#if PRINT
            Console.WriteLine(" -- New flash ! --");
#endif

            void print(int p, int t)
            {
#if PRINT
                Enumerable.Range(0, _input.Count)
                    .ForEach(i =>
                    {
                        int j = 0;
                        _input[i].ForEach(b =>
                        {
                            if (i == y && j == x)
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.Write(b.Item1);
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                            else if (i == p && j == t)
                            {
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                Console.Write(b.Item1);
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                            else if (b.Item1 == 9)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.Write(b.Item1);
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                            else
                            {
                                Console.Write(b.Item1);
                            }

                            j++;
                        });
                        Console.WriteLine();
                    });
                Console.WriteLine();
#endif
            }


            _input[y][x] = (0, true);
            print(x - 1, y - 1);
            if (x > 0 && y > 0 && _input[y - 1][x - 1].Item1 != 0)
            {
                var c = _input[y - 1][x - 1];
                c = (c.Item1 + 1, c.Item2);
                if (c.Item1 >= 10 && !c.Item2)
                    flashes = this.MakeItFlash(++flashes, x - 1, y - 1);
                _input[y - 1][x - 1] = c;
            }

            print(y - 1, x);
            if (y > 0 && _input[y - 1][x].Item1 != 0)
            {
                var c = _input[y - 1][x];
                c = (c.Item1 + 1, c.Item2);
                if (c.Item1 >= 10 && !c.Item2)
                    flashes = this.MakeItFlash(++flashes, x, y - 1);
                _input[y - 1][x] = c;
            }

            print(y - 1, x + 1);
            if (x < _input[y].Count - 1 && y > 0 && _input[y - 1][x + 1].Item1 != 0)
            {
                var c = _input[y - 1][x + 1];
                c = (c.Item1 + 1, c.Item2);
                if (c.Item1 >= 10 && !c.Item2)
                    flashes = this.MakeItFlash(++flashes, x + 1, y - 1);
                _input[y - 1][x + 1] = c;
            }

            print(y, x - 1);
            if (x > 0 && _input[y][x - 1].Item1 != 0)
            {
                var c = _input[y][x - 1];
                c = (c.Item1 + 1, c.Item2);
                if (c.Item1 >= 10 && !c.Item2)
                    flashes = this.MakeItFlash(++flashes, x - 1, y);
                _input[y][x - 1] = c;
            }

            print(y + 1, x + 1);
            if (x < _input[y].Count - 1 && _input[y][x + 1].Item1 != 0)
            {
                var c = _input[y][x + 1];
                c = (c.Item1 + 1, c.Item2);
                if (c.Item1 >= 10 && !c.Item2)
                    flashes = this.MakeItFlash(++flashes, x + 1, y);
                _input[y][x + 1] = c;
            }

            print(y + 1, x - 1);
            if (x > 0 && y < _input.Count - 1 && _input[y + 1][x - 1].Item1 != 0)
            {
                var c = _input[y + 1][x - 1];
                c = (c.Item1 + 1, c.Item2);
                if (c.Item1 >= 10 && !c.Item2)
                    flashes = this.MakeItFlash(++flashes, x - 1, y + 1);
                _input[y + 1][x - 1] = c;
            }

            print(y + 1, x);
            if (y < _input.Count - 1 && _input[y + 1][x].Item1 != 0)
            {
                var c = _input[y + 1][x];
                c = (c.Item1 + 1, c.Item2);
                if (c.Item1 >= 10 && !c.Item2)
                    flashes = this.MakeItFlash(++flashes, x, y + 1);
                _input[y + 1][x] = c;
            }

            print(y + 1, x + 1);
            if (x < _input[y].Count - 1 && y < _input.Count - 1 && _input[y + 1][x + 1].Item1 != 0)
            {
                var c = _input[y + 1][x + 1];
                c = (c.Item1 + 1, c.Item2);
                if (c.Item1 >= 10 && !c.Item2)
                    flashes = this.MakeItFlash(++flashes, x + 1, y + 1);
                _input[y + 1][x + 1] = c;
            }

            print(x, y);


            return flashes;
        }
    }
}