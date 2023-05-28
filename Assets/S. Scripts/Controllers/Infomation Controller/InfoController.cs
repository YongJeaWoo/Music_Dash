using DTT.InfiniteScroll;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI m_musicName;
    [SerializeField]
    private TextMeshProUGUI m_musicComposer;
    [SerializeField]
    private Image m_icon;

    [SerializeField]
    private Image m_backGround;

    private ScrollController scrollController;

    private void Start()
    {
        scrollController = FindObjectOfType<ScrollController>();
    }

    public void OnRefresh()
    {
        MusicData data = MusicDataManager.Instance.GetCurrentMusic();

        if (null == data) return;

        if (scrollController != null && scrollController.CurrentSelectedEntity != null)
        {
            m_musicName.text = data.musicName;
            m_musicComposer.text = data.artist;
            m_icon.sprite = data.icon;
            m_backGround.sprite = data.icon;
            AudioManager.Instance.PlayMusicData(data);
        }
    }
}
