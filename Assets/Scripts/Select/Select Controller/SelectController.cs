using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectController : MonoBehaviour
{
    [SerializeField]
    private GameObject exitPanel;

    private Button selectButton;

    private Vector2 centerPosition;

    private void Update()
    {
        PressKeys();
    }

    private void PressKeys()
    {
        if (!OptionManager.Instance.IsOption && !exitPanel.activeSelf && Input.GetKeyDown(KeyCode.Return))
        {
            LoadingController.LoadScene("Select");
        }

        if (Input.GetKeyDown(KeyCode.F1) && !OptionManager.Instance.IsOption && !exitPanel.activeSelf)
        {
            OptionManager.Instance.ToggleOptionPanel();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (OptionManager.Instance.IsOption) OptionManager.Instance.ToggleOptionPanel();
            else exitPanel.SetActive(!exitPanel.activeSelf);
        }

        if (exitPanel.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Return)) ReturnGoTitle();
        }
    }

    private void ReturnGoTitle()
    {
        LoadingController.LoadScene("Title");
    }
}
