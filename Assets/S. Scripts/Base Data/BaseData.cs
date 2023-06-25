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

public enum E_Judge
{
    None,
    Cool,
    Perfect
}

public enum E_GameState
{
    Init,
    Count,
    Ready,
    Play,
    GameOver,
    Result
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

public class AudioStorage
{
    public const string RANDOM_PATH = "Titles/Music/";
    public const string MUSIC_PATH = "InGame/Music/";
}

public static class JudgeStorage
{
    public const string JUDGE_COOL_PATH = "InGame/Judge/Cool";
    public const string JUDGE_PERFECT_PATH = "InGame/Judge/Perfect";
}

public static class AnimatorName
{
    public const string READY_NAME = "Ready";
}

public static class Number
{
    public const float PERFECT_DISTANCE = .6f;
    public const float COOL_DISTANCE = 1.2f;
    public const float MISS_DISTANCE = -1f;

    public const float UPPER_Y = 0f;
    public const float UNDER_Y = -5.8f;
}
#endregion