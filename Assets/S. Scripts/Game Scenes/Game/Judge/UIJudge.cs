using UnityEngine;

public class UIJudge : MonoBehaviour
{
    [SerializeField]
    private Vector2 myPositionTransform;
    [SerializeField]
    private Vector2 offset;

    private void Start()
    {
        InitUpJudge();
    }

    private void InitUpJudge()
    {
        transform.position = Camera.main.WorldToScreenPoint(myPositionTransform + offset);
    }
}