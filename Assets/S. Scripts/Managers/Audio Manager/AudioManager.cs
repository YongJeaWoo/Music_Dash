using SingletonComponent.Component;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AudioManager : SingletonComponent<AudioManager>
{
    [SerializeField]
    private List<AudioClip> randomClipList = new List<AudioClip>();

    [SerializeField]
    private List<AudioClip> clipList = new List<AudioClip>();

    public AudioSource audioSource;

    [Header("Volume")]
    [SerializeField]
    private AudioMixer audioMixer;

    private Slider masterSlider;
    public Slider MasterSlider
    {
        get => masterSlider;
        set => masterSlider = value;
    }

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

    #region Utils
    public void Play()
    {
        audioSource.Play();
    }

    public void Play(AudioClip _clip)
    {
        audioSource.clip = _clip;
        Play();
    }

    public void Stop()
    {
        audioSource.Stop();
    }

    public void Pause()
    {
        audioSource.Pause();
    }

    public void UnPause()
    {
        audioSource.UnPause();
    }

    public void CountPlay(string _clipName)
    {
        AudioClip clip = LoadClip(_clipName);
        audioSource.clip = clip;
        Play();
        audioSource.loop = false;
    }

    public void RandomMusicPlay()
    {
        audioSource.clip = RandomTitleClip();
        Play();
        audioSource.loop = true;

        if (SceneManager.GetActiveScene().name == "Loading") audioSource.clip = null;
    }
    #endregion

    #region Function
    public AudioClip RandomTitleClip() 
    {
        if (randomClipList.Count == 0)
        {
            AudioClip[] clips = Resources.LoadAll<AudioClip>(AudioStorage.RANDOM_PATH);
            randomClipList.AddRange(clips);
        }

        int index = Random.Range(0, randomClipList.Count);
        AudioClip randomClip = randomClipList[index];        

        return randomClip;
    }

    public AudioClip LoadClip(string _loadClipName)
    {
        AudioClip clip = clipList.Find(x => x.name.Equals(_loadClipName));

        if (clip != null) return clip;

        clip = Resources.Load<AudioClip>($"{AudioStorage.MUSIC_PATH}{_loadClipName}");

        if (null != clip) clipList.Add(clip);

        return clip;
    }

    public void PlayMusicData(MusicData _data)
    {
        AudioClip clip = clipList.Find(x => x.name.Equals(_data.musicClip.name));

        if (clip != null)
        {
            Play(clip);
            return;
        }

        clip = _data.musicClip;

        if (clip != null) clipList.Add(clip);

        Play(clip);
    }
    #endregion
}
