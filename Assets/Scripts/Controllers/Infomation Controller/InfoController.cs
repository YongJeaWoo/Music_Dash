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

    private ScrollController scrollController;

    private void Start()
    {
        scrollController = FindObjectOfType<ScrollController>();
    }

    public void OnSelect()
    {
        if (scrollController != null && scrollController.CurrentSelectedButton != null)
        {
            m_musicName.text = scrollController.CurrentSelectedButton.GetComponentInChildren<MusicSelectEntity>().m_musicName.text;
            m_musicComposer.text = scrollController.CurrentSelectedButton.GetComponentInChildren<MusicSelectEntity>().m_musicComposer.text;
            m_icon.sprite = scrollController.CurrentSelectedButton.GetComponentInChildren<MusicSelectEntity>().m_icon.sprite;
        }
    }
}
