using SingletonComponent.Component;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : SingletonComponent<NoteManager>
{
    #region Static

    private ObjectPool objectPool;

    #endregion

    #region Private Field



    #endregion

    #region Singleton

    protected override void AwakeInstance()
    {
        InitAwake();
    }

    protected override bool InitInstance()
    {
        return true;
    }

    protected override void ReleaseInstance()
    {
        
    }

    #endregion

    #region Method

    private void InitAwake()
    {
        //objectPool = ObjectPool.Instance;
        //objectPool.Initialize();
    }

    #endregion
}
