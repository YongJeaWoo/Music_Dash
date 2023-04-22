using UnityEngine;

public class MovingBackGround : MonoBehaviour
{
    [SerializeField]
    private Transform nextObj;

    [SerializeField]
    private float scrollAmount, moveSpeed;

    [SerializeField]
    private Vector3 moveDirection;

    private void Update()
    {
        MoveBackGround();
    }

    private void MoveBackGround()
    {
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        if (transform.position.x <= -scrollAmount)
            transform.position = nextObj.position - moveDirection * scrollAmount;
    }
}
