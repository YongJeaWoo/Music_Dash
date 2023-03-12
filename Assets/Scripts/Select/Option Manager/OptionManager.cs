using SingletonComponent.Component;
using UnityEngine;

public class OptionManager : SingletonComponent<OptionManager>
{
    public bool IsOption => PopupManager.Instance.IsUsePopup<OptionPanel>();

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

    private void Update()
    {
        HandleMouseEvent();
    }

    private void HandleMouseEvent()
    {
        Cursor.visible = IsOption;
        Cursor.lockState = IsOption ? CursorLockMode.None : CursorLockMode.Locked;
    }

    public void ToggleOptionPanel()
    {
        if (PopupManager.Instance.IsUsePopup<OptionPanel>())
        {
            var panel = PopupManager.Instance.Find<OptionPanel>();
            panel.OnExitOptionPanel();
            return;
        }

        var optionPanel = PopupManager.Instance.GetPopUp<OptionPanel>();
        optionPanel.InitializeOptionPanel();
    }
}
