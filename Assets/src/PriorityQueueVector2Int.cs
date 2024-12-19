using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.src
{
    public class PriorityQueueVector2Int
    {
        private readonly SortedSet<(float priority, Vector2Int item)> _queue = new(new PriorityQueueComparerVector2Int());

        public int Count => _queue.Count;

        public void Enqueue(Vector2Int item, float priority)
        {
            _queue.Add((priority, item));
        }

        public Vector2Int Dequeue()
        {
            if (_queue.Count == 0)
            {
                throw new InvalidOperationException("The priority queue is empty.");
            }

            var item = _queue.Min;
            _queue.Remove(item);
            return item.item;
        }

        public bool Contains(Vector2Int item)
        {
            return _queue.Any(x => x.item == item);
        }

        private class PriorityQueueComparerVector2Int : IComparer<(float priority, Vector2Int item)>
        {
            public int Compare((float priority, Vector2Int item) x, (float priority, Vector2Int item) y)
            {
                int priorityComparison = x.priority.CompareTo(y.priority);
                if (priorityComparison != 0)
                {
                    return priorityComparison;
                }

                int xComparison = x.item.x.CompareTo(y.item.x);
                if (xComparison != 0)
                {
                    return xComparison;
                }

                return x.item.y.CompareTo(y.item.y);
            }
        }
    }
}