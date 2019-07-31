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
        ScoreResult,        // リザルトのスコア発表のアニメーション
    }

    [SerializeField]
    Animator playerAnim = default;          // アニメーター

    /// <summary>
    /// 起動処理
    /// </summary>
    void OnEnable()
    {
        // 開始時にリザルトの待機モーションへ移動させる
        playerAnim.SetTrigger("Result");
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
                if (GameDataManager.Inst.PlayData.LastScore < ScoreManager.GoodScore)
                {
                    playerAnim.SetTrigger("LowScore");
                }
                {
                    playerAnim.SetTrigger("HighScore");
                }

                break;
        }
    }
}
