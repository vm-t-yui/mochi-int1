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

    public bool isPunch = false;                                    // パンチフラグ
    public bool isRescue = false;                                   // 救助フラグ
    public bool isSpecialArts = false;                              // 大技フラグ
    public bool IsWait { get; private set; } = true;                // 待機中フラグ

    bool isEnd = false;                                             // 処理終了フラグ

    int punchSide = (int)MainAnim.RightPunch;                       // パンチの種類

    /// <summary>
    /// 起動処理
    /// </summary>
    void OnEnable()
    {
        // フラグリセット
        isPunch = false;
        isRescue = false;
        isSpecialArts = false;
        IsWait = true;
        isEnd = false;
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    void Update()
    {
        // タイムアップになったら
        if (Timer.Inst.IsTimeup && !isEnd)
        {
            // 大技開始
            SpecialArts();
            isEnd = true;
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
        MainPlayerAnimator.Inst.AnimStart(punchSide);

        isPunch = true;
    }

    /// <summary>
    /// うさぎ救助
    /// </summary>
    void Rescue()
    {
        // アニメーション開始
        MainPlayerAnimator.Inst.AnimStart((int)MainAnim.Rescue);

        isRescue = true;
    }

    /// <summary>
    /// 最後の大技
    /// </summary>
    void SpecialArts()
    {
        // アニメーション開始
        MainPlayerAnimator.Inst.AnimStart((int)MainAnim.SpecialArts);

        isSpecialArts = true;
    }

    /// <summary>
    /// 待機状態を解除(アニメーションイベント用)
    /// </summary>
    void NotWait()
    {
        IsWait = false;
    }

    /// <summary>
    /// 待機状態に入る(アニメーションイベント用)
    /// </summary>
    void Wait()
    {
        IsWait = true;
        touch.ResetPermission();
    }

    /// <summary>
    /// パンチフラグのゲット＋リセット関数
    /// </summary>
    /// <returns>パンチフラグの値</returns>
    public bool GetIsPunch()
    {
        bool returnflg = isPunch;
        isPunch = false;

        return returnflg;
    }

    /// <summary>
    /// 救助フラグのゲット＋リセット関数
    /// </summary>
    /// <returns>救助フラグの値</returns>
    public bool GetIsRescue()
    {
        bool returnflg = isRescue;
        isRescue = false;

        return returnflg;
    }

    /// <summary>
    /// 大技フラグのゲット＋リセット関数
    /// </summary>
    /// <returns>大技フラグの値</returns>
    public bool GetIsSpecialArts()
    {
        bool returnflg = isSpecialArts;
        isSpecialArts = false;

        return returnflg;
    }
}
