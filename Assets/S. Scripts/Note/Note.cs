using UnityEngine;
using UnityEngine.Pool;

public class Note : MonoBehaviour
{
    private Vector3 direction;
    private float speed = 2f;
    private IObjectPool<Note> pool;
    private Bounds bounds;

    private void Awake()
    {
        InitAwake();
    }

    private void InitAwake()
    {
        bounds = GetComponent<Renderer>().bounds;
        CheckYPos();
    }

    private void Update()
    {
        MoveNote();
    }

    private void MoveNote()
    {
        transform.Translate(direction * Time.deltaTime * speed);
    }

    public void DirectionNote(Vector3 _dir)
    {
        direction = _dir;

        if (Camera.main.WorldToScreenPoint(transform.position + bounds.extents.x * Vector3.right).x < 0f)
        {
            Invoke(nameof(DestroyNote), 2f);
        }
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
