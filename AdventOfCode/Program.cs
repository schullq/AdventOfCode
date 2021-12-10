using System;
using System.Threading.Tasks;
using AdventOfCode.Commons;
using Microsoft.Extensions.Configuration;

namespace AdventOfCode
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var newDay = new _2021.Day10
            {
                Parser = new DataParser(config["aoc_session"])
            };

            await newDay.Parser.FetchData(newDay.FilePath);
            newDay.ExecuteDay();

            Console.WriteLine("Answer 1: " + newDay.PartA);
            Console.WriteLine("Answer 2: " + newDay.PartB);
        }
    }
}
