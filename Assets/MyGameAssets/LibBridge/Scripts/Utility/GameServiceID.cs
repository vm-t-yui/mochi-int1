//== 実績・リーダーボード一覧 ================================================================================//
// ＜実績１＞ 初めてのパンチ
// ・ID (Android) : CgkIk5zJ6IMPEAIQAQ
// ・ID (IOS)     : 
// ・解放条件     : 初プレイ時のリザルトで解放

// ＜実績２＞ うさぎ界の救世主
// ・ID (Android) : CgkIk5zJ6IMPEAIQAg
// ・ID (IOS)     : 
// ・解放条件     : うさぎの合計救出回数が500を超えた時点でのリザルトで解放

// ＜実績３＞ もちの破壊王
// ・ID (Android) : CgkIk5zJ6IMPEAIQAw
// ・ID (IOS)     : 
// ・解放条件     : 合計スコアが5,000を超えた時点でのリザルトで解放

// ＜実績４＞ うさぎ博士
// ・ID (Android) : CgkIk5zJ6IMPEAIQBA
// ・ID (IOS)     : 
// ・解放条件     : 図鑑コンプリート時のリザルトで解放


// ＜リーダーボード１＞ 最大粉砕数
// ・ID (Android) : CgkIk5zJ6IMPEAIQAA
// ・ID (IOS)     : maximumBroken

// ＜リーダーボード２＞ 合計粉砕数
// ・ID (Android) : CgkIk5zJ6IMPEAIQBQ
// ・ID (IOS)     : totalBroken
//=========================================================================================//

/// <summary>
/// 実績・リーダーボードIDの定数クラス
/// </summary>
public class GameServiceID
{
#if UNITY_ANDROID
    public const string ACHIEVEMENT_1 = "CgkIk5zJ6IMPEAIQAQ";
    public const string ACHIEVEMENT_2 = "CgkIk5zJ6IMPEAIQAg";
    public const string ACHIEVEMENT_3 = "CgkIk5zJ6IMPEAIQAw";
    public const string ACHIEVEMENT_4 = "CgkIk5zJ6IMPEAIQBA";

    public const string LEADERBOARD_1 = "";
    public const string LEADERBOARD_2 = "";
#elif UNITY_IOS
    public const string ACHIEVEMENT_1 = "unexpected_platform";
    public const string ACHIEVEMENT_2 = "unexpected_platform";
    public const string ACHIEVEMENT_3 = "unexpected_platform";
    public const string ACHIEVEMENT_4 = "unexpected_platform";

    public const string LEADERBOARD_1 = "";
    public const string LEADERBOARD_2 = "";
#else
    public const string ACHIEVEMENT_1 = "unexpected_platform";
    public const string ACHIEVEMENT_2 = "unexpected_platform";
    public const string ACHIEVEMENT_3 = "unexpected_platform";
    public const string ACHIEVEMENT_4 = "unexpected_platform";

    public const string LEADERBOARD_1 = "unexpected_platform";
    public const string LEADERBOARD_2 = "unexpected_platform";
#endif
}
