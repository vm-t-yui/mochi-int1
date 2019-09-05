using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ウサギの抽選テスト
/// NOTE : テスト用のスクリプトなのでゲームには一切使用しない。
/// </summary>
public class RabbitLotteryTest : MonoBehaviour
{
    // 抽選回数
    [SerializeField]
    int lotteryNum = 0;

    // ウサギの抽選クラス
    [SerializeField]
    RabbitLotteryMachine rabbitLotteryMachine = default;

    /// <summary>
    /// 開始
    /// </summary>
    void OnEnable()
    {
        // 指定の回数だけ抽選を行う
        for (int i = 0; i < lotteryNum; i++)
        {
            // ウサギのレアリティの抽選を行う
            string rarityId = rabbitLotteryMachine.LotteryRarity();
            // 決定したレアリティに属しているウサギで抽選を行う
            string rabbitId = rabbitLotteryMachine.LotterySpawnRabbit(rarityId, false);

            // 抽選したウサギをログに表示
            Debug.Log("RabbitLotteryed [Rarity : " + rarityId.Substring(("RabbitRarity").Length, 1) + "]");
        }
    }
}
