using System.Collections.Generic;
using UnityEngine;

public class DownJudge : MonoBehaviour
{
    private static DownJudge instance;
    public static DownJudge Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject obj = new GameObject("UpJudge");
                instance = obj.AddComponent<DownJudge>();
            }
            return instance;
        }
    }

    private AnimationController ani;
    private Queue<UnderNote> noteQueue;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;

        JudgeManager.Instance.SetDownJudge(this);

        ani = GetComponent<AnimationController>();
        noteQueue = new Queue<UnderNote>();
    }

    private void Update()
    {
        Judge();
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

    private void Judge()
    {
        if (GameManager.Instance.CurrentState != E_GameState.Play) return;

        if (Input.GetButtonDown("KeyDown"))
        {
            if (noteQueue.Count > 0)
            {
                UnderNote note = noteQueue.Peek();

                Vector2 notePos = note.transform.position;
                Vector2 judgementPos = JudgeManager.Instance.GetDownJudgeMentPosition();

                float distance = Mathf.Abs(notePos.x - judgementPos.x);

                if (distance <= Number.PERFECT_DISTANCE)
                {
                    Debug.Log("Perfect Under Note");
                    ScoreManager.Instance.ScoreProcess(E_Judge.Perfect);
                    noteQueue.Dequeue();
                    ObjectPoolManager.Instance.Return(note.gameObject);
                }
                else if (distance <= Number.COOL_DISTANCE)
                {
                    Debug.Log("Cool Under Note");
                    ScoreManager.Instance.ScoreProcess(E_Judge.Cool);
                    noteQueue.Dequeue();
                    ObjectPoolManager.Instance.Return(note.gameObject);
                }

                UIManager.Instance.GetShowUI();
            }
        }
    }

    public void AddNoteToQueue(Note _note)
    {
        if (_note is UnderNote underNote)
        {
            noteQueue.Enqueue(underNote);
        }
    }
}
