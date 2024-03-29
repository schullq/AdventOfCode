using System.Text.RegularExpressions;
using MoreLinq;

namespace AdventOfCode._2022
{
    public partial class Day05
    {
        public void ExecuteDayLinq()
        {
#if !TEST
            _input = Parser.Parse(keepEmptyLines: true);
#endif
            var inputStacks = _input.Split("").First();
            var input = _input.Split("").Last();

            var stacks = Enumerable.Repeat(0, inputStacks.Last()
                    .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .Last())
                .Select(_ => new List<char>())
                .ToArray();

            inputStacks.Reverse()
                .Skip(1)
                .ForEach(e => Enumerable.Range(0, 9).Where(i => e[1 + i * 4] != ' ')
                    .ForEach(j => stacks[j].Add(e[1 + j * 4])));

            input.Select(l => Regex.Replace(l, "[a-zA-Z]", "")
                .Split(" ")
                .Select(int.Parse));

            this.PartA = "";

            this.PartB = "";
        }
    }
}