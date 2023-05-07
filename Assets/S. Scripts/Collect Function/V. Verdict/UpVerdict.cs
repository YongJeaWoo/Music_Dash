using UnityEngine;

public class UpVerdict : MonoBehaviour
{
    private AnimationController ani;

    private void OnEnable()
    {
        SetInit();
    }

    private void SetInit()
    {
        ani = GetComponent<AnimationController>();

        JudgeManager.Instance.SetUpVerdict(this);
    }
}
