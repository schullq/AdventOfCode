using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Commons;

namespace AdventOfCode._2021
{
    public class Day06 : AbstractDay
    {
        private static List<int> _input = new List<int>
#if TEST
        {
            3,4,3,1,2
        };
#else
            ();
#endif

        private Dictionary<int, int> _fishes;

        public Day06() : base(6)
        { }

        public long PhaseOne()
        {
#if !TEST
            _input = this.Parser.ParseInt(',');
#endif
            this.PrepareFishes();

            long nfish = this._fishes.Values.Sum();
            for (int i = 0; i < 80; i++)
            {
                int newFish = this._fishes[0];
                for (int j = 1; j <= 8; j++)
                {
                    this._fishes[j - 1] = this._fishes[j];
                }

                this._fishes[8] = newFish;
                this._fishes[6] += newFish;
                nfish += newFish;
            }

            return nfish;
        }

        public long PhaseTwo()
        {
#if !TEST
            if (_input == null || _input.Count == 0)
                _input = this.Parser.ParseInt();
#endif
            this.PrepareFishes();

            long nfish = this._fishes.Values.Sum();
            for (int i = 0; i < 256; i++)
            {
                int newFish = this._fishes[0];
                for (int j = 1; j <= 8; j++)
                {
                    this._fishes[j - 1] = this._fishes[j];
                }

                this._fishes[8] = newFish;
                this._fishes[6] += newFish;
                nfish += newFish;
            }

            return nfish;
        }

        private void PrepareFishes()
        {
            this._fishes = new Dictionary<int, int>();

            for (byte i = 0; i <= 8; i++)
            {
                this._fishes[i] = 0;
            }

            foreach (var n in _input)
            {
                this._fishes[n]++;
            }
        }
    }
}