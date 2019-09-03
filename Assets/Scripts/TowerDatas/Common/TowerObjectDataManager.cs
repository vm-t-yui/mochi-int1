using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VMUnityLib;

/// <summary>
/// タワー関連のオブジェクトを保存しておくコンテナクラス
/// </summary>
public class TowerObjectDataManager : SingletonMonoBehaviour<TowerObjectDataManager>
{
    // モチのデータパス
    [SerializeField]
    string mochiDataPath = default;

    // ウサギのデータパス
    [SerializeField]
    string rabbitDataPath = default;

    // ウサギのレアリティのデータパス
    [SerializeField]
    string rabbitRarityDataPath = default;

    // モチのデータマネージャー
    public IdentifiedDataManager<MochiData> MochiDataManager { get; private set; }

    // ウサギのデータマネージャー
    public IdentifiedDataManager<RabbitData> RabbitDataManager { get; private set; }

    // ウサギのレアリティのデータマネージャー
    public IdentifiedDataManager<RabbitRarityData> RabbitRarityDataManager { get; private set; }

    /// <summary>
    /// 全てのデータを読み込む
    /// </summary>
    void Awake()
    {
        // モチのアセットデータを読み込む
        MochiDataManager = new IdentifiedDataManager<MochiData>(mochiDataPath);
        MochiDataManager.LoadData();

        // ウサギのアセットデータを読み込む
        RabbitDataManager = new IdentifiedDataManager<RabbitData>(rabbitDataPath);
        RabbitDataManager.LoadData();

        // ウサギのレアリティのアセットデータを読み込む
        RabbitRarityDataManager = new IdentifiedDataManager<RabbitRarityData>(rabbitRarityDataPath);
        RabbitRarityDataManager.LoadData();
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
}
