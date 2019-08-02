﻿using UnityEngine;
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
    Image   rarity0         = default;    // 非解放用レアリティ
    [SerializeField]
    Image[] rarity1         = default,    // ★１群
            rarity2         = default,    // ★２群
            rarity3         = default,    // ★３群
            rarity4         = default,    // ★４群
            rarity5         = default;    // ★５群

    /// <summary>
    /// 起動処理.
    /// </summary>
    void Awake()
    {
        // 各スプライトをアタッチ
        pictureBookButton.sprite = atlas.GetSprite(SpriteName.PictureBookButton);
        settingButton.sprite     = atlas.GetSprite(SpriteName.SettingButton);
        leaderboardButton.sprite = atlas.GetSprite(SpriteName.LeaderboardButton);
        shareButton.sprite       = atlas.GetSprite(SpriteName.ShareButton);
        rarity0.sprite           = atlas.GetSprite(SpriteName.Rarity0);
        SetRaritySprite(rarity1, SpriteName.Rarity1);
        SetRaritySprite(rarity2, SpriteName.Rarity2);
        SetRaritySprite(rarity3, SpriteName.Rarity3);
        SetRaritySprite(rarity4, SpriteName.Rarity4);
        SetRaritySprite(rarity5, SpriteName.Rarity5);
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