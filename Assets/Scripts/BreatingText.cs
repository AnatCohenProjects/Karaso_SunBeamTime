using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.Localization.Settings;

[RequireComponent(typeof(CanvasGroup))]
public class BreathingText : MonoBehaviour
{
    [Header("TextMeshProUGUI to switch")]
    public TextMeshProUGUI textBox;

    [Header("Messages to cycle through (6 messages)")]
    public string[] He_messages = new string[6];
    public string[] En_messages = new string[6];
    public string[] Ar_messages = new string[6];

    [Header("Total duration in seconds (8 minutes = 480s)")]
    public float totalDuration = 480f;

    [Header("Number of cycles")]
    public int iterations = 6;

    [Header("Fade in/out duration (seconds)")]
    public float fadeDuration = 1f;

    private CanvasGroup _canvasGroup;

    void Awake()
    {
        // ensure we have a CanvasGroup
        _canvasGroup = GetComponent<CanvasGroup>();
        if (_canvasGroup == null)
            _canvasGroup = gameObject.AddComponent<CanvasGroup>();

        // start invisible
        _canvasGroup.alpha = 0;
    }

    void Start()
    {
        // compute how long we display each message
        float cycleTime = totalDuration / iterations;
        float displayTime = Mathf.Max(0, cycleTime - 2 * fadeDuration);
        var seq = DOTween.Sequence();

        for (int i = 0; i < iterations; i++)
        {
            int idx = i % He_messages.Length;

            // fade out (breathe out)
            seq.Append(_canvasGroup.DOFade(0f, fadeDuration).SetEase(Ease.InOutSine));

            // switch text at fully hidden
            string code = LocalizationSettings.SelectedLocale.Identifier.Code;
            if(code == "he")
            seq.AppendCallback(() => textBox.text = He_messages[idx]);
            else if (code == "en")
                seq.AppendCallback(() => textBox.text = En_messages[idx]);
            else if (code == "ar")
                seq.AppendCallback(() => textBox.text = Ar_messages[idx]);
            // fade in (breathe in)
            seq.Append(_canvasGroup.DOFade(1f, fadeDuration).SetEase(Ease.InOutSine));

            // hold before next cycle
            if (i < iterations - 1)
                seq.AppendInterval(displayTime);
        }

        seq.Play();
    }
}
