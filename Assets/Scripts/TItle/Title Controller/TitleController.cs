using UnityEngine;

public class TitleController : MonoBehaviour
{
    [SerializeField]
    private GameObject exitPanel;

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

        if (!OptionManager.Instance.IsOption && !exitPanel.activeSelf && Input.GetKeyDown(KeyCode.F1))
        {
            OptionManager.Instance.ToggleOptionPanel(true);
        }

        if (Input.GetKeyDown(KeyCode.Escape) && !OptionManager.Instance.IsHowUse)
        {
            if (OptionManager.Instance.IsOption) OptionManager.Instance.ToggleOptionPanel(false);
            else exitPanel.SetActive(!exitPanel.activeSelf);
        }

        if (exitPanel.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Return)) GameEnd();
        }
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
