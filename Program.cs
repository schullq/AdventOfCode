using System;
using System.Threading.Tasks;
using AdventOfCode.Commons;
using Microsoft.Extensions.Configuration;

namespace AdventOfCode
{
    public class Program
    {
        static void Main(string[] args)
        {

            var newDay = new _2022.Day09
            {
                Parser = new DataParser()
            };

            newDay.ExecuteDay();

            Console.WriteLine("Answer 1: " + newDay.PartA);
            Console.WriteLine("Answer 2: " + newDay.PartB);
        }
    }
}
