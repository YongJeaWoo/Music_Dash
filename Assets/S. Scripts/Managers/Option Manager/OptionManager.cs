using SingletonComponent.Component;

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