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
    GameObject newRabbitText = default;                     // うさぎ図鑑用Newテキスト

    [SerializeField]
    float showAdTime = 2;                                   // 広告表示までの待機時間

    bool isShowAd = false;                                  // リザルト広告表示フラグ

    /// <summary>
    /// 起動処理
    /// </summary>
    void OnEnable()
    {
        // 表示フラグをリセット
        isShowAd = false;

        // バナー表示
        AdManager.Inst.ShowBanner((int)AdBannerController.BANNER.TOP);

        // Newテキスト表示
        ShowNewText();
    }

    /// <summary>
    /// 終了処理
    /// </summary>
    void OnDisable()
    {
        // 広告非表示
        HideAd();
        HideBanner();
        buttons.SetActive(false);
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
            Invoke("ShowAd", showAdTime);
        }

        // 動画広告を見終わったらシーンを切り替える
        if (AdManager.Inst.EndAdVideo())
        {
            sceneChanger.ChangeScene();
        }
    }

    /// <summary>
    /// Newテキスト表示
    /// </summary>
    void ShowNewText()
    {
        // うさぎ図鑑
        if (GameDataManager.Inst.PlayData.IsNewRabbit)
        {
            newRabbitText.gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// ボタン用うさぎ図鑑Newテキスト非表示関数
    /// </summary>
    public void HideRabbitNewText()
    {
        newRabbitText.gameObject.SetActive(false);

        // データフラグをfalseにしてセーブ
        GameDataManager.Inst.PlayData.IsNewRabbit = false;
        JsonDataSaver.Save(GameDataManager.Inst.PlayData);
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
    ///  インタースティシャルか動画広告表示関数
    /// </summary>
    void ShowAd()
    {
        AdManager.Inst.ShowResultAd();

        // 連続で表示しないようにする表示フラグを立てて、ボタンを表示
        isShowAd = true;
        buttons.SetActive(true);
    }

    /// <summary>
    ///  インタースティシャルか動画広告非表示
    /// </summary>
    public void HideAd()
    {
        // 広告表示がされていないなら瞬時に、されているならアニメーションで消す。
        AdManager.Inst.HideResultAd();
    }

    /// <summary>
    /// バナーの非表示
    /// </summary>
    public void HideBanner()
    {
        AdManager.Inst.HideBanner();
    }
}