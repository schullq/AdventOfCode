using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using AdventOfCode.Commons;
using MoreLinq;

namespace AdventOfCode._2021
{
    public class Day18 : AbstractDay
    {
        #region Init

        private static List<string> _input = new List<string>
#if TEST
        {
            //"[[[[[9,8],1],2],3],4]",
            //"[7,[6,[5,[4,[3,2]]]]]",
            //"[[6,[5,[4,[3,2]]]],1]",
            //"[1,2]",
            //"[[1,2],3]",
            //"[9,[8,7]]",
            //"[[1,9],[8,5]]",
            "[[[[1,2],[3,4]],[[5,6],[7,8]]],9]",
            "[[[9,[3,8]],[[0,9],6]],[[[3,7],[4,9]],3]]",
            "[[[[1,3],[5,3]],[[1,3],[8,7]]],[[[4,9],[6,9]],[[8,2],[7,3]]]]"
        };
#else
            ();
#endif

        public Day18() : base(18)
        { }

        #endregion

        public void ExecuteDay()
        {
#if !TEST
            _input = this.Parser.Parse();
#endif
            string test = "[[[[[4,3],4],4],[7,[[8,4],9]]],[1,1]]";

            var tree = this.ConstructTree(test,
                0,
                test.Length - 1,
                null
            );

            tree.DumpTree();
            Console.WriteLine(string.Join(", ", this._linearList.Select(x => x.v)));
            //Reduce(tree, tree);

            //tree.DumpTree();
            this.PartA = 0;

            this.PartB = 0;
        }

        private List<(int v, Pair p)> _linearList = new List<(int, Pair)>();

        public class Pair
        {
            public Pair? A { get; set; }
            public Pair? B { get; set; }

            public int? AValue { get; set; }
            public int? BValue { get; set; }

            private Pair? _parent;

            public Pair? Parent
            {
                get => this._parent;
                set
                {
                    int i = 0;
                    var parent = value;
                    while (parent != null)
                    {
                        parent = parent.Parent;
                        i++;
                    }

                    this.ParentCount = i;
                    this._parent = value;
                }
            }

            public int ParentCount { get; private set; }

            public void DumpTree()
            {
                Console.Write("[");
                if (this.AValue.HasValue)
                    Console.Write($"{this.AValue}");
                else
                    this.A.DumpTree();
                Console.Write(",");
                if (this.BValue.HasValue)
                    Console.Write($"{this.BValue}");
                else
                    this.B.DumpTree();
                Console.Write("]");

                if (this.Parent == null)
                    Console.WriteLine();
            }
        }

        private void Explode(Pair pair)
        {
            Console.WriteLine($"Explosion needed on [{pair.AValue},{pair.BValue}]");
            var leftIndex = this._linearList.IndexOf(this._linearList.FirstOrDefault(x => x.p == pair)) - 1;
            if (leftIndex >= 0)
            {
                var left = this._linearList[leftIndex].p;
                if (left == pair.Parent)
                {
                    left.AValue = pair.AValue.Value + this._linearList[leftIndex].v;
                    this._linearList[leftIndex] = (left.AValue.Value, left);
                    left.B = null;
                    left.BValue = 0;
                    this._linearList[leftIndex + 1] = (0, left);
                }
                else
                {
                    left.BValue = pair.BValue.Value + this._linearList[leftIndex].v;
                    this._linearList[leftIndex] = (left.BValue.Value, left);
                }
            }

            var rightIndex = this._linearList.IndexOf(this._linearList.LastOrDefault(x => x.p == pair)) + 1;
            if (rightIndex > 0 && rightIndex < this._linearList.Count)
            {
                var right = this._linearList[rightIndex].p;
                if (right == pair.Parent)
                {
                    right.BValue = pair.BValue.Value + this._linearList[rightIndex].v;
                    this._linearList[rightIndex] = (right.BValue.Value, right);
                    right.A = null;
                    right.AValue = 0;
                    this._linearList[rightIndex - 1] = (0, right);
                }
                else
                {
                    right.AValue = pair.AValue.Value + this._linearList[rightIndex].v;
                    this._linearList[rightIndex] = (right.AValue.Value, right);
                }
            }
        }

        private void Split(Pair pair)
        {
            if (pair.AValue is > 9)
            {
                Console.WriteLine($"Split needed on {pair.AValue}");
                pair.A = new Pair
                {
                    Parent = pair,
                    AValue = (int)Math.Round(Convert.ToDouble(pair.AValue) / 2, MidpointRounding.ToZero),
                    BValue = (int)Math.Round(Convert.ToDouble(pair.AValue) / 2, MidpointRounding.AwayFromZero),
                };

                var listIndex = this._linearList.IndexOf((pair.AValue.Value, pair));
                pair.AValue = null;
                this._linearList.Insert(listIndex + 1, (pair.A.AValue.Value, pair.A));
                this._linearList.Insert(listIndex + 2, (pair.A.BValue.Value, pair.A));
                this._linearList.RemoveAt(listIndex);
            }

            if (pair.BValue is > 9)
            {
                Console.WriteLine($"Split needed on {pair.BValue}");
                pair.B = new Pair
                {
                    Parent = pair,
                    AValue = (int)Math.Round(Convert.ToDouble(pair.BValue) / 2, MidpointRounding.ToZero),
                    BValue = (int)Math.Round(Convert.ToDouble(pair.BValue) / 2, MidpointRounding.AwayFromZero),
                };

                var listIndex = this._linearList.IndexOf((pair.BValue.Value, pair));
                pair.BValue = null;
                this._linearList.Insert(listIndex + 1, (pair.B.AValue.Value, pair.B));
                this._linearList.Insert(listIndex + 2, (pair.B.BValue.Value, pair.B));
                this._linearList.RemoveAt(listIndex);
            }
        }

