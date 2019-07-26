using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// メインゲーム時のアニメーションクラスのenum
using MainAnim = MainPlayerAnimator.AnimKind;

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
    MainPlayerAnimator playerAnim = default;                        // プレイヤーのアニメーションクラス

    public bool IsPunch { get; private set; } = false;              // パンチフラグ
    public bool IsRescue { get; private set; } = false;             // 救助フラグ
    public bool IsSpecialArts { get; private set; } = false;        // 救助フラグ

    int punchSide = (int)MainAnim.RightPunch;    // パンチの種類

    /// <summary>
    /// 更新処理
    /// </summary>
    void Update()
    {
        // タイムアップになったら
        if(timer.IsTimeup)
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
        if (punchSide == (int)MainAnim.RightPunch)
        {
            punchSide = (int)MainAnim.LeftPunch;
        }
        else
        {
            punchSide = (int)MainAnim.RightPunch;
        }

        // アニメーション開始
        playerAnim.AnimStart(punchSide);
        IsPunch = true;
    }

    /// <summary>
    /// うさぎ救助
    /// </summary>
    void Rescue()
    {
        // アニメーション開始
        playerAnim.AnimStart((int)MainAnim.Rescue);

        IsRescue = true;
    }

    /// <summary>
    /// 最後の大技
    /// </summary>
    void SpecialArts()
    {
        // アニメーション開始
        playerAnim.AnimStart((int)MainAnim.SpecialArts);

        IsSpecialArts = true;
    }

    /// <summary>
    /// 待機中かどうか
    /// </summary>
    /// <returns></returns>
    public bool IsWait()
    {
        // パンチも救助も大技をしていなかったら待機中
        if(!IsPunch && !IsRescue && !IsSpecialArts)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
