using UnityEngine;

[CreateAssetMenu(menuName = "Music/Add Data", fileName = "MusicData")]
public class MusicData : ScriptableObject
{
    public string musicName;
    public string artist;
    public string pattern;
    public Sprite icon;
    public AudioClip musicClip;
}
