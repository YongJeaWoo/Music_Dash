#region Class Collect
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
    public const float UPPER_Y = 0f;
    public const float UNDER_Y = -5.8f;
}
#endregion

#region Enum Collect
public enum E_Judge
{
    None,
    Miss,
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