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

    [SerializeField]
    SceneChanger sceneChanger = default;                    // シーンチェンジクラス

    [SerializeField]
    GameObject buttons = default;                           // リザルトのボタン達

    [SerializeField]
    GameObject highScoreText = default;                     // ハイスコアテキスト

    bool isShowAd = false;                                  // リザルト広告表示フラグ

    /// <summary>
    /// 起動処理
    /// </summary>
    void OnEnable()
    {
        // 表示フラグをリセット
        isShowAd = false;

        // バナー表示
        AdManager.Inst.ShowBanner((int)AdBannerController.BANNER.BOTTOM);
    }

    /// <summary>
    /// 終了処理
    /// </summary>
    void OnDisable()
    {
        // 広告非表示
        AdManager.Inst.HideResultAd();
        HideBanner();
        buttons.SetActive(false);
        highScoreText.SetActive(false);
    }

    /// <summary>
    /// UI表示
    /// </summary>
    public void AcitveUI()
    {
        buttons.SetActive(true);

        // ハイスコアを超えたらハイスコアの文字表示
        if (GameDataManager.Inst.PlayData.HighScore > ScoreManager.Inst.NowBreakNum)
        {
            highScoreText.SetActive(true);
        }
    }

    /// <summary>
    /// 初期化.
    /// </summary>
    public override void Start()
    {
        GameServiceUtil.Auth();
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    protected override void FixedUpdate()
    {
        // プレイヤーのアニメーションが終わったら
        if (resultPlayerAnimator.IsEnd && !isShowAd)
        {
            // インタースティシャルか動画広告表示
            ShowAd();
        }

        // 動画広告を見終わったらシーンを切り替える
        if (AdManager.Inst.EndAdVideo())
        {
            sceneChanger.ChangeScene();
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
    ///  インタースティシャルか動画広告表示
    /// </summary>
    public void ShowAd()
    {
        AdManager.Inst.ShowResultAd();

        // 連続で表示しないようにする
        isShowAd = true;
    }

    /// <summary>
    /// バナーの非表示
    /// </summary>
    public void HideBanner()
    {
        AdManager.Inst.HideBanner();
    }
}