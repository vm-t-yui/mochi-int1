using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// タワーオブジェクトのスポーンの制御を行う
/// </summary>
public class TowerObjectSpawnController : MonoBehaviour
{
    // オブジェクトのスポナークラス
    [SerializeField]
    TowerObjectSpawner towerObjectSpawner = default;

    // スポーンさせる数
    [SerializeField] int spawnNum = 0;

    /// <summary>
    /// 更新
    /// </summary>
    void Update()
    {
        // 現在のオブジェクトの数がスポーンさせる数を下回っていて、かつ終了していなければ
        // 新たにオブジェクトを生成する
        if (towerObjectSpawner.StackedObjects.Count < spawnNum)
        {
            // オブジェクトをスポーンする
            towerObjectSpawner.Spawn(spawnNum);
        }
    }
}
