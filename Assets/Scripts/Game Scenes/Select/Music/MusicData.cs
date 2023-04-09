using UnityEngine;

[CreateAssetMenu(menuName = "Music/Add Data", fileName = "MusicData")]
public class MusicData : ScriptableObject
{
    public string name;
    public string composer;
    public Sprite iconImage;
    public AudioClip musicClip;
}
