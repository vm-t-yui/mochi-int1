using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VMUnityLib;

/// <summary>
/// モチとウサギの出現抽選を行う
/// </summary>
public class ObjectTypeLotteryMachine : MonoBehaviour
{
    // モチの出現率
    [SerializeField]
    [Range(0,100)]
    float mochiSpawnRate = 0;

    // ウサギの出現率
    [SerializeField]
    [Range(0, 100)]
    float rabbitSpawnRate = 0;

    /// <summary>
    /// モチとウサギの出現抽選を行う
    /// </summary>
    public string SpawnLotteryMochiAndRabbit()
    {
        // それぞれの出現率の合計を算出
        float totalRate = mochiSpawnRate + rabbitSpawnRate;

        // 乱数を生成（０～出現率の合計）
        float random = Random.Range(0, totalRate);

        // 乱数をモチの出現率で引く
        random -= mochiSpawnRate;

        // 残りの乱数が０以下であればモチ
        if (random <= 0)
        {
            return TagName.Mochi;
        }
        // そうでなければウサギ
        return TagName.Rabbit;
    }
}
