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
    [SerializeField]
    TextMeshProUGUI scoreText = default;    // スコア用テキスト

    // 処理なし。メッセージ受信エラー避け.
    protected override void InitSceneChange() { }
    protected override void OnSceneDeactive() { }
    protected override void OnFadeInEnd() { }

    /// <summary>
    /// 初期化.
    /// </summary>
    public override void Start()
    {
        // バナー表示
        AdManager.Inst.ShowBanner((int)AdBannerController.BANNER.BOTTOM);

        // スコアカウントアップ開始
        ScoreCounter.Inst.ScoreCountUp();

        GameServiceUtil.Auth();
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    protected override void FixedUpdate()
    {
        // スコアをテキストに
        scoreText.text = ScoreCounter.Inst.DisplayBreakNum.ToString();
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
    }

    /// <summary>
    /// 動画リワード広告表示
    /// </summary>
    public void PlayAdVideo()
    {
        AdManager.Inst.PlayAdVideo();
    }
}
