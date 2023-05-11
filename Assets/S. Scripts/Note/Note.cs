using UnityEngine;
using UnityEngine.Pool;

public class Note : MonoBehaviour
{
    private Vector3 direction;

    private float speed = 1f;

    private IObjectPool<Note> pool;

    private void Awake()
    {
        CheckYPos();
    }

    private void Update()
    {
        MoveNote();
    }

    private void MoveNote()
    {
        transform.Translate(-speed * Time.deltaTime, 0, 0);
        
        if (transform.position.x < -10f)
        {
            Invoke(nameof(DestroyNote), 5f);
        }
    }

    public void MakeNote(Vector3 _dir)
    {
        direction = _dir;
    }

    public void SetObjectPool(IObjectPool<Note> _pool)
    {
        pool = _pool;
    }

    public void DestroyNote()
    {
        pool.Release(this);
    }

    #region Virtual Method
    public virtual void CheckYPos() { }
    #endregion
}
