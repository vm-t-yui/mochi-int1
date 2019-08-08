using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using VMUnityLib;

/// <summary>
/// タイマークラス
/// </summary>
public class Timer : CmnMonoBehaviour
{
    // 処理なし。メッセージ受信エラー避け.
    public override void Start() { }
    protected override void InitSceneChange() { }
    protected override void OnSceneDeactive() { }

    [SerializeField]
    TextMeshProUGUI timer = default;                        // タイマー用テキスト

    [SerializeField]
    float startTime = 0;                                    // ゲームスタートまでの秒数

    [SerializeField]
    float gameTime = 0;                                     // ゲーム内の秒数

    [SerializeField]
    float plusSeconds = 0;                                  // プラスする秒数

    public bool IsTimeup { get; private set; } = false;     // タイムアップフラグ

    public bool IsStart { get; private set; } = false;      // ゲームスタートまでのカウントダウンフラグ

    float oldTime = 0;                                      // 非起動時の秒数

    /// <summary>
    /// フェード終了時
    /// </summary>
    protected override void OnFadeInEnd()
    {
        // 非起動時の秒数を更新
        oldTime = Time.timeSinceLevelLoad;

        // 各フラグのリセット
        IsTimeup = false;
        IsStart = false;
    }


    /// <summary>
    /// 更新処理
    /// </summary>
    protected override void FixedUpdate()
    {
        if (!IsTimeup)
        {
            // ゲームスタートまでのカウントダウン開始
            if (!IsStart)
            {
                // ゲームスタートまでのカウントダウン
                // NOTE:非起動時にもTime.timeSinceLevelLoadはカウントし続けているため、
                //      非起動時の秒数を引くことにより、0からカウントさせるようにしている。
                float countDown = startTime - (Time.timeSinceLevelLoad - oldTime);

                // カウントダウンが終わったらゲーム開始
                if (countDown < 0)
                {
                    IsStart = true;
                    oldTime = Time.timeSinceLevelLoad;
                }
                // 数え終わってない場合は、数え続ける
                else
                {
                    timer.text = ((int)countDown).ToString();
                }
            }
            // ゲーム開始されたらゲーム内のカウントダウン開始
            else
            {
                // 今の秒数のカウント
                // NOTE:非起動時にもTime.timeSinceLevelLoadはカウントし続けているため、
                //      非起動時の秒数を引くことにより、0からカウントさせるようにしている。
                float nowTime = gameTime - (Time.timeSinceLevelLoad - oldTime);

                // 小数点第2位まで表示
                timer.text = nowTime.ToString("f2");

                // 指定の秒数を数え終わったらタイムアップ
                if (nowTime < 0)
                {
                    timer.text = "Time UP";

                    IsTimeup = true;
                }
            }
        }
    }

    /// <summary>
    /// うさぎを助けた時のタイムプラス
    /// </summary>
    public void TimePlus()
    {
        gameTime += plusSeconds;
    }
}
