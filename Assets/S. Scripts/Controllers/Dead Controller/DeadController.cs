using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DeadController : MonoBehaviour
{
    [SerializeField]
    private Image deadPanel;

    private bool isFading = false;
    private float fadeTime = 0f;

    private void Awake()
    {
        Init();
    }

    private void Update()
    {
        CheckState();
    }

    private void Init()
    {
        deadPanel.gameObject.SetActive(false);
    }

    private void CheckState()
    {
        if (GameManager.Instance.CurrentState != E_GameState.GameOver) return;

        if (!isFading)
        {
            isFading = true;
            StartCoroutine(FadeOut());
        }
    }

    private IEnumerator FadeOut()
    {
        deadPanel.gameObject.SetActive(true);

        while (fadeTime < 1f)
        {
            fadeTime += Time.deltaTime;
            deadPanel.color = new Color(0f, 0f, 0f, fadeTime);
            yield return null;
        }

        GameManager.Instance.GameOver();
    }
}