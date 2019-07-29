using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VMUnityLib;

/// <summary>
/// タワー関連のオブジェクトを保存しておくコンテナクラス
/// </summary>
public class TowerObjectContainer : SingletonMonoBehaviour<TowerObjectContainer>
{
    [SerializeField]
    string mochiDataPath = default;

    [SerializeField]
    string rabbitDataPath = default;

    [SerializeField]
    string rabbitRarityDataPath = default;

    public IdentifiedDataManager<MochiData>        MochiDataManager        { get; private set; }
    public IdentifiedDataManager<RabbitData>       RabbitDataManager       { get; private set; }
    public IdentifiedDataManager<RabbitRarityData> RabbitRarityDataManager { get; private set; }

    /// <summary>
    /// 全てのデータを読み込む
    /// </summary>
    void Awake()
    {
        MochiDataManager = new IdentifiedDataManager<MochiData>(mochiDataPath);
        MochiDataManager.LoadData();

        RabbitDataManager = new IdentifiedDataManager<RabbitData>(rabbitDataPath);
        RabbitDataManager.LoadData();

        RabbitRarityDataManager = new IdentifiedDataManager<RabbitRarityData>(rabbitRarityDataPath);
        RabbitRarityDataManager.LoadData();
    }
}
