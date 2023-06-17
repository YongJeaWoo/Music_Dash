using SingletonComponent.Component;

public class ScoreManager : SingletonComponent<ScoreManager>
{
    private int score = 0;
    private int combo = 0;

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

    public void ScoreProcess(E_Judge judge)
    {
        switch (judge)
        {
            case E_Judge.None:
                break;
            case E_Judge.Miss:
                {
                    combo = 0;
                    score -= 0;
                    // 플레이어 체력 감소

                    if (score <= 0) score = 0;
                }
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
