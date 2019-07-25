using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NendUnityPlugin.AD;
using NendUnityPlugin.Common;

/// <summary>
/// nendインタースティシャル広告コントロールクラス
/// </summary>
public class NendInterstitialController : MonoBehaviour
{
    [SerializeField]
    NendAdInterstitial nendAdInterstitial = default;        // nendインタースティシャルクラス

    const string ApiKey =                                   // APIキー
#if UNITY_ANDROID
        "8c278673ac6f676dae60a1f56d16dad122e23516";
#elif UNITY_IOS
        "308c2499c75c4a192f03c02b2fcebd16dcb45cc9";
#else
        "unexpected_platform";
#endif

    const string SpotID =                                   // スポットID
#if UNITY_ANDROID
        "213206";
#elif UNITY_IOS
        "213208";
#else
        "unexpected_platform";
#endif

    public bool IsLoaded { get; private set; } = false;     // 広告ロード完了フラグ

    /// <summary>
    /// 広告ロード
    /// </summary>
    public void Load()
    {
        nendAdInterstitial.Load(ApiKey, SpotID);

        nendAdInterstitial.AdLoaded += OnAdLoaded;
    }

    /// <summary>
    /// 広告表示
    /// </summary>
    public void Show()
    {
        nendAdInterstitial.Show();
    }

    /// <summary>
    /// ロード完了時コールバック
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    void OnAdLoaded(object sender, NendAdInterstitialLoadEventArgs args)
    {
        IsLoaded = true;
    }
}
