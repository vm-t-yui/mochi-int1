using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤークラス
/// </summary>
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    TouchController touch = default;                                // タッチクラス

    [SerializeField]
    Timer timer = default;                                          // タイマークラス

    [SerializeField]
    PlayerAnimationController playerAnim = default;                 // プレイヤーのアニメーションクラス

    public bool isPunch { get; private set; } = false;              // パンチフラグ
    public bool isRescue { get; private set; } = false;             // 救助フラグ
    public bool isSpecialArts { get; private set; } = false;        // 救助フラグ

    int punchSide = (int)PlayerAnimationController.AnimKind.Right;  // パンチの種類

    /// <summary>
    /// 更新処理
    /// </summary>
    void Update()
    {
        // タイムアップになったら
        if(timer.isTimeup)
        {
            // 大技開始
            SpecialArts();
        }
        // まだ時間が余っていたら
        else
        {
            // タッチされたら
            if (touch.GetIsTouch())
            {
                // パンチ開始
                Punch();
            }
            // スワイプされたら
            if (touch.GetIsSwipe())
            {
                // うさぎ救助開始
                Rescue();
            }
        }
    }

    /// <summary>
    /// パンチ
    /// </summary>
    void Punch()
    {
        // 右なら左、左なら右のパンチに変更
        if (punchSide == (int)PlayerAnimationController.AnimKind.Right)
        {
            punchSide = (int)PlayerAnimationController.AnimKind.Left;
        }
        else
        {
            punchSide = (int)PlayerAnimationController.AnimKind.Right;
        }

        // アニメーション開始
        playerAnim.AnimStart(punchSide);
    }

    /// <summary>
    /// うさぎ救助
    /// </summary>
    void Rescue()
    {
        // アニメーション開始
        playerAnim.AnimStart((int)PlayerAnimationController.AnimKind.Rescue);
    }

    /// <summary>
    /// 最後の大技
    /// </summary>
    void SpecialArts()
    {
        // アニメーション開始
        playerAnim.AnimStart((int)PlayerAnimationController.AnimKind.SpecialArts);
    }

    /// <summary>
    /// アニメーションイベント用パンチフラグセット関数
    /// </summary>
    public void PunchStart()
    {
        isPunch = true;
    }

    /// <summary>
    /// アニメーションイベント用救助フラグセット関数
    /// </summary>
    public void RescueStart()
    {
        isRescue = true;
    }

    /// <summary>
    /// アニメーションイベント用最後の大技フラグセット関数
    /// </summary>
    public void SpecialArtsStart()
    {
        isSpecialArts = true;
    }
}
