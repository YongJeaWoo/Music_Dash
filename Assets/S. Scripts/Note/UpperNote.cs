public class UpperNote : Note
{
    public override void CheckYPos()
    {
        var posY = transform.position.y;
        posY = 0f;
    }
}
