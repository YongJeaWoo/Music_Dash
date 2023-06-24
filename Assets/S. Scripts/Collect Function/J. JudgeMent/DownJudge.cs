public class DownJudge : Judges
{
    protected override void Awake()
    {
        base.Awake();
        JudgeManager.Instance.SetDownJudge(this);
        keyCode = "KeyDown";
    }
}