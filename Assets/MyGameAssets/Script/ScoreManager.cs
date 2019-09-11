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
    public enum Score
    {
        Low,     // 低
        Normal,  // 中
        Good,    // 高
        VeryGood,// とても高い
    }

    public int NowBreakNum { get; private set; } = 0;     // 壊した数の合計

    public int MaxBreakNum { get; private set; } = 0;     // 壊した数の合計(表示用)

    public const int LowScore = 10;                       // 低スコアの基準
    public const int NormalScore = 30;                    // 良スコアの基準
    public const int GoodScore = 60;                      // 高スコアの基準
    public const int VeryGoodScore = 100;                      // 高スコアの基準

    /// <summary>
    /// 起動処理
    /// </summary>
    void OnEnable()
    {
        MaxBreakNum = GameDataManager.Inst.PlayData.HighScore;
    }

    /// <summary>
    /// スコアセーブ
    /// </summary>
    public void Save()
    {
        // プレイデータのインスタンスを取得
        PlayData playData = GameDataManager.Inst.PlayData;

        // 前回のスコアとハイスコアを更新
        if (playData.HighScore < NowBreakNum)
        {
            playData.HighScore = NowBreakNum;
     //       GameServiceUtil.ReportScore(playData.HighScore, 0);
        }
        playData.LastScore = NowBreakNum;

        // 合計スコアを加算
        playData.TotalScore += NowBreakNum;
        if (playData.TotalScore > PlayData.MaxTotalScore)
        {
            playData.TotalScore = PlayData.MaxTotalScore;
        }
//        GameServiceUtil.ReportScore(playData.TotalScore, 1);

        // データセーブ
        JsonDataSaver.Save(GameDataManager.Inst.PlayData);
    }

    /// <summary>
    /// スコアリセット
    /// </summary>
    public void Reset()
    {
        // データをリセット
        NowBreakNum = 0;
        MaxBreakNum = GameDataManager.Inst.PlayData.HighScore;
    }

    /// <summary>
    /// 壊した数の更新
    /// </summary>
    public void UpdateGetNum()
    {
        NowBreakNum++;
    }
}