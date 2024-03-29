using AdventOfCode.Commons;

namespace AdventOfCode._2022
{
    public partial class Day07
    {
        public void ExecuteDayLinq()
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

    }
}