using UnityEngine;

namespace Assets.src
{
    public struct LabyrinthCell
    {
        public readonly Vector2 Position;
        
        public float Weight { get; private set; }

        public LabyrinthCell(Vector2 position, float weight)
        {
            Position = position;
            Weight = weight;
        }
    }
}