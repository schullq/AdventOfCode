using System;
using System.Collections.Generic;
using System.Reflection;
using AdventOfCode.Commons;
using MoreLinq;
using MoreLinq.Extensions;

namespace AdventOfCode._2022
{
    public partial class Day08 : AbstractDay
    {
        #region Init

        private static List<string> _input = new List<string>
#if TEST
        {
            "30373",
            "25512",
            "65332",
            "33549",
            "35390",
        };
#else
            ();
#endif

        public Day08() : base(08)
        {
        }

        #endregion

        public void ExecuteDay()
        {
#if !TEST
            _input = Parser.Parse();
#endif
            int totalA = 0;
            int totalB = 0;

            List<List<int>> trees = _input
                .Select(x => x
                    .Chunk(1)
                    .Select(y => int.Parse(y))
                    .ToList())
                .ToList();

            Dictionary<(int x, int y), bool> validTrees = new Dictionary<(int x, int y), bool>();

            for (int i = 0; i < trees.Count; i++)
            {
                if (i == 0 || i == trees.Count - 1)
                    for (int j = 0; j < trees[i].Count; j++)
                        validTrees[(i, j)] = true;
                else
                {
                    validTrees[(i, 0)] = true;
                    validTrees[(i, trees[0].Count - 1)] = true;
                }
            }

            // Gauche vers la droite
            int bigger;
            for (int y = 1; y < trees.Count - 1; y++)
            {
                bigger = trees[y][0];
                for (int x = 1; x < trees[y].Count - 1; x++)
                {
                    if (trees[y][x] > bigger)
                    {
                        validTrees[(x, y)] = true;
                        bigger = trees[y][x];
                    }
                }
            }

            // Droite vers la gauche
            for (int y = 1; y < trees.Count - 1; y++)
            {
                bigger = trees[y][^1];
                for (int x = trees[y].Count - 2; x > 0; x--)
                {
                    if (trees[y][x] > bigger)
                    {
                        validTrees[(x, y)] = true;
                        bigger = trees[y][x];
                    }
                }
            }

            // Haut vers le bas
            for (int x = 1; x < trees[0].Count - 1; x++)
            {
                bigger = trees[0][x];
                for (int y = 1; y < trees.Count - 1; y++)
                {
                    if (trees[y][x] > bigger)
                    {
                        validTrees[(x, y)] = true;
                        bigger = trees[y][x];
                    }
                }
            }

            // Bas vers le haut
            for (int x = 1; x < trees[0].Count - 1; x++)
            {
                bigger = trees[^1][x];
                for (int y = trees.Count - 2; y > 0; y--)
                {
                    if (trees[y][x] > bigger)
                    {
                        validTrees[(x, y)] = true;
                        bigger = trees[y][x];
                    }
                }
            }

            foreach (var tree in validTrees.Where(x => x.Key.x != 0 && x.Key.x != trees[0].Count - 1 && x.Key.y != 0 && x.Key.y != trees.Count - 1))
            {
                int score = 1;
                int x, y;

                // Gauche vers la droite
                x = tree.Key.x;
                while (++x < trees[0].Count)
                {
                    if (trees[tree.Key.y][x] >= trees[tree.Key.y][tree.Key.x])
                        break;
                    if (x + 1 == trees[0].Count)
                        break;
                }

                score *= x - tree.Key.x;

                // Droite vers la gauche
                x = tree.Key.x;
                while (--x >= 0)
                {
                    if (trees[tree.Key.y][x] >= trees[tree.Key.y][tree.Key.x])
                        break;
                    if (x - 1 < 0)
                        break;
                }

                score *= tree.Key.x - x;

                // Haut vers le bas
                y = tree.Key.y;
                while (++y < trees.Count)
                {
                    if (trees[y][tree.Key.x] >= trees[tree.Key.y][tree.Key.x])
                        break;
                    if (y + 1 == trees.Count)
                        break;
                }

                score *= y - tree.Key.y;

                // Bas vers le haut
                y = tree.Key.y;
                while (--y >= 0)
                {
                    if (trees[y][tree.Key.x] >= trees[tree.Key.y][tree.Key.x])
                        break;
                    if (y - 1 < 0)
                        break;
                }

                score *= tree.Key.y - y;

                if (score > totalB) totalB = score;
            }

            this.PartA = validTrees.Values.Count;
            this.PartB = totalB;
        }
    }
}