using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using AdventOfCode.Commons;
using MoreLinq;

namespace AdventOfCode._2021
{
    public class Day19 : AbstractDay
    {
        #region Init

        private static List<string> _input = new List<string>
#if TEST
        {
            "--- scanner 0 ---",
            "404,-588,-901",
            "528,-643,409",
            "-838,591,734",
            "390,-675,-793",
            "-537,-823,-458",
            "-485,-357,347",
            "-345,-311,381",
            "-661,-816,-575",
            "-876,649,763",
            "-618,-824,-621",
            "553,345,-567",
            "474,580,667",
            "-447,-329,318",
            "-584,868,-557",
            "544,-627,-890",
            "564,392,-477",
            "455,729,728",
            "-892,524,684",
            "-689,845,-530",
            "423,-701,434",
            "7,-33,-71",
            "630,319,-379",
            "443,580,662",
            "-789,900,-551",
            "459,-707,401",
            "--- scanner 1 ---",
            "686,422,578",
            "605,423,415",
            "515,917,-361",
            "-336,658,858",
            "95,138,22",
            "-476,619,847",
            "-340,-569,-846",
            "567,-361,727",
            "-460,603,-452",
            "669,-402,600",
            "729,430,532",
            "-500,-761,534",
            "-322,571,750",
            "-466,-666,-811",
            "-429,-592,574",
            "-355,545,-477",
            "703,-491,-529",
            "-328,-685,520",
            "413,935,-424",
            "-391,539,-444",
            "586,-435,557",
            "-364,-763,-893",
            "807,-499,-711",
            "755,-354,-619",
            "553,889,-390",
            "--- scanner 2 ---",
            "649,640,665",
            "682,-795,504",
            "-784,533,-524",
            "-644,584,-595",
            "-588,-843,648",
            "-30,6,44",
            "-674,560,763",
            "500,723,-460",
            "609,671,-379",
            "-555,-800,653",
            "-675,-892,-343",
            "697,-426,-610",
            "578,704,681",
            "493,664,-388",
            "-671,-858,530",
            "-667,343,800",
            "571,-461,-707",
            "-138,-166,112",
            "-889,563,-600",
            "646,-828,498",
            "640,759,510",
            "-630,509,768",
            "-681,-892,-333",
            "673,-379,-804",
            "-742,-814,-386",
            "577,-820,562",
            "--- scanner 3 ---",
            "-589,542,597",
            "605,-692,669",
            "-500,565,-823",
            "-660,373,557",
            "-458,-679,-417",
            "-488,449,543",
            "-626,468,-788",
            "338,-750,-386",
            "528,-832,-391",
            "562,-778,733",
            "-938,-730,414",
            "543,643,-506",
            "-524,371,-870",
            "407,773,750",
            "-104,29,83",
            "378,-903,-323",
            "-778,-728,485",
            "426,699,580",
            "-438,-605,-362",
            "-469,-447,-387",
            "509,732,623",
            "647,635,-688",
            "-868,-804,481",
            "614,-800,639",
            "595,780,-596",
            "--- scanner 4 ---",
            "727,592,562",
            "-293,-554,779",
            "441,611,-461",
            "-714,465,-776",
            "-743,427,-804",
            "-660,-479,-426",
            "832,-632,460",
            "927,-485,-438",
            "408,393,-506",
            "466,436,-512",
            "110,16,151",
            "-258,-428,682",
            "-393,719,612",
            "-211,-452,876",
            "808,-476,-593",
            "-575,615,604",
            "-485,667,467",
            "-680,325,-822",
            "-627,-443,-432",
            "872,-547,-609",
            "833,512,582",
            "807,604,487",
            "839,-516,451",
            "891,-625,532",
            "-652,-548,-490",
            "30,-46,-14",
        };
#else
            ();
#endif

        public Day19() : base(19)
        { }

        #endregion

        public List<(int x, int y, int z)> _beacons = new List<(int x, int y, int z)>();
        public Dictionary<(int a, int b), double> _distances = new Dictionary<(int a, int b), double>();

        public List<(int x, int y, int z)> coordRemaps =
            new List<(int x, int y, int z)>
            {
                (0, 1, 2),
                (0, 2, 1),
                (1, 0, 2),
                (1, 2, 0),
                (2, 0, 1),
                (2, 1, 0)
            };

