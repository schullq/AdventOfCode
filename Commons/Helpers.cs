using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Commons.Containers;

namespace AdventOfCode.Commons
{
    public static class Helpers
    {
        private static readonly (int x, int y)[] Neighbors = { (0, 1), (0, -1), (1, 0), (-1, 0), };

        private static readonly (int x, int y)[] Adjacent =
            { (-1, -1), (0, -1), (1, -1), (-1, 0), (0, 0), (1, 0), (-1, 1), (0, 1), (1, 1) };

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
        public static IEnumerable<(int x, int y)> GetCartesianAdjacent(
            this (int x, int y) p,
            bool withCurrent = false) =>
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
        public static IEnumerable<(int x, int y)> GetCartesianAdjacent<T>(
            this (int x, int y) p,
            IReadOnlyList<IReadOnlyList<T>> map,
            bool withCurrent = false) =>
            p.GetCartesianAdjacent(withCurrent)
                .Where(q => q.y >= 0 && q.y < map.Count && q.x >= 0 && q.x < map[q.y].Count);

        public static (Dictionary<(int x, int y), int> paths, (int x, int y) end, int weight) Dijkstra(
            (int x, int y) start,
            Func<(int x, int y), bool> endCondition,
            Func<(int x, int y), IEnumerable<((int x, int y) state, int weight)>> getNextStates)
        {
            var dist = new Dictionary<(int x, int y), int>();
            var pq = new PriorityQueue<(int x, int y), int>();

            ((int x, int y) p, int weight) = (start, default);
            pq.Enqueue(p, weight);
            while (pq.Count != 0)
            {
                do
                {
                    pq.TryDequeue(out p, out weight);
                } while (pq.Count != 0 && dist.ContainsKey(p));

                if (endCondition(p))
                    break;
                dist[p] = weight;

                pq.EnqueueRange(getNextStates(p).Select(s => (s.state, weight + s.weight)));
            }

            return (dist, p, weight);
        }

        /// <summary>
        /// Search for the shortest path using BFS algorithm.
        /// </summary>
        /// <param name="start">The start coordinate</param>
        /// <param name="endCondition">The end condition.</param>
        /// <param name="getNextStates">A delegate returning the next states.</param>
        /// <returns>The smallest weight.</returns>
        public static int Bfs(
            (int x, int y) start,
            Func<(int x, int y), bool> endCondition,
            Func<(int x, int y), IEnumerable<((int x, int y) state, int weight)>> getNextStates)
        {
            var queue = new Queue<((int x, int y), int weight)>();
            var visited = new List<(int x, int y)>();

            queue.Enqueue(((start.x, start.y), 0));

            while (queue.Any())
            {
                ((int x, int y), int weight) = queue.Dequeue();
                if (visited.Contains((x, y)))
                    continue;
                visited.Add((x, y));

                if (endCondition((x, y)))
                    return weight;

                foreach (var c in getNextStates((x, y)).Select(s => (s.state, weight + s.weight)))
                {
                    queue.Enqueue(c);
                }
            }

            return 0;
        }
    }
}