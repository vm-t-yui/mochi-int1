using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// スコアカウントアップクラス
/// </summary>
public class ScoreCountUpper : MonoBehaviour
{
    public float NowCount { get; private set; } = 0;    // 現在のカウント

    public bool IsStart { get; private set; } = false;  // 開始フラグ
    public bool IsEnd { get; private set; } = false;    // 終了フラグ

    [SerializeField]
    UIResult uIResult = default;                        // リザルトのUI

    [SerializeField]
    float waitTime = 5.0f;                              // カウントにかかるの時間

    [SerializeField]
    TextMeshProUGUI text = default;                     // テキスト

    [SerializeField]
    ResultPlayerAnimator playerAnim = default;          // プレイヤーのアニメーション

    /// <summary>
    /// 起動処理
    /// </summary>
    void OnEnable()
    {
        NowCount = 0;
        IsEnd = false;
        IsStart = true;

    }

    /// <summary>
    /// 更新処理
    /// </summary>
    void Update()
    {
        // カウント開始されたらスコアをカウントアップ
        if (IsStart && !IsEnd)
        {
            NowCount += (ScoreManager.Inst.NowBreakNum * (Time.deltaTime / waitTime));

            // カウントし終わったら
            if (ScoreManager.Inst.NowBreakNum <= NowCount)
            {
                // カウントダウン終了
                IsEnd = true;
                playerAnim.AnimStart((int)ResultPlayerAnimator.AnimKind.ScoreResult);

                // リザルトのUI表示
                uIResult.AcitveUI();

                // スコアリセット
                ScoreManager.Inst.Reset();
            }
        }

        // テキストにセット
        text.text = ((int)NowCount).ToString();
    }
}