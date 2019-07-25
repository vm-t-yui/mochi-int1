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
    public enum BANNER
    {
        TOP,
        BOTTOM,
        MAX
    }

    BannerView[] bannerView;                                              // バナー広告制御クラス

    const string AdUnitId =                                             // 広告ユニットID（テスト用ID）
#if UNITY_ANDROID
        /*"ca-app-pub-3940256099942544/6300978111"*/"ca-app-pub-7073050807259252/3521607807";
#elif UNITY_IOS
        /*"ca-app-pub-3940256099942544/2934735716"*/"ca-app-pub-7073050807259252/4695088676";
#else
        "unexpected_platform";
#endif

    public bool IsLoaded { get; private set; } = false;                 // ロード完了フラグ

    /// <summary>
    /// バナー広告生成
    /// </summary>
    public void RequestBanner()
    {
        bannerView[0] = new BannerView(AdUnitId, AdSize.Banner, AdPosition.Top);
        bannerView[1] = new BannerView(AdUnitId, AdSize.Banner, AdPosition.Bottom);

        for (int i = 0; i < (int)BANNER.MAX; i++)
        {
            // 空の広告リクエストを作成
            AdRequest request = new AdRequest.Builder().Build();

            // bannerViewにrequestをロード
            bannerView[i].LoadAd(request);

            // 表示状態で生成されるので非表示にする
            bannerView[i].Hide();
        }

        IsLoaded = true;
    }

    /// <summary>
    /// 表示
    /// </summary>
    public void Show(int posNum)
    {
        if (posNum == (int)BANNER.TOP)
        {
            bannerView[0].Show();
        }
        else
        {
            bannerView[1].Show();
        }
    }

    /// <summary>
    /// 非表示
    /// </summary>
    public void Hide()
    {
        bannerView[0].Hide();
        bannerView[1].Hide();
    }
}
