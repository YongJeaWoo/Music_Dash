public class UpJudge : Judges
{
    protected override void Awake()
    {
        base.Awake();
        JudgeManager.Instance.SetUpJudge(this);
        keyCode = "KeyUp";
    }

    protected override Verdicts GetVerdicts()
    {
        return JudgeManager.Instance.GetUpVerdict();
    }
}