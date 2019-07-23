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

    public bool isTimeup { get; private set; } = false; // タイムアップフラグ

    /// <summary>
    /// 更新処理
    /// </summary>
    void Update()
    {
        if (!isTimeup)
        {
            float nowTime = seconds - Time.time;    // 今の秒数

            // 指定の秒数を数え終わったらタイムアップ
            if (nowTime < 0)
            {
                isTimeup = true;
            }
            // 数え終わってない場合は、数え続ける
            else
            {
                timer.text = nowTime.ToString("f2");
            }
        }
    }
}
