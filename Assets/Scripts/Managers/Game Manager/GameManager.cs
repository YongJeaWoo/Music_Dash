using SingletonComponent.Component;

public class GameManager : SingletonComponent<GameManager>
{
    // �÷��̾��� ü��
    // ��Ʈ
    // ī��Ʈ ���ǰ� ���� �ε� �� ����


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
        InitMusic();
    }

    private void Update()
    {
        
    }

    private void InitMusic()
    {
        AudioManager.Instance.CountPlay("Start");

        
    }
}
