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
    /// <summary>
    /// 新しく手に入れた時に表示されるNewアイコンの種類
    /// </summary>
    enum NewIcon
    {
        Rabbit,         // うさぎ
        Skin,           // スキン
        Achieve,        // 実績
        Menu,           // メニュー
        Length,         // enumの長さ
    }

    [SerializeField]
    GameObject[] newIcon = new GameObject[(int)NewIcon.Length];　// 新しく手に入れた時に表示されるNewアイコン

    // 処理なし。メッセージ受信エラー避け.
    protected override void InitSceneChange() { }
    protected override void OnSceneDeactive() { }
    protected override void OnFadeInEnd() { }

    [SerializeField]
    ResultPlayerAnimator resultPlayerAnimator = default;    // リザルトプレイヤーアニメータークラス
    [SerializeField]
    SceneChanger sceneChanger = default;                    // シーンチェンジクラス
    [SerializeField]
    ScoreCountUpper scoreCountUpper = default;              // スコアカウントアップクラス

    [SerializeField]
    GameObject buttons = default;                           // リザルトのボタン達

    [SerializeField]
    GameObject highScoreText = default;                     // ハイスコアテキスト
    [SerializeField]
    GameObject shareText = default;                         // シェア促しのテキスト

    [SerializeField]
    float showAdTime = 1;                                   // 広告表示までの待機時間

    bool isShowAd = false;                                  // リザルト広告表示フラグ

    /// <summary>
    /// 起動処理
    /// </summary>
    void OnEnable()
    {
        // 一旦非表示にさせる
        shareText.gameObject.SetActive(false);
        highScoreText.SetActive(false);
        isShowAd = false;

        // バナー表示
        AdManager.Inst.ShowBanner((int)AdBannerController.BANNER.TOP);

        // Newテキスト表示
        ShowNewIcon();
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
        if (resultPlayerAnimator.IsEnd && scoreCountUpper.IsEnd && !isShowAd)
        {
            // インタースティシャルか動画広告表示
            Invoke("ShowAd", showAdTime);

            // 連続で表示しないようにする表示フラグを立てて、ボタンを表示
            isShowAd = true;
        }

        // 動画広告を見終わったらシーンを切り替える
        if (AdManager.Inst.EndAdVideo())
        {
            sceneChanger.ChangeScene();
        }
    }

    /// <summary>
    /// Newアイコン表示
    /// </summary>
    void ShowNewIcon()
    {
        // 新たにうさぎ図鑑のページが解放されたらNewアイコンを表示
        if (GameDataManager.Inst.PlayData.IsNewReleasedRabbit)
        {
            newIcon[(int)NewIcon.Rabbit].gameObject.SetActive(true);
        }
        // 新たにもちスキンが解放されたらNewアイコンを表示
        if (GameDataManager.Inst.PlayData.IsNewReleasedSkin)
        {
            newIcon[(int)NewIcon.Skin].gameObject.SetActive(true);
        }

        // 新たに実績解除されたらNewアイコンを表示
        if (GameDataManager.Inst.PlayData.IsNewReleasedAchieve)
        {
            newIcon[(int)NewIcon.Achieve].gameObject.SetActive(true);
            newIcon[(int)NewIcon.Menu].gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// Newアイコン非表示
    /// </summary>
    public void HideNewIcon(int num)
    {
        // 番号に応じたNewアイコンを非表示、フラグをリセット
        newIcon[num].gameObject.SetActive(false);
        switch (num)
        {
            case (int)NewIcon.Rabbit:
                // 全項目がNewアイコン表示中ではなかったら
                if (!GameDataManager.Inst.PlayData.ExistDrawNewIconRabbit())
                {
                    GameDataManager.Inst.PlayData.IsNewReleasedRabbit = false;
                }
                break;

            case (int)NewIcon.Skin:
                // 全項目がNewアイコン表示中ではなかったら
                if (!GameDataManager.Inst.PlayData.ExistDrawNewIconSkin())
                {
                    GameDataManager.Inst.PlayData.IsNewReleasedSkin = false;
                }
                break;

            case (int)NewIcon.Achieve:
                GameDataManager.Inst.PlayData.IsNewReleasedAchieve = false;
                newIcon[(int)NewIcon.Menu].gameObject.SetActive(false); break;
        }

        // 処理が終わったらデータをセーブ
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
        // 広告表示前にスクショをとる
        ShareHelper.Inst.CaptureScreenShot();

        // 広告を表示したらボタン表示
        AdManager.Inst.ShowResultAd();
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

    /// <summary>
    /// シーン切り替えようネイティブ広告
    /// </summary>
    public void ShowSceneAdNative()
    {
        AdManager.Inst.ShowSceneAdNative();
    }

    /// <summary>
    /// ハイスコア時のテキスト表示
    /// </summary>
    public void ShowHighScoreText()
    {
        // メニューのNewアイコン、シェア促しアイコン、ハイスコアテキストを表示
        shareText.gameObject.SetActive(true);
        highScoreText.SetActive(true);
        newIcon[(int)NewIcon.Menu].gameObject.SetActive(true);
    }
}
