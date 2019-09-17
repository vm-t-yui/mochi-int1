/******************************************************************************/
/*!    \brief  プレイデータ.
*******************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayData
{
    public const int AllRabbitNum             = 25;           // うさぎの種類の総数
    public const int AllSkinNum               = 6;            // スキンの種類の総数
    public const int AllAchievementNum        = 4;            // 実績の総数
    public const int ReleaseNormalSkinScore   = 0;            // 普通の餅のスキン解放スコア
    public const int ReleaseKouhakuSkinScore  = 1000;         // 紅白餅のスキン解放スコア
    public const int ReleaseYomogiSkinScore   = 3000;         // よもぎ餅のスキン解放スコア     
    public const int ReleaseIchigoSkinScore   = 5000;         // いちご大福のスキン解放スコア
    public const int ReleaseKashiwaSkinScore  = 8000;         // かしわ餅のスキン解放スコア
    public const int ReleaseIsobeSkinScore    = 10000;        // 磯部餅のスキン解放スコア

    public const int CountStopValue           = 999;          // データのカンスト値
    public const int TotalScoreCountStopValue = 9999999;      // 合計スコア用のカンスト値

    [SerializeField]
    int playCount = 0;                                        // プレイ回数
    [SerializeField]
    int highScore = 0;                                        // ハイスコア
    [SerializeField]
    int lastScore = 0;                                        // 最終プレイ時のスコア
    [SerializeField]
    int punchCount = 0;                                       // パンチされたうさぎの数
    [SerializeField]
    int totalPunchCount = 0;                                  // パンチされたウサギの数の合計
    [SerializeField]
    int totalRescueCount = 0;                                 // うさぎの合計救出回数
    [SerializeField]
    int totalScore = 0;                                       // 合計スコア
    [SerializeField]
    bool[] isReleasedRabbit = new bool[AllRabbitNum];         // うさぎの解放フラグ（図鑑用）
    [SerializeField]
    bool[] isReleasedSkin = new bool[AllSkinNum];             // スキンの解放フラグ（スキンウィンドウ用）
    [SerializeField]
    bool[] isDrawRabbitNewIcon = new bool[AllRabbitNum];      // ウサギのNewアイコン表示フラグ(項目別)
    [SerializeField]
    bool[] isDrawSkinNewIcon = new bool[AllSkinNum];          // スキンのNewアイコン表示フラグ(項目別)
    [SerializeField]
    bool isReward = false;                                    // リワードフラグ
    [SerializeField]
    bool isNewReleasedRabbit = false;                         // ウサギのNewアイコン表示フラグ(図鑑自体)
    [SerializeField]
    bool isNewReleasedSkin = false;                           // スキンのNewアイコン表示フラグ(スキン画面自体)
    [SerializeField]
    bool[] isReleasedAchieve = new bool[AllAchievementNum];   // 実績の解除状況
    [SerializeField]
    bool isNewReleasedAchieve = false;                        // 新しく実績を解放したときのフラグ

    /// <summary>
    /// 各データのプロパティ
    /// </summary>
    public int PlayCount              { get { return playCount; }           set { playCount++; if (playCount > CountStopValue) playCount = CountStopValue; } }
    public int HighScore              { get { return highScore; }           set { highScore = value; } }
    public int LastScore              { get { return lastScore; }           set { lastScore = value; } }
    public int PunchCount             { get { return punchCount; }          set { punchCount = value; } }
    public int TotalPunchCount        { get { return totalPunchCount; }     set { totalPunchCount = value; if (totalPunchCount > CountStopValue) totalPunchCount = CountStopValue; } }
    public int TotalRescueCount       { get { return totalRescueCount; }    set { totalRescueCount = value; if (totalRescueCount > CountStopValue) totalRescueCount = CountStopValue; } }
    public int TotalScore             { get { return totalScore; }          set { totalScore = value; if (totalScore > TotalScoreCountStopValue) totalScore = TotalScoreCountStopValue; } }
    public bool IsReward              { get { return isReward; }            set { isReward = value; } }
    public bool IsNewReleasedRabbit   { get { return isNewReleasedRabbit; } set { isNewReleasedRabbit = value; } }
    public bool IsNewReleasedSkin     { get { return isNewReleasedSkin; }   set { isNewReleasedSkin = value; } }
    public bool IsNewReleasedAchieve  { get { return isNewReleasedAchieve; }set { isNewReleasedAchieve = value; } }
    public bool[] IsReleasedRabbit    { get { return isReleasedRabbit; }    set { isReleasedRabbit = value; } }
    public bool[] IsReleasedSkin      { get { return isReleasedSkin; }      set { isReleasedSkin = value; } }
    public bool[] IsDrawRabbitNewIcon { get { return isDrawRabbitNewIcon; } set { isDrawRabbitNewIcon = value; } }
    public bool[] IsDrawSkinNewIcon   { get { return isDrawSkinNewIcon; }   set { isDrawSkinNewIcon = value; } }
    public bool[] IsReleasedAchieve   { get { return isReleasedAchieve; }   set { isReleasedAchieve = value; } }

    /// <summary>
    /// うさぎをコンプリートしているかどうか
    /// </summary>
    public bool RabbitComplete()
    {
        // NOTE:Distinct().Count() == 1は重複が1つなら(配列の全ての要素が同じ値なら)trueを返すという条件式で
        //      うさぎをコンプリートしていれば配列のすべてがtrueなり、重複が1となるのでこの条件式が通る
        if (IsReleasedRabbit.Distinct().Count() == 1)
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// Newアイコン表示中のウサギが存在するか
    /// </summary>
    /// <returns>存在しているかどうかのフラグを返す</returns>
    public bool ExistDrawNewIconRabbit()
    {
        // 存在している場合
        if (isDrawRabbitNewIcon.ToList().Find(flag => flag))
        {
            return true;
        }
        // 存在していなければ
        return false;
    }

    /// <summary>
    /// Newアイコン表示中のスキンが存在するか
    /// </summary>
    /// <returns>存在しているかどうかのフラグを返す</returns>
    public bool ExistDrawNewIconSkin()
    {
        // 存在している場合
        if (isDrawSkinNewIcon.ToList().Find(flag => flag))
        {
            return true;
        }
        // 存在していなければ
        return false;
    }
}
