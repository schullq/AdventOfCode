using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Commons.Containers;

namespace AdventOfCode.Commons
{
    public static class Helpers
    {
        private static readonly (int x, int y)[] Neighbors = new (int x, int y)[] { (0, 1), (0, -1), (1, 0), (-1, 0), };

        public static IEnumerable<(int x, int y)> GetCartesianNeighbors(this (int x, int y) p) =>
            Neighbors.Select(d => (p.x + d.x, p.y + d.y));

        public static IEnumerable<(int x, int y)> GetCartesianNeighbors<T>(this (int x, int y) p, List<List<T>> map) =>
            p.GetCartesianNeighbors().Where(q => q.y >= 0 && q.y < map.Count && q.x >= 0 && q.x < map[q.y].Count);

        private static readonly (int x, int y)[] Adjacent = new (int x, int y)[]
        {
            (-1, -1), (0, -1), (1, -1), (-1, 0), (0, 0), (1, 0), (-1, 1), (0, 1), (1, 1)
        };

        public static IEnumerable<(int x, int y)>
            GetCartesianAdjacent(this (int x, int y) p, bool withCurrent = false) =>
            withCurrent
                ? Adjacent.Select(d => (p.x + d.x, p.y + d.y))
                : Adjacent.Where(x => x != (0, 0)).Select(d => (p.x + d.x, p.y + d.y));

        public static IEnumerable<(int x, int y)> GetCartesianAdjacent<T>(this (int x, int y) p,
            IReadOnlyList<IReadOnlyList<T>> map,
            bool withCurrent = false
        ) =>
            p.GetCartesianAdjacent(withCurrent).Where(q => q.y >= 0 && q.y < map.Count && q.x >= 0 && q.x < map[q.y].Count);

        public static (Dictionary<(int x, int y), int>, (int x, int y), int) Dijkstra((int x, int y) start,
            Func<(int x, int y), IEnumerable<((int x, int y) state, int cost)>> getNextStates,
            Func<Dictionary<(int x, int y), int>, (int x, int y), bool> endCondition
        )
        {
            var totalCost = new Dictionary<(int x, int y), int>();
            var pq = new UpdateablePriorityQueue<(int x, int y)>();
            pq.Enqueue(start, 0);

            ((int x, int y) p, int cost) = (default, default);
            while (pq.Count != 0)
            {
                pq.TryDequeue(out p, out cost);

                while (pq.Count != 0 && totalCost.ContainsKey(p))
                    pq.TryDequeue(out p, out cost);

                totalCost[p] = cost;
                if (endCondition(totalCost, p))
                    break;

                pq.EnqueueRange(getNextStates(p).Select(s => (s.state, cost + s.cost)));
            }

            return (totalCost, p, cost);
        }
    }
}
