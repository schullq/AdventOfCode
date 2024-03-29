using MoreLinq;

namespace AdventOfCode._2022
{
    public partial class Day04
    {
        public void ExecuteDayLinq()
        {
#if !TEST
            _input = Parser.Parse();
#endif
            this.PartA = _input.Select(l => l.Split(','))
                .Select(l => (
                    a: (s: int.Parse(l[0].Split('-')[0]),
                        e: int.Parse(l[0].Split('-')[1])),
                    b: (s: int.Parse(l[1].Split('-')[0]),
                        e: int.Parse(l[1].Split('-')[1]))))
                .Count(l => l.a.s >= l.b.s && l.a.e <= l.b.e || 
                            l.b.s >= l.a.s && l.b.e <= l.a.e);

            this.PartB = _input.Select(l => l.Split(','))
                .Select(l => (
                    a: (s: int.Parse(l[0].Split('-')[0]),
                        e: int.Parse(l[0].Split('-')[1])),
                    b: (s: int.Parse(l[1].Split('-')[0]),
                        e: int.Parse(l[1].Split('-')[1]))))
                .Count(l => l.a.s >= l.b.s && l.a.e <= l.b.e || 
                            l.b.s >= l.a.s && l.b.e <= l.a.e ||
                            l.a.e >= l.b.s && l.a.s <= l.b.e ||
                            l.b.e >= l.a.s && l.b.s <= l.a.e);
        }
    }
}