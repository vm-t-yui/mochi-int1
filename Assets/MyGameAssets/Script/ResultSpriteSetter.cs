using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

/// <summary>
/// リザルトシーンのスプライトセットクラス
/// </summary>
public class ResultSpriteSetter : MonoBehaviour
{
    [SerializeField]
    SpriteAtlas atlas = default;        // スプライトアトラス

    [SerializeField]
    Image pictureBookButton = default;  // 図鑑ボタン
    [SerializeField]
    Image leaderboardButton = default;  // リーダーボードボタン
    [SerializeField]
    Image shareButton = default;        // 共有ボタン
    [SerializeField]
    Image skinButton = default;         // スキンボタン
    [SerializeField]
    Image achievementButton = default;  // 実績ボタン

    [SerializeField]
    Image page = default;               // ノート
    [SerializeField]
    Image dish = default;               // スキン画面用お皿
    [SerializeField]
    Image shareIcon = default;          // シェア促しテキスト用アイコン

    [SerializeField]
    Image[] menuButton = default;       // メニューロゴ
    [SerializeField]
    Image[] window = default;           // ウィンドウ     
    [SerializeField]
    Image[] scoreMochi = default;       // カウントアップ時にスコアに応じて増減するもち
    [SerializeField]
    Image[] newIcon = default;          // 新たに手に入れた時にボタンの上に表示するのアイコン

    /// <summary>
    /// 起動処理.
    /// </summary>
    void Awake()
    {
        // 各スプライトをアタッチ
        pictureBookButton.sprite = atlas.GetSprite(SpriteName.PictureBookButton);
        leaderboardButton.sprite = atlas.GetSprite(SpriteName.LeaderboardButton);
        shareButton.sprite = atlas.GetSprite(SpriteName.ShareButton);
        achievementButton.sprite = atlas.GetSprite(SpriteName.Achievement);
        skinButton.sprite = atlas.GetSprite(SpriteName.SkinButton);
        page.sprite = atlas.GetSprite(SpriteName.Page);
        dish.sprite = atlas.GetSprite(SpriteName.Dish);
        shareIcon.sprite = atlas.GetSprite(SpriteName.ShareIcon);

        scoreMochi[(int)ScoreManager.Score.Low].sprite = atlas.GetSprite(SpriteName.LowScoreMochi);
        scoreMochi[(int)ScoreManager.Score.Normal].sprite = atlas.GetSprite(SpriteName.NormalScoreMochi);
        scoreMochi[(int)ScoreManager.Score.Good].sprite = atlas.GetSprite(SpriteName.GoodScoreMochi);
        scoreMochi[(int)ScoreManager.Score.VeryGood].sprite = atlas.GetSprite(SpriteName.VeryGoodScoreMochi);

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

    /// <summary>
    /// レアリティ用スプライトセッター
    /// </summary>
    void SetRaritySprite(Image[] images, string spriteName)
    {
        // 受け取ったレアリティ画像配列すべてに指定したスプライトをアタッチ
        for (int i = 0; i < images.Length; i++)
        {
            images[i].sprite = atlas.GetSprite(spriteName);
        }
    }
}