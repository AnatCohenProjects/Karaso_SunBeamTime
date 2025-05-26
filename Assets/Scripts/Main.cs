
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
    [SerializeField] SpriteRenderer spriteRay;
    [SerializeField] int rayDuration;//in minutes
    [SerializeField] CountDownTimer countDownTimer;
    [SerializeField] Image rayMask;
    [SerializeField] InfoBox infoBox;
    private RectTransform rayMaskRectTransform;
    [SerializeField] Button resetBtn;
    [SerializeField] GameObject title;

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

    void ResetGame()
    {
        isRunning = false;
        infoBox.Hide();
        countDownTimer.gameObject.SetActive(false);
        startCountDownBtn.gameObject.SetActive(true);

        float maxWidth = 2020f; // או כל רוחב אחר שמתאים לך
        var sd = rayMaskRectTransform.sizeDelta;
        sd.x = Mathf.Lerp(0f, maxWidth, 0);
        rayMaskRectTransform.sizeDelta = sd;

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
        float maxWidth = 2020f; // או כל רוחב אחר שמתאים לך

        // 3. עדכני את ה‑RectTransform על ציר ה‑X
        //    אפשר בגישה הישנה:
        var sd = rayMaskRectTransform.sizeDelta;
        sd.x = Mathf.Lerp(0f, maxWidth, percentage);
        rayMaskRectTransform.sizeDelta = sd;
        
    }
}