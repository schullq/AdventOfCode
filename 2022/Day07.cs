using System;
using System.Collections.Generic;
using AdventOfCode.Commons;
using MathNet.Numerics.Distributions;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace AdventOfCode._2022
{
    public partial class Day07 : AbstractDay
    {
        #region Init

        private static List<string> _input = new List<string>
#if TEST
        {
"$ cd /",
"$ ls",
"dir a",
"14848514 b.txt",
"8504156 c.dat",
"dir d",
"$ cd a",
"$ ls",
"dir e",
"29116 f",
"2557 g",
"62596 h.lst",
"$ cd e",
"$ ls",
"584 i",
"$ cd ..",
"$ cd ..",
"$ cd d",
"$ ls",
"4060174 j",
"8033020 d.log",
"5626152 d.ext",
"7214296 k",
        };
#else
            ();
#endif

        public Day07() : base(07)
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

            Directory? filesystem = null;
            Directory? current = null;
            bool inLs = false;

            foreach (string line in _input)
            {
                if (line.StartsWith('$'))
                {
                    var cmd = line.Split(' ');

                    switch (cmd[1])
                    {
                        case "cd":
                            inLs = false;
                            switch (cmd[2])
                            {
                                case "/":
                                    filesystem = new Directory(cmd[2], null);
                                    current = filesystem;
                                    break;
                                case "..":
                                    current = current.Parent;
                                    break;
                                default:
                                {
                                    if (current != null && current.Children.All(c => c.Name != cmd[2]))
                                    {
                                        Directory child = new Directory(cmd[2], current);
                                        current.Children.Add(child);
                                        current = child;
                                    }
                                    else
                                        current = current.Children.First(c => c.Name == cmd[2]);

                                    break;
                                }
                            }

                            break;
                        case "ls":
                            inLs = true;
                            break;
                    }
                }
                else if (inLs)
                {
                    var elem = line.Split(' ');
                    switch (elem[0])
                    {
                        case "dir":
                            if (current != null && current.Children.All(c => c.Name != elem[1]))
                            {
                                Directory child = new(elem[1], current);
                                current.Children.Add(child);
                            }

                            break;
                        default:
                            current.Files.Add((elem[1], int.Parse(elem[0])));
                            break;
                    }
                }
            }

#if PRINT
            PrintFilesystem(filesystem);
#endif
            int GetPartASize(Directory current)
            {
                int size = 0;
                if (current != null)
                {
                    foreach (var child in current.Children)
                    {
                        size += GetPartASize(child);
                    }

                    size = current.Size < 100000 ? size + current.Size : size;
                }

                return size;
            }

            this.PartA = GetPartASize(filesystem);

            int fileSystemSize = filesystem.Size;

            IEnumerable<int> GetPartBSize(Directory directory)
            {
                List<int> bigSize = new();
                if (fileSystemSize - directory.Size <= 40000000)
                    bigSize.Add(directory.Size);
                foreach (var child in directory.Children)
                {
                    bigSize.AddRange(GetPartBSize(child));
                }

                return bigSize;
            }

            totalB = GetPartBSize(filesystem).Where(x => fileSystemSize - x <= 40000000).Min();
            this.PartB = totalB;
        }

        internal class Directory
        {
            public string Name { get; set; }

            public Directory? Parent { get; set; }

            public List<Directory> Children { get; set; }

            public List<(string name, int size)> Files { get; set; }

            public int Size => this.Files.Sum(f => f.size) + this.Children.Sum(c => c.Size);

            public Directory(string name, Directory? parent)
            {
                Name = name;
                Parent = parent;
                Children = new List<Directory>();
                Files = new List<(string name, int size)>();
            }
        }

        private static void PrintFilesystem(Directory current, int tab = 0)
        {
            for (int i = 0; i < tab; i++)
            {
                Console.Write('\t');
            }

            Console.WriteLine($"- {current.Name} (dir, size={current.Size})");
            foreach (var child in current.Children)
            {
                PrintFilesystem(child, tab + 1);
            }

            foreach (var file in current.Files)
            {
                for (int i = 0; i < tab + 1; i++)
                {
                    Console.Write('\t');
                }

                Console.WriteLine($"- {file.name} (file, size={file.size})");
            }
        }
    }
}