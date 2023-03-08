using SingletonComponent.Component;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionManager : SingletonComponent<OptionManager>
{
    [SerializeField]
    private GameObject optionPanel, howUsePanel;

    private float panelMoveTime = 0.25f;

    private Vector3 hideOptionPosition;
    private Vector3 showOptionPosition;

    private RectTransform rt;

    private bool optionAnimating;

    private bool isHowUse = false;
    public bool IsHowUse 
    { 
        get { return isHowUse; }
        set { isHowUse = value; }
    }

    private bool isOption;
    public bool IsOption
    {
        get { return isOption; }
        set { isOption = value; }
    }

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
    }

    private void HandleMouseEvent()
    {
        Cursor.visible = isOption;
        Cursor.lockState = isOption ? CursorLockMode.None : CursorLockMode.Locked;
    }

    public void ToggleOptionPanel(bool _show)
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

    public void HowToUse()
    {
        isHowUse = true;
        howUsePanel.SetActive(true);
    }

    public void ConfirmHowToUse()
    {
        isHowUse = false;
        howUsePanel.SetActive(false);
    }
}
