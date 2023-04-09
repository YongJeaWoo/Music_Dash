using SingletonComponent.Component;
using System.Collections;
using UnityEngine;

public class GameManager : SingletonComponent<GameManager>
{
    // 플레이어의 체력
    // 노트
    // 카운트 음악과 실제 로드 될 음악


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
