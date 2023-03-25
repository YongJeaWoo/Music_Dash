using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectController : MonoBehaviour
{
    [SerializeField]
    private GameObject exitPanel;

    [SerializeField]
    private GameObject buttonPrefab;
    
    private List<GameObject> buttons = new List<GameObject>();

    private Button selectButton;

    private Vector2 centerPosition;

    private void Start()
    {
        InstantButton();

        selectButton = buttons[0].GetComponent<Button>();
        SetSelectedButton(selectButton);
    }

    private void Update()
    {
        PressKeys();
    }

    private void InstantButton()
    {
        for (int i = 0; i < 9; i++)
        {
            GameObject button = Instantiate(buttonPrefab, transform);
            buttons.Add(button);
        }
    }

    private void PressKeys()
    {
        #region Music
        Music items = selectButton.GetComponent<Music>();

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (selectButton != null)
            {
                SetUnSelectedButton(selectButton);

                int index = GetSelectedButtonIndex() - 1;

                if (index < 0)
                {
                    index = buttons.Count - 1;
                }

                selectButton = buttons[index].GetComponent<Button>();

                SetSelectedButton(selectButton);
            }
        }

        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (selectButton != null)
            {
                SetUnSelectedButton(selectButton);

                int index = GetSelectedButtonIndex() + 1;

                if (index >= buttons.Count)
                {
                    index = 0;
                }

                selectButton = buttons[index].GetComponent<Button>();

                SetSelectedButton(selectButton);
            }
        }    

        else if (exitPanel.activeSelf &&
            !OptionManager.Instance.IsOption &&
            Input.GetKeyDown(KeyCode.Return))
        {

        }

        #endregion

        #region ETC
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
        #endregion
    }

    private int GetSelectedButtonIndex()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            if (buttons[i].GetComponent<Button>() == selectButton) return i;
        }

        return -1;
    }

    private void SetSelectedButton(Button button)
    {
        button.GetComponent<Image>().color = Color.green;

        Vector2 targetPosition = centerPosition - button.GetComponent<RectTransform>().anchoredPosition;

        foreach(GameObject music in buttons)
        {
            music.GetComponent<RectTransform>().anchoredPosition += targetPosition;
        }
    }

    private void SetUnSelectedButton(Button button)
    {
        button.GetComponent<Image>().color = Color.white;

        Vector2 targetPosition = centerPosition - button.GetComponent<RectTransform>().anchoredPosition;

        foreach(GameObject music in buttons)
        {
            music.GetComponent<RectTransform>().anchoredPosition -= targetPosition;
        }
    }

    private void ReturnGoTitle()
    {
        LoadingController.LoadScene("Title");
    }
}
