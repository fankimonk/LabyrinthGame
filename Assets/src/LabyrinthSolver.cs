using System.Collections.Generic;
using System;

namespace Assets.src
{
    public static class LabyrinthSolver
    {
        public static List<(int x, int y)> AStar(float[,] labyrinthData, (int x, int y) start, (int x, int y) end)
        {
            int rows = labyrinthData.GetLength(0);
            int cols = labyrinthData.GetLength(1);

            var openSet = new SortedSet<(float fCost, (int x, int y) position)>();
            var gCosts = new Dictionary<(int x, int y), float>();
            var fCosts = new Dictionary<(int x, int y), float>();
            var cameFrom = new Dictionary<(int x, int y), (int x, int y)>();

            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    gCosts[(i, j)] = float.MaxValue;

            gCosts[start] = labyrinthData[start.x, start.y];
            fCosts[start] = gCosts[start] + Heuristic(start, end);
            openSet.Add((fCosts[start], start));

            while (openSet.Count > 0)
            {
                var current = openSet.Min.position;
                openSet.Remove(openSet.Min);

                if (current == end)
                    return ReconstructPath(cameFrom, current);

                foreach (var neighbor in GetNeighbors(current))
                {
                    if (IsInBounds(neighbor.x, neighbor.y, rows, cols))
                    {
                        float tentativeGCost = gCosts[current] + labyrinthData[neighbor.x, neighbor.y];
                        if (tentativeGCost < gCosts[neighbor])
                        {
                            cameFrom[neighbor] = current;
                            gCosts[neighbor] = tentativeGCost;
                            fCosts[neighbor] = tentativeGCost + Heuristic(neighbor, end);
                            openSet.Add((fCosts[neighbor], neighbor));
                        }
                    }
                }
            }

            return null; // Путь не найден
        }

        private static List<(int x, int y)> GetNeighbors((int x, int y) pos)
        {
            return new List<(int x, int y)>
            {
                (pos.x + 1, pos.y), // Вниз
                (pos.x - 1, pos.y), // Вверх
                (pos.x, pos.y + 1), // Вправо
                (pos.x, pos.y - 1)  // Влево
            };
        }

        private static bool IsInBounds(int x, int y, int rows, int cols)
        {
            return x >= 0 && x < rows && y >= 0 && y < cols;
        }

        private static List<(int x, int y)> ReconstructPath(Dictionary<(int x, int y), (int x, int y)> cameFrom, (int x, int y) current)
        {
            var path = new List<(int x, int y)>();

            while (cameFrom.ContainsKey(current))
            {
                path.Add(current);
                current = cameFrom[current];
            }

            path.Reverse();
            return path;
        }

        private static float Heuristic((int x, int y) a, (int x, int y) b)
        {
            // Используем манхэттенское расстояние как эвристику
            return Math.Abs(a.x - b.x) + Math.Abs(a.y - b.y);
        }
    }
}
