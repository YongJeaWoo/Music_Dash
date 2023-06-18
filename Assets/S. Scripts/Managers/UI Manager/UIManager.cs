using SingletonComponent.Component;
using UnityEngine;
using TMPro;

public class UIManager : SingletonComponent<UIManager>
{
    [SerializeField] GameObject canvasObj;

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI comboText;

    #region Singleton

    protected override void AwakeInstance()
    {
        canvasObj.SetActive(false);
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

    public void GetShowUI()
    {
        SetShowUI();
    }

    private void SetShowUI()
    {
        var combo = ScoreManager.Instance.GetCombo();

        scoreText.text = $"{ScoreManager.Instance.GetScore()}";

        if (combo >= 5)
        {
            comboText.text = $"{ScoreManager.Instance.GetCombo()}";
        }
    }
}
