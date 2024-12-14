using UnityEngine;

public class GameUI : MonoBehaviour
{
    private void Start()
    {
        Debug.Log("Game is active");
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(false);
    }
}
