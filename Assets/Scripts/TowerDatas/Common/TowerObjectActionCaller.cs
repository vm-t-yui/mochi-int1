using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathologicalGames;
using System.Linq;

/// <summary>
/// プレイヤーのアクションをもとにオブジェクトのアクションをコールするクラス
/// </summary>
public class TowerObjectActionCaller : MonoBehaviour
{
    // プレイヤーの制御クラス
    [SerializeField]
    PlayerController playerController = default;

    // オブジェクトのスポナークラス
    [SerializeField]
    TowerObjectSpawner towerObjectSpawner = default;

    // オブジェクトの制御クラスを管理するクラス
    [SerializeField]
    MoveControllerList moveControllerList = default;

    // オブジェクトの落下制御クラス
    [SerializeField]
    ObjectFallingController objectFallingController = default;

    // タイマークラス
    [SerializeField]
    Timer timer = default;

    /// <summary>
    /// パンチされたとき
    /// </summary>
    public void OnPunched()
    {
        // 一番下のオブジェクトを取得
        Transform bottomObject = towerObjectSpawner.StackedObjects.First();
        // 一番下のオブジェクトの制御クラスを取得
        MoveControllerBase moveController = moveControllerList.MoveControllers[bottomObject.name];

        if (!objectFallingController.IsFalling)
        {
            // 一番下のオブジェクトをリストから削除
            towerObjectSpawner.RemoveTowerBottomObject();
            // 制御クラスのコールバックを呼ぶ
            moveController.OnPlayerPunched();
        }
    }

    /// <summary>
    /// 救出されたとき
    /// </summary>
    public void OnRescued()
    {
        // 一番下のオブジェクトを取得
        Transform bottomObject = towerObjectSpawner.StackedObjects.First();
        // 一番下のオブジェクトの制御クラスを取得
        MoveControllerBase moveController = moveControllerList.MoveControllers[bottomObject.name];

        if (!objectFallingController.IsFalling)
        {
            // 一番下のオブジェクトをリストから削除
            towerObjectSpawner.RemoveTowerBottomObject();
            // 制御クラスのコールバックを呼ぶ
            moveController.OnPlayerRescued();
        }
    }

    /// <summary>
    /// 最後の大技を受けたとき
    /// </summary>
    public void OnSpecialArtsed()
    {
        // 一番下のオブジェクトを取得
        Transform bottomObject = towerObjectSpawner.StackedObjects.First();
        // 一番下のオブジェクトの制御クラスを取得
        MoveControllerBase moveController = moveControllerList.MoveControllers[bottomObject.name];

        if (!objectFallingController.IsFalling)
        {
            // 一番下のオブジェクトをリストから削除
            towerObjectSpawner.RemoveTowerBottomObject();
            // 制御クラスのコールバックを呼ぶ
            moveController.OnPlayerSpecialArts();
        }
    }
}
