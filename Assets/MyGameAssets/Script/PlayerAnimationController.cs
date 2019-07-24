using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーのアニメーションクラス
/// </summary>
public class PlayerAnimationController : MonoBehaviour
{
    // アニメーションの種類
    public enum AnimKind
    {
        Right,      // 右パンチ
        Left,       // 左パンチ
        Rescue,     // うさぎ救助
        SpecialArts,// 最後の大技
        Lenght,     // enumの長さ
    }

    [SerializeField]
    Animator playerAnim = default;          // アニメーター

    [SerializeField]
    PlayerController player = default;      // プレイヤークラス

    /// <summary>
    ///  アニメーション再生
    /// </summary>
    /// <param name="kind">アニメーションの種類</param>
    public void AnimStart(int kind)
    {
        // アニメーションの種類が右パンチなら右パンチのアニメーションスタート
        if (kind == (int)AnimKind.Right)
        {
            playerAnim.SetBool("RPunch", true);
        }
        // 左パンチなら左パンチのアニメーションスタート
        if (kind == (int)AnimKind.Left)
        {
            playerAnim.SetBool("LPunch", true);
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
        playerAnim.SetBool("RPunch", false);
        playerAnim.SetBool("LPunch", false);
    }
}
