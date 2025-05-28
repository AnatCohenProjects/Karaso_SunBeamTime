
using UnityEngine;
using UnityEngine.UI; 
using System.Collections;
using TMPro;
using System;
using UnityEngine.SocialPlatforms.Impl;
using RTLTMPro;
using UnityEngine.Localization.Settings;

public class Main : MonoBehaviour
{
    [SerializeField] Button startCountDownBtn;
    [SerializeField] int rayDuration;//in minutes
    [SerializeField] CountDownTimer countDownTimer;
    [SerializeField] GameObject ray;
    [SerializeField] InfoBox infoBox;
    private RectTransform rayMaskRectTransform;
    [SerializeField] Button resetBtn;
    [SerializeField] GameObject title;
    [SerializeField] private Transform particleEffect;
    [SerializeField] private SpriteRenderer beamSpriteRenderer;

    private float timeElapsed = 0f;
    private bool isRunning = false;
    private CountDownTimer TimerScript;
    private LocalizationGroup titleTexts;

    // Start is called before the first frame update
    void Start()
    {
        titleTexts = title.GetComponent<LocalizationGroup>();   
        startCountDownBtn.onClick.AddListener(StartStopwatch);
        resetBtn.onClick.AddListener(ResetGame);


    }
   
    
        void Update()
    {
        if (isRunning)
        {
            timeElapsed += Time.deltaTime;
            UpdateRaySize();
        }
    }

    void ResetGame()
    {
        isRunning = false;
        infoBox.Hide();
        countDownTimer.gameObject.SetActive(false);
        startCountDownBtn.gameObject.SetActive(true);

        Vector3 newScale = ray.gameObject.transform.localScale;
        newScale.x = 0;
        ray.gameObject.transform.localScale = newScale;

    }

    void StartStopwatch()
    {

        countDownTimer.gameObject.SetActive(true);
        startCountDownBtn.gameObject.SetActive(false);
        infoBox.Show();

        string code = LocalizationSettings.SelectedLocale.Identifier.Code;
       
        BreathingText[] InfoTextsBoxes =  infoBox.GetComponentsInChildren<BreathingText>();
     
        foreach (BreathingText infoTxt in InfoTextsBoxes)
            infoTxt.Startanimation();

        countDownTimer.StartCountdown(rayDuration);
        isRunning = true;
        timeElapsed = 0f;
    }


    void UpdateRaySize()
    {
        float percentage = countDownTimer.GetProgressionPrecentage();
        percentage = Mathf.Clamp01(percentage);

        float maxScale = 20f;
        Vector3 newScale = ray.transform.localScale;
        newScale.x = maxScale * percentage;
        ray.transform.localScale = newScale;

        // רוחב מקורי של הספייט (לפני סקייל)
        float originalWidth = beamSpriteRenderer.sprite.bounds.size.x;

        // סקייל נוכחי
        float scaleX = ray.transform.localScale.x;

        // המרחק מה-pivot (שנמצא בימין) לקצה השמאלי, בעולמי
        float offsetX = -originalWidth * scaleX - 0.5f;

        // חשב את המיקום העולמי של הקצה השמאלי
        Vector3 leftEdgeWorldPos = ray.transform.position + ray.transform.right * offsetX;

        // מקם את אפקט החלקיקים שם
        particleEffect.position = new Vector3(leftEdgeWorldPos.x, ray.transform.position.y, ray.transform.position.z);
    }

}