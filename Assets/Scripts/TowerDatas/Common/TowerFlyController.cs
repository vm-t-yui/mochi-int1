using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// タワーが飛ばされる処理を制御
/// </summary>
public class TowerFlyController : MonoBehaviour
{
    // 積まれているオブジェクトの親
    [SerializeField]
    Transform stackedObjectParent = default;
    // 飛ぶスピード
    [SerializeField]
    float flySpeed = 0;
    // 飛ぶ時間
    [SerializeField]
    int flyTime = 0;

    // 初期位置
    Vector3 initPos = Vector3.zero;

    /// <summary>
    /// 開始処理
    /// </summary>
    void Start()
    {
        // 初期位置を設定
        initPos = stackedObjectParent.transform.position;
    }

    /// <summary>
    /// 更新
    /// </summary>
    void Update()
    {
        // タワーを上に飛ばす
        stackedObjectParent.Translate(0, flySpeed, 0);
    }

    /// <summary>
    /// 停止処理
    /// </summary>
    void OnDisable()
    {
        // ポジションをリセットして非アクティブにする
        stackedObjectParent.localPosition = initPos;

        // NOTE: このスクリプトを持っている親オブジェクト自体が非アクティブの状態になっても、
        //       このスクリプト自体が非アクティブになるわけではないのでenabled = false;をつけました。
        enabled = false;
    }
}