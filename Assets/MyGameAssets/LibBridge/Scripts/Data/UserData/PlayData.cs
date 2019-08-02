/******************************************************************************/
/*!    \brief  プレイデータ.
*******************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayData
{
    public const int AllRabbitNum = 25;    // うさぎの種類の総数

    [SerializeField]
    int playCount = 0;     // プレイ回数
    [SerializeField]
    int highScore = 0;     // ハイスコア
    [SerializeField]
    int lastScore = 0;     // 最終プレイ時のスコア
    [SerializeField]
    int punchCount = 0;    // パンチされたうさぎの数

    [SerializeField]
    bool[] isReleasedRabbit = new bool[AllRabbitNum];    // うさぎの解放フラグ（図鑑用）

    /// <summary>
    /// 各データのプロパティ
    /// </summary>
    public int PlayCount           { get { return playCount; }        set { playCount++; } }
    public int HighScore           { get { return highScore; }        set { highScore = value; } }
    public int LastScore           { get { return lastScore; }        set { lastScore = value; } }
    public int PunchCount          { get { return punchCount; }       set { punchCount = value; } }
    public bool[] IsReleasedRabbit { get { return isReleasedRabbit; } set { isReleasedRabbit = value; } }
}
