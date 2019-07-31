using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// タッチクラス
/// </summary>
public class TouchController : MonoBehaviour
{
    [SerializeField]
    PlayerController player = default;  // プレイヤークラス

    bool isTouch = false;               // タッチフラグ
    bool isSwipe = false;               // スワイプフラグ
    bool isTouchPermission = true;      // タッチの許可フラグ
    bool isSwipePermission = true;      // スワイプの許可フラグ

    /// <summary>
    /// 更新処理
    /// </summary>
    void Update()
    {
        // ゲームがスタートしていて、プレイヤーが待機中なら入力を受け付ける
        if (Input.touchCount > 0 && player.IsWait && Timer.Inst.IsStart)
        {
            Touch touch = Input.GetTouch(0);

            // タッチ(タッチの許可がされてあればタッチを取得)
            if (touch.phase == TouchPhase.Began && isTouchPermission)
            {
				isTouch = true;
                isTouchPermission = false;
            }
            // スワイプ(スワイプの許可がされてあればタッチを取得)
            if (touch.deltaPosition.magnitude > 1.5f && isSwipePermission)
            {
				isSwipe = true;
                isSwipePermission = false;
            }
        }
    }

    /// <summary>
    /// フラグを再許可
    /// </summary>
    public void ResetPermission()
    {
        isTouchPermission = true;
        isSwipePermission = true;
    }

    /// <summary>
    /// タッチフラグのゲット＋リセット関数
    /// </summary>
    /// <returns>タッチフラグの値</returns>
    public bool GetIsTouch()
    {
        bool returnflg = isTouch;
        isTouch = false;

        return returnflg;
    }

    /// <summary>
    /// スワイプフラグのゲット＋リセット関数
    /// </summary>
    /// <returns>スワイプフラグの値</returns>
    public bool GetIsSwipe()
    {
        bool returnflg = isSwipe;
        isSwipe = false;

        return returnflg;
    }
}
