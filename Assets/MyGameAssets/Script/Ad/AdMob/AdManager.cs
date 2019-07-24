using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//== ID一覧 ================================================================================//
// アプリID               : Android
// アプリID               : ios

// アプリID（サンプル）     : Android    ca-app-pub-7073050807259252~7297201289
// アプリID（サンプル）     : ios        ca-app-pub-7073050807259252~7875785788

// AdMob banner          : Android 
// AdMob banner          : ios   

// AdMob Interstitial    : Android   
// AdMob Interstitial    : ios  

// Nend Interstitial     : Android   
// Nend Interstitial     : ios   

// AdMob Rewarded Video  : Android  
// AdMob Rewarded Video  : ios      
//=========================================================================================//

// 必要な広告
//・タイトルのバナー(下)
//・タイトルのおすすめアプリ（ネイティブアド小）
//・タイトルからメインへの偽ローディング画面（中央に表示されるnendネイティブ大）
//・リザルト時および偽ロード時バナー（上）
//・リザルト時インタースティシャル(nend -> nend -> nend -> adMob -> 自社 -> 非表示のループ)
//・インタースティシャル非表示時の動画リワード広告

/// <summary>
/// 広告管理クラス
/// </summary>
public class AdManager : MonoBehaviour
{
    [SerializeField]
    AdMobManager adMob = default;                                     // AdMob広告管理クラス
    [SerializeField]
    NendInterstitialController nendInterstitial = default;            // nendインタースティシャル広告コントロールクラス
    [SerializeField]
    OwnCompAdInterstitialController ownCompInterstitial = default;    // 自社アプリインタースティシャル広告コントロールクラス
    [SerializeField]
    AdVideoRecommender adVideoRecommender = default;                  // 動画リワード広告クラス

    int showCount = 0;                                                // インタースティシャル用表示回数
    const string ShowCountKey = "ShowCount";                          // 表示回数データのキー

    const int OwnCompAdCount = 4;                                     // 自社広告使用時の表示回数
    const int AdMobCount = 3;                                         // AdMob使用時の表示回数
    const int RewardCount = 5;                                        // 動画リワード使用時の表示回数

    const string AppId =                                              // アプリID
#if UNITY_ANDROID
        "ca-app-pub-7073050807259252~7297201289";
#elif UNITY_IOS
        "ca-app-pub-7073050807259252~7875785788";
#else
        "unexpected_platform";
#endif

    public bool IsAdView { get; private set; }                        // 広告表示してるかどうか 

    bool isOnline = false;                                            // オンラインかどうか

    /// <summary>
    /// ロード完了検知
    /// </summary>
    public bool IsLoaded()
    {
        // バナー、インタースティシャルどちらもロードが完了したらtrueを返す、オフラインの場合も同様
        if ((adMob.IsLoaded() && nendInterstitial.IsLoaded) || !isOnline)
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
    void OnEnable()
    {
        // オンラインかどうか判断
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            isOnline = false;
        }
        else
        {
            isOnline = true;
        }

        // オフラインなら処理を抜ける
        if (!isOnline) { return; }

        // AdMobとnend動画リワード広告の広告ロード
        adMob.RequestAdMob();
        nendInterstitial.Load();
        adVideoRecommender.Init();
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    void Update()
    {
        // 動画リワード広告動画終了待ち
        adVideoRecommender.WaitTermination();
    }

    /// <summary>
    /// バナー広告表示
    /// </summary>
    public void ShowBanner()
    {
        // オフラインなら処理を抜ける
        if (!isOnline) { return; }

        adMob.ShowBanner();
    }

    /// <summary>
    /// バナー広告非表示
    /// </summary>
    public void HideBanner()
    {
        // オフラインなら処理を抜ける
        if (!isOnline) { return; }

        adMob.HideBanner();
    }

    /// <summary>
    /// 動画リワード広告再生
    /// </summary>
    public void PlayAdVideo()
    {
        // オフラインなら処理を抜ける
        if (!isOnline) { return; }

        adVideoRecommender.PlayAdVideo();
    }

    /// <summary>
    /// インタースティシャル広告表示
    /// </summary>
    public void ShowInterstitial()
    {
        // オフラインなら処理を抜ける
        if (!isOnline) { return; }

        // 表示回数をロード
        showCount = PlayerPrefs.GetInt(ShowCountKey, 1);

        // 4回毎に自社広告を使用
        if (showCount % OwnCompAdCount == 0)
        {
            ownCompInterstitial.enabled = true;
        }
        // 3回毎にAdMobを使用
        else if (showCount % AdMobCount == 0)
        {
            adMob.ShowInterstitial();
        }
        // 上記以外ならnendを使用、5回毎の動画リワードを出す際は表示しない
        else if (showCount % RewardCount != 0)
        {
            nendInterstitial.Show();
        }

        // 表示回数をカウント
        showCount++;
        // 5回毎(動画リワードの番が来るたび)に初期化
        if (showCount > RewardCount)
        {
            showCount = 1;
        }

        // 表示回数をセーブ
        PlayerPrefs.SetInt(ShowCountKey, showCount);
        PlayerPrefs.Save();
    }
}
