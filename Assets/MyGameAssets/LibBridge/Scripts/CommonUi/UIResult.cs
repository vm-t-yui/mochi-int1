using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VMUnityLib;
using TMPro;

/// <summary>
/// リザルト用UI
/// </summary>
public class UIResult : CmnMonoBehaviour
{
    // 処理なし。メッセージ受信エラー避け.
    protected override void InitSceneChange() { }
    protected override void OnSceneDeactive() { }
    protected override void OnFadeInEnd() { }

    [SerializeField]
    ResultPlayerAnimator resultPlayerAnimator = default;    // リザルトプレイヤーアニメータークラス

    bool isInterstitial = false;                            // インタースティシャル表示フラグ


    /// <summary>
    /// 初期化.
    /// </summary>
    public override void Start()
    {
        // 表示フラグをリセット
        isInterstitial = false;

        // バナー表示
        AdManager.Inst.ShowBanner((int)AdBannerController.BANNER.BOTTOM);

        GameServiceUtil.Auth();
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    protected override void FixedUpdate()
    {
        // プレイヤーのアニメーションが終わったら
        if(resultPlayerAnimator.IsEnd && !isInterstitial)
        {
            // インタースティシャル表示
            ShowInterstitial();
        }
    }

    /// <summary>
    /// リーダーボード表示.
    /// </summary>
    public void ShowLeaderboard()
    {
        GameServiceUtil.Auth();
        GameServiceUtil.ShowLeaderboardUI();
    }

    /// <summary>
    /// 実績表示.
    /// </summary>
    public void ShowAchive()
    {
        GameServiceUtil.Auth();
        GameServiceUtil.ShowAchivementUI();
    }

    /// <summary>
    /// シェア.
    /// </summary>
    public void Share()
    {
        ShareHelper.Inst.Share(LibBridgeInfo.SHARE_TEXT + LibBridgeInfo.TWITTER_TAG, LibBridgeInfo.APP_URL);
    }

    /// <summary>
    /// インタースティシャル広告表示
    /// </summary>
    public void ShowInterstitial()
    {
        AdManager.Inst.ShowInterstitial();

        // 連続で表示しないようにする
        isInterstitial = true;
    }

    /// <summary>
    /// バナーの非表示
    /// </summary>
    public void HideBanner()
    {
        AdManager.Inst.HideBanner();
    }

    /// <summary>
    /// 動画リワード広告表示
    /// </summary>
    public void PlayAdVideo()
    {
        AdManager.Inst.PlayAdVideo();
    }
}
