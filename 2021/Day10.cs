using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using AdventOfCode.Commons;
using MoreLinq.Extensions;

namespace AdventOfCode._2021
{
    public class Day10 : AbstractDay
    {
        private static List<string> _input = new List<string>
#if TEST
        {
            "[({(<(())[]>[[{[]{<()<>>",
            "[(()[<>])]({[<{<<[]>>(",
            "{([(<{}[<>[]}>{[]{[(<()>",
            "(((({<>}<{<{<>}{[]{[]{}",
            "[[<[([]))<([[{}[[()]]]",
            "[{[{({}]{}}([{[{{{}}([]",
            "{<[[]]>}<{[{[{[]{()[[[]",
            "[<(<(<(<{}))><([]([]()v",
            "<{([([[(<>()){}]>(<<{{",
            "<{([{{}}[<[[[<>{}]]]>[]]"
        };
#else
            ();
#endif
        public Day10() : base(10)
        {
        }

        public void ExecuteDay()
        {
#if !TEST
            _input = Parser.Parse();
#endif

        }
    }
}