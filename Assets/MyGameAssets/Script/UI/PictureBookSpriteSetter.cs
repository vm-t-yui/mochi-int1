using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

/// <summary>
/// 図鑑用スプライトセッタークラス
/// </summary>
public class PictureBookSpriteSetter : MonoBehaviour
{
    // ウサギのスプライト名
    string[] rabbitSpriteName = {
        "NormalRabbit",
        "BlackRabbit",
        "LehmanRabbit",
        "SumouRabbit",
        "WoodRabbit",
        "JoysonRabbit",
        "ZombieRabbit",
        "GhostRabbit",
        "SpaceRabbit",
        "RedRabbit",
        "BlueRabbit",
        "YellowRabbit",
        "GreenRabbit",
        "PinkRabbit",
        "MechaRabbit",
        "ZariganRabbit",
        "KagamimochiRabbit",
        "WarriorRabbit",
        "ChocosoftRabbit",
        "DiamondRabbit",
        "BossRabbit",
        "JusaburoRabbit",
        "OgisanRabbit",
        "MaidRabbit",
        "RabbitMan",
        };

    [SerializeField]
    SpriteAtlas atlas = default;      // スプライトアトラス

    [SerializeField]
    Image[]     window  = default,    // 
                memo    = default,    // 
                newIcon = default,    // 
                rarity1 = default,    // ★１群
                rarity2 = default,    // ★２群
                rarity3 = default,    // ★３群
                rarity4 = default,    // ★４群
                rarity5 = default;    // ★５群

    [SerializeField]
    Image       page    = default,    // 
                rarity0 = default;    // 非解放用レアリティ

    /// <summary>
    /// 起動処理
    /// </summary>
    void Awake()
    {
        // 各画像にスプライトをセット
        SetSpriteArray(window, SpriteName.Window);
        SetSpriteArray(memo, SpriteName.Memo);
        SetSpriteArray(newIcon, SpriteName.NewIcon);
        SetSpriteArray(rarity1, SpriteName.Rarity1);
        SetSpriteArray(rarity2, SpriteName.Rarity2);
        SetSpriteArray(rarity3, SpriteName.Rarity3);
        SetSpriteArray(rarity4, SpriteName.Rarity4);
        SetSpriteArray(rarity5, SpriteName.Rarity5);

        page.sprite = atlas.GetSprite(SpriteName.Page);
        rarity0.sprite = atlas.GetSprite(SpriteName.Rarity0);

        SetRabbitSprite(memo);
    }

    /// <summary>
    /// 画像配列用のセット関数
    /// </summary>
    void SetSpriteArray(Image[] images, string spriteName)
    {
        // 受け取った画像配列すべてに指定したスプライトをセット
        for (int i = 0; i < images.Length; i++)
        {
            images[i].sprite = atlas.GetSprite(spriteName);
        }
    }

    /// <summary>
    /// 各ウサギのボタン用のセット関数
    /// </summary>
    void SetRabbitSprite(Image[] images)
    {
        // 解放前と解放後用に各ボタンの子２つにスプライトをセット
        for (int i = 0; i < images.Length; i++)
        {
            images[i].transform.GetChild(0).GetComponent<Image>().sprite = atlas.GetSprite(rabbitSpriteName[i]);
            images[i].transform.GetChild(1).GetComponent<Image>().sprite = atlas.GetSprite(rabbitSpriteName[i]);
        }
    }
}
