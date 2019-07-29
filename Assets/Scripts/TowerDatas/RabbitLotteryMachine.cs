using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VMUnityLib;

/// <summary>
/// ウサギの出現抽選を行う
/// </summary>
public class RabbitLotteryMachine : MonoBehaviour
{
    // ウサギのレアリティデータ
    IEnumerable<RabbitRarityData> rabbitRarityDatas;
    
    // 各ウサギのデータ
    IEnumerable<RabbitData> rabbitDatas;

    // レアリティ抽選率の合計値
    int totalRarityLotteryRate = 0;

    // ウサギの出現率の合計値
    int totalRabbitSpawnRate = 0;

    /// <summary>
    /// 開始
    /// </summary>
    void Start()
    {
        // 乱数生成のシード値を設定
        Random.InitState(System.DateTime.Now.Millisecond);

        // データマネージャーから全てのレアリティのデータを取得する
        rabbitRarityDatas = TowerObjectContainer.Inst.RabbitRarityDataManager.GetAllData();
        // データマネージャーから全てのウサギのデータを取得する
        rabbitDatas = TowerObjectContainer.Inst.RabbitDataManager.GetAllData();
    }

    /// <summary>
    /// レアリティの抽選を行う
    /// </summary>
    /// <returns>抽選結果のレアリティのIDを返す</returns>
    public string LotteryRarity()
    {
        // 複数のレアリティの抽選率の合計を算出
        int totalRarityLotteryRate = GetTotalRarityRate(rabbitRarityDatas);

        // 乱数を生成（０～抽選率の合計）
        int random = Random.Range(0, totalRarityLotteryRate);

        // 乱数をそれぞれのレアリティの抽選率で引いていく
        foreach (RabbitRarityData rarityData in rabbitRarityDatas)
        {
            random -= rarityData.LotteryRate;

            // 残りの乱数が０以下になったら、その時点のレアリティのIDを返す
            if (random <= 0)
            {
                return rarityData.Id;
            }
        }

        // ここにきたら抽選失敗
        Debug.LogError("rarity is lottery faild.");
        return null;
    }

    /// <summary>
    /// 出現するウサギの抽選を行う
    /// </summary>
    /// <param name="rarityId">抽選を行うウサギが属しているレアリティのID</param>
    /// <returns>抽選結果のウサギのIDを返す</returns>
    public string LotterySpawnRabbit(string rarityId)
    {
        // レアリティに属しているウサギのデータを取得する
        IEnumerable<RabbitData> belongRabbits = GetRarityBelongRabbits(rarityId);

        // レアリティに属しているウサギの出現率の合計を取得する
        int totalSpawnRate = GetTotalSpawnRate(belongRabbits);

        // 乱数を生成（０～出現率の合計）
        int random = Random.Range(0, totalSpawnRate);

        // 乱数をそれぞれのウサギの出現率で引いていく
        foreach (RabbitData rabbitData in belongRabbits)
        {
            random -= rabbitData.SpawnRate;

            // 乱数が０になった時点のウサギのIDを返す
            if (random <= 0)
            {
                return rabbitData.Id;
            }
        }

        // ここにきたら抽選失敗
        Debug.LogError("rabbit is lottery faild.");
        return null;
    }

    /// <summary>
    /// 指定のレアリティに属しているウサギのデータを取得する
    /// </summary>
    /// <param name="rarityId">指定のレアリティ</param>
    /// <returns>属しているウサギのデータ</returns>
    public IEnumerable<RabbitData> GetRarityBelongRabbits(string rarityId)
    {
        // レアリティのデータを取得
        RabbitRarityData rarityData;
        TowerObjectContainer.Inst.RabbitRarityDataManager.GetData(rarityId,out rarityData);

        // 指定のレアリティに属しているウサギをIDで判定して取得していく
        foreach(string rabbitId in rarityData.RabbitIds)
        {
            foreach (RabbitData rabbitData in rabbitDatas)
            {
                if (rabbitId == rabbitData.Id)
                {
                    yield return rabbitData;
                }
            }
        }
    }

    /// <summary>
    /// 複数のレアリティの抽選率の合計を取得する
    /// </summary>
    /// <param name="rarityDatas">取得対象の複数のレアリティ</param>
    /// <returns>レアリティの抽選率の合計値</returns>
    public int GetTotalRarityRate(IEnumerable<RabbitRarityData> rarityDatas)
    {
        int totalRarityLotteryRate = 0;
        foreach (RabbitRarityData rarityData in rabbitRarityDatas)
        {
            totalRarityLotteryRate += rarityData.LotteryRate;
        }
        return totalRarityLotteryRate;
    }

    /// <summary>
    /// 複数のウサギの出現率の合計を取得する
    /// </summary>
    /// <param name="rabbitDatas">取得対象の複数のウサギ</param>
    /// <returns>出現率の合計を返す</returns>
    public int GetTotalSpawnRate(IEnumerable<RabbitData> rabbitDatas)
    {
        int totalSpawnRate = 0;
        foreach(RabbitData rabbit in rabbitDatas)
        {
            totalSpawnRate += rabbit.SpawnRate;
        }
        return totalSpawnRate;
    }
}
