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

    public RTLTextMeshPro textHE_Shadow;
    public TMP_Text textEN_Shadow;
    public RTLTextMeshPro textAR_Shadow;

    private List<bool> _originalStates = new List<bool>();
    public void SetLanguage(string code)
    {
        bool isHE = code == "he";
        bool isEN = code == "en";
        bool isAR = code == "ar";

        textHE.gameObject.SetActive(isHE);
        textEN.gameObject.SetActive(isEN);
        textAR.gameObject.SetActive(isAR);

        if(textHE_Shadow) { textHE_Shadow.gameObject.SetActive(isHE); }
        if(textEN_Shadow) { textEN_Shadow.gameObject.SetActive(isEN); }
        if(textAR_Shadow) { textAR_Shadow.gameObject.SetActive(isAR); }

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
        if(textHE_Shadow != null)
          textHE_Shadow.text = text;
    }
    public void SetEnglishText(string text)
    {
        textEN.text = text;
        if (textEN_Shadow != null)
            textEN_Shadow.text = text;
    }
    public void SetArabicText(string text)
    {
        textAR.text = text;
        if (textAR_Shadow != null)
            textAR_Shadow.text = text;
    }
  
}
