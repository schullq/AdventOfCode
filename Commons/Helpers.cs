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
        private static readonly (int x, int y)[] Neighbors = { (0, 1), (0, -1), (1, 0), (-1, 0), };
        private static readonly (int x, int y)[] Adjacent = { (-1, -1), (0, -1), (1, -1), (-1, 0), (0, 0), (1, 0), (-1, 1), (0, 1), (1, 1) };
        
        /// <summary>
        /// Return the neighbors on same line and column.
        /// </summary>
        /// <param name="p">The point coordinate.</param>
        /// <returns>The neighbors coordinates.</returns>
        public static IEnumerable<(int x, int y)> GetCartesianNeighbors(this (int x, int y) p) => 
            Neighbors.Select(d => (p.x + d.x, p.y + d.y));

        /// <summary>
        /// Return the neighbors on same line and column in the map.
        /// </summary>
        /// <typeparam name="T">The type parameter of the map.</typeparam>
        /// <param name="p">The point coordinate.</param>
        /// <param name="map">The map.</param>
        /// <returns>The neighbors coordinates.</returns>
        public static IEnumerable<(int x, int y)> GetCartesianNeighbors<T>(this (int x, int y) p, List<List<T>> map) =>
            p.GetCartesianNeighbors().Where(q => q.y >= 0 && q.y < map.Count && q.x >= 0 && q.x < map[q.y].Count);

        /// <summary>
        /// Return the adjacent coordinates of a point.
        /// </summary>
        /// <param name="p">The point coordinate.</param>
        /// <param name="withCurrent">True if the point <param name="p"/> coordinates must be consider.</param>
        /// <returns>The adjacent points coordinates</returns>
        public static IEnumerable<(int x, int y)> GetCartesianAdjacent(this (int x, int y) p, bool withCurrent = false) =>
            withCurrent
                ? Adjacent.Select(d => (p.x + d.x, p.y + d.y))
                : Adjacent.Where(x => x != (0, 0)).Select(d => (p.x + d.x, p.y + d.y));

        /// <summary>
        /// Return the adjacent coordinates of a point in a map.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="p">The point coordinate.</param>
        /// <param name="withCurrent">True if the point <param name="p"/> coordinates must be consider.</param>
        /// <param name="map">The map.</param>
        /// <returns>The adjacent points coordinates</returns>
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
