
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using System;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.Localization.Settings;

public class ChangeLanguage : MonoBehaviour
{
    [SerializeField] Button heBtn;
    [SerializeField] Button ArBtn;
    [SerializeField] Button EnBtn;


    void Start()
    {

        heBtn.onClick.AddListener(() => SetLanguage(0));
        EnBtn.onClick.AddListener(() => SetLanguage(1));
        ArBtn.onClick.AddListener(() => SetLanguage(2));
    }



    public void SetLanguage(int index)
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[index];
    }
}