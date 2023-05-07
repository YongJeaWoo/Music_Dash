public class UnderNote : Note
{
    public override void CheckYPos()
    {
        var posY = transform.position.y;
        posY = -5.8f;
    }
}
