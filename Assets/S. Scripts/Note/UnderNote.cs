using UnityEngine;

public class UnderNote : Note
{
    public override void CheckYPos()
    {
        Vector3 pos = transform.position;
        pos.y = -5.8f;
        transform.position = pos;
    }
}
