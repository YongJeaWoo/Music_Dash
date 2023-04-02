using UnityEngine;
using UnityEngine.UI;

public class PauseController : MonoBehaviour
{
    [SerializeField]
    private Button[] buttons;

    private int selectedIndex = 0;

    [SerializeField]
    private GameObject optionPanel;

    private float pauseTimer = 5f;

    private void Update()
    {
        PauseControll();
    }

    private void PauseControll()
    {
        pauseTimer -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            SetButtonSelect(buttons[selectedIndex], false);

            selectedIndex = (selectedIndex + buttons.Length - 1) % buttons.Length;

            SetButtonSelect(buttons[selectedIndex], true);
        }

        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            SetButtonSelect(buttons[selectedIndex], false);

            selectedIndex = (selectedIndex + 1) % buttons.Length;

            SetButtonSelect(buttons[selectedIndex], true);
        }

        else if (Input.GetKeyDown(KeyCode.Return))
        {
            buttons[selectedIndex].onClick.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.Escape) &&
            pauseTimer <= 0f)
        {
            PauseOption();
        }
    }

    private void SetButtonSelect(Button button, bool isSelect)
    {
        var colors = button.colors;

        colors.normalColor = isSelect ? Color.gray : Color.white;
        colors.highlightedColor = isSelect ? Color.gray : Color.white;
        colors.selectedColor = isSelect ? Color.gray : Color.white;
        button.colors = colors;
    }

    private void PauseOption()
    {
        optionPanel.SetActive(true);
        Time.timeScale = 0;
        pauseTimer = 0;
    }

    public void ContinueBTN()
    {
        
    }

    public void RestartBTN()
    {

    }

    public void SelectBTN()
    {

    }

    public void ExitBTN()
    {

    }
}
