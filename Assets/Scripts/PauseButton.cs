using System;
using TMPro;
using UnityEngine;

public class PauseButton : MonoBehaviour
{
    [SerializeField] private TMP_Text PauseText;
    
    private bool _isPaused = false;
    
    public void TogglePause()
    {
        if (_isPaused == false)
        {
            PauseText.text = "Unpause";
            _isPaused = true;
            Debug.Log("Paused");
            Time.timeScale = 0;
        }
        else
            Unpause();
    }

    public void Unpause()
    {
        PauseText.text = "Pause";
        _isPaused = false;
        Debug.Log("Unpaused");
        Time.timeScale = 1;
    }
}
