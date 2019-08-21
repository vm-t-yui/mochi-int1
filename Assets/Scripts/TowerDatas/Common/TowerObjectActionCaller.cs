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
    /// <summary>
    /// プレイヤーのアクションの種類
    /// </summary>
    enum PlayerActionType
    {
        Punch,          // パンチ
        Rescue,         // レスキュー
        SpecialArts,    // 最後の大技
    }

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
        // オブジェクトのアクションをコールする
        CallObjectAction(PlayerActionType.Punch);
    }

    /// <summary>
    /// 救出されたとき
    /// </summary>
    public void OnRescued()
    {
        // オブジェクトのアクションをコールする
        CallObjectAction(PlayerActionType.Rescue);
    }

    /// <summary>
    /// 最後の大技を受けたとき
    /// </summary>
    public void OnSpecialArtsed()
    {
        // オブジェクトのアクションをコールする
        CallObjectAction(PlayerActionType.SpecialArts);
    }

    /// <summary>
    /// オブジェクトのアクションをコールする
    /// NOTE : プレイヤー側のアクションによって、コールするアクションを分ける
    /// </summary>
    /// <param name="actionType">プレイヤー側のアクション</param>
    void CallObjectAction(PlayerActionType actionType)
    {
        // 一番下のオブジェクトを取得
        Transform bottomObject = towerObjectSpawner.StackedObjects.First();
        // 一番下のオブジェクトの制御クラスを取得
        MoveControllerBase moveController = moveControllerList.MoveControllers[bottomObject.name];

        // タワーが落下中じゃなければ、プレイヤーからのアクションを受け付ける
        if (!objectFallingController.IsFalling)
        {
            // 一番下のオブジェクトをリストから削除
            towerObjectSpawner.RemoveTowerBottomObject();

            // パンチを受けたとき
            if (actionType == PlayerActionType.Punch)
            {
                // 制御クラスのコールバックを呼ぶ
                moveController.OnPlayerPunched();
            }
            // 救助されたとき
            else if (actionType == PlayerActionType.Rescue)
            {
                // 制御クラスのコールバックを呼ぶ
                moveController.OnPlayerRescued();
            }
            // 最後の大技を受けたとき
            else
            {
                // 制御クラスのコールバックを呼ぶ
                moveController.OnPlayerSpecialArts();
            }
        }
    }
}
