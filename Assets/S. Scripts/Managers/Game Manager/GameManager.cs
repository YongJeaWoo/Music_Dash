using SingletonComponent.Component;
using System.Collections;
using UnityEngine;

public class GameManager : SingletonComponent<GameManager>
{
    [SerializeField] private GameObject refreshParent;

    private E_GameState currentState = E_GameState.Init;

    public delegate void GameStateChanged(E_GameState newState);
    public event GameStateChanged OnGameStateChanged;

    #region Property
    public E_GameState CurrentState
    {
        get => currentState;
        set
        {
            ChangeState(value);
        }
    }

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
        OnGameStateChanged?.Invoke(_state);

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
                    StartCreateNote();
                    break;
                }
            case E_GameState.GameOver:
                {
                    Debug.Log(CurrentState);
                    break;
                }
            case E_GameState.Result:
                {
                    Debug.Log(CurrentState);
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

    public void GameOver()
    {
        OptionManager.Instance.GameOverPanel();
    }

    private void Result() 
    {
        
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

    private void MusicStart()
    {
        MusicData data = MusicDataManager.Instance.GetCurrentMusic();
        AudioManager.Instance.PlayMusicData(data);

        if (!AudioManager.Instance.audioSource.isPlaying)
        {
            Invoke(nameof(ChangeResult), 2f);
        }
    }

    private void ChangeResult()
    {
        CurrentState = E_GameState.Result;
    }

    private void StartCreateNote()
    {
        StartCoroutine(CreateNoteCoolTime(2f));
    }

    private IEnumerator CreateNoteCoolTime(float _coolTime)
    {
        while (true)
        {
            ObjectPoolManager.Instance.Create("UpperNote", null, refreshParent.transform.position);
            ObjectPoolManager.Instance.Create("UnderNote", null, refreshParent.transform.position);
            NoteManager.Instance.GetNote().CheckYPos();
            yield return new WaitForSeconds(_coolTime);
        }
    }
}