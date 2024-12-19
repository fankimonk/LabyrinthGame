using Assets.src;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    public UnityEvent OnLose = new UnityEvent();
    
    public PathDrawer PathDrawer;
    
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
