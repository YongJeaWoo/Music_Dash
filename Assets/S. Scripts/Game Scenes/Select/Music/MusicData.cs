using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Music/Add Data", fileName = "MusicData")]
public class MusicData : ScriptableObject
{
    public string musicName;
    public string musicComposer;
    public Sprite icon;
    public AudioClip musicClip;
}
