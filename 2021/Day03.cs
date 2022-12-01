using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Commons;

namespace AdventOfCode._2021
{
    public class Day03 : AbstractDay
    {
        private static List<char[]> _input = new List<char[]>
#if TEST
        {
            "00100".ToCharArray(),
            "11110".ToCharArray(),
            "10110".ToCharArray(),
            "10111".ToCharArray(),
            "10101".ToCharArray(),
            "01111".ToCharArray(),
            "00111".ToCharArray(),
            "11100".ToCharArray(),
            "10000".ToCharArray(),
            "11001".ToCharArray(),
            "00010".ToCharArray(),
            "01010".ToCharArray()
        };
#else
            ();
#endif

        public Day03() : base(3)
        { }

        public int PhaseOne()
        {
#if !TEST
            _input = this.Parser.Parse().Select(x => x.ToCharArray()).ToList();
#endif
            string gamma = "";
            string epsilon = "";

            for (int i = 0; i < _input[0].Length; i++)
            {
                int one = 0;
                int zero = 0;
                for (int j = 0; j < _input.Count - 1; j++)
                {
                    if (_input[j][i] == '0')
                        zero++;
                    else
                    {
                        one++;
                    }
                }

                if (one > zero)
                {
                    gamma += '1';
                    epsilon += '0';
                }
                else
                {
                    gamma += '0';
                    epsilon += '1';
                }

                one = 0;
                zero = 0;
            }

            return Convert.ToInt32(gamma, 2) * Convert.ToInt32(epsilon, 2);
        }

        public int PhaseTwo()
        {
#if !TEST
            _input ??= this.Parser.Parse().Select(x => x.ToCharArray()).ToList();
# endif
            var _inputCopy = new List<char[]>(_input);
            var _inputList = _input.Select(x => new string(x)).ToList();

            string ox = "";
            string o2 = "";

            char main = char.MinValue;
            char other = char.MinValue;
            for (int i = 0; i < _input[0].Length; i++)
            {
                int one = 0;
                int zero = 0;

                List<int> toRemove = new List<int>();
                for (int j = 0; j < _input.Count; j++)
                {
                    if (_input[j].Length == 0)
                    {
                        break;
                    }

                    if (i == 0 || _input[j][i - 1] == main)
                    {
                        if (_input[j][i] == '0')
                            zero++;
                        else
                        {
                            one++;
                        }
                    }
                    else if (_input[j][i - 1] != main)
                    {
                        toRemove.Add(j);
                    }
                }

                toRemove.Reverse();
                toRemove.ForEach(x => _input.RemoveAt(x));

                if (one >= zero)
                {
                    main = '1';
                    other = '0';
                    ox += '1';
                }
                else
                {
                    main = '0';
                    other = '1';
                    ox += '0';
                }

                if (_inputList.Count(x => x.StartsWith(ox)) == 1)
                {
                    ox = _inputList.Single(x => x.StartsWith(ox));
                    Console.WriteLine(
                        $"ox: {ox} - {_inputList.Count(x => x.StartsWith(ox))} - {Convert.ToInt32(ox, 2)}");
                    break;
                }

                Console.WriteLine($"ox: {ox} - {_inputList.Count(x => x.StartsWith(ox))}");
            }

            _input = _inputCopy;
            for (int i = 0; i < _input[0].Length; i++)
            {
                int one = 0;
                int zero = 0;

                List<int> toRemove = new List<int>();
                for (int j = 0; j < _input.Count; j++)
                {
                    if (_input[j].Length == 0)
                    {
                        break;
                    }

                    if (i == 0 || _input[j][i - 1] == other)
                    {
                        if (_input[j][i] == '0')
                            zero++;
                        else
                        {
                            one++;
                        }
                    }
                    else if (_input[j][i - 1] != other)
                    {
                        toRemove.Add(j);
                    }
                }

                toRemove.Reverse();
                toRemove.ForEach(x => _input.RemoveAt(x));

                if (one >= zero)
                {
                    main = '1';
                    other = '0';
                    o2 += '0';
                }
                else
                {
                    main = '0';
                    other = '1';
                    o2 += '1';
                }

                if (_inputList.Count(x => x.StartsWith(o2)) == 1)
                {
                    o2 = _inputList.Single(x => x.StartsWith(o2));
                    Console.WriteLine(
                        $"o2: {o2} - {_inputList.Count(x => x.StartsWith(o2))} - {Convert.ToInt32(o2, 2)}");
                    break;
                }

                Console.WriteLine($"o2: {o2} - {_inputList.Count(x => x.StartsWith(o2))}");
            }

            return Convert.ToInt32(o2, 2) * Convert.ToInt32(ox, 2);
        }

    }
}