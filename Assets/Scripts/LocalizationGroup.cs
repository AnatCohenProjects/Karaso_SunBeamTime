using UnityEngine;
using TMPro;
using RTLTMPro;
using System.Collections.Generic;
using UnityEngine.Localization.Settings;

public class LocalizationGroup : MonoBehaviour
{
 
    public RTLTextMeshPro textHE;
    public TMP_Text textEN;
    public RTLTextMeshPro textAR;

    private List<bool> _originalStates = new List<bool>();
    public void SetLanguage(string code)
    {
        bool isHE = code == "he";
        bool isEN = code == "en";
        bool isAR = code == "ar";

        textHE.gameObject.SetActive(isHE);
        textEN.gameObject.SetActive(isEN);
        textAR.gameObject.SetActive(isAR);
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
    }

    void OnDisable()
    {
        LanguageSelector.OnLanguageChanged -= SetLanguage;
    }
    public void SetHebrewText(string text)
    {
        textHE.text = text; 
    }
    public void SetEnglishText(string text)
    {
        textEN.text = text;
    }
    public void SetArabicText(string text)
    {
        textAR.text = text;
    }
  
}
