using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// メインゲーム時のプレイヤーのアニメーション管理クラス
/// </summary>
public class MainPlayerAnimator : MonoBehaviour
{
    // アニメーションの種類
    public enum AnimKind
    {
        RightPunch,     // 右パンチ
        LeftPunch,      // 左パンチ
        Rescue,         // うさぎ救助
        SpecialArts,    // 最後の大技
        Lenght,         // enumの長さ
    }

    [SerializeField]
    Animator playerAnim = default;          // アニメーター

    /// <summary>
    ///  アニメーション再生
    /// </summary>
    /// <param name="kind">アニメーションの種類</param>
    public void AnimStart(int kind)
    {
        // アニメーションの種類が右パンチなら右パンチのアニメーションスタート
        if (kind == (int)AnimKind.RightPunch)
        {
            playerAnim.SetTrigger("RPunch");
        }
        // 左パンチなら左パンチのアニメーションスタート
        if (kind == (int)AnimKind.LeftPunch)
        {
            playerAnim.SetTrigger("LPunch");
        }
        // うさぎ救助ならうさぎ救助のアニメーションスタート
        if (kind == (int)AnimKind.Rescue)
        {
            // パンチを中断して救出へ
            PunchEnd();
            playerAnim.SetTrigger("Rescue");
        }
        // 最後の大技なら最後の大技のアニメーションスタート
        if (kind == (int)AnimKind.SpecialArts)
        {
            // パンチを中断して救出へ
            PunchEnd();
            playerAnim.SetTrigger("SpecialArts");
        }
    }

    /// <summary>
    /// パンチ終了
    /// </summary>
    public void PunchEnd()
    {
        playerAnim.ResetTrigger("RPunch");
        playerAnim.ResetTrigger("LPunch");
    }
}
