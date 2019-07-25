/******************************************************************************/
/*!    \brief  プレイデータ.
*******************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayData
{
    [SerializeField]
    int playCount = 0;    // プレイ回数
    [SerializeField]
    int highScore = 0;    // ハイスコア
    [SerializeField]
    int lastScore = 0;    // 最終プレイ時のスコア

    /// <summary>
    /// 各データのプロパティ
    /// </summary>
    public int PlayCount { get { return playCount; } set { playCount++; } }
    public int HighScore { get { return highScore; } set { highScore = value; } }
    public int LastScore { get { return lastScore; } set { lastScore = value; } }
}
