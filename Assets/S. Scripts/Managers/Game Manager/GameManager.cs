using SingletonComponent.Component;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletonComponent<GameManager>
{
    private E_GameState currentState = E_GameState.Init;

    public GameObject refreshParent;

    public event GameStateChanged OnGameStateChanged;
    public delegate void GameStateChanged(E_GameState newState);

    #region Property
    public E_GameState CurrentState
    {
        get => currentState;
        set
        {
            ChangeState(value);
        }
    }

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
                    Play();
                    
                    break;
                }
            case E_GameState.GameOver:
                {
                    
                    break;
                }
            case E_GameState.Result:
                {
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
        NoteManager.Instance.ResetMidiFile();
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
        PanelManager.Instance.GameOverPanel();
        GameOverInputKey();
    }

    private void GameOverInputKey()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (PanelManager.Instance.IsOver) PanelManager.Instance.GameOverPanel();

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (PanelManager.Instance.IsOver) PanelManager.Instance.GameOverPanel();

            LoadingController.LoadScene("Select");
        }
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
        StartCoroutine(InitMidiFileAndPlay());
    }

    private IEnumerator InitMidiFileAndPlay()
    {
        NoteManager.Instance.StartInitMidiFile();
        yield return new WaitUntil(() => NoteManager.Instance.IsMidiFileInitialized);
        CurrentState = E_GameState.Play;
    }

    private void MusicStart()
    {
        MusicData data = MusicDataManager.Instance.GetCurrentMusic();

        if (data != null)
        {
            AudioManager.Instance.PlayMusicData(data);
        }
        else
        {
            Debug.LogError("No music data selected.");
        }
    }
    
    private void ChangeResult()
    {
        CurrentState = E_GameState.Result;
    }
}