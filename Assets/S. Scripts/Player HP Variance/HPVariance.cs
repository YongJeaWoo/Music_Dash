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

    private void PlayerHpSlider()
    {
        if (Player.player.CurrentHp < 0) return;

        Player.player.CurrentHp = Mathf.Max(Player.player.CurrentHp, 0);
        sliderHP.value = Player.player.CurrentHp / PlayerInfo.PLAYER_MAXHP;
        hpText.text = $"{Player.player.CurrentHp} / {PlayerInfo.PLAYER_MAXHP}";
    }
}
