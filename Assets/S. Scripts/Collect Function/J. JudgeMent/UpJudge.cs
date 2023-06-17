using System.Collections.Generic;
using UnityEngine;

public class UpJudge : MonoBehaviour
{
    private static UpJudge instance;
    public static UpJudge Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject obj = new GameObject("UpJudge");
                instance = obj.AddComponent<UpJudge>();
            }
            return instance;
        }
    }

    private AnimationController ani;
    private Queue<UpperNote> noteQueue;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;

        JudgeManager.Instance.SetUpJudge(this);
        
        ani = GetComponent<AnimationController>();
        noteQueue = new Queue<UpperNote>();
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

        if (Input.GetButtonDown("KeyUp"))
        {
            if (noteQueue.Count > 0)
            {
                UpperNote note = noteQueue.Peek();

                Vector2 notePos = note.transform.position;
                Vector2 judgementPos = JudgeManager.Instance.GetUpJudgeMentPosition();

                float distance = Mathf.Abs(notePos.x - judgementPos.x);

                if (distance <= Number.PERFECT_DISTANCE)
                {
                    Debug.Log("Perfect Upper Note");
                    ObjectPoolManager.Instance.Return(note.gameObject);
                }
                else if (distance <= Number.COOL_DISTANCE)
                {
                    Debug.Log("Cool Upper Note");
                    ObjectPoolManager.Instance.Return(note.gameObject);
                }

                noteQueue.Dequeue();
            }
        }
    }

    public void AddNoteToQueue(Note _note)
    {
        if (_note is UpperNote upperNote)
        {
            noteQueue.Enqueue(upperNote);
        }
    }
}