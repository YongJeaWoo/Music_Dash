public class DownJudge : Judges
{
    protected override void Awake()
    {
        base.Awake();
        JudgeManager.Instance.SetDownJudge(this);
        keyCode = "KeyDown";
    }

    protected override Verdicts GetVerdicts()
    {
        return JudgeManager.Instance.GetDownVerdict();
    }
}