/******************************************************************************/
/*!    \brief  プレイデータ.
*******************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayData
{
    public const int AllRabbitNum = 25;                 // うさぎの種類の総数

    [SerializeField]
    int playCount = 0;                                  // プレイ回数
    [SerializeField]
    int highScore = 0;                                  // ハイスコア
    [SerializeField]
    int lastScore = 0;                                  // 最終プレイ時のスコア
    [SerializeField]
    int punchCount = 0;                                 // パンチされたうさぎの数
    [SerializeField]
    bool[] isReleasedRabbit = new bool[AllRabbitNum];   // うさぎの解放フラグ（図鑑用）
    [SerializeField]
    bool isReward = false;                              // リワードフラグ
    [SerializeField]
    bool isNewRabbit = false;                           // 新しいうさぎフラグ
    [SerializeField]
    bool isNewSkin = false;                             // 新しいスキンフラグ

    /// <summary>
    /// 各データのプロパティ
    /// </summary>
    public int PlayCount           { get { return playCount; }        set { playCount++; } }
    public int HighScore           { get { return highScore; }        set { highScore = value; } }
    public int LastScore           { get { return lastScore; }        set { lastScore = value; } }
    public int PunchCount          { get { return punchCount; }       set { punchCount = value; } }
    public bool IsReward           { get { return isReward; }         set { isReward = value; } }
    public bool IsNewRabbit        { get { return isNewRabbit; }      set { isNewRabbit = value; } }
    public bool IsNewSkin          { get { return isNewSkin; }        set { isNewSkin = value; } }
    public bool[] IsReleasedRabbit { get { return isReleasedRabbit; } set { isReleasedRabbit = value; } }

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
}
