using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using VMUnityLib;

/// <summary>
/// タイマークラス
/// </summary>
public class Timer : SingletonMonoBehaviour<Timer>
{
    [SerializeField]
    TextMeshProUGUI timer = default;                    // タイマー用テキスト

    [SerializeField]
    float seconds = 0;                                  // 数える秒数

    [SerializeField]
    float plusSeconds = 0;                              // プラスする秒数

    public bool IsTimeup { get; private set; } = false; // タイムアップフラグ

    float oldTime = 0;                                  // 非起動時の秒数

    /// <summary>
    /// 起動処理
    /// </summary>
    void OnEnable()
    {
        // 非起動時の秒数を更新
        oldTime = Time.timeSinceLevelLoad;
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    void Update()
    {
        if (!IsTimeup)
        {
            // 今の秒数のカウント
            // NOTE:非起動時にもTime.timeSinceLevelLoadはカウントし続けているため、
            //      非起動時の秒数を引くことにより、0からカウントさせるようにしている。
            float nowTime = seconds - (Time.timeSinceLevelLoad - oldTime);

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
