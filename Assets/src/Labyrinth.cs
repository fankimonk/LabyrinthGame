using System;
using UnityEngine;

namespace Assets.src
{
    public class Labyrinth
    {
        public LabyrinthCell[,] Cells { get; private set; }
        
        public int Height => Cells.GetLength(0);
        public int Width => Cells.GetLength(1);

        public bool IsInBounds(int y, int x) => y >= 0 && y < Height && x >= 0 && x < Width; 
        public bool IsInBounds(Vector2Int v) => v.y >= 0 && v.y < Height && v.x >= 0 && v.x < Width;

        public Labyrinth(int width = 15, int height = 15, float minValue = 0.0f, float maxValue = 3.0f, float cellScaleX = 1.0f, float cellScaleZ = 1.0f)
        {
            Cells = new LabyrinthCell[height, width];

            Vector2 startPos = new Vector2(-((float)Width / 2 - 0.5f) * cellScaleX, 
                ((float)Height / 2 - 0.5f) * cellScaleZ);
            Vector2 currentPos = startPos;
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    var weight = UnityEngine.Random.Range(minValue, maxValue);
                    weight = (float)Math.Round(weight, 2);
                    Cells[i, j] = new LabyrinthCell(currentPos, weight);
                    currentPos.x += cellScaleX;
                }

                currentPos.x = startPos.x;
                currentPos.y -= cellScaleZ;
            }
        }

        public LabyrinthCell this[int y, int x]
        {
            get => Cells[y, x];
            set => Cells[y, x] = value;
        }

        public LabyrinthCell GetCell(Vector2Int v)
        {
            if (!IsInBounds(v)) throw new ArgumentOutOfRangeException(nameof(v));
            return this[v.y, v.x];
        }
    }
}
