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

    private AudioSource audioSource;


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


    #region Play Method
    public void CountPlay(string _clipName)
    {
        audioSource = GetComponent<AudioSource>();
        AudioClip clip = LoadClip(_clipName);
        audioSource.clip = clip;
        audioSource.Play();
    }

    public void InGameMusicPlay(string _clipName)
    {
        audioSource = GetComponent<AudioSource>();
        AudioClip clip = InGameLoadClip(_clipName);
        audioSource.clip = clip;
        audioSource.Play();
    }

    public void RandomMusicPlay()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = RandomTitleClip();
        audioSource.Play();
        audioSource.loop = true;

        if (SceneManager.GetActiveScene().name == "Loading") audioSource.clip = null;
    }

    public void StopCountDownMusic()
    {

    }

    public void MusicAllStop()
    {

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

    // TODO : ���� �ʿ� MusicData�� ������ �����;� ��
    public AudioClip InGameLoadClip(string _loadClipName)
    {
        AudioClip clip = clipList.Find(x => x.name.Equals(_loadClipName)) as AudioClip;

        if (clip != null) return clip;

        clip = Resources.Load<AudioClip>($"{AudioStorage.INGAME_MUSIC_PATH}{_loadClipName}");

        if (clip != null) clipList.Add(clip);

        return clip;
    }
    #endregion
}
