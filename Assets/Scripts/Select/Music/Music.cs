using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Music : UIBehaviour
{
    [SerializeField]
    private TextMeshProUGUI uiText;
    [SerializeField]
    private Image uiBackGround;
    [SerializeField]
    private Image uiIcon;

    private readonly Color[] colors = new Color[]
    {
        new Color(1, 1, 1, 1),
        new Color(0.9f, 0.9f, 1, 1),
    };

    public void UpdateItem(int count)
    {
        uiBackGround.color = colors[Mathf.Abs(count) % colors.Length];
        uiIcon.sprite = Resources.Load<Sprite>($"{Mathf.Abs(count) % 30 + 1}Icon{000}");
    }
}
