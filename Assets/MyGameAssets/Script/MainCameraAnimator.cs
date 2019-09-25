using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// メインのカメラのアニメーションクラス
/// </summary>
public class MainCameraAnimator : MonoBehaviour
{
    // アニメーションの種類
    public enum AnimKind
    {
        Wait,                                   // 待機
        FeverIn,                                // 右パンチ
        FeverOut,                               // 左パンチ
        SpecialArts,                            // 最後の大技
        CountDown,                              // 最初のカウントダウン
    }

    [SerializeField]
    Animator mainCameraAnim = default;          // カメラのアニメーター

    /// <summary>
    ///  アニメーション再生
    /// </summary>
    /// <param name="kind">アニメーションの種類</param>
    public void AnimStart(int kind)
    {
        switch(kind)
        {
            case (int)AnimKind.FeverIn: mainCameraAnim.SetTrigger("FeverIn"); break;
            case (int)AnimKind.FeverOut: mainCameraAnim.SetTrigger("FeverOut"); break;
            case (int)AnimKind.SpecialArts: mainCameraAnim.SetTrigger("SpecialArts"); break;
            case (int)AnimKind.CountDown: mainCameraAnim.SetTrigger("CountDown"); break;
        }
    }
}
