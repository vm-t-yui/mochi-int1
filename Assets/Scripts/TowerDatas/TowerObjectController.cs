using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// タワーオブジェクト全体の制御を行う
/// </summary>
/// NOTE : テスト用のコードも入っており汚いです。今後綺麗にしていきます。
public class TowerObjectController : MonoBehaviour
{
    // オブジェクトのスポナークラス
    [SerializeField]
    TowerObjectSpawner towerObjectSpawner = default;

    // 積みあがったオブジェクト
    Queue<(Transform, string)> stackedObjects = new Queue<(Transform, string)>();

    [SerializeField]
    PlayerController playerController = default;

    // スポーンさせる数
    [SerializeField] int spawnNum = 0;

    // プレイヤーのアクションを表すテキスト
    // NOTE : 実機でもプレイヤーがどのアクションを行ったかが分かるようにするためのテキストです。
    //        アクションごとのイベントが完成したら消します。
    [SerializeField] Text testActionText = default;

    /// <summary>
    /// 更新
    /// </summary>
    void Update()
    {
        // スポーンの制御を行う
        ControlSpawn();

        // ディスポーンの処理を行う
        ControlDespawn();
    }

    /// <summary>
    /// スポーンの制御を行う
    /// </summary>
    void ControlSpawn()
    {
        // 現在のオブジェクトの数がスポーンさせる数を下回ったら、新たにスポーンさせる
        if (stackedObjects.Count < spawnNum)
        {
            // スポーンしたオブジェクト
            IEnumerable<(Transform, string)> spawnedObjects;

            // オブジェクトをスポーンする
            spawnedObjects = towerObjectSpawner.Spawn(spawnNum);

            foreach ((Transform, string) spawnedObject in spawnedObjects)
            {
                stackedObjects.Enqueue(spawnedObject);
            }
        }
    }

    /// <summary>
    /// ディスポーンの処理を行う
    /// </summary>
    void ControlDespawn()
    {
        // プレイヤーがパンチしたら
        if (playerController.GetIsPunch())
        {
            // タワーの下のオブジェクトを取得
            (Transform, string) underObject = stackedObjects.Dequeue();
            // パンチされたオブジェクトを消す
            towerObjectSpawner.Despawn(underObject.Item2, underObject.Item1);

            // モチにパンチしたら
            if (underObject.Item2 == TagName.Mochi)
            {
                // テスト用テキスト表示
                testActionText.text = "モチにパンチ！";
            }
            // ウサギにパンチしたら
            else
            {
                // テスト用テキスト表示
                testActionText.text = "ウサギにパンチ！";
            }
        }
        else if (playerController.GetIsRescue())
        {
            // タワーの下のオブジェクトを取得
            (Transform, string) underObject = stackedObjects.Dequeue();
            // パンチされたオブジェクトを消す
            towerObjectSpawner.Despawn(underObject.Item2, underObject.Item1);

            // モチにパンチしたら
            if (underObject.Item2 == TagName.Mochi)
            {
                // テスト用テキスト表示
                testActionText.text = "モチを助けた？";
            }
            // ウサギにパンチしたら
            else
            {
                // テスト用テキスト表示
                testActionText.text = "ウサギを助けた！";
            }
        }
    }
}
