using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MusicSelectEntity : MonoBehaviour
{
    public Button m_button;
    public TextMeshProUGUI m_musicName;
    public TextMeshProUGUI m_musicComposer;
    public Image m_icon;

    private MusicData musicData = null;

    public void Initialize(MusicData _data)
    {
        musicData = _data;

        m_musicName.text = musicData.name;
        m_musicComposer.text = musicData.musicComposer;
        m_icon.sprite = musicData.icon;
    }

    public void OnSelect(bool _selected)
    {
        m_button.targetGraphic.color = _selected ? Color.grey : Color.white;

        if(_selected)
        {
            MusicDataManager.Instance.SetCurrentMusic(musicData);
        }
    }
}