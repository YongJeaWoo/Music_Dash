using SingletonComponent.Component;
using System.Collections;
using UnityEngine;

public class GameManager : SingletonComponent<GameManager>
{
    // ³ëÆ®

    E_GameStat currentStat = E_GameStat.Init;

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

    private void StatClassification()
    {
        switch (currentStat)
        {
            case E_GameStat.Init:
                {
                    Init();
                }
                break;
            case E_GameStat.Ready:
                {

                }
                break;
            case E_GameStat.Play:
                {

                }
                break;
            case E_GameStat.Clear:
                {

                }
                break;
            case E_GameStat.GameOver:
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

    private IEnumerator WaitForCountPlay()
    {
        yield return new WaitForSeconds(1f);

        JudgeManager.Instance.UpJudgeAni.SetTrigger(AnimatorName.JUDGEMENT_NAME);
        JudgeManager.Instance.DownJudgeAni.SetTrigger(AnimatorName.JUDGEMENT_NAME);

        yield return new WaitUntil(() => !AudioManager.Instance.audioSource.isPlaying);

        AudioManager.Instance.Stop();

        MusicData data = MusicDataManager.Instance.GetCurrentMusic();

        AudioManager.Instance.PlayMusicData(data);
    }
}