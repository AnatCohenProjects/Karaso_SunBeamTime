using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.Localization.Settings;
using System.Collections;
using RTLTMPro;

[RequireComponent(typeof(CanvasGroup))]
public class BreathingText : MonoBehaviour
{
  
    [Header("Messages to cycle through (6 messages)")]
    public string[] _messages = new string[7];

    [Header("Total duration in seconds (8 minutes = 480s)")]
    public float totalDuration = 5;//480f;

    [Header("Number of cycles")]
    public int iterations = 7;

    [Header("Fade in/out duration (seconds)")]
    public float fadeDuration = 1f;

    private CanvasGroup _canvasGroup;
    void Awake()
    {
        // הוספת CanvasGroup אם אין
        _canvasGroup = GetComponent<CanvasGroup>();
        if (_canvasGroup == null)
            _canvasGroup = gameObject.AddComponent<CanvasGroup>();

    }

    void Start()
    {
       
    }

    IEnumerator PlayFadeSequence()
    {
        // זמן מחזור יחיד: חלוקה שווה
        float cycleTime = totalDuration / iterations;

        for (int i = 0; i < iterations; i++)
        {
            RTLTextMeshPro rtlTextBox = GetComponent<RTLTextMeshPro>();
            if (rtlTextBox != null ) // hebrew of arabic
                rtlTextBox.text = _messages.Length > 0 ? _messages[i % _messages.Length] : "";
            else // english
            {
                TextMeshProUGUI ltrTextBox = GetComponent<TextMeshProUGUI>();
                if(ltrTextBox != null )
                {
                    ltrTextBox.text = _messages.Length > 0 ? _messages[i % _messages.Length] : "";
                }
            }
           
            // פייד אין
            yield return StartCoroutine(Fade(0f, 1f, fadeDuration));

            // המתנה עד לפייד אאוט
            float displayTime = cycleTime - 2f * fadeDuration;
            if (displayTime > 0f)
                yield return new WaitForSeconds(displayTime);

            // פייד אאוט
            yield return StartCoroutine(Fade(1f, 0f, fadeDuration));
        }
    }

    IEnumerator Fade(float from, float to, float duration)
    {
        float elapsed = 0f;
        _canvasGroup.alpha = from;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            _canvasGroup.alpha = Mathf.Lerp(from, to, elapsed / duration);
            yield return null;
        }
        _canvasGroup.alpha = to;
    }
    public void Startanimation()
    {
        StartCoroutine(PlayFadeSequence());
    }
    /* void Awake()
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
     }*/
}
