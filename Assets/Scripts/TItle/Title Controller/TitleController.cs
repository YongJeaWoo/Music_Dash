using System.Collections;
using UnityEngine;

public class TitleController : MonoBehaviour
{
    [SerializeField]
    private GameObject optionPanel, howUsePanel, exitPanel;

    private float panelMoveTime = 0.8f;

    private Vector3 hidePosition;
    private Vector3 showPosition;

    private RectTransform rectTransform;

    private bool isAnimating;
    private bool isOption;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        hidePosition = new Vector3(-Screen.width / 2 - gameObject.GetComponent<RectTransform>().rect.width / 2, 0, 0);
        showPosition = new Vector3(-Screen.width / 4, 0, 0);

        rectTransform.anchoredPosition = hidePosition;
    }

    private void Update()
    {
        MouseEvent();
        PressKeys();
    }

    private void MouseEvent()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        if (isOption)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    private void PressKeys()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            if (isAnimating || isOption) return;
            StartCoroutine(MovePanel(true));
            isOption = true;
        }

        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isAnimating || !isOption) return;
            StartCoroutine(MovePanel(false));
            isOption = false;
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (exitPanel.activeSelf) ExitGame();

            if (isOption || isAnimating) return;
        }
    }

    IEnumerator MovePanel(bool isShow)
    {
        isAnimating = true;

        rectTransform = gameObject.GetComponent<RectTransform>();

        Vector3 startPosition = isShow ? hidePosition : showPosition;
        Vector3 endPosition = isShow ? showPosition : hidePosition;

        float startTime = Time.time;
        float endTime = startTime + panelMoveTime;

        while (Time.time < endTime)
        {
            float t = (Time.time - startTime) / panelMoveTime;
            rectTransform.anchoredPosition = Vector3.Lerp(startPosition, endPosition, t);
            yield return null;
        }

        rectTransform.anchoredPosition = endPosition;
        isAnimating = false;

        if (!isShow) Time.timeScale = 1f;
    }

    private void ExitGame()
    {
        if (Input.GetKeyDown(KeyCode.Return)) GameEnd();   
    }

    private void GameEnd()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

}
