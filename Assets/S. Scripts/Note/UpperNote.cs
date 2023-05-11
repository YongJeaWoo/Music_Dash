using UnityEngine;

public class UpperNote : Note
{
    public override void CheckYPos()
    {
        Vector3 pos = transform.position;
        pos.y = 0f;
        transform.position = pos;
    }
}
