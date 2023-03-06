using SingletonComponent.Component;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : SingletonComponent<AudioManager>
{
    [SerializeField] 
    private List<AudioClip> clipList = new List<AudioClip>();

    private AudioSource startMusic;

    private const string MUSIC_PATH = "InGame/Music/";
    
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

    public void PlayMusic(string _clipName)
    {
        startMusic = GetComponent<AudioSource>();
        AudioClip clip = LoadClip(_clipName);
        startMusic.clip = clip;
        startMusic.Play();
    }

    public AudioClip LoadClip(string _loadClipName)
    {
        AudioClip clip = clipList.Find(x => x.name.Equals(_loadClipName));

        if (clip != null) return clip;

        clip = Resources.Load<AudioClip>($"{MUSIC_PATH}{_loadClipName}");

        if (null != clip) clipList.Add(clip);

        return clip;
    }

}
