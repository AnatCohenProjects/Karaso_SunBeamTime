
using UnityEngine;
using UnityEngine.UI; 
using System.Collections;
using TMPro;
using System;
using UnityEngine.SocialPlatforms.Impl;
using RTLTMPro;

public class Main : MonoBehaviour
{
    [SerializeField] Button startCountDownBtn;
    [SerializeField] SpriteRenderer spriteRay;
    [SerializeField] int rayDuration;//in minutes
    [SerializeField] CountDownTimer countDownTimer;
    [SerializeField] Image rayMask;
    [SerializeField] InfoBox infoBox;
    private RectTransform rayMaskRectTransform;
    [SerializeField] Button resetBtn;

    private float timeElapsed = 0f;
    private bool isRunning = false;
    private CountDownTimer TimerScript;

    // Start is called before the first frame update
    void Start()
    {

        startCountDownBtn.onClick.AddListener(StartStopwatch);

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
        /* textsGroup = startCountDownBtn.GetComponent<LocalizationGroup>();
        textsGroup.SetHebrewText("הקרן בדרך!");
        textsGroup.SetEnglishText("The beam is on its way!");
        textsGroup.SetArabicText("الشعاع في الطريق!");*/

        countDownTimer.gameObject.SetActive(true);
        startCountDownBtn.gameObject.SetActive(false);
        infoBox.Show();

        countDownTimer.StartCountdown(rayDuration);
        isRunning = true;
        timeElapsed = 0f;
    }


    void UpdateRaySize()
    {
        float percentage = countDownTimer.GetProgressionPrecentage();
        percentage = Mathf.Clamp01(percentage);
 
        // 2. חשבי רוחב מקסימלי שברצונך להגיע אליו
        float maxWidth = 2020f; // או כל רוחב אחר שמתאים לך

        // 3. עדכני את ה‑RectTransform על ציר ה‑X
        //    אפשר בגישה הישנה:
        var sd = rayMaskRectTransform.sizeDelta;
        sd.x = Mathf.Lerp(0f, maxWidth, percentage);
        rayMaskRectTransform.sizeDelta = sd;
        
    }
}