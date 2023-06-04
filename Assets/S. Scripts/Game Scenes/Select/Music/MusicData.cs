using UnityEngine;

[CreateAssetMenu(menuName = "Music/Add Data", fileName = "MusicData")]
public class MusicData : ScriptableObject
{
    public string musicName;
    public string artist;
    public string patternName;
    public Sprite icon;
    public AudioClip musicClip;
}
