using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(SpriteRenderer))]
public class RainbowWorldController : MonoBehaviour
{
    SpriteRenderer sr;
    [SerializeField] float fadeDuration = 1f;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        var c = sr.color;
        c.a = 0;
        sr.color = c;
    }

    public void ShowRainbow()
    {
        sr.DOKill();
        sr.DOFade(1f, fadeDuration)
                      .From(0f)             
                      .SetEase(Ease.InExpo);
    }

    public void HideRainbow()
    {
        sr.DOKill();
        sr.DOFade(0f, 0.1f)
          .SetEase(Ease.Linear);
    }
}
