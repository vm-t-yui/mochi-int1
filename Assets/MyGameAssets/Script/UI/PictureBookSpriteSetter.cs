using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

/// <summary>
/// 図鑑用スプライトセッタークラス
/// </summary>
public class PictureBookSpriteSetter : MonoBehaviour
{
    [SerializeField]
    SpriteAtlas atlas = default;      // スプライトアトラス

    [SerializeField]
    Image[]     window     = default,    // 
                windowDark = default,    // 
                memo       = default,    // 
                newIcon    = default,    // 
                rarity1    = default,    // ★１群
                rarity2    = default,    // ★２群
                rarity3    = default,    // ★３群
                rarity4    = default,    // ★４群
                rarity5    = default;    // ★５群

    [SerializeField]
    Image       page    = default,    // 
                rarity0 = default;    // 非解放用レアリティ

    [SerializeField]
    Transform descriptions = default,    // 図鑑説明欄の親オブジェクト
              bigImages    = default;    // ウサギの拡大画像の親オブジェクト

    /// <summary>
    /// 起動処理
    /// </summary>
    void Awake()
    {
        // 各画像にスプライトをセット
        SetSpriteArray(window, SpriteName.Window);
        SetSpriteArray(windowDark, SpriteName.WindowDark);
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

        SetDescriptionsSprite();
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
            string spriteName = TowerObjectDataManager.Inst.GetRabbitDataFromNumber(i).name;

            images[i].transform.GetChild(0).GetComponent<Image>().sprite = atlas.GetSprite(spriteName);
            images[i].transform.GetChild(1).GetComponent<Image>().sprite = atlas.GetSprite(spriteName);

            // ウサギの拡大画像も同時にセット
            bigImages.GetChild(i).GetChild(0).GetComponent<Image>().sprite = atlas.GetSprite(spriteName);
        }
    }

    /// <summary>
    /// 図鑑説明欄のスプライトセット関数
    /// </summary>
    void SetDescriptionsSprite()
    {
        int num = 0;
        foreach (Transform item in descriptions)
        {
            string spriteName = TowerObjectDataManager.Inst.GetRabbitDataFromNumber(num).name;
            item.GetChild(2).GetComponent<Image>().sprite = atlas.GetSprite(spriteName);

            Debug.Log(item.GetChild(1));

            num++;

            if (num >= 25) break;
        }
    }
}
