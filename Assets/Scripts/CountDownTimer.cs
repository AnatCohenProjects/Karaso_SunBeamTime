using System;
using TMPro;
using UnityEngine;

public class CountDownTimer : MonoBehaviour
{
    private TextMeshProUGUI  clockText; // The UI Text element to display the countdown time
    private float timeRemaining; // Time remaining in seconds
    private bool IsStartTimer = false;
    private int maxTime;
    public static event Action OnTimerStopped;
    private int curMin;
    private int curSec;

    // Set countdown duration in seconds
    public void StartCountdown(int minutes)
    {
        IsStartTimer = true;
        clockText = GetComponent<TextMeshProUGUI>();
        clockText.text = "00:00";
        timeRemaining = 0;//minutes * 60;
        maxTime =  minutes * 60;
        curMin = 0;
        curSec = 0;

    }
    public float GetProgressionPrecentage()
    {
        return (timeRemaining) /maxTime  ;
    }

    void Update()
    {
        if (IsStartTimer)
        {
            if (timeRemaining <= maxTime)
            {
                timeRemaining += Time.deltaTime;
                UpdateClockDisplay(timeRemaining);
              
            }
            else
            { 
                IsStartTimer = false;
                 UpdateClockDisplay(maxTime);

                OnTimerStopped?.Invoke();
                //clockText.text = "08:00"; // Countdown has reached 0
            }
        }
    }

    void UpdateClockDisplay(float time)
    {

        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        if (curMin != seconds)
        {
            SoundManager.Instance.PlaySFX(SFX.ClockTik,0.1f);
            curMin = seconds;
        }
        
        clockText.text = string.Format("{0:D2}:{1:D2}", minutes, seconds);
        
       
    }
}


