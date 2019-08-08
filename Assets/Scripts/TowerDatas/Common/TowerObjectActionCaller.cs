using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーのアクションをもとにオブジェクトのアクションをコールする関数
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
    ObjectControllerList objectControllerList = default;

    // 積み上げられてたオブジェクトを制御するクラス
    [SerializeField]
    Transform stackedObjectParent = default;

    /// <summary>
    /// 更新
    /// </summary>
    void Update()
    {
#if UNITY_EDITOR
        // デバッグ用のプレイヤーアクション
        // A : パンチ
        // S : 救出

        bool punch = false;
        bool rescue = false;

        punch = Input.GetKeyDown(KeyCode.A);
        rescue = Input.GetKeyDown(KeyCode.S);

        if (punch || rescue)
        {
            // タワーの一番下のオブジェクトを取得
            Transform obj = stackedObjectParent.GetChild(0);
            // オブジェクトの親を元に戻す
            towerObjectSpawner.UndoParent(obj);
            // 取得したオブジェクトの名前から制御クラスを取得
            ObjectControllerBase objCtrl = objectControllerList.ObjectControllers[obj.gameObject.name];

            if (punch)
            {
                // パンチされたときのコールバック
                objCtrl.OnPlayerPunched();
            }
            else if (rescue)
            {
                // 救出されたときのコールバック
                objCtrl.OnPlayerRescued();
            }
        }
#endif

        // プレイヤーのそれぞれのアクションのフラグを取得する
        bool isPaunched = playerController.GetIsPunch();
        bool isRescued = playerController.GetIsRescue();

        // プレイヤーが何らかのアクションを起こしたときのみ、以下の処理を行う
        if (!isPaunched && !isRescued)
        {
            return;
        }

        // タワーの一番下のオブジェクトを取得
        Transform underObject = stackedObjectParent.GetChild(0);
        // オブジェクトの親を元に戻す
        towerObjectSpawner.UndoParent(underObject);
        // 取得したオブジェクトの名前から制御クラスを取得
        ObjectControllerBase objectController = objectControllerList.ObjectControllers[underObject.gameObject.name];

        // プレイヤーからパンチされたとき
        if (punch)
        {
            // パンチされたときのコールバック
            objectController.OnPlayerPunched();
        }
        // プレイヤーから救出されたとき
        else if (rescue)
        {
            // 救出されたときのコールバック
            objectController.OnPlayerRescued();
        }
    }
}
