using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MusicSelectEntity : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI name;
    [SerializeField]
    private Image iconImage;

    private MusicData musicData = null;

    public void Initialize(MusicData _data)
    {
        musicData = _data;
    }

}