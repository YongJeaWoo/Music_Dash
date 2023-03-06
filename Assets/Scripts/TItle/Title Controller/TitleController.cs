using System.Collections;
using UnityEngine;

public class TitleController : MonoBehaviour
{
    [SerializeField]
    private GameObject optionPanel, howUsePanel, exitPanel;

    private float panelMoveTime = 0.3f;

    private Vector3 hideOptionPosition;
    private Vector3 showOptionPosition;

    private RectTransform rt;

    private bool optionAnimating;
    private bool isOption;

    private void Awake()
    {
        rt = optionPanel.GetComponent<RectTransform>();
    }

    private void Start()
    {
        hideOptionPosition = new Vector3(-Screen.width / 2 - optionPanel.GetComponent<RectTransform>().rect.width / 2, 0, 0);
        showOptionPosition = new Vector3(-Screen.width / 4, 0, 0);

        rt.anchoredPosition = hideOptionPosition;
    }

    private void Update()
    {
        HandleMouseEvent();
        PressKeys();
    }

    private void HandleMouseEvent()
    {
        Cursor.visible = isOption;
        Cursor.lockState = isOption ? CursorLockMode.None : CursorLockMode.Locked;
    }

    private void PressKeys()
    {
        if (!isOption && !exitPanel.activeSelf && Input.GetKeyDown(KeyCode.Return))
        {
            LoadingController.LoadScene("Select");
        }

        if (Input.GetKeyDown(KeyCode.F1) && !isOption && !exitPanel.activeSelf)
        {
            ToggleOptionPanel(true);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isOption) ToggleOptionPanel(false);
            else exitPanel.SetActive(!exitPanel.activeSelf);
        }

        if (exitPanel.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Return)) GameEnd();
        }

        if (!isOption && !exitPanel.activeSelf && howUsePanel.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            howUsePanel.SetActive(false);
        }
    }

    private void ToggleOptionPanel(bool _show)
    {
        if (optionAnimating) return;

        optionAnimating = true;

        StartCoroutine(MovePanel(_show));

        isOption = _show;
    }

    private IEnumerator MovePanel(bool _isShow)
    {
        optionAnimating = true;

        rt = optionPanel.GetComponent<RectTransform>();

        Vector3 startPosition = _isShow ? hideOptionPosition : showOptionPosition;
        Vector3 endPosition = _isShow ? showOptionPosition : hideOptionPosition;

        float startTime = Time.time;
        float endTime = startTime + panelMoveTime;

        while (Time.time < endTime)
        {
            float t = (Time.time - startTime) / panelMoveTime;
            rt.anchoredPosition = Vector3.Lerp(startPosition, endPosition, t);
            yield return null;
        }

        rt.anchoredPosition = endPosition;
        optionAnimating = false;

        if (!_isShow) Time.timeScale = 1f;
    }

    private void GameEnd()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void HowToUse()
    {
        howUsePanel.SetActive(true);
    }
}
