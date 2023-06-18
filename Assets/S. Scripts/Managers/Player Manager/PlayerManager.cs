using SingletonComponent.Component;
using System.Collections;
using UnityEngine;

public class PlayerManager : SingletonComponent<PlayerManager>
{
    private Player player;

    [SerializeField]
    private Player playerPrefab;

    private AnimationController playerAnimationController;

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
        playerAnimationController = player.GetComponentInChildren<AnimationController>();
    }

    public void JumpAttackPlayer()
    {
        StartCoroutine(nameof(JumpAttack));
    }

    private IEnumerator JumpAttack()
    {
        player.transform.position = new Vector2(player.transform.position.x, PlayerInfo.JUMP_POWER);
        playerAnimationController.AttackRandomPlay();
        yield return new WaitForEndOfFrame();

        player.transform.position = Vector2.Lerp(player.transform.position, new Vector2(player.transform.position.x, PlayerInfo.JUMP_POWER),
                    PlayerInfo.GRAVITY_POWER * Time.deltaTime);
    }
    
    #endregion

    #region Player HP
    public float GetPlayerHP() => GetPlayer().CurrentHp;
    #endregion

    #region Player Position
    public Vector3 GetPlayerPos() => GetPlayer().transform.position;
    #endregion
}