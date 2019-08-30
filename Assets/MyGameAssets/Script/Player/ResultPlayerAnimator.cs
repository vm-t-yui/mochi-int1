using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VMUnityLib;

/// <summary>
/// リザルト時のプレイヤーのアニメーション管理クラス
/// </summary>
public class ResultPlayerAnimator : SingletonMonoBehaviour<ResultPlayerAnimator>
{
    // アニメーションの種類
    public enum AnimKind
    {
        Title,              // タイトル
        Main,               // メイン
        Result,             // リザルトの待機アニメーション
        ButResult,          // うさぎを３回殴ってしまった時のリザルトアニメーション
        ScoreResult,        // リザルトのスコア発表のアニメーション
    }

    [SerializeField]
    Animator playerAnim = default;                      // アニメーター

    [SerializeField]
    ResultJunction resultJunction = default;            // リザルト分岐クラス

    public bool IsEnd { get; private set; } = false;    // アニメーション終了フラグ

    /// <summary>
    /// 起動処理
    /// </summary>
    void OnEnable()
    {
        // 終了フラグリセット
        IsEnd = false;

        // 開始時にリザルト状態に応じたアニメーションを再生
        if (!resultJunction.IsJunction)
        {
            // 良い時
            playerAnim.SetTrigger("Result");
        }
        else
        {
            // 悪い時
            playerAnim.SetTrigger("ButResult");
        }
    }

    /// <summary>
    ///  アニメーション再生
    /// </summary>
    /// <param name="kind">アニメーションの種類</param>
    public void AnimStart(int kind)
    {
        // 種類に応じたアニメーション開始
        switch (kind)
        {
            case (int)AnimKind.Title: playerAnim.SetTrigger("Title"); break;
            case (int)AnimKind.Main: playerAnim.SetTrigger("Main"); break;
            case (int)AnimKind.ScoreResult:

                // スコアに応じたアニメーション
                if (ScoreManager.Inst.NowBreakNum < ScoreManager.NormalScore)
                {
                    playerAnim.SetTrigger("LowScore");
                }
                else
                {
                    playerAnim.SetTrigger("HighScore");
                }

                break;

        }
    }

    /// <summary>
    /// アニメーション終了関数
    /// </summary>
    public void AnimEnd()
    {
        IsEnd = true;
    }
}
