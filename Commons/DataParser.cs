using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace AdventOfCode.Commons
{
    public class DataParser
    {
        private static readonly string INPUT_PATH = @"C:\Users\Quentin SCHULLER\Documents\Projets\AdventOfCode\input.txt";
        public string SessionCookie { get; set; }
        public string Input { get; set; }

        public DataParser()
        {
            this.Input = File.ReadAllText(INPUT_PATH);
        }

        public DataParser(string sessionCookie)
        {
            this.SessionCookie = sessionCookie;
        }

        public List<int> ParseInt(char delim = '\n')
        {
            return this.Input
               .Split(delim, StringSplitOptions.RemoveEmptyEntries)
               .Select(int.Parse)
               .ToList();
        }

        public List<byte> ParseByte(char delim = '\n')
        {
            return this.Input.Split(delim, StringSplitOptions.RemoveEmptyEntries).Select(byte.Parse).ToList();
        }

        public List<string> Parse(char delim = '\n', bool keepEmptyLines = false)
        {
            return this.Input
               .Split(delim, keepEmptyLines ? StringSplitOptions.None : StringSplitOptions.RemoveEmptyEntries)
               .ToList();
        }

        public List<char[]> ParseCharArrayList(char delim = '\n', bool keepEmptyLines = false)
        {
            return this.Input
               .Split(delim, keepEmptyLines ? StringSplitOptions.None : StringSplitOptions.RemoveEmptyEntries)
               .Select(x => x.ToCharArray())
               .ToList();
        }

        public List<char> ParseCharList()
        {
            return this.Input
                .ToCharArray()
                .ToList();
        }

        public async Task FetchData(string url)
        {
            if (string.IsNullOrEmpty(this.SessionCookie))
                throw new ArgumentNullException("SessionCookie");
            var baseAddress = new Uri(url);
            var cookieContainer = new CookieContainer();
            using var handler = new HttpClientHandler { CookieContainer = cookieContainer };
            using var client = new HttpClient(handler) { BaseAddress = baseAddress };
            cookieContainer.Add(baseAddress, new Cookie("session", this.SessionCookie));
            this.Input = await client.GetStringAsync(url);
        }
    }
}
