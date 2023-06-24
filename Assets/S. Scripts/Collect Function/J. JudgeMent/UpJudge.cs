public class UpJudge : Judges
{
    protected override void Awake()
    {
        base.Awake();
        JudgeManager.Instance.SetUpJudge(this);
        keyCode = "KeyUp";
    }
}