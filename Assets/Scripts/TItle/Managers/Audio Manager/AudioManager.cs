using SingletonComponent.Component;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioManager : SingletonComponent<AudioManager>
{
    [SerializeField] 
    private List<AudioClip> clipList = new List<AudioClip>();

    private AudioSource startMusic;

    [Header("Volume")]
    [SerializeField]
    private AudioMixer audioMixer;

    [SerializeField]
    private Slider masterSlider;

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

    public void SetMasterVolume()
    {
        audioMixer.SetFloat("Master", Mathf.Log10(masterSlider.value) * 20);
    }

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
