using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    private const string READY_NAME = "Ready";

    private Animator comboAnimator;
    private Animator readyAnimator;

    private bool isCounting = false;
    public bool IsCounting => isCounting;

    public TextMeshProUGUI readyText, scoreText, comboText;

    private void Start()
    {
        GetComponentInit();
    }

    private void GetComponentInit() 
    {
        readyAnimator = GetComponent<Animator>();
        comboAnimator = GetComponent<Animator>();
    }

    public void GetShowUI()
    {
        SetShowUI();
    }

    private void SetShowUI()
    {
        scoreText.text = $"{GameManager.Instance.Score}";

        if (GameManager.Instance.Combo >= 5)
        {
            comboText.text = $"{GameManager.Instance.Combo}";
            comboAnimator.SetTrigger("Combo");
        }
    }

    public void ReadyCount()
    {
        StartCoroutine(StartCount());
    }

    private IEnumerator StartCount()
    {
        var readyTimer = new WaitForSeconds(1.5f);
        var goTimer = new WaitForSeconds(0.4f);
        var aniTimer = new WaitForSeconds(2.0f);

        readyText.gameObject.SetActive(true);
        readyText.text = "Ready ...";
        readyAnimator.SetBool(READY_NAME, true);
        yield return readyTimer;

        readyText.text = "Go !";
        yield return goTimer;
        readyAnimator.SetBool(READY_NAME, false);
        yield return aniTimer;
        isCounting = false;
        readyText.gameObject.SetActive(false);

        yield return null;
    }
}
