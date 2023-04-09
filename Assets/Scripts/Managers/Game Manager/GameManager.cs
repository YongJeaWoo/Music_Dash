using SingletonComponent.Component;
using System.Collections;
using UnityEngine;

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
        StartCoroutine(WaitForCountPlay());
    }

    private IEnumerator WaitForCountPlay()
    {
        yield return new WaitUntil(() => !AudioManager.Instance.audioSource.isPlaying);

        AudioManager.Instance.Stop();

        MusicData data = MusicDataManager.Instance.GetCurrentMusic();

        AudioManager.Instance.PlayMusicData(data);
    }
}
