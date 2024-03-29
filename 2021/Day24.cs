using System;
using System.Collections.Generic;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;
using AdventOfCode.Commons;
using MoreLinq;

namespace AdventOfCode._2021
{
    public class Day24 : AbstractDay
    {
        #region Init

        private static List<string> _input = new List<string>
#if TEST
        {
"inp w",
"add z w",
"mod z 2",
"div w 2",
"add y w",
"mod y 2",
"div w 2",
"add x w",
"mod x 2",
"div w 2",
"mod w 2"
        };
#else
            ();
#endif

        public Day24() : base(24)
        { }

        #endregion

        private List<int> addX = new List<int>();
        private List<int> divZ = new List<int>();
        private List<int> addY = new List<int>();

        public void ExecuteDay()
        {
#if !TEST
            _input = this.Parser.Parse();
#endif
            Regex regex = new Regex(@"^(?<inst>inp|mul|add|mod|div|eql) (?<var>[w-z]{1}) ?(?<value>[\dw-z]+)?$");

            IEnumerable<(string inst, string var, string value)> instructions = _input.Select(l => regex.Match(l))
               .Select(m => (inst: m.Groups["inst"]
                       .Value, var: m.Groups["var"]
                       .Value, value: m.Groups["value"]
                       .Value)
                );

            long max = -1;

            Dictionary<string, long> vars = new Dictionary<string, long>
            {
                { "w", 0 },
                { "x", 0 },
                { "y", 0 },
                { "z", 0 },
            };


            foreach (var i in instructions)
            {
                switch (i.inst)
                {
                    case "add" when i.var == "x" && i.value != "z":
                        this.addX.Add(int.Parse(i.value));
                        break;
                    case "div" when i.var == "z":
                        this.divZ.Add(int.Parse(i.value));
                        break;
                    case "add" when i.var == "y":
                        this.addY.Add(int.Parse(i.value));
                        break;
                }
            }



            
            this.PartA = max;

            this.PartB = 0;
        }

        private int ProcessInst(int i,
            int z,
            int w
        )
        {
            int x = this.addX[i] + z % 26;
            z = z / this.divZ[i];
            if (x != w)
            {
                z *= 26;
                z += w + this.addY[i];
            }

            return z;
        }
    }
}