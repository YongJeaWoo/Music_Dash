using SingletonComponent.Component;
using UnityEngine;
using TMPro;

public class UIManager : SingletonComponent<UIManager>
{
    [SerializeField] private GameObject canvasObj;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI comboText;
    public GameObject comboBackground;

    #region Singleton

    protected override void AwakeInstance()
    {
        canvasObj.SetActive(false);
        comboBackground.SetActive(false);
    }

    protected override bool InitInstance()
    {
        return true;
    }

    protected override void ReleaseInstance()
    {
        
    }

    #endregion

    public void TurnOnText(bool _isTurn)
    {
        canvasObj.SetActive(_isTurn);
    }

    public void ShowUI()
    {
        int score = ScoreManager.Instance.GetScore();
        scoreText.text = score.ToString("D6");
        
        var combo = ScoreManager.Instance.GetCombo();

        if (combo >= 5)
        {
            comboBackground.SetActive(true);
            comboText.text = $"{ScoreManager.Instance.GetCombo()}";
        }
    }
}
