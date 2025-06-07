using System;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.U2D;
using UnityEngine.UI;

public class LanguageSelector : MonoBehaviour

  {
    public static LanguageSelector Instance { get; private set; }
    public static event Action<string> OnLanguageChanged;

    [Header("הספרייטים להצגה")]
    public Sprite  HeBg;
    public Sprite  EnBg;
    public Sprite  ArBg;

    [SerializeField] Button HeBtn;
    [SerializeField] Button EnBtn;
    [SerializeField] Button ARBtn;

    [SerializeField] Image displayImage;

    // קריאה ישירה לשפות לפי הקוד
    public void SetHebrew()
    {
        SetLocale("he");

    }
    public void SetEnglish()
    {
        SetLocale("en");
        
    }

    public void SetArabic()
    {
        SetLocale("ar");
 
    }

    void SetLocale(string code)
    {
        SoundManager.Instance.PlaySFX(SFX.ChangeLang);
        var locales = LocalizationSettings.AvailableLocales.Locales;
        // מוצא את ה‑Locale לפי קוד (en, he, ar)
        var locale = locales.Find(l => l.Identifier.Code == code);
        if (locale != null)
            LocalizationSettings.SelectedLocale = locale;
        else
            Debug.LogWarning($"Locale '{code}' לא נמצא ב־AvailableLocales");

        OnLanguageChanged?.Invoke(code);
        switch (code)
        {
            case "he":
                displayImage.sprite = HeBg;
                break;
            case "en":
                displayImage.sprite = EnBg;
                break;
            case "ar":
                displayImage.sprite = ArBg;
                break;

        }
    }
}
