using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResolutionOption : MonoBehaviour
{
    FullScreenMode mFullScreenMode;

    [SerializeField]
    TMP_Dropdown resolutionDropdown;

    List<Resolution> resolutions = new List<Resolution>();

    int resolutionNumber;

    private void Start()
    {
        InitUI();
    }

    void InitUI()
    {
        resolutions.AddRange(Screen.resolutions);
        resolutionDropdown.options.Clear();

        int optionNumber = 0;

        foreach (Resolution item in resolutions)
        {
            TMP_Dropdown.OptionData option = new TMP_Dropdown.OptionData();
            option.text = item.width + "x" + item.height + " " + item.refreshRateRatio + "hz";
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
}
