using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// プレイヤーのUnityEventのデバッグ用クラス
/// NOTE : オブジェクトのアクション関数をコールするためのデバッグ用プログラムです
///        エディター上でのみ機能します。
/// </summary>
public class PlayerActionEventTest : MonoBehaviour
{
    // パンチ用イベント
    [SerializeField]
    UnityEvent punched = default;

    // 救出用イベント
    [SerializeField]
    UnityEvent rescued = default;

    // 大技用イベント
    [SerializeField]
    UnityEvent specialArts = default;

    /// <summary>
    /// 更新
    /// </summary>
    void Update()
    {
        // パンチ
        if (Input.GetKeyDown(KeyCode.A))
        {
            punched.Invoke();
        }
        // 救出
        else if (Input.GetKeyDown(KeyCode.S))
        {
            rescued.Invoke();
        }
        // 最後の切り札
        else if (Input.GetKeyDown(KeyCode.D))
        {
            specialArts.Invoke();
        }
    }
}
