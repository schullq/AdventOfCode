using System;
using System.Collections.Generic;
using System.Text.Json.Nodes;
using AdventOfCode.Commons;
using MoreLinq;

namespace AdventOfCode._2022
{
    public partial class Day13 : AbstractDay
    {
        #region Init

        private static List<string> _input = new List<string>
#if TEST
        {
"[1,1,3,1,1]",
"[1,1,5,1,1]",
"[[1],[2,3,4]]",
"[[1],4]",
"[9]",
"[[8,7,6]]",
"[[4,4],4,4]",
"[[4,4],4,4,4]",
"[7,7,7,7]",
"[7,7,7]",
"[]",
"[3]",
"[[[]]]",
"[[]]",
"[1,[2,[3,[4,[5,6,7]]]],8,9]",
"[1,[2,[3,[4,[5,6,0]]]],8,9]",
        };
#else
            ();
#endif

        public Day13() : base(13)
        { }

        #endregion

        public void ExecuteDay()
        {
#if !TEST
            _input = Parser.Parse();
#endif
            int totalA = 0;
            int totalB = 0;

            List<int> pairs = new List<int>();
            int i = 0;
            int tabs = 0;
            
            foreach (var pair in _input.Chunk(2))
            {
                var a = JsonNode.Parse(pair.First());
                var b = JsonNode.Parse(pair.Last());

                pairs.Add(Compare(a, b));
            }

            int Compare(JsonNode a, JsonNode b)
            {
                if (a is JsonValue && b is JsonValue)
                {
                    return (int)a - (int)b;
                }

                var arrayA = a as JsonArray ?? new JsonArray((int)a);
                var arrayB = b as JsonArray ?? new JsonArray((int)b);

                return arrayA.Zip(arrayB)
                    .Select(x => Compare(x.First, x.Second))
                    .FirstOrDefault(e => e != 0, arrayA.Count - arrayB.Count);
            }

            this.PartA = pairs.Select((e, i) => e < 0 ? i + 1 : 0).Sum();
            
            var nodes = _input.Select(x => JsonNode.Parse(x)).ToList();
            var dividers = new[] { JsonNode.Parse("[[2]]"), JsonNode.Parse("[[6]]") };
            nodes.AddRange(dividers);
            nodes.Sort(Compare);

            this.PartB = (nodes.IndexOf(dividers[0]) + 1) * (nodes.IndexOf(dividers[1]) + 1);
        }

    }
}
