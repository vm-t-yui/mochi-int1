using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VMUnityLib;
using TMPro;
using scoreNum = ScoreCounter.TargetObject;

/// <summary>
/// リザルト用UI
/// </summary>
public class UIResult : CmnMonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI[] scoreText = default;    // スコア用テキスト

    bool isCountUp = false;

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

        GameServiceUtil.Auth();
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    protected override void FixedUpdate()
    {
        if (!isCountUp)
        {
            // スコアカウントアップ開始
            for (int i = 0; i < (int)scoreNum.Length; i++)
            {
                scoreText[i].text = ScoreCounter.Inst.ScoreCountUp(i).ToString();
            }

            // カウントを終わらせる
            if (ScoreCounter.Inst.IsCountUp[(int)scoreNum.Mochi] && ScoreCounter.Inst.IsCountUp[(int)scoreNum.Rabbit])
            {
                isCountUp = true;
            }
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
    }

    /// <summary>
    /// バナーの非表示
    /// </summary>
    public void HideBanner()
    {
        AdManager.Inst.HideBanner();
    }

    /// <summary>
    /// スコアのリセット
    /// </summary>
    public void ScoreReset()
    {
        ScoreCounter.Inst.Reset();
        isCountUp = false;
    }

    /// <summary>
    /// 動画リワード広告表示
    /// </summary>
    public void PlayAdVideo()
    {
        AdManager.Inst.PlayAdVideo();
    }
}
