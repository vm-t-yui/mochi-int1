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

    // プレイヤーコントローラー
    [SerializeField]
    PlayerController playerController = default;

    // スポーンさせる数
    [SerializeField] int spawnNum = 0;

    // 終了フラグ
    bool isFinish = false;

    /// <summary>
    /// 起動処理
    /// </summary>
    void OnEnable()
    {
        // 終了フラグをオフにする
        isFinish = false;
    }

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
        // 現在のオブジェクトの数がスポーンさせる数を下回っていて、かつ終了していなければ
        // 新たにオブジェクトを生成する
        if (stackedObjects.Count < spawnNum　&& !isFinish)
        {
            // スポーンしたオブジェクト
            IEnumerable<(Transform, string)> spawnedObjects;

            // オブジェクトをスポーンする
            spawnedObjects = towerObjectSpawner.Spawn(spawnNum);

            // 指定の数だけオブジェクトを新たに生成する
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
            towerObjectSpawner.Despawn(underObject.Item2, underObject.Item1,0);
        }
        // プレイヤーが助けたら
        else if (playerController.GetIsRescue())
        {
            // タワーの下のオブジェクトを取得
            (Transform, string) underObject = stackedObjects.Dequeue();

            if (underObject.Item2 == TagName.Rabbit)
            {
                // アニメーターのコンポーネントを取得
                Animator rabbitAnim = underObject.Item1.GetChild(0).GetComponent<Animator>();
                // 救出アニメーションを再生する
                rabbitAnim.SetTrigger("RabbitRescue");

                
                // パンチされたオブジェクトを消す
                towerObjectSpawner.Despawn(underObject.Item2, underObject.Item1, 2);
            }
            else
            {
                // パンチされたオブジェクトを消す
                towerObjectSpawner.Despawn(underObject.Item2, underObject.Item1, 0);
            }
        }
        // プレイヤーが大技を決めたら
        else if (playerController.GetIsSpecialArts())
        {
            // タワーのオブジェクトを全て消す
            foreach((Transform, string) stackedObject in stackedObjects)
            {
                towerObjectSpawner.Despawn(stackedObject.Item2, stackedObject.Item1,0);
            }
            // 終了フラグをオンにする
            isFinish = true;
        }
    }
}
