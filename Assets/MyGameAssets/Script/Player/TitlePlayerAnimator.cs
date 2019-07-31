using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VMUnityLib;

/// <summary>
/// タイトル時のプレイヤーのアニメーション管理クラス
/// </summary>
public class TitlePlayerAnimator : SingletonMonoBehaviour<TitlePlayerAnimator>
{
    // アニメーションの種類
    public enum AnimKind
    {
        Title,                       // メインアニメーション
    }

    [SerializeField]
    Animator playerAnim = default;   // アニメーター

    /// <summary>
    /// 起動処理
    /// </summary>
    void OnEnable()
    {
        // 開始時にタイトルの待機モーションへ移動させる
        playerAnim.SetTrigger("Title");
    }
}
