using System.Collections.Generic;
using UnityEngine;

public abstract class Judges : MonoBehaviour
{
    [SerializeField] protected string keyCode = "KeyDown";

    protected AnimationController ani;
    protected Queue<Note> noteQueue;

    protected virtual void Awake()
    {
        ani = GetComponent<AnimationController>();
        noteQueue = new Queue<Note>();
    }

    protected virtual void Update()
    {
        if (GameManager.Instance.CurrentState == E_GameState.Init) return;

        MissNote();
        Judge();
    }

    protected virtual void OnEnable()
    {
        GameManager.Instance.OnGameStateChanged += HandleGameStateChanged;
    }

    protected virtual void OnDisable()
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

    protected void MissNote()
    {
        if (noteQueue.Count > 0)
        {
            Note note = noteQueue.Peek();

            Vector2 notePos = note.transform.position;
            Vector2 judgementPos = transform.position;

            float distance = notePos.x - judgementPos.x;

            if (distance < Number.MISS_DISTANCE)
            {
                noteQueue.Dequeue();
            }
        }
    }

    protected Note ActiveNote()
    {
        if (noteQueue.Count > 0)
        {
            Note note = noteQueue.Peek();

            if (note.isActiveAndEnabled)
            {
                return note;
            }
            else
            {
                noteQueue.Dequeue();
                return ActiveNote();
            }
        }

        return null;
    }


    public void AddNoteToQueue(Note _note)
    {
        if (_note != null && _note.gameObject.activeSelf)
        {
            noteQueue.Enqueue(_note);
        }
    }

    private void Judge()
    {
        if (GameManager.Instance.CurrentState != E_GameState.Play) return;

        if (Input.GetButtonDown(keyCode))
        {
            Debug.Log(keyCode);
            if (noteQueue.Count > 0)
            {
                Note note = ActiveNote();

                if (note == null) return;

                Vector2 notePos = note.transform.position;
                Vector2 judgementPos = transform.position;

                float distance = Mathf.Abs(notePos.x - judgementPos.x);

                if (keyCode == "KeyUp") PlayerManager.Instance.PlayerJumpAttack();

                if (distance <= Number.PERFECT_DISTANCE)
                {
                    Debug.Log("Perfect");
                    ScoreManager.Instance.ScoreProcess(E_Judge.Perfect);
                    noteQueue.Dequeue();
                    ObjectPoolManager.Instance.Return(note.gameObject);
                }
                else if (distance <= Number.COOL_DISTANCE)
                {
                    Debug.Log("Cool");
                    ScoreManager.Instance.ScoreProcess(E_Judge.Cool);
                    noteQueue.Dequeue();
                    ObjectPoolManager.Instance.Return(note.gameObject);
                }

                UIManager.Instance.ShowUI();
            }
        }
    }
}