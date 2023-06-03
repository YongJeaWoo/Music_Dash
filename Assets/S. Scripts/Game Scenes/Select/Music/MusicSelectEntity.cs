using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MusicSelectEntity : MonoBehaviour
{
    [SerializeField] private Button m_button;
    [SerializeField] private TextMeshProUGUI m_musicName;
    [SerializeField] private TextMeshProUGUI m_artist;
    [SerializeField] private Image m_icon;

    private string m_patternName;

    private MusicData musicData = null;

    public void Initialize(MusicData _data)
    {
        musicData = _data;

        m_musicName.text = musicData.name;
        m_artist.text = musicData.artist;
        m_patternName = musicData.pattern;
        m_icon.sprite = musicData.icon;

        NoteManager.Instance.SetFileLocation(m_patternName);
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