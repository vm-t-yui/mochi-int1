using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VMUnityLib;

//== ID一覧 ================================================================================//
// アプリID              : Android    ca-app-pub-7073050807259252~9091757973
// アプリID              : ios        ca-app-pub-7073050807259252~3188694607

// アプリID（サンプル）  : Android    ca-app-pub-3940256099942544~3347511713
// アプリID（サンプル）  : ios        ca-app-pub-3940256099942544~1458002511

// AdMob banner          : Android    ca-app-pub-7073050807259252/4089337954
// AdMob banner          : ios        ca-app-pub-7073050807259252/9433642902

// AdMob Interstitial    : Android    ca-app-pub-7073050807259252/4827704556
// AdMob Interstitial    : ios        ca-app-pub-7073050807259252/1555152888

// Nend Native (small)   : Android    Key : ce8ac21d194e35fbc3f1e226023d83160f679392
//                                    ID  : 974417
// Nend Native (small)   : ios        Key : 5b1c911a26bf65047debed3c460fe6b14e5eb519
//                                    ID  : 974413

// Nend Native (large)   : Android    Key : 7fd308d53b80fc92fbeef68816c5781a167c1634
//                                    ID  : 974416
// Nend Native (large)   : ios        Key : bc08e4d0e39a8591ccf974cfe2bf284cf89fc663
//                                    ID  : 974412

// Nend Interstitial     : Android    Key : 93acaf823cb61686fd0974a3d25d7f3957449b33
//                                    ID  : 974415
// Nend Interstitial     : ios        Key : 518baa8cd707b10d41f6ef0b4a69a8aa70eeb975
//                                    ID  : 974411

// AdMob Rewarded Video  : Android    ca-app-pub-7073050807259252/9190100326
// AdMob Rewarded Video  : ios        ca-app-pub-7073050807259252/5677751791
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
public class AdManager : SingletonMonoBehaviour<AdManager>
{
    [SerializeField]
    AdMobManager adMob = default;                                     // AdMob広告管理クラス
    [SerializeField]
    NendInterstitialController nendInterstitial = default;            // nendインタースティシャル広告コントロールクラス
    [SerializeField]
    OwnCompAdInterstitialController ownCompInterstitial = default;    // 自社アプリインタースティシャル広告コントロールクラス
    [SerializeField]
    AdVideoRecommender adVideoRecommender = default;                  // 動画リワード広告クラス
    [SerializeField]
    SceneAdNativeController sceneAdNative = default;                  // シーン切り替え時のネイティブ広告

    [SerializeField]
    Animator adVideoRecommenderAnim = default;                        // 動画広告用アニメーター
    [SerializeField]
    Animator OwnCompAdCanvasAnim = default;                           // 自社広告用アニメーター

    [SerializeField]
    CanvasGroup OwnCompAdCanvas = default;                            // 自社広告用カンバス
    [SerializeField]
    GameObject adVideoRecommenderWindow = default;                    // 動画リワード広告ウィンドウ

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
    void Awake()
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
    /// リザルトの広告表示
    /// </summary>
    public void ShowResultAd()
    {
        // オフラインなら処理を抜ける
        if (!isOnline) { return; }

        // 表示回数をロード
        showCount = GameDataManager.Inst.PlayData.PlayCount;

        // うさぎをコンプリートいなかったら5回毎に動画リワードを表示
        if (showCount % RewardCount == 0 && GameDataManager.Inst.PlayData.RabbitComplete())
        {
            adVideoRecommender.Recommend();
        }
        // 4回毎に自社広告を使用
        else if (showCount % OwnCompAdCount == 0)
        {
            ownCompInterstitial.enabled = true;
        }
        // 3回毎にAdMobを使用
        else if (showCount % AdMobCount == 0)
        {
            adMob.ShowInterstitial();
        }
        // 上記以外ならnendを使用
        else
        {
            nendInterstitial.Show();
        }
    }

    /// <summary>
    /// リザルトの広告非表示
    /// </summary>
    public void HideResultAd()
    {
        // 5回毎の動画リワードを非表示
        if (showCount % RewardCount == 0)
        {
            adVideoRecommenderAnim.SetTrigger("Small");
        }
        // それ以外
        else
        {
            OwnCompAdCanvasAnim.SetTrigger("FadeOut");
        }
    }

    /// <summary>
    /// シーン切り替えネイティブ広告表示
    /// </summary>
    public void ShowSceneAdNative()
    {
        sceneAdNative.Display();
    }

    /// <summary>
    /// 動画広告再生終了検知
    /// </summary>
    /// <returns>動画広告再生終了フラグ</returns>
    public bool EndAdVideo()
    {
        return adVideoRecommender.EndAdVideo();
    }

    /// <summary>
    /// シーン切り替えネイティブ広告のフェードアウト終了検知
    /// </summary>
    /// <returns>フェードアウト終了フラグ</returns>
    public bool EndFade()
    {
        return sceneAdNative.EndFade();
    }
}