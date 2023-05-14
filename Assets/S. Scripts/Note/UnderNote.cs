using UnityEngine;

public class UnderNote : Note
{
    public override void CheckYPos()
    {
        Vector3 pos = transform.position;
        pos.y = Number.UNDER_Y;
        transform.position = pos;
    }
}
