using UnityEngine;

public class ButtonsBehavior : MonoBehaviour
{
    [SerializeField] private RainbowWorldController rainbow;
    Animator animator;

    private void Awake()
    {
        animator = GameObject.Find("RayRoot")
                     .GetComponent<Animator>();

    }
    public void StartAnim(AnimationType animationType)
    {
        animator.Play("Idle", 0, 0f);
        animator.Update(0f);
        rainbow.HideRainbow();

        string stateName = animationType.ToString();
        animator.Play(stateName, 0, 0f);
        if(stateName == "Light")
        {
            rainbow.ShowRainbow();
        }

        Debug.Log("Starting animation: " + stateName);
    }

    public void StartIR() => StartAnim(AnimationType.IR);
    public void StartUV() => StartAnim(AnimationType.UV);
    public void StartLight() => StartAnim(AnimationType.Light);
    public void StartMeteorite() => StartAnim(AnimationType.Meteorite);


    public enum AnimationType
    {
        IR,
        UV,
        Light,
        Meteorite
    }

}
