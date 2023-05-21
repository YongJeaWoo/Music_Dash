using UnityEngine;

public class Note : MonoBehaviour
{
    private Vector3 direction;
    private float speed = 2f;

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

    #region Virtual Method
    public virtual void CheckYPos() { }
    #endregion
}
