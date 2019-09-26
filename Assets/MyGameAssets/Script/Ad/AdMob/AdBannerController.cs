using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

using GoogleMobileAds.Api;    // Google AdMob広告用

/// <summary>
/// バナー広告テストクラス
/// </summary>
public class AdBannerController : MonoBehaviour
{
    // バナーの位置
    public enum BANNER
    {
        TOP,
        BOTTOM,
        MAX
    }

    BannerView bannerViewTop;                                           // バナー広告制御クラス(上)
    BannerView bannerViewBottom;                                        // バナー広告制御クラス(下)

    const string AdUnitId =                                             // 広告ユニットID（テスト用ID）
#if UNITY_ANDROID
        "ca-app-pub-3940256099942544/6300978111";
#elif UNITY_IOS
        "ca-app-pub-3940256099942544/2934735716";
#else
        "unexpected_platform";
#endif

    public bool IsLoaded { get; private set; } = false;                 // ロード完了フラグ

    /// <summary>
    /// バナー広告生成
    /// </summary>
    public void RequestBanner()
    {
        bannerViewTop = new BannerView(AdUnitId, AdSize.Banner, AdPosition.Top);
        bannerViewBottom = new BannerView(AdUnitId, AdSize.Banner, AdPosition.Bottom);

        // 上下バナーの空の広告リクエストを作成
        AdRequest requestTop = new AdRequest.Builder().Build();
        AdRequest requestBottom = new AdRequest.Builder().Build();

        // それぞれのbannerViewにrequestをロード
        bannerViewTop.LoadAd(requestTop);
        bannerViewBottom.LoadAd(requestBottom);

        // 表示状態で生成されるので非表示にする
        bannerViewTop.Hide();
        bannerViewBottom.Hide();

        IsLoaded = true;
    }

    /// <summary>
    /// 表示
    /// </summary>
    public void Show()
    {
        bannerViewTop.Show();
    }

    /// <summary>
    /// 非表示
    /// </summary>
    public void Hide()
    {
        bannerViewTop.Hide();
        bannerViewBottom.Hide();
    }
}
