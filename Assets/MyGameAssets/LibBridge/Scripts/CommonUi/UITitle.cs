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
    protected override void FixedUpdate() { }

    /// <summary>
    /// 起動処理
    /// </summary>
    void OnEnable()
    {
        // バナー表示
        ShowBanner();

        // Newテキスト表示
        ShowNewIcon();
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
        GameServiceUtil.Auth();
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
                GameDataManager.Inst.PlayData.IsNewReleasedRabbit = false; break;

            case (int)NewIcon.Skin:
                GameDataManager.Inst.PlayData.IsNewReleasedSkin = false; break;

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
