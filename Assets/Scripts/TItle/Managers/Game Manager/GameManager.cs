using SingletonComponent.Component;
using UnityEngine;

public class GameManager : SingletonComponent<GameManager>
{
    

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

}
