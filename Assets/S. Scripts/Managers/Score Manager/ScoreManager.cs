using SingletonComponent.Component;

public class ScoreManager : SingletonComponent<ScoreManager>
{
    private int score = 0;
    private int combo = 0;

    private Verdicts upVerdict;
    private Verdicts downVerdict;

    #region Singleton
    protected override void AwakeInstance()
    {

    }

    protected override bool InitInstance()
    {
        return true;
    }

    protected override void ReleaseInstance()
    {
        
    }
    #endregion

    public int GetScore() => score;
    public int GetCombo() => combo;

    public int SetComboZero() => combo = 0;

    public void SetVerdict()
    {
        upVerdict = JudgeManager.Instance.GetUpVerdict();
        downVerdict = JudgeManager.Instance.GetDownVerdict();
    }

    public void SetVerdict(Verdicts verdict, E_Judge judge)
    {
        verdict.HandleVerdictStateChanged(judge);
    }

    public void ScoreProcess(E_Judge judge)
    {
        switch (judge)
        {
            case E_Judge.None:
                break;
            case E_Judge.Cool:
                {
                    combo++;
                    score += 10;
                }
                break;
            case E_Judge.Perfect:
                {
                    combo++;
                    score += 20;
                }
                break;
        }
    }
}
