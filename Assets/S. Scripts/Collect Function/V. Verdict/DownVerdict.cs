using UnityEngine;

public class DownVerdict : MonoBehaviour
{
    private Animator ani;

    private void Awake()
    {
        SetInit();
    }

    private void SetInit()
    {
        ani = GetComponent<Animator>();

        JudgeManager.Instance.SetDownVerdict(this);
    }
}
