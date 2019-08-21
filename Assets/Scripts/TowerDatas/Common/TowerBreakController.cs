using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// タワーが飛ばされる処理を制御
/// </summary>
public class TowerBreakController : MonoBehaviour
{
    // スポナークラス
    [SerializeField]
    TowerObjectSpawner towerObjectSpawner = default;

    // 飛ぶスピード
    [SerializeField]
    float objectFlySpeed = 0;

    // 全てのオブジェクトの飛ぶ方向
    List<Vector3> objectFlyDirections = new List<Vector3>();

    /// <summary>
    /// 開始
    /// </summary>
    void OnEnable()
    {
        // 各オブジェクトの飛ぶ方向をランダムで決定する
        for (int i = 0; i < towerObjectSpawner.StackedObjects.Count; i++)
        {
            // 飛ぶ方向をランダムに生成
            Vector3 flyDirection = new Vector3(Random.Range(-1.0f, 1.0f),
                                               Random.Range(-1.0f, 1.0f),
                                               Random.Range(-1.0f, 1.0f));

            // 決定した方向をリストに追加
            objectFlyDirections.Add(flyDirection);
        }
    }

    /// <summary>
    /// 更新
    /// </summary>
    void Update()
    {
        for (int i = 0; i < towerObjectSpawner.StackedObjects.Count; i++)
        {
            // タワーオブジェクトを取得
            Transform towerObject = towerObjectSpawner.StackedObjects[i];
            // ランダムで決まった方向にオブジェクトを飛ばす
            towerObject.Translate(objectFlyDirections[i] * objectFlySpeed);
        }
    }
    
    /// <summary>
    /// 終了
    /// </summary>
    void OnDisable()
    {
        // 方向を格納したリストを全削除
        objectFlyDirections.Clear();
    }
}