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
    [SerializeField]
    int spawnNum = 0;

    // オブジェクトの最低個数
    [SerializeField]
    int objectKeepNum = 0;

    // フィーバータイムコントローラー
    [SerializeField]
    FeverTimeController feverTimeController = default;

    /// <summary>
    /// 更新
    /// </summary>
    void Update()
    {
        // 現在のオブジェクトの数が最低個数を下回っていれば、新たにオブジェクトを生成する
        if (towerObjectSpawner.StackedObjects.Count < objectKeepNum)
        {
            // フィーバータイムはモチのみをスポーンする
            if (feverTimeController.IsFever)
            {
                // オブジェクトをスポーンする
                towerObjectSpawner.SpawnMochiOnly(spawnNum);
            }
            // それ以外は通常通り、抽選結果をもとにスポーンする
            else
            {
                // オブジェクトをスポーンする
                towerObjectSpawner.Spawn(spawnNum);
            }

            // リワード広告の視聴フラグがオンであれば、新しいウサギもスポーンさせる
            if (GameDataManager.Inst.PlayData.IsReward)
            {
                // 救出されていないウサギを一体だけスポーンさせる
                towerObjectSpawner.SpawnNotReleasedRabbit(1);
                // フラグをオフにする
                GameDataManager.Inst.PlayData.IsReward = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            GameObject.Find("TowerObject").GetComponent<TowerBreakController>().ReplaceRabbitObject(true);
        }
    }
}
