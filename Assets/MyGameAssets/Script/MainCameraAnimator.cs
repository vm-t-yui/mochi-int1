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
    GameObject mainCanvasObj  = default,        // メインカンバス
               guageCanvasObj = default;        // ゲージのカンバス

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
    /// 
    /// </summary>
    void Update()
    {
        // タイムアップ時
        if (timer.IsTimeup && !isEnd)
        {
            // スコアによってカメラアニメーションを分岐
            if (ScoreManager.Inst.NowBreakNum < ScoreManager.NormalScore)
            {
                mainCameraAnim.SetTrigger("LowScore");
            }
            else
            {
                mainCameraAnim.SetTrigger("SpecialArts");
            }

            // カンバスを非表示にする
            mainCanvasObj.GetComponent<Canvas>().enabled = false;
            guageCanvasObj.GetComponent<Canvas>().enabled = false;

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

        // カンバスを表示させておく
        mainCanvasObj.GetComponent<Canvas>().enabled = true;
        guageCanvasObj.GetComponent<Canvas>().enabled = true;

        isEnd = false;
    }
}
