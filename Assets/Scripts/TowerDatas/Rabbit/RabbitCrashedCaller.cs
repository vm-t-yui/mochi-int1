using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// ウサギが耐えられなくなったときの関数をコールするクラス
/// </summary>
public class RabbitCrashedCaller : MonoBehaviour
{
    // ウサギの耐久ゲージクラス
    [SerializeField]
    RabbitEndureGauge rabbitEndureGauge = default;

    [SerializeField]
    TowerObjectSpawner towerObjectSpawner = default;

    [SerializeField]
    MoveControllerList moveControllerList = default;

    /// <summary>
    /// 更新
    /// </summary>
    void Update()
    {
        if (rabbitEndureGauge.GetIsCrushed())
        {
            // 一番下のオブジェクトを取得
            Transform bottomObject = towerObjectSpawner.StackedObjects.First();
            // 取得したオブジェクトがウサギだったら
            if (bottomObject.tag == TagName.Rabbit)
            {
                // 一番下のオブジェクトをリストから削除
                towerObjectSpawner.RemoveTowerBottomObject();
                // オブジェクトの制御クラスを取得
                MoveControllerBase moveController = moveControllerList.MoveControllers[bottomObject.name];
                // コールバック関数を呼ぶ
                moveController.OnCrashed();
            }
        }
    }
}
