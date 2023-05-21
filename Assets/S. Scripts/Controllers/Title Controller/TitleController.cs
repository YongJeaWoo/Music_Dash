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
        if (!PanelManager.Instance.IsOption && !exitPanel.activeSelf && Input.GetKeyDown(KeyCode.Return))
        {
            LoadingController.LoadScene("Select");
        }

        if (!PanelManager.Instance.IsOption && !exitPanel.activeSelf && Input.GetKeyDown(KeyCode.F1))
        {
            PanelManager.Instance.ToggleOptionPanel();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (PanelManager.Instance.IsOption) PanelManager.Instance.ToggleOptionPanel();
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
