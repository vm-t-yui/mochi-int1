using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

/// <summary>
/// スキン画面のスプライトセット
/// </summary>
public class SkinSpriteSetter : MonoBehaviour
{
    [SerializeField]
    SpriteAtlas atlas = default;    // スプライトアトラス

    [SerializeField]
    Image dish = default;           // スキン画面用お皿

    [SerializeField]
    Image[] normalMochiButton = default;
    [SerializeField]
    Image[] kouhakuMochiButton = default;
    [SerializeField]
    Image[] yomogiMochiButton = default;
    [SerializeField]
    Image[] ichigoDaihukuButton = default;
    [SerializeField]
    Image[] kashiwaMochiButton = default;
    [SerializeField]
    Image[] isobeMochiButton = default;

    [SerializeField]
    Image[] useSkin = default;

    [SerializeField]
    Image[] memo = default;         // メモ
    [SerializeField]
    Image[] window = default;       // ウィンドウ
    [SerializeField]
    Image[] newIcon = default;      // 新たに手に入れた時にボタンの上に表示するのアイコン

    /// <summary>
    /// 起動処理.
    /// </summary>
    void Awake()
    {
        // 各スプライトをアタッチ
        dish.sprite = atlas.GetSprite(SpriteName.Dish);

        SetSprite(normalMochiButton, SpriteName.NormalMochiButton);
        SetSprite(kouhakuMochiButton, SpriteName.KouhakuMochiButton);
        SetSprite(yomogiMochiButton, SpriteName.YomogiMochiButton);
        SetSprite(ichigoDaihukuButton, SpriteName.IchigoDaihukuButton);
        SetSprite(kashiwaMochiButton, SpriteName.KashiwaMochiButton);
        SetSprite(isobeMochiButton, SpriteName.IsobeMochiButton);

        SetSprite(memo, SpriteName.Memo);
        SetSprite(window, SpriteName.Window);
        SetSprite(newIcon, SpriteName.NewIcon);

    }

    /// <summary>
    /// 使用中スキン表示テキスト変更
    /// </summary>
    /// <param name="skinType">餅スキンの種類</param>
    public void ChangeUseSkin(SettingData.SkinType skinType)
    {
        // 受け取った餅スキンによって表示テキスト変更
        // TODO: 仮で作った機能なので要らない場合は削除します。そのまま使えそうならローカライズにも対応させます。
        switch (skinType)
        {
            case SettingData.SkinType.NormalMochi:
                SetSprite(useSkin, SpriteName.NormalMochi);
                break;
            case SettingData.SkinType.KouhakuMochi:
                SetSprite(useSkin, SpriteName.KouhakuMochi);
                break;
            case SettingData.SkinType.YomogiMochi:
                SetSprite(useSkin, SpriteName.YomogiMochi);
                break;
            case SettingData.SkinType.IchigoDaihuku:
                SetSprite(useSkin, SpriteName.IchigoDaihuku);
                break;
            case SettingData.SkinType.KashiwaMochi:
                SetSprite(useSkin, SpriteName.KashiwaMochi);
                break;
            case SettingData.SkinType.IsobeMochi:
                SetSprite(useSkin, SpriteName.IsobeMochi);
                break;
        }
    }

    /// <summary>
    /// スプライトセット関数(配列)
    /// </summary>
    /// <param name="images">イメージの配列</param>
    /// <param name="names">スプライトネーム</param>
    void SetSprite(Image[] images, string names)
    {
        for (int i = 0; i < images.Length; i++)
        {
            images[i].sprite = atlas.GetSprite(names);
        }
    }
}