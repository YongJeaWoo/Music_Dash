using SingletonComponent.Component;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : SingletonComponent<AudioManager>
{
    [SerializeField] 
    private List<AudioClip> clipList = new List<AudioClip>();

    [SerializeField] 
    private List<AudioClip> randomClipList = new List<AudioClip>();

    private AudioSource startMusic;
    private AudioSource randomMusic;

    private const string MUSIC_PATH = "InGame/Music/";
    private const string RANDOM_PATH = "Titles/Music";
    
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

    public void PlayRandomMusic()
    {
        randomMusic.GetComponent<AudioSource>();
        randomMusic.clip = RandomClip();
        randomMusic.Play();
    }

    public AudioClip RandomClip()
    {
        AudioClip randomClip = randomClipList.Find(x => x.name.Equals(randomClipList.Count));

        if (randomClip != null) return randomClip;

        randomClip = Resources.Load<AudioClip>($"{RANDOM_PATH}");

        if (randomClip != null) randomClipList.Add(randomClip);

        return randomClip;
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
