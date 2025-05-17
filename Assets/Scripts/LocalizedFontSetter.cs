using UnityEngine;
using UnityEngine.Localization;
using TMPro;
using UnityEngine.Localization.Settings;

public class LocalizedFontSetter : MonoBehaviour
{
    public LocalizedAsset<TMP_FontAsset> localizedFont;
    public TextMeshProUGUI /*TMP_Text*/ textMesh;
    public LocalizedString localizedString;

    void Start()
    {
        localizedFont.AssetChanged += font =>
        {
            if (font != null && textMesh != null)
            {
                textMesh.font = font;
            }
        };

        // localizedFont.RefreshAsset(); 
    }
    void OnEnable()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        localizedString.StringChanged += UpdateTextWithDirection;
        localizedString.RefreshString(); 
    }

    void OnDisable()
    {
        localizedString.StringChanged -= UpdateTextWithDirection;
    }

    void UpdateTextWithDirection(string rawText)
    {
        string langCode = LocalizationSettings.SelectedLocale.Identifier.Code;

        string prefix = langCode switch
        {
            "he" => "\u200F", //  RTL
            "ar" => "\u200F", //  RTL
            _ => "\u200E"     // LTR
        };

        textMesh.text = prefix + rawText;

        textMesh.alignment = (langCode == "he" || langCode == "ar")
        ? TextAlignmentOptions.Right
        : TextAlignmentOptions.Left;
    }
}
