using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.Tables;

public class DynamicKeyChanger : MonoBehaviour
{
    // גררי את ה-LocalizeStringEvent מתוך ה-Inspector
    public LocalizeStringEvent localizeEvent;

    /// <summary>
    /// קוראים לפונקציה הזו (למשל מתוך כפתור)
    /// כדי לשנות את ה-key ל־newKey
    /// </summary>
    public void SetKey(string newKey)
    {
        // 1. שנה את ה־Entry (ה-key) בטבלת התרגום
        localizeEvent.StringReference.TableEntryReference = newKey;
        // 2. טען מחדש את המחרוזת
        localizeEvent.RefreshString();
    }
}
