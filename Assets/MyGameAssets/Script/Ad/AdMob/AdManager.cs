using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VMUnityLib;

//== ID一覧 ================================================================================//
// アプリID               : Android
// アプリID               : ios

// アプリID（サンプル）     : Android    ca-app-pub-3940256099942544~3347511713
// アプリID（サンプル）     : ios        ca-app-pub-3940256099942544~1458002511

// AdMob banner          : Android    ca-app-pub-3940256099942544/6300978111
// AdMob banner          : ios        ca-app-pub-3940256099942544/2934735716

// AdMob Interstitial    : Android    ca-app-pub-3940256099942544/1033173712
// AdMob Interstitial    : ios        ca-app-pub-3940256099942544/4411468910

// Nend Native (small)   : Android    Key : 16cb170982088d81712e63087061378c71e8aa5c
//                                    ID  : 485516
// Nend Native (small)   : ios        Key : 10d9088b5bd36cf43b295b0774e5dcf7d20a4071
//                                    ID  : 485500

// Nend Native (large)   : Android    Key : a88c0bcaa2646c4ef8b2b656fd38d6785762f2ff
//                                    ID  : 485520
// Nend Native (large)   : ios        Key : 30fda4b3386e793a14b27bedb4dcd29f03d638e5
//                                    ID  : 485504

// Nend Interstitial     : Android    Key : 8c278673ac6f676dae60a1f56d16dad122e23516
//                                    ID  : 213206
// Nend Interstitial     : ios        Key : 308c2499c75c4a192f03c02b2fcebd16dcb45cc9
//                                    ID  : 213208

// AdMob Rewarded Video  : Android    ca-app-pub-3940256099942544/5224354917
// AdMob Rewarded Video  : ios        ca-app-pub-3940256099942544/1712485313
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
    Animator adVideoRecommenderAnim = default;                        //
    [SerializeField]
    Animator OwnCompAdCanvasAnim = default;                           //

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

    public bool isRewardEnd { get; private set; } = false;            // リワード広告をスキップせずに見終わったかどうか

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
    public void ShowBanner(int posNum)
    {
        // オフラインなら処理を抜ける
        if (!isOnline) { return; }

        adMob.ShowBanner(posNum);
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

        // 5回毎の動画リワードを表示
        if (showCount % RewardCount == 0)
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
    /// リサルト広告の非表示
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
    /// 動画広告終了フラグ(スキップ無し)セット
    /// </summary>
    public void SetIsRewardEnd()
    {
        isRewardEnd = true;
    }

    /// <summary>
    /// 動画広告終了フラグ(スキップ無し)受け渡し処理
    /// </summary>
    public bool GetIsRewardEnd()
    {
        bool returnflg = false;

        // trueなら受け渡し
        if(isRewardEnd)
        {
            returnflg = isRewardEnd;
            isRewardEnd = false;
        }

        return returnflg;
    }
}
