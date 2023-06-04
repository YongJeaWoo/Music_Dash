using UnityEngine;

public class Note : MonoBehaviour
{
    private string noteName;
    private int noteNumber;

    private Vector3 direction;
    private float speed = 8f;

    public string NoteName => noteName;
    public int NoteNumber => noteNumber;

    private void OnEnable()
    {
        NoteManager.Instance.InitNote(this);
    }

    private void Update()
    {
        MoveNote();
    }

    private void MoveNote()
    {
        transform.Translate(direction * Time.deltaTime * speed);
        DirectionNote(Vector3.left.normalized);
    }

    private void DirectionNote(Vector3 _dir)
    {
        direction = _dir;
    }

    private void OnBecameInvisible()
    {
        ObjectPoolManager.Instance.Return(gameObject);
    }

    public void InitializeNoteData(string _noteName, int _noteNumber, float _noteStartTick, float _noteDurationTick)
    {
        noteName = _noteName;
        noteNumber = _noteNumber;
    }

    #region Virtual Method
    public virtual void CheckYPos() { }
    #endregion
}
