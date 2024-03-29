using System;
using System.Collections.Generic;
using AdventOfCode.Commons;

namespace AdventOfCode._2021
{
    public class Day23 : AbstractDay
    {
        #region Init

        private static List<string> _input = new List<string>
#if TEST
        {
"#############",
"#...........#",
"###B#C#B#D###",
"  #A#D#C#A#  ",
"  #########  ",
        };
#else
            ();
#endif

        public Day23() : base(23)
        { }

        #endregion

        public void ExecuteDay()
        {
#if !TEST
            _input = this.Parser.Parse();
#endif

            this.PartA = Run(0, _input);

            this.PartB = 0;
        }

        private int Run(int currentEnergy, List<string> input)
        {
            if (this.AreInPlace('A', input) && this.AreInPlace('B', input) && this.AreInPlace('C', input) && this.AreInPlace('D', input))
                return currentEnergy;

            var canMove = CanBeMoved(input);

            foreach (var c in canMove)
            {
                var possiblePositions = PossiblePositions(c, input);
            }

            return 0;
        }

        private List<(int x, int y)> PossiblePositions((int x, int y) c, List<string> input)
        {
            var positions = new List<(int, int)>();

            if (c.y == 1)
            {
                for (int i = c.x + 1; i < input[c.y].Length - 1; i++)
                {
                    if (input[c.y][i] == '.' && input[c.y + 1][i] == '#')
                        positions.Add((i, c.y));
                    if (input[c.y + 1][i] == '.')
                        positions.Add((i, c.y + 1));
                    if (input[c.y + 2][i] == '.')
                        positions.Add((i, c.y + 1));
                }

                for (int i = c.x - 1; i > 0; i--)
                {
                    if (input[c.y][i] == '.' && input[c.y + 1][i] == '#')
                        positions.Add((i, c.y));
                    if (input[c.y + 1][i] == '.')
                        positions.Add((i, c.y + 1));
                    if (input[c.y + 2][i] == '.')
                        positions.Add((i, c.y + 1));
                }
            }
            else if (c.y == 2)
            {
                for (int i = c.x + 1; i < input[c.y - 1].Length - 1; i++)
                {
                    if (input[c.y - 1][i] == '.' && input[c.y][i] == '#')
                        positions.Add((i, c.y - 1));
                    if (input[c.y][i] == '.')
                        positions.Add((i, c.y));
                    if (input[c.y + 1][i] == '.')
                        positions.Add((i, c.y + 1));
                }

                for (int i = c.x - 1; i > 0; i--)
                {
                    if (input[c.y - 1][i] == '.' && input[c.y][i] == '#')
                        positions.Add((i, c.y - 1));
                    if (input[c.y][i] == '.')
                        positions.Add((i, c.y));
                    if (input[c.y + 1][i] == '.')
                        positions.Add((i, c.y + 1));
                }
            }
            else if (c.y == 3)
            {
                for (int i = c.x + 1; i < input[c.y - 2].Length - 1; i++)
                {
                    if (input[c.y - 2][i] == '.' && input[c.y - 1][i] == '#')
                        positions.Add((i, c.y - 1));
                    if (input[c.y - 1][i] == '.')
                        positions.Add((i, c.y));
                    if (input[c.y][i] == '.')
                        positions.Add((i, c.y + 1));
                }

                for (int i = c.x - 1; i > 0; i--)
                {
                    if (input[c.y - 2][i] == '.' && input[c.y - 1][i] == '#')
                        positions.Add((i, c.y - 1));
                    if (input[c.y - 1][i] == '.')
                        positions.Add((i, c.y));
                    if (input[c.y][i] == '.')
                        positions.Add((i, c.y + 1));
                }
            }
            return positions;
        }

        private List<(int x, int y)> CanBeMoved(List<string> input)
        {
            var canMove = new List<(int, int)>();
            string chars = "ABCD";
            for (int i = 1; i < input.Count - 1; i++)
                for (int j = 1; j < input[i].Length; j++)
                {
                    if (!chars.Contains(input[i][j]))
                        continue;
                    var neighbors = (j, i).GetCartesianNeighbors();
                    if (neighbors.Any(n => input[n.y][n.x] == '.'))
                        canMove.Add((j, i));
                }

            return canMove;
        }

        private List<string> Swap((int x, int y) indexA, (int x, int y) indexB, List<string> input)
        {
            List<string> copy = new List<string>(input);

            var charArrayA = copy[indexA.y].ToCharArray();
            var charArrayB = copy[indexB.y].ToCharArray();

            var tmp = copy[indexA.y][indexA.x];
            charArrayA[indexA.x] = copy[indexB.y][indexB.x];
            charArrayB[indexB.x] = tmp;

            copy[indexA.y] = new string(charArrayA);
            copy[indexB.y] = new string(charArrayB);

            return new List<string>();
        }

        private bool AreInPlace(char pod, IReadOnlyList<string> input)
        {
            return input[2].IndexOf(pod) == input[3].IndexOf(pod);
        }
    }
}
