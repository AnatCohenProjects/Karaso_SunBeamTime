using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(RectTransform))]
public class InfoBox : MonoBehaviour
{
    [Header("Object")]
    public RectTransform panel;

    [Header("Animation Duration")]
    public float duration = 0.5f;

    [Header("Y when show")]
    public float visibleY = 0f;

    [Header("Y when hide")]
    public float hiddenY = -500f;

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
    }
}