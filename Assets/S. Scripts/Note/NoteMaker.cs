using UnityEngine;
using UnityEngine.Pool;

public enum E_NoteState
{
    Active,
    Inactive,
    Destroyed
}

public class NoteMaker : MonoBehaviour
{
    [SerializeField]
    private GameObject upperNotePrefab;
    [SerializeField]
    private GameObject underNotePrefab;

    private IObjectPool<Note> upperNotePool;
    private IObjectPool<Note> underNotePool;

    private const int maxPoolSize = 10;
    private const float noteCreationInterval = 2f;
    
    private float elapsedTime;

    private void Awake()
    {
        NoteObjectPool();
    }

    private void Update()
    {
        CheckNote();
    }

    private void NoteObjectPool()
    {
        upperNotePool = new ObjectPool<Note>(CreateUpperNote, OnGetNote, OnReleaseNote, OnDestroyNote, maxSize: maxPoolSize);
        underNotePool = new ObjectPool<Note>(CreateUnderNote, OnGetNote, OnReleaseNote, OnDestroyNote, maxSize: maxPoolSize);
    }

    private void CheckNote()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= noteCreationInterval)
        {
            CreateNotes();
            elapsedTime = 0f;
        }
    }

    private void CreateNotes()
    {
        var upperNote = upperNotePool.Get();
        var underNote = underNotePool.Get();

        upperNote.DirectionNote(Vector2.left.normalized);
        underNote.DirectionNote(Vector2.left.normalized);
    }

    private void SetNoteState(Note note, E_NoteState state)
    {
        switch (state)
        {
            case E_NoteState.Active:
                note.gameObject.SetActive(true);
                break;
            case E_NoteState.Inactive:
                note.gameObject.SetActive(false);
                break;
            case E_NoteState.Destroyed:
                note.DestroyNote();
                break;
        }
    }

    #region Object Pool Method
    private Note CreateUpperNote()
    {
        Note upperNote = Instantiate(upperNotePrefab).GetComponent<Note>();
        upperNote.SetObjectPool(upperNotePool);
        return upperNote;
    }

    private Note CreateUnderNote()
    {
        Note underNote = Instantiate(underNotePrefab).GetComponent<Note>();
        underNote.SetObjectPool(underNotePool);
        return underNote;
    }

    private void OnGetNote(Note note)
    {
        SetNoteState(note, E_NoteState.Active);
    }

    private void OnReleaseNote(Note note)
    {
        SetNoteState(note, E_NoteState.Inactive);
    }

    private void OnDestroyNote(Note note)
    {
        SetNoteState(note, E_NoteState.Destroyed);
    }
    #endregion
}