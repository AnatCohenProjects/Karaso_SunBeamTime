using UnityEngine;
using DG.Tweening;
using RTLTMPro;
using System.Collections.Generic;
using TMPro;
using UnityEngine.Localization.Settings;

[RequireComponent(typeof(RectTransform))]
public class InfoBox : MonoBehaviour
{
    public RTLTextMeshPro textHE;
    public TMP_Text textEN;
    public RTLTextMeshPro textAR;


    [Header("Object")]
    public RectTransform panel;

    [Header("Animation Duration")]
    public float duration = 0.5f;

    [Header("Y when show")]
    public float visibleY = 0f;

    [Header("Y when hide")]
    public float hiddenY = -500f;

    public CountDownTimer countDownTimer;

    void Reset()
    {

        panel = GetComponent<RectTransform>();
    }

    /// <summary>
    /// מראה את התיבה במיקום visibleY
    /// </summary>
    public void Show()
    {
        // מבטל tweens קודמים
        panel.DOKill();
        // מזיז ל‑visibleY על ציר Y
        panel.DOAnchorPosY(visibleY, duration)
             .SetEase(Ease.InOutQuad);
    }

    /// <summary>
    /// מסתיר את התיבה למיקום hiddenY
    /// </summary>
    public void Hide()
    {
        panel.DOKill();
        panel.DOAnchorPosY(hiddenY, duration)
             .SetEase(Ease.InOutQuad);

        textHE.GetComponent<BreathingText>().Stopanimation();
        textEN.GetComponent<BreathingText>().Stopanimation();
        textAR.GetComponent<BreathingText>().Stopanimation();
    }

    public void SetLanguage(string code)
    {
        bool isHE = code == "he";
        bool isEN = code == "en";
        bool isAR = code == "ar";

        textHE.GetComponent<CanvasGroup>().alpha = (isHE) ? 1f : 0f;
        textEN.GetComponent<CanvasGroup>().alpha = (isEN) ? 1f : 0f;
        textAR.GetComponent<CanvasGroup>().alpha = (isAR) ? 1f : 0f;

        textHE.GetComponent<BreathingText>().IsActive = isHE;
        textEN.GetComponent<BreathingText>().IsActive = isEN;
        textAR.GetComponent<BreathingText>().IsActive = isAR;

    }

    void Start()
    {
        // מאתחל לשפה ששמורה ב‑PlayerPrefs או 'he' כברירת מחדל
        var code = PlayerPrefs.GetString("lang", "he");
        SetLanguage(code);
    }
    void OnEnable()
    {
        LanguageSelector.OnLanguageChanged += SetLanguage;
        SetLanguage(LocalizationSettings.SelectedLocale.Formatter.ToString());
        CountDownTimer.OnTimerStopped += SetStoppedTimer;
    }

    void OnDisable()
    {
        LanguageSelector.OnLanguageChanged -= SetLanguage;
        CountDownTimer.OnTimerStopped -= SetStoppedTimer;
    }

    public void SetStoppedTimer()
    {
        textHE.GetComponent<BreathingText>().SetStopFadeSequence();
        textEN.GetComponent<BreathingText>().SetStopFadeSequence();
        textAR.GetComponent<BreathingText>().SetStopFadeSequence();

    }
}
