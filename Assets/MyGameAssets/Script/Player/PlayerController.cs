using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// メインゲーム時のアニメーションクラスのenum
using MainAnim = MainPlayerAnimator.AnimKind;

/// <summary>
/// プレイヤークラス
/// </summary>
public class PlayerController : PlayerActionBase
{
    int punchSide = (int)MainAnim.RightPunch;   // パンチの種類

    /// <summary>
    /// パンチ処理
    /// </summary>
    public override void OnPunch()
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

        throw new System.NotImplementedException();
    }

    /// <summary>
    /// うさぎ救助処理
    /// </summary>
    public override void OnRescue()
    {
        // アニメーション開始
        MainPlayerAnimator.Inst.AnimStart((int)MainAnim.Rescue);

        throw new System.NotImplementedException();
    }

    /// <summary>
    /// 大技処理
    /// </summary>
    public override void OnSpecialArts()
    {
        // アニメーション開始
        MainPlayerAnimator.Inst.AnimStart((int)MainAnim.SpecialArts);

        throw new System.NotImplementedException();
    }
}