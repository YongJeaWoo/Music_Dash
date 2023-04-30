using SingletonComponent.Component;
using UnityEngine;

public class PlayerManager : SingletonComponent<PlayerManager>
{
    private Player player;

    [SerializeField]
    private Player playerPrefab;

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

    #region Player Frame
    //public void SetPlayer(Player _player)
    //{
    //    player = _player;

    //    if (player != null) return;
    //}

    public Player GetPlayer() => player;

    public void InitPlayer()
    {
        player = Instantiate(playerPrefab);
    }
    #endregion

    #region Player HP
    public float GetPlayerHP() => GetPlayer().CurrentHp;
    #endregion

    #region Player Position
    public Vector3 GetPlayerPos() => GetPlayer().transform.position;
    #endregion
}