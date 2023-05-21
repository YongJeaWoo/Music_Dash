using SingletonComponent.Component;

public class PanelManager : SingletonComponent<PanelManager>
{
    public bool IsOption => PopupManager.Instance.IsUsePopup<OptionPanel>();
    public bool IsOver => PopupManager.Instance.IsUsePopup<GameOverPanel>();

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

    public void GameOverPanel()
    {
        if (PopupManager.Instance.IsUsePopup<GameOverPanel>())
        {
            var panel = PopupManager.Instance.Find<GameOverPanel>();
            panel.OnExitOverPanel();
            return;
        }

        var gameOverPanel = PopupManager.Instance.GetPopUp<GameOverPanel>();
        gameOverPanel.InitializeOverPanel();
    }
}