        private void Reduce(Pair root, Pair? current)
        {
            if (current == null)
                return;
            if (current.ParentCount >= 4)
            {
                root.DumpTree();
                this.Explode(current);
                this.Reduce(root, root);
            }

            if (current.AValue > 9 || current.BValue > 9)
            {
                root.DumpTree();
                this.Split(current);
                this.Reduce(root, root);
            }

            this.Reduce(root, current.A);
            this.Reduce(root, current.B);
            root.DumpTree();
        }

        private string Addition(string a, string b)
        {
            return $"[{a},{b}]";
        }

        private Pair? ConstructTree(string numbers,
            int si,
            int ei,
            Pair? parent
        )
        {
            // Base case
            if (si > ei)
                return null;

            // new root
            Pair root = new Pair() { Parent = parent };
            int index = -1;
            bool alreadyAdded = false;
            if (char.IsDigit(numbers[si + 1]))
            {
                root.AValue = numbers[si + 1] - '0';
                alreadyAdded = true;
                if (root.Parent != null)
                {
                    Pair parentTmp = root;
                    int linearIndex;
                    do
                    {
                        parentTmp = parentTmp.Parent;
                        linearIndex = this._linearList.IndexOf(this._linearList.FirstOrDefault(x => x.p == parentTmp));
                        if (linearIndex > -1) break;
                    } while (parentTmp != null);

                    if (parentTmp == null)
                        this._linearList.Add((root.AValue.Value, root));
                    else if (!parentTmp.BValue.HasValue)
                        this._linearList.Insert(linearIndex + 1, (root.AValue.Value, root));
                    else
                        this._linearList.Insert(linearIndex, (root.AValue.Value, root));
                }
                else
                    this._linearList.Add((root.AValue.Value, root));
            }

            if (char.IsDigit(numbers[ei - 1]))
            {
                root.BValue = numbers[ei - 1] - '0';
                if (alreadyAdded)
                {
                    var linearIndex = this._linearList.IndexOf(this._linearList.FirstOrDefault(x => x.p == root));

                    this._linearList.Insert(linearIndex + 1, (root.BValue.Value, root));
                }
                else
                {
                    Pair parentTmp = root;
                    int linearIndex;
                    do
                    {
                        parentTmp = parentTmp.Parent;
                        linearIndex = this._linearList.IndexOf(this._linearList.FirstOrDefault(x => x.p == parentTmp));
                        if (linearIndex > -1) break;
                    } while (parentTmp != null);

                    if (parentTmp == null)
                        this._linearList.Add((root.BValue.Value, root));
                    else if (!parentTmp.AValue.HasValue)
                        this._linearList.Insert(linearIndex + 1, (root.BValue.Value, root));
                    else
                        this._linearList.Insert(linearIndex, (root.BValue.Value, root));
                }
            }

            if (root.AValue.HasValue && root.BValue.HasValue)
                return root;

            int ci = this.FindCommaIndex(numbers,
                si,
                ei
            );

            // if next char is '[' find the index of
            // its complement ']'
            if (si + 1 <= ci - 1 && numbers[si + 1] == '[')
            {
                index = this.FindIndex(numbers,
                    si + 1,
                    ci - 1
                );
                // if index found
                if (index != -1)
                {
                    root.A = this.ConstructTree(numbers,
                        si + 1,
                        index,
                        root
                    );
                }
            }

            if (si + 1 <= ei - 1 && numbers[ci + 1] == '[')
            {
                index = this.FindIndex(numbers,
                    ci + 1,
                    ei - 1
                );
                // if index found
                if (index != -1)
                {
                    root.B = this.ConstructTree(numbers,
                        ci + 1,
                        index,
                        root
                    );
                }
            }

            return root;
        }

        public int FindCommaIndex(string str,
            int si,
            int ei
        )
        {
            if (si > ei)
                return -1;

            // Inbuilt stack
            Stack<char> s = new Stack<char>();
            int ci = -1;
            for (int i = si; i <= ei; i++)
            {
                switch (str[i])
                {
                    case '[':
                        s.Push(str[i]);
                        break;
                    case ']':
                    {
                        if (s.Peek() == '[')
                        {
                            s.Pop();
                        }

                        break;
                    }
                    case ',' when s.Count == 1:
                        ci = i;
                        break;
                }
            }

            return ci;
        }

        public int FindIndex(string str,
            int si,
            int ei
        )
        {
            if (si > ei)
                return -1;

            // Inbuilt stack
            Stack<char> s = new Stack<char>();
            for (int i = si; i <= ei; i++)
            {
                switch (str[i])
                {
                    case '[':
                        s.Push(str[i]);
                        break;
                    case ']':
                    {
                        if (s.Peek() == '[')
                        {
                            s.Pop();

                            if (s.Count == 0)
                                return i;
                        }

                        break;
                    }
                }
            }

            return -1;
        }
    }
}
