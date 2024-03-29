using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using AdventOfCode.Commons;
using MoreLinq;

namespace AdventOfCode._2022
{
    public partial class Day11 : AbstractDay
    {
        #region Init

        private static List<string> _input = new List<string>
#if TEST
        {
            "Monkey 0:",
            "Starting items: 79, 98",
            "Operation: new = old * 19",
            "Test: divisible by 23",
            "If true: throw to monkey 2",
            "If false: throw to monkey 3",
            "",
            "Monkey 1:",
            "Starting items: 54, 65, 75, 74",
            "Operation: new = old + 6",
            "Test: divisible by 19",
            "If true: throw to monkey 2",
            "If false: throw to monkey 0",
            "",
            "Monkey 2:",
            "Starting items: 79, 60, 97",
            "Operation: new = old * old",
            "Test: divisible by 13",
            "If true: throw to monkey 1",
            "If false: throw to monkey 3",
            "",
            "Monkey 3:",
            "Starting items: 74",
            "Operation: new = old + 3",
            "Test: divisible by 17",
            "If true: throw to monkey 0",
            "If false: throw to monkey 1",
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
            _input = this.Parser.Parse(keepEmptyLines: true);
#endif
            List<Monkey> monkeysA = new List<Monkey>();
            List<Monkey> monkeysB = new List<Monkey>();
            long common = 1;

            foreach (var l in _input.Select(e => e.Trim()).Split(""))
            {
                var m = l.ToList();

                // Numéro
                Regex numbers = new Regex("(?<numbers>\\d+)");
                int n = int.Parse(numbers.Match(m[0]).Groups["numbers"].Value);

                // Starting items
                var itemsA = new List<long>();
                var itemsB = new List<long>();
                foreach (Match match in numbers.Matches(m[1]))
                {
                    itemsA.Add(long.Parse(match.Value));
                    itemsB.Add(long.Parse(match.Value));
                }

                // Operation
                var o = m[2].Split('=')[1].Trim().Split(' ');
                bool isA = long.TryParse(o[0], out long a);
                bool isB = long.TryParse(o[2], out long b);
                Func<long, long> f = o[1] switch
                {
                    "+" => old => isA ? a + (isB ? b : old) : old + (isB ? b : old),
                    "*" => old => isA ? a * (isB ? b : old) : old * (isB ? b : old),
                };

                // Test
                long test = long.Parse(numbers.Match(m[3]).Value);
                int testA = int.Parse(numbers.Match(m[4]).Value);
                int testB = int.Parse(numbers.Match(m[5]).Value);
                common *= test;

                monkeysA.Add(new Monkey(n, itemsA, f, test, testA, testB));
                monkeysB.Add(new Monkey(n, itemsB, f, test, testA, testB));
            }

            for (int i = 0; i < 20; i++)
            {
                foreach (var monkey in monkeysA)
                {
                    var repartition = monkey.InspectItems();

                    foreach (var v in repartition)
                    {
                        monkeysA[v.id].Items.Add(v.item);
                    }
                }
            }

            var orderedMonkeys = monkeysA.OrderByDescending(x => x.InspectedItems).Select(x => x.InspectedItems);

            this.PartA = orderedMonkeys.First() * orderedMonkeys.Skip(1).First();

            for (int i = 0; i < 10000; i++)
            {
                foreach (var monkey in monkeysB)
                {
                    var repartition = monkey.InspectItems(common);

                    foreach (var v in repartition)
                    {
                        monkeysB[v.id].Items.Add(v.item);
                    }
                }
            }

            orderedMonkeys = monkeysB.OrderByDescending(x => x.InspectedItems).Select(x => x.InspectedItems);

            this.PartB = orderedMonkeys.First() * orderedMonkeys.Skip(1).First();
        }

        public class Monkey
        {
            private readonly Func<long, long> _operation;
            private readonly long _test;
            private readonly int _testTrue;
            private readonly int _testFalse;

            public int Id { get; set; }
            public List<long> Items { get; set; }
            public long InspectedItems { get; set; } = 0;

            public Monkey(
                int id,
                List<long> items,
                Func<long, long> operation,
                long test,
                int testTrue,
                int testFalse)
            {
                this.Id = id;
                this.Items = items;
                this._operation = operation;
                this._test = test;
                this._testTrue = testTrue;
                this._testFalse = testFalse;
            }

            public List<(int id, long item)> InspectItems(long c = -1)
            {
                InspectedItems += (long)this.Items.LongCount();
                var r = this.Items
                    .Select(n => this.OperateWorry(n, c))
                    .Select(n => (this.Test(n), n))
                    .ToList();
                this.Items.Clear();
                return r;
            }

            private long OperateWorry(long n, long c) => c < 0 ? this._operation(n) / 3 : this._operation(n) % c;

            private int Test(long n) => n % this._test == 0 ? this._testTrue : this._testFalse;
        }
    }
}