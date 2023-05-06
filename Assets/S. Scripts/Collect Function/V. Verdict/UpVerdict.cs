using UnityEngine;

public class UpVerdict : MonoBehaviour
{
    private Animator ani;

    private void Awake()
    {
        SetInit();
    }

    private void SetInit()
    {
        ani = GetComponent<Animator>();

        JudgeManager.Instance.SetUpVerdict(this);
    }
}
