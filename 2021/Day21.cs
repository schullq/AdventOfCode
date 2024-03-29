using System;
using System.Collections.Generic;
using AdventOfCode.Commons;

namespace AdventOfCode._2021
{
    public class Day21 : AbstractDay
    {
        #region Init

        private static List<string> _input = new List<string>
#if TEST
        {
            "Player 1 starting position: 4", "Player 2 starting position: 8"
        };
#else
            ();
#endif

        public Day21() : base(21)
        { }

        #endregion

        public void ExecuteDay()
        {
#if !TEST
            _input = Parser.Parse();
#endif
            (int start, int score) p1 = (int.Parse(_input[0]
               .Split(' ')
               .Last()
            ), 0);

            (int start, int score) p2 = (int.Parse(_input[1]
               .Split(' ')
               .Last()
            ), 0);

            //int dice = 1;
            //bool turn = true;
            //while (p1.score < 1000 && p2.score < 1000)
            //{
            //    if (turn)
            //    {
            //        var score = (p1.start + Enumerable.Range(dice, 3).Sum()) % 10;
            //        score = score == 0 ? 10 : score;
            //        p1 = (score, p1.score + score);

            //    }
            //    else
            //    {
            //        var score2 = (p2.start + Enumerable.Range(dice, 3).Sum()) % 10;
            //        score2 = score2 == 0 ? 10 : score2;
            //        p2 = (score2, p2.score + score2);
            //    }

            //    dice += 3;
            //    turn = !turn;
            //}

            //dice--;
            //this.PartA = turn ? p1.score * dice : p2.score * dice;

            static (int, int) Run((int start, int score) p1, (int start, int score) p2, bool turn)
            {
                (int p1, int p2) wins = (0, 0);

                foreach (int l1 in Enumerable.Range(1, 3))
                    foreach (int l2 in Enumerable.Range(1, 3))
                        foreach (int l3 in Enumerable.Range(1, 3))
                        {
                            if (turn)
                            {
                                var score = (p1.start + l1 + l2 + l3) % 10;
                                score = score == 0 ? 10 : score;
                                if (p1.score + score >= 21)
                                    wins = (wins.p1 + 1, wins.p2);
                                else
                                {
                                    (int p1, int p2) ret = Run((score, p1.score + score), p2, !turn);
                                    wins = (wins.p1 + ret.p1, wins.p2 + ret.p2);
                                }
                            }
                            else
                            {
                                var score = (p2.start + 1) % 10;
                                score = score == 0 ? 10 : score;
                                if (p2.score + score >= 21)
                                    wins = (wins.p1, wins.p2 + 1);
                                else
                                {
                                    (int p1, int p2) ret = Run(p1, (score, p2.score + score), !turn);
                                    wins = (wins.p1 + ret.p1, wins.p2 + ret.p2);
                                }
                            }
                        }

                return wins;
            }

            (int p1, int p2) ret = Run(p1, p2, true);

            this.PartB = Math.Max(ret.p1, ret.p2);
        }
    }
}
