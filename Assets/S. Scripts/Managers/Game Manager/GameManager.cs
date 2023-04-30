using SingletonComponent.Component;
using System.Collections;
using UnityEngine;

public class GameManager : SingletonComponent<GameManager>
{
    private E_GameState currentState = E_GameState.Init;
    public E_GameState CurrentState
    {
        get => currentState;
        set
        {
            ChangeState(value);
        }
    }

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

    private bool ChangeState(E_GameState _state)
    {
        currentState = _state;

        switch (_state)
        {
            case E_GameState.Init:
                {
                    Debug.Log(CurrentState);
                    Init();
                    CurrentState = E_GameState.Count;
                    break;
                }
            case E_GameState.Count:
                {
                    Debug.Log(CurrentState);
                    Count();
                    break;
                }
            case E_GameState.Ready:
                {
                    Debug.Log(CurrentState);
                    Ready();
                    break;
                }
            case E_GameState.Play:
                {
                    Debug.Log(CurrentState);
                    Play();

                    if (PlayerManager.Instance.GetPlayerHP() <= 0) CurrentState = E_GameState.GameOver;
                    break;
                }
            case E_GameState.Clear:
                {
                    Debug.Log(CurrentState);
                    break;
                }
            case E_GameState.GameOver:
                {
                    Debug.Log(CurrentState);
                    GameOver();
                    break;
                }
            case E_GameState.Result:
                {
                    Debug.Log(CurrentState);
                    Result();
                    break;
                }
        }
        return false;
    }

    #region Check status
    private void Init()
    {
        PlayerManager.Instance.InitPlayer();
        var getPlayer = PlayerManager.Instance.GetPlayer();
        getPlayer.InitializePlayer();

        JudgeManager.Instance.SetJudgementPosition();
    }

    private void Count()
    {
        AudioManager.Instance.CountPlay("Start");
    }

    private void Ready()
    {
        StartCoroutine(GetReady());
    }

    private void Play()
    {
        MusicStart();
    }

    private void Clear()
    {

    }

    private void GameOver()
    {
        StartCoroutine(Change());
    }

    private void Result() 
    {
        OptionManager.Instance.GameOverPanel();
    }
    #endregion

    private IEnumerator GetReady()
    {
        yield return new WaitUntil(() => !AudioManager.Instance.audioSource.isPlaying);
        AudioManager.Instance.Stop();
        yield return new WaitForSeconds(0.3f);
        CurrentState = E_GameState.Play;
        yield return null;
    }

    private IEnumerator Change()
    {
        yield return new WaitForSeconds(3.0f);
        CurrentState = E_GameState.Result;
        yield return null;
    }

    private void MusicStart()
    {
        MusicData data = MusicDataManager.Instance.GetCurrentMusic();
        AudioManager.Instance.PlayMusicData(data);
    }
}