using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField] float slowdownFactor = 0.05f;

    public float SlowdownFactor
    {
        get => slowdownFactor;
        set => slowdownFactor = value;
    }

    public void DoSlowMotion()
    {
        Time.timeScale = SlowdownFactor;
        Time.fixedDeltaTime = Time.timeScale * .02f;
    }

    public static void ResetTimeScale()
    {
        Time.timeScale      = 1f;
        Time.fixedDeltaTime = Time.timeScale * .02f;
    }
}