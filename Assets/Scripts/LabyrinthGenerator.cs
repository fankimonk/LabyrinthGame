using UnityEngine;

public class LabyrinthGenerator : MonoBehaviour
{
    [SerializeField] private int Width = 15;
    [SerializeField] private int Height = 15;

    [SerializeField] private float MinValue = 0.0f;
    [SerializeField] private float MaxValue = 3.0f;

    private float[,] _labyrinthData;

    private void Start()
    {
        for (int i = 0; i < Height; i++)
            for (int j = 0; j < Width; j++)
                _labyrinthData[i, j] = Random.Range(MinValue, MaxValue);
    }

    private void Update()
    {
        
    }
}
