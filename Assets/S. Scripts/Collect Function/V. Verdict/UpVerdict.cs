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

    public void HandleVerdictStateChanged(E_Judge judgeState)
    {
        switch (judgeState)
        {
            case E_Judge.None:
                break;
            case E_Judge.Miss:
                break;
            case E_Judge.Cool:
                ani.VerdictAnimationPlay(E_Judge.Cool);
                break;
            case E_Judge.Perfect:
                ani.VerdictAnimationPlay(E_Judge.Perfect);
                break;
        }
    }
}