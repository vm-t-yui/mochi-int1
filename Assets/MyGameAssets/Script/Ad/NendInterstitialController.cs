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
        "93acaf823cb61686fd0974a3d25d7f3957449b33";
#elif UNITY_IOS
        "518baa8cd707b10d41f6ef0b4a69a8aa70eeb975";
#else
        "unexpected_platform";
#endif

    const string SpotID =                                   // スポットID
#if UNITY_ANDROID
        "974415";
#elif UNITY_IOS
        "974411";
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
