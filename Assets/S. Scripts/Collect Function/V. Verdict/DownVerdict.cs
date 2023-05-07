using UnityEngine;

public class DownVerdict : MonoBehaviour
{
    private AnimationController ani;

    private void OnEnable()
    {
        SetInit();
    }

    private void SetInit()
    {
        ani = GetComponent<AnimationController>();

        JudgeManager.Instance.SetDownVerdict(this);
    }
}
