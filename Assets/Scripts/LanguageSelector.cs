using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class LanguageSelector : MonoBehaviour

  {

    [SerializeField] Button HeBtn;
    [SerializeField] Button EnBtn;
    [SerializeField] Button ARBtn;

    // קריאה ישירה לשפות לפי הקוד
    public void SetHebrew()
    {
        SetLocale("he");
        HeBtn.gameObject.SetActive(true);
        EnBtn.gameObject.SetActive(false);
        ARBtn.gameObject.SetActive(false);

    }

    public void SetEnglish()
    {
        SetLocale("en");
        HeBtn.gameObject.SetActive(false);
        EnBtn.gameObject.SetActive(true);
        ARBtn.gameObject.SetActive(false);
    }

    public void SetArabic()
    {
        SetLocale("ar");
         HeBtn.gameObject.SetActive(false);
        EnBtn.gameObject.SetActive(false);
        ARBtn.gameObject.SetActive(true);
    }

    void SetLocale(string code)
    {
        var locales = LocalizationSettings.AvailableLocales.Locales;
        // מוצא את ה‑Locale לפי קוד (en, he, ar)
        var locale = locales.Find(l => l.Identifier.Code == code);
        if (locale != null)
            LocalizationSettings.SelectedLocale = locale;
        else
            Debug.LogWarning($"Locale '{code}' לא נמצא ב־AvailableLocales");
    }
}
