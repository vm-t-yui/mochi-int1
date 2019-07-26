using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// タイマークラス
/// </summary>
public class Timer : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI timer = default;                    // タイマー用テキスト

    [SerializeField]
    float seconds = 0;                                  // 数える秒数

    [SerializeField]
    float plusSeconds = 0;                              // プラスする秒数

    public bool IsTimeup { get; private set; } = false; // タイムアップフラグ

    /// <summary>
    /// 更新処理
    /// </summary>
    void Update()
    {
        if (!IsTimeup)
        {
            // 今の秒数のカウント
            float nowTime = seconds - Time.timeSinceLevelLoad;

            // 指定の秒数を数え終わったらタイムアップ
            if (nowTime < 0)
            {
                IsTimeup = true;
            }
            // 数え終わってない場合は、数え続ける
            else
            {
                timer.text = nowTime.ToString("f2");
            }
        }
    }

    /// <summary>
    /// うさぎを助けた時のタイムプラス
    /// </summary>
    public void TimePlus()
    {
        seconds += plusSeconds;
    }
}
