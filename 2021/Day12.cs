using System.Collections.Immutable;
using AdventOfCode.Commons;
using MoreLinq;

namespace AdventOfCode._2021
{
    public class Day12 : AbstractDay
    {
        #region Init

        private static List<string> _input = new List<string>
#if TEST
        {
            "start-A",
            "start-b",
            "A-c",
            "A-b",
            "b-d",
            "A-end",
            "b-end",
        };
#else
            ();
#endif

        public Day12() : base(12)
        {
        }

        #endregion


        private Dictionary<string, List<string>> _caves;

        public void ExecuteDay()
        {
#if !TEST
            _input = this.Parser.Parse();
#endif

            this._caves = new Dictionary<string, List<string>>();

            foreach (string input in _input)
            {
                var f = input.Split('-')[0];
                var t = input.Split('-')[1];
                if (!this._caves.ContainsKey(f))
                    this._caves[f] = new List<string>();
                if (!this._caves.ContainsKey(t))
                    this._caves[t] = new List<string>();
                this._caves[f].Add(t);
                this._caves[t].Add(f);
            }

            this.PartA = 0;//Run("start", new List<string>());
            this.PartB = this.Run("start", new List<string>(), true); ;
        }

        private int Run(string current, List<string> visited, bool allowTwoVisit = false)
        {
            if (current == "end")
                return 1;
            if ((!allowTwoVisit && current.Any(char.IsLower) && visited.Contains(current))
                || (allowTwoVisit && current.Any(char.IsLower) 
                                  && visited.Count(v => v == current) > 1)
                || current == "start" && visited.Contains(current))
                return 0;

            visited.Add(current);
            int paths = 0;
            foreach (var neighbor in this._caves[current])
            {
                paths += this.Run(neighbor, new List<string>(visited), allowTwoVisit);
            }

            Console.WriteLine(string.Join(" - ", visited));

            return paths;
        }
    }
}