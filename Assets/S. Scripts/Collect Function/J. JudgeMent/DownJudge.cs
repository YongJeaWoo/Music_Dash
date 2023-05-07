using UnityEngine;

public class DownJudge : MonoBehaviour
{
    private AnimationController ani;

    private void Awake()
    {
        JudgeManager.Instance.SetDownJudge(this);

        ani = GetComponent<AnimationController>();
    }

    private void OnEnable()
    {
        GameManager.Instance.OnGameStateChanged += HandleGameStateChanged;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnGameStateChanged -= HandleGameStateChanged;
    }

    private void HandleGameStateChanged(E_GameState newState)
    {
        switch (newState)
        {
            case E_GameState.Init:
                ani.JudgeAnimationPlay(E_JudgeState.Init);
                break;
            case E_GameState.Count:
                ani.JudgeAnimationPlay(E_JudgeState.Ready);
                break;
            default:
                break;
        }
    }
}
