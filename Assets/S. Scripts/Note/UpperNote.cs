using UnityEngine;

public class UpperNote : Note
{
    public override void CheckYPos()
    {
        Vector3 pos = transform.position;
        pos.y = Number.UPPER_Y;
        transform.position = pos;
    }
}
