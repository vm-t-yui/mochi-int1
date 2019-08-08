using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// スコアカウントアップクラス
/// </summary>
public class ScoreCountUpper : MonoBehaviour
{
    int nowCount = 0;                   // 現在のカウント
    bool isEnd = false;                 // 終了フラグ 
    bool isStart = false;               // 開始フラグ

    [SerializeField]
    float waitTime = 5.0f;              // カウントにかかるの時間

    [SerializeField]
    TextMeshProUGUI text = default;     // テキスト

    /// <summary>
    /// 起動処理
    /// </summary>
    void OnEnable()
    {
        isStart = true;
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    void Update()
    {
        // カウント開始されたらスコアをカウントアップ
        if (isStart && !isEnd)
        {
            nowCount += (int)(ScoreManager.Inst.NowBreakNum * (Time.deltaTime / waitTime));

            if (ScoreManager.Inst.NowBreakNum <= nowCount)
            {
                // カウントダウン終了
                isEnd = true;
            }
        }

        // テキストにセット
        text.text = nowCount.ToString();
    }

    /// <summary>
    /// 停止処理
    /// </summary>
    void OnDisable()
    {
        // リセット
        nowCount = 0;
        isEnd = false;
        ScoreManager.Inst.Reset();
    }
}