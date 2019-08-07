using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// タワーオブジェクトの消滅の制御を行う
/// </summary>
public class TowerObjectDespawnController : MonoBehaviour
{
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
        // プレイヤーが何らかのアクションを起こしたときのみ、以下の処理を行う
        if (!playerController.GetIsPunch()   || 
            !playerController.GetIsRescue()  ||
            !playerController.GetIsSpecialArts())
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
        if (playerController.GetIsPunch())
        {
            // パンチされたときのコールバック
            objectController.OnPlayerPunched();
        }
        // プレイヤーから救出されたとき
        else if (playerController.GetIsRescue())
        {
            // 救出されたときのコールバック
            objectController.OnPlayerPunched();
        }
        // プレイヤーから最後の大技を受けたとき
        else if (playerController.GetIsSpecialArts())
        {
            // 大技を受けたときのコールバック
            objectController.OnPlayerPunched();
        }
    }
}
