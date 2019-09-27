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

    [SerializeField]
    Timer timer = default;                      // タイマークラス

    [SerializeField]
    float SlowTimeScale = 0.1f;                 // スローモーション時のタイムスケール

    [SerializeField]
    GameObject scoreUI = default,               // スコアUI
               guageUI = default;               // ゲージUI

    bool isLowScore = false;

    bool isEnd = false;                         // 処理終了フラグ

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

    /// <summary>
    /// タイムスケール変更
    /// </summary>
    void ChangeTimeScale()
    {
        Time.timeScale = SlowTimeScale;
    }

    /// <summary>
    /// タイムスケールリセット
    /// </summary>
    void ResetTimeScale()
    {
        Time.timeScale = 1;
    }

    /// <summary>
    /// ロースコア時のアニメーション再生
    /// </summary>
    void StartLowScoreAnim()
    {
        if (ScoreManager.Inst.NowBreakNum < ScoreManager.NormalScore)
        {
            mainCameraAnim.SetTrigger("LowScore");
        }
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    void Update()
    {
        // タイムアップ時
        if (timer.IsTimeup && !isEnd)
        {
            mainCameraAnim.SetTrigger("SpecialArts");

            // ゲージとスコアを非表示にする
            scoreUI.SetActive(false);
            guageUI.SetActive(false);

            isEnd = true;
        }
    }

    /// <summary>
    /// 終了処理
    /// </summary>
    void OnDisable()
    {
        // アニメーションステートを待機に戻す
        mainCameraAnim.SetTrigger("Wait");

        // ゲージとスコアを表示させておく
        scoreUI.SetActive(true);
        guageUI.SetActive(true);

        isEnd = false;
    }
}
