using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

/// <summary>
/// タイトル用スプライトセッタークラス
/// </summary>
public class TitleSpriteSetter : MonoBehaviour
{
    [SerializeField]
    SpriteAtlas atlas       = default;    // スプライトアトラス

    [SerializeField]
    Image[] menuButton      = default;    // メニューロゴ
    [SerializeField]
    Image titleLogo         = default;    // タイトルロゴ
    [SerializeField]
    Image pictureBookButton = default;    // 図鑑ボタン
    [SerializeField]
    Image settingButton     = default;    // 設定ボタン
    [SerializeField]
    Image leaderboardButton = default;    // リーダーボードボタン
    [SerializeField]
    Image shareButton       = default;    // 共有ボタン
    [SerializeField]
    Image skinButton        = default;    // スキンボタン
    [SerializeField]
    Image offerWallButton   = default;    // おすすめアプリボタン
    [SerializeField]
    Image achievementButton = default;    // 実績ボタン

    [SerializeField]
    Image[] window          = default;    // ウィンドウ
    [SerializeField]
    Image[] newIcon         = default;    // 新たに手に入れた時にボタンの上に表示するのアイコン

    /// <summary>
    /// 起動処理.
    /// </summary>
    void Awake()
    {
        // 各スプライトをアタッチ
        pictureBookButton.sprite = atlas.GetSprite(SpriteName.PictureBookButton);
        settingButton.sprite     = atlas.GetSprite(SpriteName.SettingButton);
        leaderboardButton.sprite = atlas.GetSprite(SpriteName.LeaderboardButton);
        achievementButton.sprite = atlas.GetSprite(SpriteName.Achievement);
        shareButton.sprite       = atlas.GetSprite(SpriteName.ShareButton);
        skinButton.sprite        = atlas.GetSprite(SpriteName.SkinButton);
        offerWallButton.sprite   = atlas.GetSprite(SpriteName.OfferWallButton);

        for (int i = 0; i < menuButton.Length; i++)
        {
            menuButton[i].sprite = atlas.GetSprite(SpriteName.MenuButton);
        }

        for (int i = 0; i < window.Length; i++)
        {
            window[i].sprite = atlas.GetSprite(SpriteName.Window);
        }

        for (int i = 0; i < newIcon.Length; i++)
        {
            newIcon[i].sprite = atlas.GetSprite(SpriteName.NewIcon);
        }
    }
}
