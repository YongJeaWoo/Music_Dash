using UnityEngine;

public class DownJudge : MonoBehaviour
{
    private Animator ani;

    private void Awake()
    {
        SetInit();
    }

    private void SetInit()
    {
        ani = GetComponent<Animator>();

        JudgeManager.Instance.SetDownJudge(this);

        if (GameManager.Instance.CurrentState == E_GameState.Init)
        {
            ani.SetTrigger(AnimatorName.JUDGEMENT_NAME);
        }
    }
}
