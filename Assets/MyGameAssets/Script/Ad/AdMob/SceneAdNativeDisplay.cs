using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VMUnityLib;

/// <summary>
/// シーン切り替え時のネイティブ広告表示クラス
/// </summary>
public class SceneAdNativeDisplay : MonoBehaviour
{
    [SerializeField]
    GameObject senceAdNative = default;     // シーン切り替え時のネイティブ広告のオブジェクト

    [SerializeField]
    CanvasGroup canvas = default;           // カンバス

    [SerializeField]
    SceneChanger sceneChanger = default;    // シーン変更クラス

    /// <summary>
    /// 表示関数
    /// </summary>
    public void Display()
    {
        // ネイティブ広告表示
        senceAdNative.SetActive(true);

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
            // 徐々に
            canvas.alpha += 0.1f;

            yield return new WaitForSeconds(0.01f);
        }

        // シーンをプッシュ
        sceneChanger.PushScene();

        yield return new WaitForSeconds(4.0f);

        // フェードアウト
        while (canvas.alpha > 0)
        {
            // 徐々に
            canvas.alpha -= 0.1f;

            yield return new WaitForSeconds(0.01f);
        }

        // 透明になったら非表示
        senceAdNative.SetActive(false);
    }
}