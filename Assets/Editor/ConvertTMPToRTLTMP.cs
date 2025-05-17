#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using TMPro;
using RTLTMPro;

public static class ConvertTMPToRTLTMP
{
    [MenuItem("Tools/Convert TMP UGUI → RTLTMP")]
    public static void ConvertAll()
    {
        // מחפש בכל הסצנות (גם GameObjects כבויים)
        var allTMP = Object.FindObjectsByType<TextMeshProUGUI>(FindObjectsSortMode.InstanceID);
        int converted = 0;

        foreach (var tmp in allTMP)
        {
            var go = tmp.gameObject;
            // שומר מאפיינים עיקריים
            var text = tmp.text;
            var fontAsset = tmp.font;
            var fontSize = tmp.fontSize;
            var color = tmp.color;
            var alignment = tmp.alignment;
            var enableRich = tmp.richText;
            var margins = tmp.margin;
            var lineSpacing = tmp.lineSpacing;

            // רישום לביצוע Undo
            Undo.RecordObject(go, "Convert TMP → RTLTMP");
            // הסר את TMPUGUI
            Undo.DestroyObjectImmediate(tmp);

            // הוסף את הרכיב RTLTextMeshPro
            var rtl = Undo.AddComponent<RTLTextMeshPro>(go);

            // העתק מאפיינים
            rtl.text = text;
            rtl.font = fontAsset;
            rtl.fontSize = fontSize;
            rtl.color = color;
            rtl.alignment = alignment;
            rtl.richText = enableRich;
            rtl.margin = margins;
            rtl.lineSpacing = lineSpacing;

            converted++;
        }

        Debug.Log($"[ConvertTMPToRTLTMP] המרה הושלמה: {converted} רכיבי TMPUGUI הומרו ל־RTLTMP.");
    }
}
#endif
