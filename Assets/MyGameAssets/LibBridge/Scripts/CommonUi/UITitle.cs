/******************************************************************************/
/*!    \brief  タイトルシーン.
*******************************************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using VMUnityLib;

/// <summary>
/// タイトルのUIクラス
/// </summary>
public sealed class UITitle : CmnMonoBehaviour
{
#if USE_TWEEN
    uTweenAlpha tweenAlphe;
#endif
    [SerializeField]
    GameObject newRabbitText = default;     // うさぎ図鑑用Newテキスト
    [SerializeField]
    GameObject newSkinText = default;       // もちスキン用Newテキスト
 
    // 処理なし。メッセージ受信エラー避け.
    protected override void InitSceneChange() { }
    protected override void OnSceneDeactive() { }
    protected override void OnFadeInEnd() { }
    protected override void FixedUpdate() { }

    /// <summary>
    /// 起動処理
    /// </summary>
    void OnEnable()
    {
        // バナー表示
        ShowBanner();

        // Newテキスト表示
        ShowNewText();
    }

    /// <summary>
    /// 終了処理
    /// </summary>
    void OnDisable()
    {
        HideBanner();
    }

    /// <summary>
    /// 初期化.
    /// </summary>
    public override void Start()
    {
        //NOTE:まだPlayGameServiceの情報を作っていないためコメント化
        //GameServiceUtil.Auth();
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
        // もちスキン
        if (GameDataManager.Inst.PlayData.IsNewSkin)
        {
            newSkinText.gameObject.SetActive(true);
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
    /// ボタン用もちスキンNewテキスト非表示関数
    /// </summary>
    public void HideSkinNewText()
    {
        newSkinText.gameObject.SetActive(false);

        // データフラグをfalseにしてセーブ
        GameDataManager.Inst.PlayData.IsNewSkin = false;
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
    /// バナーの表示
    /// </summary>
    public void ShowBanner()
    {
        AdManager.Inst.ShowBanner((int)AdBannerController.BANNER.TOP);
    }

    /// <summary>
    /// バナーの非表示
    /// </summary>
    public void HideBanner()
    {
        AdManager.Inst.HideBanner();
    }

    /// <summary>
    /// シェア.
    /// </summary>
    public void Share()
    {
        ShareHelper.Inst.Share(LibBridgeInfo.SHARE_TEXT + LibBridgeInfo.TWITTER_TAG, LibBridgeInfo.APP_URL);
    }
}
