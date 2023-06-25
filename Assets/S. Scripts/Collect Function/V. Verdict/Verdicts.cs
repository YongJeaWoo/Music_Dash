using UnityEngine;

public class Verdicts : MonoBehaviour
{
    protected AnimationController ani;
    protected SpriteRenderer render;

    protected virtual void Awake()
    {
        ani = GetComponent<AnimationController>();
        render = GetComponent<SpriteRenderer>();
    }

    public void HandleVerdictStateChanged(E_Judge judgeState)
    {
        switch (judgeState)
        {
            case E_Judge.None:
                break;
            case E_Judge.Cool:
                render.sprite = JudgeManager.Instance.GetJudgeSprite(E_Judge.Cool);
                ani.VerdictAnimationPlay(E_Judge.Cool);
                break;
            case E_Judge.Perfect:
                render.sprite = JudgeManager.Instance.GetJudgeSprite(E_Judge.Perfect);
                ani.VerdictAnimationPlay(E_Judge.Perfect);
                break;
        }
    }
}
