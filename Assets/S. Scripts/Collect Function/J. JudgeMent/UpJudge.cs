using UnityEngine;

public class UpJudge : MonoBehaviour
{
    private Animator ani;

    private void Awake()
    {
        SetInit();
    }

    private void SetInit()
    {
        ani = GetComponent<Animator>();

        JudgeManager.Instance.SetUpJudge(this);

        if (GameManager.Instance.CurrentState == E_GameState.Init)
        {
            ani.SetTrigger(AnimatorName.JUDGEMENT_NAME);
        }
    }
}
