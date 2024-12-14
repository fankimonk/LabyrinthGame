using System;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private TMP_Text _timerTMPText;

    private float _prevTimePassedSeconds = 0.0f;
    private float _timePassedSeconds = 0.0f;

    private float _timePassedMinutes => _timePassedSeconds / 60;

    private bool _isStopped = true;
    
    private void Start()
    {
        _timerTMPText = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        if (_isStopped) return;
        
        _prevTimePassedSeconds = _timePassedSeconds;
        _timePassedSeconds += Time.deltaTime;

        if ((int)_timePassedSeconds != (int)_prevTimePassedSeconds)
        {
            UpdateText();
            //Debug.Log(_timerTMPText.text);
        }
    }

    private void UpdateText()
    {
        _timerTMPText.text = _timePassedMinutes.ToString("00") + ":" + (_timePassedSeconds % 60).ToString("00");
    }
    
    public void StartTimer()
    {
        _isStopped = false;
        Debug.Log("Timer started");
    }

    public void StopTimer()
    {
        _timePassedSeconds = 0.0f;
        _isStopped = true; 
        UpdateText();
        Debug.Log("Timer stopped");
    }
}
