using SingletonComponent.Component;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : SingletonComponent<GameManager>
{
    // 노트
    // 카운트 음악과 실제 로드 될 음악

    [SerializeField]
    private Player player;
    [SerializeField]
    private TextMeshProUGUI textHp;
    [SerializeField]
    private Slider sliderHp;

    private int combo;
    public int Combo => combo;

    private float score;
    public float Score => score;

    #region SingleTon
    protected override void AwakeInstance()
    {
    }

    protected override bool InitInstance()
    {
        return true;
    }

    protected override void ReleaseInstance()
    {
    }
    #endregion

    private void Start()
    {
        InitMusic();
    }

    private void Update()
    {
        PlayerHpSlider();
    }

    private void PlayerHpSlider()
    {
        if (player.CurrentHp >= 0)
        {
            player.CurrentHp = Mathf.Max(player.CurrentHp, 0);
            sliderHp.value = player.CurrentHp / PlayerInfo.PLAYER_MAXHP;
            textHp.text = $"{player.CurrentHp} / {PlayerInfo.PLAYER_MAXHP}";
        }
    }

    private void InitMusic()
    {
        AudioManager.Instance.CountPlay("Start");
        StartCoroutine(WaitForCountPlay());
    }

    private IEnumerator WaitForCountPlay()
    {
        yield return new WaitUntil(() => !AudioManager.Instance.audioSource.isPlaying);

        AudioManager.Instance.Stop();

        MusicData data = MusicDataManager.Instance.GetCurrentMusic();

        AudioManager.Instance.PlayMusicData(data);
    }

}
