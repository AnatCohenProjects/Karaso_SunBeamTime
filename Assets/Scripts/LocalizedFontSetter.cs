using UnityEngine;
using UnityEngine.Localization;
using TMPro;
using UnityEngine.Localization.Settings;
using System.Xml;
using RTLTMPro;

public class LocalizedFontSetter : MonoBehaviour
{
    public LocalizedAsset<TMP_FontAsset> localizedFont;
    public RTLTextMeshPro /*TMP_Text*/ textMesh;
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

     //  localizedFont.RefreshAsset(); 
    }
    void OnEnable()
    {
        textMesh = GetComponent<RTLTextMeshPro>();
        localizedString.StringChanged += UpdateTextWithDirection;
        localizedString.RefreshString(); 
    }

    void OnDisable()
    {
        localizedString.StringChanged -= UpdateTextWithDirection;
    }

    void UpdateTextWithDirection(string rawText)
    {

        var code = LocalizationSettings.SelectedLocale.Identifier.Code;

        if (code == "ar" || code == "he")
        {
            // שפה RTL → השארי Fix On
            textMesh.Farsi = true;      // מאפשר חיבור אותיות
            textMesh.ForceFix = false;     // לא מכריח עיבוד לתווים לטיניים
           // textMesh.alignment = TextAlignmentOptions.Right;
            textMesh.text = rawText;
        }
        else
        {
            // שפה LTR → כבי את ה‑RTL
            textMesh.Farsi = false;
            textMesh.ForceFix = false;
           // textMesh.alignment = TextAlignmentOptions.Left;
            textMesh.text = "\u200E" + rawText; // תו LTR מונע בלבול כיווניות
        }
    }

        
        /*textMesh.alignment = (langCode == "he" || langCode == "ar")
        ? TextAlignmentOptions.Right
        : TextAlignmentOptions.Left;*/
    
}