        public List<(int x, int y, int z)> coordNegations = new List<(int x, int y, int z)>
        {
            (1, 1, 1),
            (1, 1, -1),
            (1, -1, 1),
            (1, -1, -1),
            (-1, 1, 1),
            (-1, 1, -1),
            (-1, -1, 1),
            (-1, -1, -1)
        };

        public class Scanner
        {
            public List<(int x, int y, int z)> Beacons { get; set; } = new List<(int x, int y, int z)>();
        }

        public void ExecuteDay()
        {
#if !TEST
            _input = this.Parser.Parse();
#endif

            List<(int x, int y, int z)> currentBeacons = new List<(int x, int y, int z)>();

            //var test = Regex.Split(Parser.Input, "^-{3}\\sscanner\\s\\d+\\s-{3}$", RegexOptions.Multiline)
            //   .Where(x => x.Length > 0)
            //   .Select(y => new Scanner
            //    {
            //       Beacons = y.Split('\n', StringSplitOptions.RemoveEmptyEntries)
            //          .Select(z => z.Split(',').Select(int.Parse))
                      
            //    })
            //   .ToList();


            //foreach (var line in _input)
            //{
            //    if (Regex.IsMatch(line, "^-{3}\\sscanner\\s\\d\\s-{3}"))
            //    {
            //        if (currentBeacons.Count > 0)
            //            Transpose(currentBeacons);
            //        currentBeacons = new List<(int x, int y, int z)>();
            //    }
            //    else
            //    {
            //        var pos = line.Split(',').Select(int.Parse).ToList();
            //        currentBeacons.Add((pos[0], pos[1], pos[2]));
            //    }
            //}

            this.PartA = 0;

            this.PartB = 0;
        }

        private void Transpose(List<(int x, int y, int z)> currentBeacons)
        {
            if (this._beacons.Count == 0)
            {
                this._beacons = new List<(int x, int y, int z)>(currentBeacons);
                this._distances = this.CalculateAllDistances(currentBeacons);
            }
            else
            {
                var currentDistances = this.CalculateAllDistances(currentBeacons);

                List<((int _1, int _2) A, (int _1, int _2) B)> duplicates = currentDistances
                   .Where(x => this._distances.ContainsValue(x.Value))
                   .Select(x => (x.Key, this._distances.First(y => y.Value == x.Value).Key))
                   .ToList();
                List<(int _1, int _2)> uniques = currentDistances.Where(x => !this._distances.ContainsValue(x.Value))
                   .Select(x => x.Key)
                   .ToList();

                if (duplicates.Count >= 12)
                {
                    // vAB = (xB - xA, yB - yA, zB - zA)

                    var A = (this._beacons[duplicates[0].A._1].x, this._beacons[duplicates[0].A._1].y,
                        this._beacons[duplicates[0].A._1].z); // scanner X
                    var B = (this._beacons[duplicates[0].B._1].x, this._beacons[duplicates[0].B._1].y,
                        this._beacons[duplicates[0].B._1].z); // scanner 0;

                    (int x, int y, int z) vector = (B.x - A.x, B.y - A.y, B.z - A.z);

                    //this.Translate(currentBeacons.Where((x, i) => ));
                }
            }
        }

        private void Translate(List<(int x, int y, int z)> currentBeacons, (int x, int y, int z) vector)
        { }

        private Dictionary<(int a, int b), double> CalculateAllDistances(List<(int x, int y, int z)> currentBeacons)
        {
            Dictionary<(int a, int b), double> currentDistances = new Dictionary<(int a, int b), double>();

            for (int i = 0; i < currentBeacons.Count; i++)
            {
                var A = currentBeacons[i];

                for (int j = 0; j < currentBeacons.Count; j++)
                {
                    if (i == j)
                        continue;
                    // if the distance has already been calculated, we continue
                    if (i < j && currentDistances.ContainsKey((i, j)) || i > j && currentDistances.ContainsKey((j, i)))
                        continue;

                    var B = currentBeacons[j];

                    double distance = Math.Sqrt(Math.Pow((B.x - A.x), 2)) +
                                      Math.Pow((B.y - A.y), 2) +
                                      Math.Pow((B.z - A.z), 2);

                    if (i < j)
                        currentDistances[(i, j)] = distance;
                    else
                        currentDistances[(j, i)] = distance;
                }
            }

            return currentDistances;
        }
    }
}
