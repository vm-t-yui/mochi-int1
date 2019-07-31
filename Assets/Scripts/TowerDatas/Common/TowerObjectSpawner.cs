using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathologicalGames;
using System;

/// <summary>
/// タワーオブジェクトを生成するクラス
/// </summary>
public class TowerObjectSpawner : MonoBehaviour
{
    // モチかウサギかを抽選するクラス
    [SerializeField]
    ObjectTypeLotteryMachine objectTypeLotteryMachine = default;

    // ウサギの抽選を行うクラス
    [SerializeField]
    RabbitLotteryMachine rabbitLotteryMachine = default;

    // モチのスポーンプール
    [SerializeField]
    SpawnPool mochiSpawnPool = default;

    // ウサギのスポーンプール
    [SerializeField]
    SpawnPool rabbitSpawnPool = default;

    // スポーン時のオブジェクト間の高さの間隔
    [SerializeField]
    float spawnHeightInterval = 0;

    // 前回スポーンしたオブジェクトの種類
    string prevSpawnObjectType = null;

    // 現在のモチのスキン
    SettingData.SkinType mochiSkinType = SettingData.SkinType.NormalMochi;

    /// <summary>
    /// 開始
    /// </summary>
    void Start()
    {
        // 現在のモチのスキンを取得する
        mochiSkinType = GameDataManager.Inst.SettingData.UseSkin;
    }

    /// <summary>
    /// オブジェクトのスポーンを行う
    /// </summary>
    /// <param name="spawnNum">スポーンする数</param>
    /// <returns>スポーンしたオブジェクトのTransformを返す</returns>
    public IEnumerable<(Transform,string)> Spawn(int spawnNum)
    {
        // スポーンしたオブジェクト
        (Transform, string) spawnedObject;

        // 指定の数だけ繰り返し抽選を行い、スポーンしていく
        for (int spawnCount = 0; spawnCount < spawnNum;)
        {
            // スポーン位置の計算
            Vector3 spawnPos = new Vector3(transform.position.x, transform.position.y + spawnHeightInterval * spawnCount, transform.position.z);

            // モチかウサギかの抽選を行う
            string towerObjectType = objectTypeLotteryMachine.SpawnLotteryMochiAndRabbit();

            // モチだった場合
            if (towerObjectType == TagName.Mochi)
            {
                // スポーンプールからモチのオブジェクトをスポーンさせる
                spawnedObject.Item1 = mochiSpawnPool.Spawn(mochiSkinType.ToString(), spawnPos, Quaternion.identity);
                // スポーンしたオブジェクトをモチとして登録
                spawnedObject.Item2 = TagName.Mochi;
                
                // 前回スポーンしたオブジェクトをモチとして登録する
                prevSpawnObjectType = TagName.Mochi;
            }
            // ウサギだった場合
            else
            {
                // 連続でウサギがスポーンするのは仕様ではないので、そうなった場合は抽選し直す。
                if (prevSpawnObjectType == TagName.Rabbit) { continue; }
                
                // ウサギの抽選を行う
                string rabbitId = LotteryRabbit();
                // 決定したウサギをスポーンさせる
                spawnedObject.Item1 = rabbitSpawnPool.Spawn(rabbitId, spawnPos,Quaternion.identity);
                // スポーンしたオブジェクトをウサギとして登録
                spawnedObject.Item2 = TagName.Rabbit;
                
                // 前回スポーンしたオブジェクトをウサギとして登録する
                prevSpawnObjectType = TagName.Rabbit; ;
            }

            // スポーンしたオブジェクトを返す
            yield return spawnedObject;

            // カウンター
            spawnCount++;
        }
    }

    /// <summary>
    /// ウサギの抽選を行う
    /// </summary>
    public string LotteryRabbit()
    {
        // ウサギのレアリティの抽選を行う
        string rarityId = rabbitLotteryMachine.LotteryRarity();
        // 決定したレアリティに属しているウサギで再度抽選を行う
        string rabbitId = rabbitLotteryMachine.LotterySpawnRabbit(rarityId);

        // 最終的に決定したウサギのIDを返す
        return rabbitId;
    }

    /// <summary>
    /// スポーンしたオブジェクトを消す
    /// </summary>
    /// <param name="objectType">消すオブジェクトの種類</param>
    /// <param name="instance">消すオブジェクトのインスタンス</param>
    /// <param name="seconds">削除までの遅延（秒）</param>
    public void Despawn(string objectType,Transform instance,float seconds)
    {
        // モチだった場合
        if (objectType == TagName.Mochi)
        {
            // モチを削除
            mochiSpawnPool.Despawn(instance);
        }
        // ウサギだった場合
        else if (objectType == TagName.Rabbit)
        {
            // ウサギを削除
            rabbitSpawnPool.Despawn(instance,1);
        }
        // それ以外だったらエラー
        else
        {
            Debug.LogError("ObjectType : " + objectType + " not found.");
        }
    }
}
