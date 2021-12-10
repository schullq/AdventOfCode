using System;

namespace AdventOfCode.Commons
{
    public abstract class AbstractDay
    {
        public string FilePath { get; set; }
        public DataParser Parser { get; set; }

        public long PartA { get; set; }
        public long PartB { get; set; }

        protected AbstractDay() : this(DateTime.Now.Day)
        {
        }

        protected AbstractDay(int day) : this(day, DateTime.Now.Year)
        { }

        protected AbstractDay(int day, int year)
        {
            FilePath = $"https://adventofcode.com/{year}/day/{day}/input";
        }
    }
}