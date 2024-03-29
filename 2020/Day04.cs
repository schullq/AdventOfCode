using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using AdventOfCode.Commons;
using MoreLinq;

namespace AdventOfCode._2020
{
    public class Day04 : AbstractDay
    {
        #region Init

        private static List<string> _input = new List<string>
#if TEST
        {
            "eyr:2021 cid:100",
            "hcl:#18171d ecl:kkk hgt:170 pid:186cm iyr:2018 byr:1926",
            "",
            "iyr:2019",
            "hcl:#602927 eyr:1967 hgt:170cm",
            "ecl:grn pid:012533040 byr:1946",
            "",
            "hcl:dab227 iyr:2012",
            "ecl:brn hgt:182cm pid:021572410 eyr:2020 byr:1992 cid:277",
            "",
            "hgt:59cm ecl:zzz",
            "eyr:2038 hcl:74454a iyr:2023",
            "pid:3556412378 byr:2007",
            "",
            "pid:087499704 hgt:74in ecl:grn iyr:2012 eyr:2030 byr:1980",
            "hcl:#623a2f",
            "",
            "eyr:2029 ecl:blu cid:129 byr:1989",
            "iyr:2014 pid:896056539 hcl:#a97842 hgt:165cm",
            "",
            "hcl:#888785",
            "hgt:164cm byr:2001 iyr:2015 cid:88",
            "pid:545766238 ecl:hzl",
            "eyr:2022",
            "",
            "iyr:2050 hgt:158cm hcl:#b6652a ecl:blu byr:1944 eyr:2021 pid:093154719",
            "",
            "byr:2002 iyr:2020 eyr:2030 hgt:193cm hcl:#123abf ecl:oth pid:999999999 cid:tamere",
            ""
        };
#else
            ();
#endif

        public Day04() : base(04)
        { }

        #endregion

        private List<string> _neededFields = new List<string>
        {
            "byr",
            "iyr",
            "eyr",
            "hgt",
            "hcl",
            "ecl",
            "pid"
        };

        public void ExecuteDay()
        {
#if !TEST
            _input = this.Parser.Parse(keepEmptyLines: true);
#endif
            Dictionary<string, string> _fields = new Dictionary<string, string>();
            int validA = 0;
            int validB = 0;

            int i = 0;

            var passports = _input.Segment(string.IsNullOrWhiteSpace).Select(p => p.SelectMany(s => s.Split()).Where(s => !string.IsNullOrWhiteSpace(s)).Select(s => s.Split(":")).ToDictionary(x => x[0], x => x[1])).ToList();


            //foreach (var line in _input)
            //{
            //    if (line == "")
            //    {
            //        if (_fields.Count == 8 && _fields.Keys.All(x => this._neededFields.Any(y => y == x)) ||
            //            _fields.Count == 7 &&
            //            _fields.Keys.All(x => this._neededFields.Any(y => y == x)) &&
            //            !_fields.ContainsKey("cid"))
            //        {
            //            validA++;
            //            bool isValid = true;

            //            isValid &= int.Parse(_fields["byr"]) >= 1920 && int.Parse(_fields["byr"]) <= 2002;
            //            isValid &= int.Parse(_fields["iyr"]) >= 2010 && int.Parse(_fields["iyr"]) <= 2020;
            //            isValid &= int.Parse(_fields["eyr"]) >= 2020 && int.Parse(_fields["eyr"]) <= 2030;
            //            isValid &= Regex.Match(_fields["hgt"], "^\\d+(cm|in)$").Success &&
            //                       (_fields["hgt"].EndsWith("cm")
            //                           ? int.Parse(_fields["hgt"][..^2]) >= 150 &&
            //                             int.Parse(_fields["hgt"][..^2]) <= 193
            //                           : int.Parse(_fields["hgt"][..^2]) >= 59 &&
            //                             int.Parse(_fields["hgt"][..^2]) <= 76);
            //            isValid &= Regex.Match(_fields["hcl"], "^#[0-9a-f]{6}").Success;
            //            isValid &= (new List<string>
            //            {
            //                "amb",
            //                "blu",
            //                "brn",
            //                "gry",
            //                "grn",
            //                "hzl",
            //                "oth"
            //            }).Contains(_fields["ecl"]);
            //            isValid &= Regex.Match(_fields["pid"], "^\\d{9}").Success;

            //            validB += isValid ? 1 : 0;

            //            //Console.WriteLine($"{i++} is {(isValid ? "valid" : "invalid")}");
            //        }

            //        _fields = new Dictionary<string, string>();
            //    }
            //    else
            //    {
            //        var elem = line.Split(" ").Select(x => (x.Split(":")[0], x.Split(":")[1])).ToList();
            //        foreach (var field in elem)
            //        {
            //            if (_fields.ContainsKey(field.Item1))
            //                break;
            //            _fields.Add(field.Item1, field.Item2);
            //        }
            //    }
            //}

            this.PartA = passports
               .Count(p => this._neededFields.All(r => p.ContainsKey(r)));

            var isValidPassports = passports
               .Select(p => (
                    p,
                    isValid: this._neededFields.All(r => p.ContainsKey(r))
                             && int.Parse(p["byr"]) >= 1920 && int.Parse(p["byr"]) <= 2002
                             && int.Parse(p["iyr"]) >= 2010 && int.Parse(p["iyr"]) <= 2020
                             && int.Parse(p["eyr"]) >= 2020 && int.Parse(p["eyr"]) <= 2030
                             && IsValidHeight(p["hgt"])
                             && Regex.IsMatch(p["hcl"], "^#[0-9a-f]{6}$")
                             && new[] { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" }.Contains(p["ecl"])
                             && Regex.IsMatch(p["pid"], "^\\d{9}$")))
               .ToArray();

            this.PartB = isValidPassports.Count(x => x.isValid);

            static bool IsValidHeight(string s) =>
                s.Length >= 4
                && (s[^2..] switch
                {
                    "in" => int.Parse(s[..^2]) >= 59 && int.Parse(s[..^2]) <= 76,
                    "cm" => int.Parse(s[..^2]) >= 150 && int.Parse(s[..^2]) <= 193,
                    _ => false,
                });
        }
    }
}
