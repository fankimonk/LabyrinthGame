using Assets.src;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    [SerializeField] private LabyrinthBuilder LabyrinthBuilder;

    public Labyrinth Labyrinth => LabyrinthBuilder.Labyrinth;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
}
