using UnityEngine;

public class Note : MonoBehaviour
{
    private Vector3 direction;
    private float speed = 2f;
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

        if (Camera.main.WorldToScreenPoint(transform.position + bounds.extents.x * Vector3.right).x < 0f)
        {
            Destroy(gameObject);
        }
    }

    public void DirectionNote(Vector3 _dir)
    {
        direction = _dir;
    }

    #region Virtual Method
    public virtual void CheckYPos() { }
    #endregion
}
