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

    // 積み上げられてたオブジェクトを制御するクラス
    [SerializeField]
    Transform stackedObjectParent = default;

    // モチのスポーンプール
    [SerializeField]
    SpawnPool mochiSpawnPool = default;

    // ウサギのスポーンプール
    [SerializeField]
    SpawnPool rabbitSpawnPool = default;

    // オブジェクトの基準の位置
    [SerializeField]
    Vector3 baseSpawnPosition = Vector3.zero;

    // スポーン時のオブジェクト間の高さの間隔
    [SerializeField]
    float spawnHeightInterval = 0;
    public float SpawnHeightInterval { get { return spawnHeightInterval; } }

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

        // スポーンの基準位置から最初のオブジェクトのスポーン位置を決定する（スポーン高さ = 地面の高さ * 1.5f）
        transform.position = new Vector3(baseSpawnPosition.x,
                                         baseSpawnPosition.y + spawnHeightInterval * 1.5f,
                                         baseSpawnPosition.z);
    }

    /// <summary>
    /// オブジェクトのスポーンを行う
    /// </summary>
    /// <param name="spawnNum">スポーンする数</param>
    public void Spawn(int spawnNum)
    {
        // スポーンしたオブジェクト
        Transform spawnedObject;

        // 基準のスポーン位置を取得
        Vector3 baseSpawnPos = GetBaseSpawnPos();

        // 指定の数だけ繰り返し抽選を行い、スポーンしていく
        for (int spawnCount = 0; spawnCount < spawnNum;)
        {
            // スポーン位置の計算
            Vector3 spawnPos = new Vector3(baseSpawnPos.x, baseSpawnPos.y + spawnHeightInterval * spawnCount, baseSpawnPos.z);

            // モチかウサギかの抽選を行う
            string towerObjectType = objectTypeLotteryMachine.SpawnLotteryMochiAndRabbit();

            // モチだった場合
            if (towerObjectType == TagName.Mochi)
            {
                // スポーンプールからモチのオブジェクトをスポーンさせる
                spawnedObject = mochiSpawnPool.Spawn(mochiSkinType.ToString(), spawnPos, Quaternion.identity,stackedObjectParent);
                // 生成したオブジェクトをオンにする
                // NOTE : 何故か自動でオンになってくれないときがあるため
                spawnedObject.gameObject.SetActive(true);

                // 前回スポーンしたオブジェクトをモチとして登録する
                prevSpawnObjectType = TagName.Mochi;
            }
            // ウサギだった場合
            else
            {
                // 連続でウサギがスポーンするのは仕様ではないので、そうなった場合は抽選し直す。
                if (prevSpawnObjectType == TagName.Rabbit) { continue; }
                
                // ウサギの抽選を行う
                string rabbitId = rabbitLotteryMachine.LotteryRabbit();
                // 決定したウサギをスポーンさせる
                spawnedObject = rabbitSpawnPool.Spawn(rabbitId, spawnPos,Quaternion.identity,stackedObjectParent);
                // 生成したオブジェクトをオンにする
                // NOTE : 何故か自動でオンになってくれないときがあるため
                spawnedObject.gameObject.SetActive(true);

                // 前回スポーンしたオブジェクトをウサギとして登録する
                prevSpawnObjectType = TagName.Rabbit;
            }

            // カウンター
            spawnCount++;
        }
    }

    /// <summary>
    /// 指定したオブジェクトを消す
    /// </summary>
    public void Despawn(Transform spawnedObject)
    {
        // モチだった場合
        if (spawnedObject.tag == TagName.Mochi)
        {
            // モチを削除
            mochiSpawnPool.Despawn(spawnedObject);
        }
        // ウサギだった場合
        else if (spawnedObject.tag == TagName.Rabbit)
        {
            // ウサギを削除
            rabbitSpawnPool.Despawn(spawnedObject);
        }
        // それ以外だったらエラー
        else
        {
            Debug.LogError("ObjectType : " + spawnedObject.tag + " not found.");
        }
    }

    /// <summary>
    /// 基準のスポーン位置を取得
    /// </summary>
    /// <returns></returns>
    Vector3 GetBaseSpawnPos()
    {
        // タワーのオブジェクトが存在していなければ、最初のオブジェクトのスポーン位置を返す
        if (stackedObjectParent.childCount <= 0)
        {
            return transform.position;
        }

        // タワーの一番上のオブジェクトを取得
        Transform topObject = stackedObjectParent.GetChild(stackedObjectParent.childCount - 1);
        // 一番上のオブジェクトのひとつ上の位置を基準のスポーン位置にする
        return new Vector3(topObject.position.x, topObject.position.y + spawnHeightInterval, topObject.position.z);
    }

    /// <summary>
    /// 生成されたオブジェクトの親を元に戻す
    /// </summary>
    /// <param name="spawnedObject">元に戻すオブジェクト</param>
    public void UndoParent(Transform spawnedObject)
    {
        // モチだった場合
        if (spawnedObject.tag == TagName.Mochi)
        {
            spawnedObject.SetParent(mochiSpawnPool.transform);
        }
        // ウサギだった場合
        else if (spawnedObject.tag == TagName.Rabbit)
        {
            spawnedObject.SetParent(rabbitSpawnPool.transform);
        }
        // それ以外だったらエラー
        else
        {
            Debug.LogError("ObjectType : " + spawnedObject.tag + " not found.");
        }
    }
}
