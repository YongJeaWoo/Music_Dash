using SingletonComponent.Component;
using System.Collections.Generic;
using UnityEngine;

public class JudgeManager : SingletonComponent<JudgeManager>
{
    #region Property
    //public SpriteRenderer UpVerdict
    //{
    //    get => upVerdict;
    //}
    //public SpriteRenderer DownVerDict
    //{
    //    get =>downVerdict;
    //}
    #endregion

    // [SerializeField]
    // private SpriteRenderer upVerdict, downVerdict;

    private UpJudge upJudgeMent = null;
    private DownJudge downJudgeMent = null;

    private Dictionary<E_Judge, Sprite> judgeSprite = new Dictionary<E_Judge, Sprite>();

    #region SingleTon
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

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        judgeSprite.Add(E_Judge.Cool, Resources.Load<Sprite>(JudgeStorage.JUDGE_COOL_PATH));
        judgeSprite.Add(E_Judge.Perfect, Resources.Load<Sprite>(JudgeStorage.JUDGE_PERFECT_PATH));
    }

    public UpJudge GetUpJudge() => upJudgeMent;
    public void SetUpJudge(UpJudge _judge)
    {
        upJudgeMent = _judge;
    }
    public DownJudge GetDownJudge() => downJudgeMent;
    public void SetDownJudge(DownJudge _judge)
    {
        downJudgeMent = _judge;
    }

    private void ScoreProcess(E_Judge judge)
    {
        switch (judge)
        {
            case E_Judge.None:
                break;
            case E_Judge.Miss:
                {
                    GameManager.Instance.Combo = 0;
                    GameManager.Instance.Score -= 0;
                    // 플레이어 체력 감소
                    

                    if (GameManager.Instance.Score <= 0) GameManager.Instance.Score = 0;
                }
                break;
            case E_Judge.Cool:
                {
                    GameManager.Instance.Combo++;
                    GameManager.Instance.Score += 10;
                }
                break;
            case E_Judge.Perfect:
                {
                    GameManager.Instance.Combo++;
                    GameManager.Instance.Score += 30;
                }
                break;
        }
    }

    public void SetJudgementPosition()
    {
        Vector2 playerPos = PlayerManager.Instance.GetPlayerPos();

        Vector2 upJudgementPos = new Vector2(playerPos.x + 12f, playerPos.y + 5.5f);
        Vector2 downJudgementPos = new Vector2(playerPos.x + 12f, playerPos.y - 0.3f);

        // Vector2 upJudgePos = new Vector2(upJudgementPos.x, upJudgementPos.y + 2f);
        // Vector2 downJudgePos = new Vector2(downJudgementPos.x, downJudgementPos.y + 2f);

        // upVerdict.transform.position = upJudgePos;
        // downVerdict.transform.position = downJudgePos;

        upJudgeMent.transform.position = upJudgementPos;
        downJudgeMent.transform.position = downJudgementPos;
    }

    public void JudgeProcess(E_Judge judge)
    {


        ScoreProcess(judge);
    }
}