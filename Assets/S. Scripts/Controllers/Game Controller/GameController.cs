using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private Image fadePanel;
    [SerializeField] private GameObject[] backGrounds;

    private bool isFading = false;
    private float fadeTime = 0f;
    private const float fadeSpeed = 1f;

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        if (PlayerManager.Instance.GetPlayerHP() > 0) return;

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
        fadePanel.gameObject.SetActive(false);
    }

    private IEnumerator FadeOut()
    {
        fadePanel.gameObject.SetActive(true);

        while (fadeTime < 1f)
        {
            fadeTime += Time.deltaTime * fadeSpeed;
            fadePanel.color = new Color(0f, 0f, 0f, fadeTime);
            yield return null;
        }

        DisableBackgrounds();
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

            fadePanel.gameObject.SetActive(false);
            NoteManager.Instance.ResetMidiFile();
            NoteManager.Instance.ClearNotes();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (PanelManager.Instance.IsOver) PanelManager.Instance.GameOverPanel();

            fadePanel.gameObject.SetActive(false);
            NoteManager.Instance.ResetMidiFile();
            NoteManager.Instance.ClearNotes();
            LoadingController.LoadScene("Select");
        }
    }
}