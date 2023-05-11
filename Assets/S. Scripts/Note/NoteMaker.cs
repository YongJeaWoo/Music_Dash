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

    private const int MaxPoolSize = 30;
    private const float NoteCreationInterval = 2f;

    private void Awake()
    {
        upperNotePool = new ObjectPool<Note>(CreateUpperNote, OnGetNote, OnReleaseNote, OnDestroyNote, maxSize: MaxPoolSize);
        underNotePool = new ObjectPool<Note>(CreateUnderNote, OnGetNote, OnReleaseNote, OnDestroyNote, maxSize: MaxPoolSize);

        Instantiate(upperNotePrefab, Vector3.zero, Quaternion.identity);
        Instantiate(underNotePrefab, Vector3.zero, Quaternion.identity);
    }

    private void Start()
    {
        InvokeRepeating(nameof(CreateNotes), NoteCreationInterval, NoteCreationInterval);
    }

    private void CreateNotes()
    {
        var upperNote = upperNotePool.Get();
        var underNote = underNotePool.Get();

        upperNote.MakeNote(Vector2.left.normalized);
        underNote.MakeNote(Vector2.left.normalized);
    }

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
                Destroy(note.gameObject);
                break;
        }
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
}
