using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VMUnityLib;

/// <summary>
/// オブジェクトのすべての制御クラスを管理するクラス
/// </summary>
public class MoveControllerList : MonoBehaviour
{
    // モチのスポーンポール
    [SerializeField]
    Transform mochiSpawnPool = default;

    // ウサギのスポーンプール
    [SerializeField]
    Transform rabbitSpawnPool = default;

    // オブジェクトの制御クラスを管理するDictionary
    Dictionary<string, MoveControllerBase> moveControllers = new Dictionary<string, MoveControllerBase>();
    public IReadOnlyDictionary<string, MoveControllerBase> MoveControllers { get { return moveControllers; }}

    /// <summary>
    /// 開始
    /// </summary>
    void Start()
    {
        // モチの全ての制御クラスを取得
        foreach (Transform mochi in mochiSpawnPool)
        {
            // モチにアタッチしている制御クラスを取得する
            MoveControllerBase mochiMoveController = mochi.GetComponent<MoveControllerBase>();
            // 取得した制御クラスをオブジェクト名とセットで追加
            moveControllers.Add(mochi.gameObject.name, mochiMoveController);
        }

        // ウサギの全ての制御クラスを取得
        foreach (Transform rabbit in rabbitSpawnPool)
        {
            // ウサギにアタッチしている制御クラスを取得する
            MoveControllerBase rabbitMoveController = rabbit.GetComponent<MoveControllerBase>();
            // 取得した制御クラスをオブジェクト名とセットで追加
            moveControllers.Add(rabbit.gameObject.name, rabbitMoveController);
        }
    }
}
