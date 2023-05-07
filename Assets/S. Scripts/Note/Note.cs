using UnityEngine;

public class Note : MonoBehaviour
{
    private E_NoteType upperNote;
    private E_NoteType underNote;

    private void Awake()
    {
        Init();
        CheckYPos();
    }

    private void Update()
    {
        
    }

    private void Init()
    {
        upperNote = E_NoteType.Upper;
        underNote = E_NoteType.Under;
    }

    public virtual void CheckYPos() { }
}
