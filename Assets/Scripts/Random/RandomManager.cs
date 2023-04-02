using SingletonComponent.Component;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RandomManager : SingletonComponent<RandomManager>
{
    [SerializeField]
    private List<AudioClip> randomClipList = new List<AudioClip>();

    private AudioSource randomMusic;

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

    public void PlayRandomMusic()
    {
        randomMusic = AudioManager.Instance.GetComponent<AudioSource>();
        randomMusic.clip = RandomClip();
        randomMusic.Play();
        randomMusic.loop = true;

        if (SceneManager.GetActiveScene().name == "Loading") randomMusic.clip = null;
    }

    public AudioClip RandomClip()
    {
        if (randomClipList.Count == 0)
        {
            AudioClip[] clips = Resources.LoadAll<AudioClip>(RANDOM_PATH);
            randomClipList.AddRange(clips);
        }

        int index = Random.Range(0, randomClipList.Count);
        AudioClip randomClip = randomClipList[index];

        return randomClip;
    }

}
