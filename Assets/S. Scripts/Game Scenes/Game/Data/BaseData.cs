using UnityEngine;

#region Enum Collect
public enum E_JudgeState
{
    None,
    Init,
    Ready
}

public enum E_AniState
{
    None,
    Run,
    Jump,
    Fall,
    Damage,
    Dead,
}

public enum E_AttackState
{
    None,
    Attack1,
    Attack2,
    Attack3,
    Length
}
#endregion

#region Class Collect
public class PlayerInfo
{
    public const int PLAYER_MAXHP = 100;
    public const float IMMUTE_TIME = 2.5f;

    [Header("Player Behaviour")]
    public const float GRAVITY_POWER = 20f;
    public const float JUMP_POWER = 2f;
    public const float LANDING = -5.5f;
    public const float LANDING_POWER = 25f;
}
#endregion