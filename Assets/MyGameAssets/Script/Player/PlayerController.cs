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
    int punchSide = (int)MainAnim.RightPunch;   // パンチの種類

    /// <summary>
    /// パンチ処理
    /// </summary>
    public void OnPunch()
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
    }

    /// <summary>
    /// うさぎ救助処理
    /// </summary>
    public void OnRescue()
    {
        // アニメーション開始
        MainPlayerAnimator.Inst.AnimStart((int)MainAnim.Rescue);
    }

    /// <summary>
    /// 大技処理
    /// </summary>
    public void OnSpecialArts()
    {
        // アニメーション開始
        MainPlayerAnimator.Inst.AnimStart((int)MainAnim.SpecialArts);
    }
}