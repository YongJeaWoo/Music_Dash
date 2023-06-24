using SingletonComponent.Component;
using System.Collections.Generic;
using UnityEngine;

public class JudgeManager : SingletonComponent<JudgeManager>
{
    private UpVerdict upVerdict = null;
    private DownVerdict downVerdict = null;

    private Judges upJudgeMent = null;
    private Judges downJudgeMent = null;

    private Vector2 upJudgementPosition;
    private Vector2 downJudgementPosition;

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

    public void SetUpVerdict(UpVerdict _verdict) => upVerdict = _verdict;
    public void SetDownVerdict(DownVerdict _verdict) => downVerdict = _verdict;

    public UpVerdict GetUpVerdict() => upVerdict;
    public DownVerdict GetDownVerdict() => downVerdict;

    public void SetUpJudge(Judges _judge) => upJudgeMent = _judge;
    public void SetDownJudge(Judges _judge) => downJudgeMent = _judge;

    public void SetJudgementPosition()
    {
        Vector2 playerPos = PlayerManager.Instance.GetPlayerPos();

        Vector2 upJudgementPos = new Vector2(playerPos.x + 12f, playerPos.y + 5.5f);
        Vector2 downJudgementPos = new Vector2(playerPos.x + 12f, playerPos.y - 0.3f);

        Vector2 upJudgePos = new Vector2(upJudgementPos.x, upJudgementPos.y + 2f);
        Vector2 downJudgePos = new Vector2(downJudgementPos.x, downJudgementPos.y + 2f);
        
        upVerdict.transform.position = upJudgePos;
        downVerdict.transform.position = downJudgePos;

        upJudgeMent.transform.position = upJudgementPos;
        downJudgeMent.transform.position = downJudgementPos;

        upJudgementPosition = upJudgeMent.transform.position;
        downJudgementPosition = downJudgeMent.transform.position;
    }

    public Judges GetUpJudgement() => upJudgeMent;
    public Judges GetdownJudgement() => downJudgeMent;
}