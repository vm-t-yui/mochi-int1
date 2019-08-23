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

    // フィーバータイムコントローラー
    [SerializeField]
    FeverTimeController feverTimeController = default;

    /// <summary>
    /// 更新
    /// </summary>
    void Update()
    {
        // 現在のオブジェクトの数がスポーンさせる数を下回っていて、かつ終了していなければ
        // 新たにオブジェクトを生成する
        if (towerObjectSpawner.StackedObjects.Count < spawnNum)
        {
            // フィーバータイムはモチのみをスポーンする
            if (feverTimeController.IsFever)
            {
                // オブジェクトをスポーンする
                towerObjectSpawner.SpawnMochiOnly(spawnNum);
            }
            // 通常通り、抽選結果をもとにスポーンする
            else
            {
                // オブジェクトをスポーンする
                towerObjectSpawner.Spawn(spawnNum);
            }
        }
    }
}
