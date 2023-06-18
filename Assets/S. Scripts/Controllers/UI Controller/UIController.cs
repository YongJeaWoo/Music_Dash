using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI comboText;


    private Animator comboAnimator;

    private void Start()
    {
        GetComponentInit();
    }

    private void GetComponentInit() 
    {
        comboAnimator = GetComponent<Animator>();
    }

    public void GetShowUI()
    {
        SetShowUI();
    }

    private void SetShowUI()
    {
        scoreText.text = $"{ScoreManager.Instance.GetScore()}";

        if (ScoreManager.Instance.GetCombo() >= 5)
        {
            comboText.text = $"{ScoreManager.Instance.GetCombo()}";
            // comboAnimator.SetTrigger("Combo");
        }
    }
}
