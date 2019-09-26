using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;    // Google AdMob広告用

/// <summary>
/// AdMob広告管理クラス
/// </summary>
public class AdMobManager : MonoBehaviour
{
    [SerializeField]
    AdBannerController adBanner             = default;            // バナー広告コントロールクラス
    [SerializeField]
    AdInterstitialController adInterstitial = default;            // インタースティシャル広告コントロールクラス
    [SerializeField]
    AdRewardVideoController adMobVideo      = default;            //　AdMob動画リワード広告クラス

    const string AppId =                                          // アプリID
#if UNITY_ANDROID
        "ca-app-pub-3940256099942544~3347511713";
#elif UNITY_IOS
        "ca-app-pub-3940256099942544~1458002511";
#else
        "unexpected_platform";
#endif

    /// <summary>
    /// ロード完了検知
    /// </summary>
    /// <returns></returns>
    public bool IsLoaded()
    {
        // バナー、インタースティシャル、動画リワード広告全てもロードが完了したらtrueを返す
        if (adBanner.IsLoaded && adInterstitial.IsLoaded() && adMobVideo.IsLoaded)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// 広告生成
    /// </summary>
    public void RequestAdMob()
    {
        // Google Mobile Ads SDKを設定したアプリIDで初期化
        MobileAds.Initialize(AppId);

        // バナー広告を生成
        adBanner.RequestBanner();

        // インタースティシャル広告を生成
        adInterstitial.RequestInterstitial();

        // AdMob動画リワード広告を生成
        adMobVideo.RequestRewardVideo();

        // 一度バナー広告を非表示にする
        HideBanner();
    }

    /// <summary>
    /// バナー広告表示
    /// </summary>
    public void ShowBanner()
    {
        // ロードし終わるまで待機、ロード完了時バナー表示
        StartCoroutine(LoadBanner());
    }

    /// <summary>
    /// バナーがロードし終わるまでのコルーチン
    /// </summary>
    IEnumerator LoadBanner()
    {
        while (!adBanner.IsLoaded)
        {
            yield return new WaitForSeconds(0.01f);
        }

        // ロードが完了時したら表示
        adBanner.Show();
    }

    /// <summary>
    /// バナー広告非表示
    /// </summary>
    public void HideBanner()
    {
        adBanner.Hide();
    }

    /// <summary>
    /// インタースティシャル表示
    /// </summary>
    public void ShowInterstitial()
    {
        // 閉じているなら表示する
        if (adInterstitial.IsClosed)
        {
            adInterstitial.Show();
        }
    }
}
