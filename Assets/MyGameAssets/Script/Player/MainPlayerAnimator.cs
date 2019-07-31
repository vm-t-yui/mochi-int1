using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VMUnityLib;

/// <summary>
/// メインゲーム時のプレイヤーのアニメーション管理クラス
/// </summary>
public class MainPlayerAnimator : SingletonMonoBehaviour<MainPlayerAnimator>
{
    // アニメーションの種類
    public enum AnimKind
    {
        Main,                               // メイン
        RightPunch,                         // 右パンチ
        LeftPunch,                          // 左パンチ
        Rescue,                             // うさぎ救助
        SpecialArts,                        // 最後の大技
        OrangeCatch,                        // ハイスコア時のみかんキャッチ
        Lenght,                             // enumの長さ
    }

    [SerializeField]
    Animator playerAnim = default;          // アニメーター

    [SerializeField]
    SceneChanger sceneChanger = default;    // シーンチェンジャー

    /// <summary>
    /// 起動処理
    /// </summary>
    void OnEnable()
    {
        // 開始時にリザルトアニメーション開始
        playerAnim.SetTrigger("Main");
    }

    /// <summary>
    ///  アニメーション再生
    /// </summary>
    /// <param name="kind">アニメーションの種類</param>
    public void AnimStart(int kind)
    {
        // 種類に応じたアニメーション開始
        switch(kind)
        {
            case (int)AnimKind.RightPunch: playerAnim.SetTrigger("RPunch"); break;
            case (int)AnimKind.LeftPunch: playerAnim.SetTrigger("LPunch"); break;
            case (int)AnimKind.Rescue: playerAnim.SetTrigger("Rescue"); break;
            case (int)AnimKind.SpecialArts: playerAnim.SetTrigger("SpecialArts"); break;
            case (int)AnimKind.OrangeCatch:
                if (GameDataManager.Inst.PlayData.LastScore >= ScoreManager.GoodScore)
                {
                    playerAnim.SetTrigger("LowScore"); break;
                }
                {
                    playerAnim.SetTrigger("HighScore"); break;
                }
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

    /// <summary>
    /// シーンチェンジ
    /// </summary>
    /// NOTE:メインゲームシーン時のみかんキャッチ後に呼ぶアニメーションイベント用関数です。
    public void ChangeScene()
    {
        // シーン切り替え
        sceneChanger.ChangeScene();
    }
}
