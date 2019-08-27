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

    public bool IsFadeEnd { get; private set; } = false;    // フェード終了フラグ

    /// <summary>
    /// 表示関数
    /// </summary>
    public void Display()
    {
        // ネイティブ広告表示
        senceAdNative.SetActive(true);
        IsFadeEnd = false;

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
        IsFadeEnd = true;
    }

    /// <summary>
    /// シーン切り替えネイティブ広告のフェードアウト終了検知
    /// </summary>
    /// <returns>フェードアウト終了フラグ</returns>
    public bool EndFade()
    {
        bool returnflg = false;

        if (IsFadeEnd)
        {
            returnflg = IsFadeEnd;
            IsFadeEnd = false;
        }

        return returnflg;
    }
}