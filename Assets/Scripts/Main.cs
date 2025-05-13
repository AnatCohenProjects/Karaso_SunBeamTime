
using UnityEngine;
using UnityEngine.UI; 
using System.Collections;
using TMPro;
using System;
using UnityEngine.SocialPlatforms.Impl;

public class Main : MonoBehaviour
{
    [SerializeField] Button startCountDownBtn;
    [SerializeField] SpriteRenderer spriteRay;
    [SerializeField] int rayDuration;//in minutes
    [SerializeField] TextMeshProUGUI countDownTimer;
    [SerializeField] Image rayMask;
    private RectTransform rayMaskRectTransform;

    private float timeElapsed = 0f;
    private bool isRunning = false;
    private CountDownTimer TimerScript;

    // Start is called before the first frame update
    void Start()
    {

        startCountDownBtn.onClick.AddListener(StartStopwatch);

        TimerScript = countDownTimer.GetComponent<CountDownTimer>();
        rayMaskRectTransform = rayMask.GetComponent<RectTransform>();
    }


    void Update()
    {
        if (isRunning)
        {
            timeElapsed += Time.deltaTime;
            UpdateRaySize();
        }
    }


    void StartStopwatch()
    {
        TimerScript.StartCountdown(rayDuration);
        isRunning = true;
        timeElapsed = 0f;
    }


    void UpdateRaySize()
    {
        float percentage = TimerScript.GetProgressionPrecentage();
        Debug.Log("percentage" + percentage);
        rayMaskRectTransform.sizeDelta = new Vector2(rayMaskRectTransform.sizeDelta.x, Mathf.Lerp(0f, 90f, percentage));
    }
}