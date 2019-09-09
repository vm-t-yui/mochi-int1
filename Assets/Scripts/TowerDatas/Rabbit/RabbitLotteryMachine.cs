using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VMUnityLib;
using System.Linq;

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
    float totalRarityLotteryRate = 0;

    // ウサギの出現率の合計値
    float totalRabbitSpawnRate = 0;

    /// <summary>
    /// 開始
    /// </summary>
    void Start()
    {
        // 乱数生成のシード値を設定
        Random.InitState(System.DateTime.Now.Millisecond);

        // データマネージャーから全てのレアリティのデータを取得する
        rabbitRarityDatas = TowerObjectDataManager.Inst.RabbitRarityDataManager.GetAllData();
        // データマネージャーから全てのウサギのデータを取得する
        rabbitDatas = TowerObjectDataManager.Inst.RabbitDataManager.GetAllData();
    }

    /// <summary>
    /// レアリティの抽選を行う
    /// </summary>
    /// <returns>抽選結果のレアリティのIDを返す</returns>
    public string LotteryRarity()
    {
        // 複数のレアリティの抽選率の合計を算出
        float totalRarityLotteryRate = GetTotalRarityRate(rabbitRarityDatas);

        // 乱数を生成（０～抽選率の合計）
        float random = Random.Range(0, totalRarityLotteryRate);

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
    /// レアリティに属しているウサギで出現抽選を行う
    /// </summary>
    /// <param name="rarityId">抽選を行うウサギが属しているレアリティのID</param>
    /// /// <param name="isNotReleasedOnly">救出されたことのないウサギのみで抽選を行うか</param>
    /// <returns>抽選結果のウサギのIDを返す</returns>
    public string LotterySpawnRabbit(string rarityId,bool isNotReleasedOnly)
    {
        // レアリティに属しているウサギのデータを取得する
        IEnumerable<RabbitData> belongRabbits;
        // 通常通り、属しているウサギを取得する
        if (!isNotReleasedOnly)
        {
            belongRabbits = GetRarityBelongRabbits(rarityId);
        }
        // レアリティの中で救出されていないウサギを取得する
        else
        {
            belongRabbits = GetNotReleasedRabbits(rarityId);
        }

        // ウサギの出現率の合計を取得する
        float totalSpawnRate = GetTotalSpawnRate(belongRabbits);

        // 乱数を生成（０～出現率の合計）
        float random = Random.Range(0, totalSpawnRate);

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
    /// <param name="cantReleasedRabbitOnly">救出されたことのないウサギのみを取得するか</param>
    /// <returns>属しているウサギのデータ</returns>
    public IEnumerable<RabbitData> GetRarityBelongRabbits(string rarityId)
    {
        // レアリティのデータを取得
        RabbitRarityData rarityData;
        TowerObjectDataManager.Inst.RabbitRarityDataManager.GetData(rarityId,out rarityData);

        // 指定のレアリティに属しているウサギをIDで判定して取得していく
        foreach(string rabbitId in rarityData.RabbitIds)
        {
            foreach (RabbitData rabbitData in rabbitDatas)
            {
                // レアリティに属しているウサギのみ
                if (rabbitId == rabbitData.Id)
                {
                    yield return rabbitData;
                }
            }
        }
    }

    /// <summary>
    /// 救出されたことのないウサギのみを取得する
    /// </summary>
    /// <param name="rarityId">指定のレアリティ</param>
    /// <returns></returns>
    public IEnumerable<RabbitData> GetNotReleasedRabbits(string rarityId)
    {
        // ウサギの救出フラグを取得
        bool[] isReleasedRabbits = GameDataManager.Inst.PlayData.IsReleasedRabbit;

        // レアリティのデータを取得
        RabbitRarityData rarityData;
        TowerObjectDataManager.Inst.RabbitRarityDataManager.GetData(rarityId, out rarityData);

        // 指定のレアリティに属しているウサギをIDで判定して取得していく
        foreach (string rabbitId in rarityData.RabbitIds)
        {
            foreach (RabbitData rabbitData in rabbitDatas)
            {
                // レアリティに属しているウサギのみ
                if (rabbitId == rabbitData.Id)
                {
                    // 救出されていないウサギのみ
                    if (!isReleasedRabbits[rabbitData.Number])
                    {
                        yield return rabbitData;
                    }
                }
            }
        }
    }

    /// <summary>
    /// 指定のレアリティに救出されていないウサギが存在するか
    /// </summary>
    /// <param name="rarityId"></param>
    /// <returns></returns>
    public bool ExistNotReleasedRabbit(string rarityId)
    {
        // ウサギの救出フラグを取得
        bool[] isReleasedRabbits = GameDataManager.Inst.PlayData.IsReleasedRabbit;

        // レアリティのデータを取得
        RabbitRarityData rarityData;
        TowerObjectDataManager.Inst.RabbitRarityDataManager.GetData(rarityId, out rarityData);

        // 指定のレアリティに属しているウサギをIDで判定して取得していく
        foreach (string rabbitId in rarityData.RabbitIds)
        {
            foreach (RabbitData rabbitData in rabbitDatas)
            {
                // レアリティに属しているウサギのみ
                if (rabbitId == rabbitData.Id)
                {
                    // 救出されていないウサギのみ
                    if (!isReleasedRabbits[rabbitData.Number])
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    /// <summary>
    /// 複数のレアリティの抽選率の合計を取得する
    /// </summary>
    /// <param name="rarityDatas">取得対象の複数のレアリティ</param>
    /// <returns>レアリティの抽選率の合計値</returns>
    public float GetTotalRarityRate(IEnumerable<RabbitRarityData> rarityDatas)
    {
        float totalRarityLotteryRate = 0;
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
    public float GetTotalSpawnRate(IEnumerable<RabbitData> rabbitDatas)
    {
        float totalSpawnRate = 0;
        foreach(RabbitData rabbit in rabbitDatas)
        {
            totalSpawnRate += rabbit.SpawnRate;
        }
        return totalSpawnRate;
    }
}
