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
        public string SessionCookie { get; set; }
        public string Input { get; set; }

        public DataParser()
        {
        }

        public DataParser(string sessionCookie)
        {
            SessionCookie = sessionCookie;
        }

        public List<int> ParseInt(char delim = '\n')
        {
            return Input
                .Split(delim, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToList();
        }

        public List<byte> ParseByte(char delim = '\n')
        {
            return Input
                .Split(delim, StringSplitOptions.RemoveEmptyEntries)
                .Select(byte.Parse)
                .ToList();
        }

        public List<string> Parse(char delim = '\n')
        {
            return Input
                .Split(delim, StringSplitOptions.RemoveEmptyEntries)
                .ToList();
        }

        public async Task FetchData(string url)
        {
            if (string.IsNullOrEmpty(SessionCookie))
                throw new ArgumentNullException("SessionCookie");
            var baseAddress = new Uri(url);
            var cookieContainer = new CookieContainer();
            using var handler = new HttpClientHandler {CookieContainer = cookieContainer};
            using var client = new HttpClient(handler) {BaseAddress = baseAddress};
            cookieContainer.Add(baseAddress, new Cookie("session", SessionCookie));
            Input = await client.GetStringAsync(url);
        }
    }
}