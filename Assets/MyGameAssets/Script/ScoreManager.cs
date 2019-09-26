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
    public const int NormalScore = 100;                    // 良スコアの基準
    public const int GoodScore = 200;                      // 高スコアの基準
    public const int VeryGoodScore = 300;                 // 高スコアの基準

    // 各スキンの解放スコア
    int[] releaseScore =
    {
        PlayData.ReleaseNormalSkinScore,
        PlayData.ReleaseKouhakuSkinScore,
        PlayData.ReleaseYomogiSkinScore,
        PlayData.ReleaseIchigoSkinScore,
        PlayData.ReleaseKashiwaSkinScore,
        PlayData.ReleaseIsobeSkinScore
    };

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
        UpdateHighScore(playData);

        // 合計スコアを加算
        UpdateTotalScore(playData);

        // スキン解放
        ReleaseSkin(playData);

        // データセーブ
        JsonDataSaver.Save(GameDataManager.Inst.PlayData);
    }

    /// <summary>
    /// ハイスコア更新
    /// </summary>
    /// <param name="playData">プレイデータのインスタンス</param>
    void UpdateHighScore(PlayData playData)
    {
        if (playData.HighScore < NowBreakNum)
        {
            playData.HighScore = NowBreakNum;
            GameServiceUtil.ReportScore(playData.HighScore, 0);
        }
        playData.LastScore = NowBreakNum;
    }

    /// <summary>
    /// トータルスコア更新
    /// </summary>
    /// <param name="playData">プレイデータのインスタンス</param>
    void UpdateTotalScore(PlayData playData)
    {
        playData.TotalScore += NowBreakNum;
        if (playData.TotalScore > PlayData.TotalScoreCountStopValue)
        {
            playData.TotalScore = PlayData.TotalScoreCountStopValue;
        }
        GameServiceUtil.ReportScore(playData.TotalScore, 1);
    }

    /// <summary>
    /// スキンの解放
    /// </summary>
    void ReleaseSkin(PlayData playData)
    {
        for (int i = 1; i < (int)SettingData.SkinType.Length; i++)
        {
            // 目標スコアを達成するとスキン解放
            if (playData.TotalScore >= releaseScore[i])
            {
                if (!playData.IsReleasedSkin[i])
                {
                    playData.IsNewReleasedSkin = true;
                    playData.IsReleasedSkin[i] = true;
                    playData.IsDrawSkinNewIcon[i] = true;
                }
            }
        }
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