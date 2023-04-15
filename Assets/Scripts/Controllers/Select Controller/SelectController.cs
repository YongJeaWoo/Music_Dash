using UnityEngine;

public class SelectController : MonoBehaviour
{
    [SerializeField]
    private GameObject exitPanel;

    private void Update()
    {
        KeyActions();
    }

    private void KeyActions()
    {
        if (!OptionManager.Instance.IsOption && !exitPanel.activeSelf && Input.GetKeyDown(KeyCode.Return))
        {
            LoadingController.LoadScene("Game");
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
