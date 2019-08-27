using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VMUnityLib;

/// <summary>
/// シーン切り替え時のネイティブ広告表示クラス
/// </summary>
public class SceneAdNativeController : MonoBehaviour
{
    [SerializeField]
    GameObject senceAdNative = default;                     // シーン切り替え時のネイティブ広告のオブジェクト

    [SerializeField]
    CanvasGroup canvas = default;                           // カンバス

    [SerializeField]
    SceneChanger sceneChanger = default;                    // シーン変更クラス

    public bool IsEnd { get; private set; } = false;        // フェード終了フラグ

    bool isDisplay = false;                                 // 表示中どうかフラグ

    /// <summary>
    /// 表示関数
    /// </summary>
    public void Display()
    {
        // ネイティブ広告表示
        senceAdNative.SetActive(true);
        IsEnd = false;
        isDisplay = true;

        // 表示時間までカウントダウンし、終わったら非表示
        StartCoroutine(DisplayCountDown());
    }

    /// <summary>
    /// 表示時間のカウントダウン
    /// </summary>
    /// <returns></returns>
    IEnumerator DisplayCountDown()
    {
        // フェードイン
        while (canvas.alpha < 1)
        {
            // 徐々に(0.01秒ごとに)フェードイン
            canvas.alpha += 0.1f;
            yield return new WaitForSeconds(0.01f);
        }

        // シーンをプッシュ
        sceneChanger.PushScene();

        // シーンプッシュ中(4秒間)表示
        yield return new WaitForSeconds(4.0f);

        // フェードアウト
        while (canvas.alpha > 0)
        {
            // 徐々に(0.01秒ごとに)フェードアウト
            canvas.alpha -= 0.1f;
            yield return new WaitForSeconds(0.01f);
        }

        // 透明になったら非表示して終了フラグをON
        senceAdNative.SetActive(false);
        IsEnd = true;
    }

    /// <summary>
    /// シーン切り替えネイティブ広告の終了検知
    /// </summary>
    /// <returns>終了フラグ</returns>
    public bool EndFade()
    {
        // return用のフラグ
        bool returnflg = false;

        // NOTE:リトライするとこの広告は表示されないため、
        //      表示されない時はそのまま終了させるようにしました。

        // 表示しているなら
        if (isDisplay)
        {
            // フェードアウトが終わったタイミングで検知
            if (IsEnd)
            {
                returnflg = IsEnd;
                isDisplay = false;
                IsEnd = false;
            }
        }
        // 表示していないのならそのまま終了検知
        else
        {
            returnflg = true;
        }

        return returnflg;
    }
}