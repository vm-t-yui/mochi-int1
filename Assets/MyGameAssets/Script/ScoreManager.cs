using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using VMUnityLib;

/// <summary>
/// スコアのマネージャークラス
/// </summary>
public class ScoreManager : SingletonMonoBehaviour<ScoreManager>
{
    // 対象のオブジェクトのenum
    public enum TargetObject
    {
        Mochi,     // もち
        Rabbit,    // うさぎ
        Length,    // enumの長さ
    }

    public int NowBreakNum { get; private set; } = 0;     // 壊した数の合計
    
    public int MaxBreakNum { get; private set; } = 0;     // 壊した数の合計(表示用)

    public const int GoodScore = 60;                      // 良スコアの目標

    /// <summary>
    /// 起動処理
    /// </summary>
    void OnEnable()
    {
        MaxBreakNum = GameDataManager.Inst.PlayData.HighScore;
    }

    /// <summary>
    /// スコアリセット
    /// </summary>
    public void Reset()
    {
        // 前回のスコアとハイスコアを更新
        if(GameDataManager.Inst.PlayData.LastScore < NowBreakNum)
        {
            GameDataManager.Inst.PlayData.HighScore = NowBreakNum;
        }
        GameDataManager.Inst.PlayData.LastScore = NowBreakNum;

        // データセーブ
        JsonDataSaver.Save(GameDataManager.Inst.PlayData);

        // データをリセット
        NowBreakNum = 0;
        MaxBreakNum = GameDataManager.Inst.PlayData.HighScore;
    }

    /// <summary>
    /// 壊した数の更新
    /// </summary>
    /// <param name="num"></param>
    public void UpdateGetNum()
    {
        NowBreakNum++;
    }
}
