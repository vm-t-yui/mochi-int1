using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VMUnityLib;

/// <summary>
/// オブジェクトのすべての制御クラスを管理する
/// </summary>
public class ObjectControllerList : MonoBehaviour
{
    // モチのスポーンポール
    [SerializeField]
    Transform mochiSpawnPool = default;

    // ウサギのスポーンプール
    [SerializeField]
    Transform rabbitSpawnPool = default;

    // オブジェクトの制御クラスを入れたDictionary
    Dictionary<string, ObjectControllerBase> objectControllers = new Dictionary<string, ObjectControllerBase>();
    public IReadOnlyDictionary<string, ObjectControllerBase> ObjectControllers { get { return objectControllers; }}

    /// <summary>
    /// 開始
    /// </summary>
    void Start()
    {
        // モチの全ての制御クラスを取得
        foreach (Transform mochi in mochiSpawnPool)
        {
            // モチにアタッチしている制御クラスを取得する
            ObjectControllerBase mochiController = mochi.GetComponent<ObjectControllerBase>();
            // 取得した制御クラスをオブジェクト名とセットで追加
            objectControllers.Add(mochi.gameObject.name,mochiController);
        }

        // ウサギの全ての制御クラスを取得
        foreach (Transform rabbit in rabbitSpawnPool)
        {
            // ウサギにアタッチしている制御クラスを取得する
            ObjectControllerBase rabbitController = rabbit.GetComponent<ObjectControllerBase>();
            // 取得した制御クラスをオブジェクト名とセットで追加
            objectControllers.Add(rabbit.gameObject.name,rabbitController);
        }
    }
}
