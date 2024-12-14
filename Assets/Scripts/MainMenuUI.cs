using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    private void Start()
    {
        Debug.Log("Main menu is active");
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }
}
