using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.Localization.Settings;
using System.Collections;
using RTLTMPro;
using NUnit.Framework;

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
    Coroutine fadeCoroutine;
    private bool _isActive;
    public bool IsActive
    {
        get => _isActive;
        set => _isActive = value;
    }

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
            if (rtlTextBox != null) // hebrew of arabic
            {
                rtlTextBox.text = _messages.Length > 0 ? _messages[i % _messages.Length] : "";
                if (i == iterations - 1)
                {
                    rtlTextBox.fontSize = 47;
                    rtlTextBox.fontStyle = FontStyles.Bold;
                }
                else
                {
                    rtlTextBox.fontSize = 40;
                    rtlTextBox.fontStyle = FontStyles.Normal;
                }
                rtlTextBox.ForceMeshUpdate();
            }
            else // english
            {
                TextMeshProUGUI ltrTextBox = GetComponent<TextMeshProUGUI>();
                if (ltrTextBox != null)
                {
                    ltrTextBox.text = _messages.Length > 0 ? _messages[i % _messages.Length] : "";
                    if (i == iterations - 1)
                    {
                        ltrTextBox.fontSize = 47;
                        ltrTextBox.fontStyle = FontStyles.Bold;
                    }
                    else
                    {
                        ltrTextBox.fontSize = 40;
                        ltrTextBox.fontStyle = FontStyles.Normal;
                    }
                    ltrTextBox.ForceMeshUpdate();
                }
            }
           
            // פייד אין
            yield return StartCoroutine(Fade(0f, 1f, fadeDuration));

            // המתנה עד לפייד אאוט
            float displayTime = cycleTime - 2f * fadeDuration;
            if (displayTime > 0f)
                yield return new WaitForSeconds(displayTime);

            if( i < iterations - 1)
            // פייד אאוט
            yield return StartCoroutine(Fade(1f, 0f, fadeDuration));
        }
    }

    IEnumerator Fade(float from, float to, float duration)
    {
       
        float elapsed = 0f;
        if (_isActive)
            _canvasGroup.alpha = from;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            if (_isActive)
                _canvasGroup.alpha = Mathf.Lerp(from, to, elapsed / duration);
            yield return null;
        }
        if (_isActive)
            _canvasGroup.alpha = to;
        
    }
    public void Startanimation()
    {
        fadeCoroutine = StartCoroutine(PlayFadeSequence());
    }
    public void Stopanimation()
    {
        StopCoroutine(fadeCoroutine);
    }

}
