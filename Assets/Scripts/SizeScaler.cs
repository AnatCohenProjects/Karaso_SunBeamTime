using UnityEngine;

public class SizeScaler : MonoBehaviour
{
    [SerializeField] private SpriteRenderer animationSpriteRenderer;
    [SerializeField] private SpriteRenderer bgSpriteRenderer;
    [SerializeField] private Camera mainCamera;
    void Start()
    {
        //FitSpriteToScreen(animationSpriteRenderer.GetComponent<SpriteRenderer>());
        //FitSpriteToScreen(bgSpriteRenderer.GetComponent<SpriteRenderer>());
    }

    void FitSpriteToScreen(SpriteRenderer sr)
    {

        if (sr == null || sr.sprite == null) return;

        float screenHeight = mainCamera.orthographicSize * 2f;
        float screenWidth = screenHeight * Screen.width / Screen.height;

        Vector2 spriteSize = sr.sprite.bounds.size;

        sr.transform.localScale = new Vector3(
            screenWidth / spriteSize.x,
            screenHeight / spriteSize.y,
            1f
        );
    }

}
