using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VMUnityLib;

[CreateAssetMenu(menuName = "Data/RabbitRarityData")]
public sealed class RabbitRarityData : BaseData
{
    [SerializeField]
    [Range(0,100)]
    int lotteryRate = 0;

    [SerializeField]
    List<string> rabbitIds = default;

    public int                 LotteryRate   { get { return lotteryRate; } }
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
