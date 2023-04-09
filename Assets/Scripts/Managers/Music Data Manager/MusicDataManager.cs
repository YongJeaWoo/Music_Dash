using SingletonComponent.Component;
using System.Collections.Generic;
using UnityEngine;

public class MusicDataManager : SingletonComponent<MusicDataManager>
{
    [SerializeField]
    private List<MusicData> musicData = null;

    private MusicData m_currentMusic = null;

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

    public List<MusicData> GetMusicDataList() => musicData;

    public MusicData GetCurrentMusic() => m_currentMusic;

    public void SetCurrentMusic(MusicData _data)
    {
        m_currentMusic = _data;
    }
}
