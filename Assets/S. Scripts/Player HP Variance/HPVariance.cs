using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HPVariance : MonoBehaviour
{
    private Slider sliderHP;

    private TextMeshProUGUI hpText;

    private void Awake()
    {
        GetComponentInit();
    }

    private void Update()
    {
        PlayerHpSlider();
    }
    
    private void GetComponentInit()
    {
        hpText = transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        sliderHP = GetComponent<Slider>();
    }

    public void PlayerHpSlider()
    {
        if (PlayerManager.Instance.GetPlayer() == null) return;

        Mathf.Max(PlayerManager.Instance.GetPlayerHP(), 0);
        sliderHP.value = PlayerManager.Instance.GetPlayerHP() / PlayerInfo.PLAYER_MAXHP;
        hpText.text = $"{PlayerManager.Instance.GetPlayerHP()} / {PlayerInfo.PLAYER_MAXHP}";
    }
}
