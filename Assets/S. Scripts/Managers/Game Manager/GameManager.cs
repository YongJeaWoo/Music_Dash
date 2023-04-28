using SingletonComponent.Component;
using System.Collections;
using UnityEngine;

public class GameManager : SingletonComponent<GameManager>
{
    // ³ëÆ®

    private E_GameState currentState = E_GameState.Init;

    #region Property
    public int Combo
    {
        get => combo;
        set => combo = value;
    }
    public int Score
    {
        get => score;
        set => score = value;
    }
    #endregion

    private int combo;
    private int score;

    #region SingleTon
    protected override void AwakeInstance()
    {
    }

    protected override bool InitInstance()
    {
        return true;
    }

    protected override void ReleaseInstance()
    {
    }
    #endregion

    private void Update()
    {
        StatClassification();
    }

    private void StatClassification()
    {
        switch (currentState)
        {
            case E_GameState.Init:
                {
                    Init();
                    currentState = E_GameState.Ready;
                }
                break;
            case E_GameState.Ready:
                {
                    Ready();
                    currentState = E_GameState.Play;
                }
                break;
            case E_GameState.Play:
                {

                }
                break;
            case E_GameState.Clear:
                {

                }
                break;
            case E_GameState.GameOver:
                {

                }
                break;
        } 
    }

    private void Init()
    {
        AudioManager.Instance.CountPlay("Start");
        StartCoroutine(WaitForCountPlay());
    }

    private void Ready()
    {
        StartCoroutine(GetReady());
    }

    private IEnumerator WaitForCountPlay()
    {
        yield return new WaitForSeconds(1f);

        JudgeManager.Instance.UpJudgeAni.SetTrigger(AnimatorName.JUDGEMENT_NAME);
        JudgeManager.Instance.DownJudgeAni.SetTrigger(AnimatorName.JUDGEMENT_NAME);

        yield return null;
    }

    private IEnumerator GetReady()
    {
        yield return new WaitUntil(() => !AudioManager.Instance.audioSource.isPlaying);

        AudioManager.Instance.Stop();

        MusicData data = MusicDataManager.Instance.GetCurrentMusic();

        AudioManager.Instance.PlayMusicData(data);
    }
}