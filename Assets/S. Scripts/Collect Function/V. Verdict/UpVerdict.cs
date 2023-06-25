public class UpVerdict : Verdicts
{
    protected override void Awake()
    {
        base.Awake();
        JudgeManager.Instance.SetUpVerdict(this);
    }
}