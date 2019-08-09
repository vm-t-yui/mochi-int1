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

    // 時間カウント
    int flyTimeCount = 0;

    Vector3 initPos = new Vector3(0, -5, 0);

    /// <summary>
    /// 開始
    /// </summary>
    void OnEnable()
    {
        // カウントを初期化
        flyTimeCount = 0;
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
        flyTimeCount = 0;
        stackedObjectParent.localPosition = initPos;
        enabled = false;
        Debug.Log("aaaaaaaaaaaaaaaaaaa");
    }
}
