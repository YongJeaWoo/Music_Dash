using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private Image deadPanel;
    [SerializeField] private GameObject[] backGrounds;

    private bool isFading = false;
    private float fadeTime = 0f;
    private const float fadeSpeed = 1f;

    private void Awake()
    {
        Init();
    }

    private void Update()
    {
        if (GameManager.Instance.CurrentState != E_GameState.GameOver)
            return;

        OverInputKeys();

        if (!isFading)
        {
            isFading = true;
            StartCoroutine(FadeOut());
        }
    }

    private void Init()
    {
        GameManager.Instance.CurrentState = E_GameState.Init;
        deadPanel.gameObject.SetActive(false);
    }

    private IEnumerator FadeOut()
    {
        deadPanel.gameObject.SetActive(true);

        while (fadeTime < 1f)
        {
            fadeTime += Time.deltaTime * fadeSpeed;
            deadPanel.color = new Color(0f, 0f, 0f, fadeTime);
            yield return null;
        }

        DisableBackgrounds();
        GameManager.Instance.GameOver();
    }

    private void DisableBackgrounds()
    {
        foreach (GameObject bg in backGrounds)
        {
            bg.gameObject.SetActive(false);
        }
    }

    private void OverInputKeys()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (PanelManager.Instance.IsOver) PanelManager.Instance.GameOverPanel();

            deadPanel.gameObject.SetActive(false);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (PanelManager.Instance.IsOver) PanelManager.Instance.GameOverPanel();

            deadPanel.gameObject.SetActive(false);
            LoadingController.LoadScene("Select");
        }
    }
}