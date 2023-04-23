using SingletonComponent.Component;
using UnityEngine;

public class PlayerManager : SingletonComponent<PlayerManager>
{
    Player player;

    public void SetPlayer(Player _player)
    {
        player = _player;
    }

    public float PlayerHP()
    {
        return player.CurrentHp;
    }

    public Vector3 PlayerPos()
    {
        return player.transform.position;
    }

    #region SingleTon
    protected override void AwakeInstance()
    {
        
    }

    protected override bool InitInstance()
    {
        throw new System.NotImplementedException();
    }

    protected override void ReleaseInstance()
    {
        throw new System.NotImplementedException();
    }
    #endregion
}