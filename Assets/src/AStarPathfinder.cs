using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.src
{
    public class AStarPathfinder
    {
        private readonly Labyrinth _labyrinth;

        public AStarPathfinder(Labyrinth labyrinth)
        {
            _labyrinth = labyrinth;
        }

        public List<Vector2Int> FindPath(Vector2Int start, Vector2Int goal)
        {
            if (!_labyrinth.IsInBounds(start) || !_labyrinth.IsInBounds(goal))
                throw new ArgumentException("Start or goal is out of labyrinth bounds.");

            var openSet = new PriorityQueue();
            var cameFrom = new Dictionary<Vector2Int, Vector2Int>();

            var gScore = new Dictionary<Vector2Int, float>();
            var fScore = new Dictionary<Vector2Int, float>();

            foreach (var cell in GetAllCells())
            {
                gScore[cell] = float.PositiveInfinity;
                fScore[cell] = float.PositiveInfinity;
            }

            gScore[start] = 0;
            fScore[start] = Heuristic(start, goal);

            openSet.Enqueue(start, fScore[start]);

            while (openSet.Count > 0)
            {
                var current = openSet.Dequeue();

                if (current == goal)
                    return ReconstructPath(cameFrom, current);

                foreach (var neighbor in GetNeighbors(current))
                {
                    float tentativeGScore = gScore[current] + _labyrinth.GetCell(neighbor).Weight;

                    if (tentativeGScore < gScore[neighbor])
                    {
                        cameFrom[neighbor] = current;
                        gScore[neighbor] = tentativeGScore;
                        fScore[neighbor] = gScore[neighbor] + Heuristic(neighbor, goal);

                        if (!openSet.Contains(neighbor))
                            openSet.Enqueue(neighbor, fScore[neighbor]);
                    }
                }
            }

            return new List<Vector2Int>(); // Path not found
        }

        private IEnumerable<Vector2Int> GetNeighbors(Vector2Int position)
        {
            var directions = new[]
            {
                new Vector2Int(0, 1),
                new Vector2Int(0, -1),
                new Vector2Int(1, 0),
                new Vector2Int(-1, 0)
            };

            foreach (var dir in directions)
            {
                var neighbor = position + dir;
                if (_labyrinth.IsInBounds(neighbor))
                    yield return neighbor;
            }
        }

        private float Heuristic(Vector2Int a, Vector2Int b)
        {
            return Math.Abs(a.x - b.x) + Math.Abs(a.y - b.y); // Манхэттенская дистанция
        }

        private List<Vector2Int> ReconstructPath(Dictionary<Vector2Int, Vector2Int> cameFrom, Vector2Int current)
        {
            var path = new List<Vector2Int> { current };
            while (cameFrom.ContainsKey(current))
            {
                current = cameFrom[current];
                path.Add(current);
            }

            path.Reverse();
            return path;
        }

        private IEnumerable<Vector2Int> GetAllCells()
        {
            for (int y = 0; y < _labyrinth.Height; y++)
            for (int x = 0; x < _labyrinth.Width; x++)
                yield return new Vector2Int(x, y);
        }
    }
}