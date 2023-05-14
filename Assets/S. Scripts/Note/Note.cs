using UnityEngine;

public class Note : MonoBehaviour
{
    private Vector3 direction;
    private float speed = 2f;
    private Bounds bounds;

    public Bounds Bounds
    {
        get => bounds;
    }

    private void Awake()
    {
        InitAwake();
    }

    private void InitAwake()
    {
        bounds = GetComponent<Renderer>().bounds;
        NoteManager.Instance.SetNote(this);
        NoteManager.Instance.SetBounds(this);
        CheckYPos();        
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

    #region Virtual Method
    public virtual void CheckYPos() { }
    #endregion
}
