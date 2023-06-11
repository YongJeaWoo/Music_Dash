using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OptionPanel : MonoBehaviour
{
    [SerializeField]
    private GameObject howUsePanel;

    private float panelMoveTime = 0.25f;
    private bool playAnimation = false;

    private bool isHowUse = false;

    private Vector3 hidePosition;
    private Vector3 showPosition;
    private Vector3 howShowPosition;
    private Vector3 howHidePosition;

    private RectTransform rectTrans;
    private RectTransform howUseRectTrans;

    #region Resolution Info
    [SerializeField]
    private TMP_Dropdown resolutionDropdown;

    private FullScreenMode mFullScreenMode;

    private List<Resolution> resolutions = new List<Resolution>();

    private int resolutionNumber;
    #endregion

    private void OnEnable()
    {
        rectTrans = GetComponent<RectTransform>();
        howUseRectTrans = howUsePanel.GetComponent<RectTransform>();

        howUseRectTrans.anchorMin = new Vector2(0.5f, 0.5f);
        howUseRectTrans.anchorMax = new Vector2(0.5f, 0.5f);
        howUseRectTrans.pivot = new Vector2(0.5f, 0.5f);

        hidePosition = new Vector3(-1440, 0, 0);
        showPosition = new Vector3(-480, 0, 0);

        howUseRectTrans.anchoredPosition = new Vector2(hidePosition.x + howUseRectTrans.rect.width / 2, howUseRectTrans.anchoredPosition.y);
        howHidePosition = new Vector3(-960, 0, 0);
    }

    public void InitializeOptionPanel()
    {
        InitUI();

        playAnimation = true;

        StartCoroutine(MovePanel(rectTrans, true));
    }

    public void OnExitOptionPanel()
    {
        if (isHowUse)
        {
            OnExitHowUsePanel();
            return;
        }

        if (playAnimation) return;

        playAnimation = true;

        StartCoroutine(MovePanel(rectTrans, false, () => PopupManager.Instance.RemovePopUp<OptionPanel>()));
    }

    public void InitializeHowUsePanel()
    {
        isHowUse = true;
        howUsePanel.SetActive(true);

        playAnimation = true;

        StartCoroutine(MovePanel(howUseRectTrans, true));
    }

    public void OnExitHowUsePanel()
    {
        if (playAnimation) return;

        playAnimation = true;

        StartCoroutine(MovePanel(howUseRectTrans, false, () => isHowUse = false));
    }

    private IEnumerator MovePanel(RectTransform rect, bool _isShow, Action _onComplete = null)
    {
        Vector3 startPosition = _isShow ? hidePosition : showPosition;
        Vector3 endPosition = _isShow ? showPosition : hidePosition;
        Vector3 howPosition = _isShow ? howShowPosition : howHidePosition;

        float startTime = Time.time;
        float endTime = startTime + panelMoveTime;

        while (Time.time < endTime)
        {
            float t = (Time.time - startTime) / panelMoveTime;

            rect.anchoredPosition = Vector3.Lerp(startPosition, endPosition, t);
            
            if (rect == howUseRectTrans) howUseRectTrans.anchoredPosition = Vector3.Lerp(howUseRectTrans.anchoredPosition, howPosition, t);

            yield return null;
        }

        rect.anchoredPosition = endPosition;

        if (rect == howUseRectTrans) howUseRectTrans.anchoredPosition = howPosition;

        playAnimation = false;

        _onComplete?.Invoke();
    }

    #region Resolution
    private void InitUI()
    {
        resolutions.AddRange(Screen.resolutions);
        resolutionDropdown.options.Clear();
        int optionNumber = 0;
        foreach (Resolution item in resolutions)
        {
            TMP_Dropdown.OptionData option = new TMP_Dropdown.OptionData();
            option.text = $"{item.width} x {item.height}  {item.refreshRateRatio} hz";
            resolutionDropdown.options.Add(option);
            if (item.width == Screen.width && item.height == Screen.height)
            {
                resolutionDropdown.value = optionNumber;
            }
            optionNumber++;
        }
        resolutionDropdown.RefreshShownValue();
    }
    public void DropboxOption(int ChangeDisplay)
    {
        resolutionNumber = ChangeDisplay;
    }
    public void FullScreenButton(bool isFull)
    {
        mFullScreenMode = isFull ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;
    }
    public void ApplyButton()
    {
        Screen.SetResolution(resolutions[resolutionNumber].width,
            resolutions[resolutionNumber].height,
            mFullScreenMode);
    }
    #endregion
}

