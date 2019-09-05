using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VMUnityLib;

/// <summary>
/// ウサギのレアリティのデータを保持するクラス
/// </summary>
[CreateAssetMenu(menuName = "Data/RabbitRarityData")]
public sealed class RabbitRarityData : BaseData
{
    // レアリティの抽選率
    [SerializeField]
    [Range(0,100)]
    float lotteryRate = 0;

    // レアリティに属しているウサギのIDリスト
    [SerializeField]
    List<string> rabbitIds = default;

    public float               LotteryRate   { get { return lotteryRate; } }
    public IEnumerable<string> RabbitIds
    {
        get
        {
            foreach(string id in rabbitIds)
            {
                yield return id;
            }
        }
    }
}
