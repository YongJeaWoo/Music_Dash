public class DownVerdict : Verdicts
{
    protected override void Awake()
    {
        base.Awake();
        JudgeManager.Instance.SetDownVerdict(this);
    }
}
