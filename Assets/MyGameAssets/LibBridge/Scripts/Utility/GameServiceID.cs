//== 実績・リーダーボード一覧 ================================================================================//
// ＜実績１＞ 初めてのパンチ
// ・ID (Android) : CgkIk5zJ6IMPEAIQAQ
// ・ID (IOS)     : firstPunch
// ・解放条件     : 初プレイ時のリザルトで解放

// ＜実績２＞ うさぎ界の救世主
// ・ID (Android) : CgkIk5zJ6IMPEAIQAg
// ・ID (IOS)     : rabbitSavior
// ・解放条件     : うさぎの合計救出回数が500を超えた時点でのリザルトで解放

// ＜実績３＞ もちの破壊王
// ・ID (Android) : CgkIk5zJ6IMPEAIQAw
// ・ID (IOS)     : destructionKing
// ・解放条件     : 合計スコアが5,000を超えた時点でのリザルトで解放

// ＜実績４＞ うさぎ博士
// ・ID (Android) : CgkIk5zJ6IMPEAIQBA
// ・ID (IOS)     : rabbitDoctor
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

    public const string LEADERBOARD_1 = "CgkIk5zJ6IMPEAIQAA";
    public const string LEADERBOARD_2 = "CgkIk5zJ6IMPEAIQBQ";
#elif UNITY_IOS
    public const string ACHIEVEMENT_1 = "firstPunch";
    public const string ACHIEVEMENT_2 = "rabbitSavior";
    public const string ACHIEVEMENT_3 = "destructionKing";
    public const string ACHIEVEMENT_4 = "rabbitDoctor";

    public const string LEADERBOARD_1 = "maximumBroken";
    public const string LEADERBOARD_2 = "totalBroken";
#else
    public const string ACHIEVEMENT_1 = "unexpected_platform";
    public const string ACHIEVEMENT_2 = "unexpected_platform";
    public const string ACHIEVEMENT_3 = "unexpected_platform";
    public const string ACHIEVEMENT_4 = "unexpected_platform";

    public const string LEADERBOARD_1 = "unexpected_platform";
    public const string LEADERBOARD_2 = "unexpected_platform";
#endif
}
