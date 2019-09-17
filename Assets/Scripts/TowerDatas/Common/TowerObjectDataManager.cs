using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VMUnityLib;

/// <summary>
/// タワー関連のオブジェクトを保存しておくコンテナクラス
/// </summary>
public class TowerObjectDataManager : Singleton<TowerObjectDataManager>
{
    // モチのデータパス
    const string MochiDataPath = "TowerObjectDatas/Mochi/MochiDatas";

    // ウサギのデータパス
    const string RabbitDataPath = "TowerObjectDatas/Rabbit/RabbitDatas";

    // ウサギのレアリティのデータパス
    const string RabbitRarityDataPath = "TowerObjectDatas/Rabbit/RarityDatas";

    // モチのデータマネージャー
    public IdentifiedDataManager<MochiData> MochiDataManager { get; private set; }

    // ウサギのデータマネージャー
    public IdentifiedDataManager<RabbitData> RabbitDataManager { get; private set; }

    // ウサギのレアリティのデータマネージャー
    public IdentifiedDataManager<RabbitRarityData> RabbitRarityDataManager { get; private set; }

    /// <summary>
    /// 全てのデータを読み込む
    /// </summary>
    public void LoadAllData()
    {
        // モチのアセットデータを読み込む
        MochiDataManager = new IdentifiedDataManager<MochiData>(MochiDataPath);
        MochiDataManager.LoadData();

        // ウサギのアセットデータを読み込む
        RabbitDataManager = new IdentifiedDataManager<RabbitData>(RabbitDataPath);
        RabbitDataManager.LoadData();

        // ウサギのレアリティのアセットデータを読み込む
        RabbitRarityDataManager = new IdentifiedDataManager<RabbitRarityData>(RabbitRarityDataPath);
        RabbitRarityDataManager.LoadData();

#if UNITY_EDITOR
        // 各レアリティの抽選確率をログに表示する
        DrawRarityPercentLog();
# endif
    }

    /// <summary>
    /// ウサギの番号からデータを取得
    /// </summary>
    /// <param name="number">取得するウサギの番号</param>
    /// <returns></returns>
    public RabbitData GetRabbitDataFromNumber(int number)
    {
        foreach(RabbitData rabbitData in RabbitDataManager.GetAllData())
        {
            // 引数の番号と一致するウサギを探す
            if (rabbitData.Number == number)
            {
                // 見つけたら、そのウサギのデータを返す
                return rabbitData;
            }
        }
        // 見つからなかったらnullを返す
        return null;
    }

    /// <summary>
    /// 各レアリティの抽選確率をログに表示する
    /// </summary>
    void DrawRarityPercentLog()
    {
        // 各確率の合計値
        float totalRate = 0;

        // 各確率の合計値を算出
        foreach(RabbitRarityData rarityData in RabbitRarityDataManager.GetAllData())
        {
            totalRate += rarityData.LotteryRate;
        }

        foreach (RabbitRarityData rarityData in RabbitRarityDataManager.GetAllData())
        {
            // 確率のパーセントを計算
            float percent = (rarityData.LotteryRate / totalRate) * 100;

            // 計算結果をログに表示
            Debug.Log("RabbitRarity : " + rarityData.Id.Substring(("RabbitRarity").Length,1) + " , RatePercent : " + percent.ToString("f2"));
        }
    }
}
