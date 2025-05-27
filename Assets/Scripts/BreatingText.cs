using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.Localization.Settings;
using System.Collections;
using RTLTMPro;
using NUnit.Framework;
using UnityEngine.Rendering;

[RequireComponent(typeof(CanvasGroup))]
public class BreathingText : MonoBehaviour
{
  
    [Header("Messages to cycle through (6 messages)")]
    public string[] _messages = new string[6];
 
    public float TextChangeFrequency = 5f;

    [Header("Fade in/out duration (seconds)")]
    public float fadeDuration = 1f;

    private CanvasGroup _canvasGroup;
    Coroutine fadeCoroutine;
    bool stop = false;
    bool isTimerStopped = false;
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
        isTimerStopped = false;
        stop = false;
        fadeCoroutine = StartCoroutine(PlayFadeSequence());
    }
    public void Stopanimation()

    { 
         isTimerStopped = true;
        stop = true;
        StopCoroutine(fadeCoroutine);
    }
  

      IEnumerator PlayFadeSequence()
      {
        int iterations = _messages.Length - 1;
         RTLTextMeshPro rtlTextBox = GetComponent<RTLTextMeshPro>();
        TextMeshProUGUI ltrTextBox = GetComponent<TextMeshProUGUI>();
        while (!stop)
          {
              // לולאה פנימית – לדוגמה 5 איטרציות
              for (int i = 0; i < iterations ; i++)
              {
                
                if (rtlTextBox != null) // hebrew of arabic
                {
                    rtlTextBox.text = _messages.Length > 0 ? _messages[i % _messages.Length] : "";
                    
                        rtlTextBox.fontSize = 40;
                        rtlTextBox.fontStyle = FontStyles.Normal;
                    
                        rtlTextBox.ForceMeshUpdate();
                }
                else // english
                {
               
                    if (ltrTextBox != null)
                    {
                           ltrTextBox.text = _messages.Length > 0 ? _messages[i % _messages.Length] : "";

                                ltrTextBox.fontSize = 40;
                                ltrTextBox.fontStyle = FontStyles.Normal;

               
                        ltrTextBox.ForceMeshUpdate();
                    }
                 }
           
            // פייד אין
            yield return StartCoroutine(Fade(0f, 1f, fadeDuration));

            // המתנה עד לפייד אאוט
            float displayTime = TextChangeFrequency - 2f * fadeDuration;
            if (displayTime > 0f)
                yield return new WaitForSeconds(displayTime);

           // if( i < iterations - 1)
            // פייד אאוט
            yield return StartCoroutine(Fade(1f, 0f, fadeDuration));
                  yield return new WaitForSeconds(0.5f); // זמן בין איטרציות


                // בדיקה אחרי כל מחזור של הלולאה
                if (isTimerStopped)
                {
                    stop = true;
                  
                    if (rtlTextBox != null) // hebrew of arabic
                    {
                        rtlTextBox.text = _messages.Length > 0 ? _messages[_messages.Length - 1] : "";

                        rtlTextBox.fontSize = 47;
                        rtlTextBox.fontStyle = FontStyles.Bold;

                        rtlTextBox.ForceMeshUpdate();
                    }
                    else // english
                    {
                       
                        if (ltrTextBox != null)
                        {
                            ltrTextBox.text = _messages.Length > 0 ? _messages[_messages.Length - 1] : "";

                            ltrTextBox.fontSize = 40;
                            ltrTextBox.fontStyle = FontStyles.Normal;

                            ltrTextBox.ForceMeshUpdate();
                        }
                    }

                    // פייד אין
                    yield return StartCoroutine(Fade(0f, 1f, fadeDuration));
                    break;

                }

            }


            yield return null;
          }

          Debug.Log("Loop ended.");
      }

    
    public void  SetStopFadeSequence()
    {

        isTimerStopped = true;
    }


}
