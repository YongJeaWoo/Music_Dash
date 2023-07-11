using SingletonComponent.Component;
using System.Collections;
using UnityEngine;

public class GameManager : SingletonComponent<GameManager>
{
    private E_GameState currentState = E_GameState.Init;

    public GameObject refreshParent;

    public event GameStateChanged OnGameStateChanged;
    public delegate void GameStateChanged(E_GameState newState);

    private WaitForSeconds waitSeconds = new WaitForSeconds(1f);

    private Judges upJudge;
    private Judges downJudge;

    private Verdicts upVerdict;
    private Verdicts downVerdict;

    #region Property
    public E_GameState CurrentState
    {
        get => currentState;
        set
        {
            ChangeState(value);
        }
    }

    public Judges UpJudge => upJudge;
    public Judges DownJudge => downJudge;

    public Verdicts UpVerdict => upVerdict;
    public Verdicts DownVerdict => downVerdict;
    #endregion

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
                    Init();
                    CurrentState = E_GameState.Count;
                    break;
                }
            case E_GameState.Count:
                {
                    Count();
                    break;
                }
            case E_GameState.Ready:
                {
                    Ready();
                    break;
                }
            case E_GameState.Play:
                {
                    Debug.Log($"Now : {_state}");
                    // Play();
                    break;
                }
            case E_GameState.GameOver:
                {
                    Debug.Log($"Now : {_state}");
                    NoteManager.Instance.ClearNotes();
                    NoteManager.Instance.ResetMidiFile();
                    StartCoroutine(nameof(ScaleTime));
                    UIManager.Instance.TurnOnText(false);
                    break;
                }
            case E_GameState.Result:
                {
                    Debug.Log($"Now : {_state}");
                    Result();
                    UIManager.Instance.TurnOnText(false);
                    break;
                }
        }

        return false;
    }

    #region Check status
    private void Init()
    {
        JudgeManager judgeManager = JudgeManager.Instance;
        
        PlayerManager.Instance.InitPlayer();
        var getPlayer = PlayerManager.Instance.GetPlayer();
        getPlayer.InitializePlayer();
        NoteManager.Instance.ResetMidiFile();
        JudgeManager.Instance.SetJudgementPosition();
        UIManager.Instance.TurnOnText(true);

        upJudge = judgeManager.GetUpJudgement();
        downJudge = judgeManager.GetdownJudgement();

        upVerdict = judgeManager.GetUpVerdict();
        downVerdict = judgeManager.GetDownVerdict();

        NoteManager.Instance.SetJudgement();
        ScoreManager.Instance.SetVerdict();
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
        // MusicStart();
    }

    private IEnumerator ScaleTime()
    {
        Time.timeScale = 0.6f;
        yield return waitSeconds;

        yield return new WaitForEndOfFrame();
        PanelManager.Instance.GameOverPanel();
        Time.timeScale = 1f;
    }

    public void ChangeGameOverState()
    {
        CurrentState = E_GameState.GameOver;
    }

    private void Result() 
    {
        PanelManager.Instance.ResultPanel();
    }

    #endregion

    private IEnumerator GetReady()
    {
        yield return new WaitUntil(() => !AudioManager.Instance.audioSource.isPlaying);
        AudioManager.Instance.Stop();
        yield return new WaitForSeconds(0.3f);
        StartCoroutine(InitMidiFileAndPlay());
    }

    private IEnumerator InitMidiFileAndPlay()
    {
        NoteManager.Instance.StartInitMidiFile();
        yield return new WaitUntil(() => NoteManager.Instance.IsMidiFileInitialized);
        CurrentState = E_GameState.Play;
    }

    public void MusicStart()
    {
        MusicData data = MusicDataManager.Instance.GetCurrentMusic();

        if (data != null)
        {
            AudioManager.Instance.PlayMusicData(data);
            StartCoroutine(nameof(WaitForMusicEnd));
        }
        else
        {
            Debug.LogError("No music data selected.");
        }
    }
    
    private IEnumerator WaitForMusicEnd()
    {
        yield return new WaitUntil(() => !AudioManager.Instance.audioSource.isPlaying);
        CurrentState = E_GameState.Result;
    }
}