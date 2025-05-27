
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
       // Debug.Log("precentage"+ percentage);

        // 2. חשבי רוחב מקסימלי שברצונך להגיע אליו
        float maxScale = 20f; // או כל רוחב אחר שמתאים לך

        Vector3 newScale = ray.gameObject.transform.localScale;
        newScale.x = maxScale * percentage;
        ray.gameObject.transform.localScale = newScale;

        //        // עדכן את מיקום אפקט החלקיקים
        float originalWidth = beamSpriteRenderer.sprite.bounds.size.x;

        // Scale נוכחי
        float scaleX = transform.localScale.x;

        // מרחק מהפיווט הימני לקצה השמאלי
        float leftEdgeLocalX = -originalWidth * scaleX;

        // מקם את אפקט החלקיקים בקצה השמאלי
        particleEffect.localPosition = new Vector3(leftEdgeLocalX, 0f, 0f);
    }
}