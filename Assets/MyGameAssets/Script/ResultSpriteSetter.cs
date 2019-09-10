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
    Image[] menuButton = default;    // メニューロゴ

    [SerializeField]
    Image page = default;               // ノート
    [SerializeField]
    Image dish = default;               // スキン画面用お皿

    [SerializeField]
    Image[] memo = default;             // うさぎ図鑑用メモ
    [SerializeField]
    Image[] window = default;           // ウィンドウ     
    [SerializeField]
    Image[] scoreMochi = default;       // カウントアップ時にスコアに応じて増減するもち

    [SerializeField]
    Image rarity0 = default;            // 非解放用レアリティ
    [SerializeField]
    Image[] rarity1 = default,          // ★１群
            rarity2 = default,          // ★２群
            rarity3 = default,          // ★３群
            rarity4 = default,          // ★４群
            rarity5 = default;          // ★５群

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

        scoreMochi[(int)ScoreManager.Score.Low].sprite = atlas.GetSprite(SpriteName.LowScoreMochi);
        scoreMochi[(int)ScoreManager.Score.Normal].sprite = atlas.GetSprite(SpriteName.NormalScoreMochi);
        scoreMochi[(int)ScoreManager.Score.Good].sprite = atlas.GetSprite(SpriteName.GoodScoreMochi);
        scoreMochi[(int)ScoreManager.Score.VeryGood].sprite = atlas.GetSprite(SpriteName.VeryGoodScoreMochi);

        rarity0.sprite = atlas.GetSprite(SpriteName.Rarity0);
        SetRaritySprite(rarity1, SpriteName.Rarity1);
        SetRaritySprite(rarity2, SpriteName.Rarity2);
        SetRaritySprite(rarity3, SpriteName.Rarity3);
        SetRaritySprite(rarity4, SpriteName.Rarity4);
        SetRaritySprite(rarity5, SpriteName.Rarity5);

        for (int i = 0; i < menuButton.Length; i++)
        {
            menuButton[i].sprite = atlas.GetSprite(SpriteName.MenuButton);
        }

        for (int i = 0; i < window.Length; i++)
        {
            window[i].sprite = atlas.GetSprite(SpriteName.Window);
        }

        for (int i = 0; i < memo.Length; i++)
        {
            memo[i].sprite = atlas.GetSprite(SpriteName.Memo);
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