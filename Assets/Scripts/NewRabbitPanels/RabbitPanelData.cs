using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;
using I2.Loc;

/// <summary>
/// ウサギのパネルのデータ
/// </summary>
public class RabbitPanelData : MonoBehaviour
{
    // ウサギの名前のローカライズデータ
    [SerializeField]
    Localize rabbitNameLocalize = default;

    // レアリティのスプライト
    [SerializeField]
    Sprite raritySprite = default;

    // レアリティの星のイメージ
    Image rarityImage = default;

    // ウサギのアイコンイメージ
    [SerializeField]
    Image rabbitIconImage = default;

    // スプライトアトラス
    [SerializeField]
    SpriteAtlas spriteAtlas = default;

    /// <summary>
    /// 開始
    /// </summary>
    void Awake()
    {
        // アトラスからウサギのアイコンをセットする
        rabbitIconImage.sprite = spriteAtlas.GetSprite(name);

        // ウサギごとに名前のローカライズをセットする
        rabbitNameLocalize.SetTerm("Rabbit/" + name);

        // レアリティのイメージを取得
        rarityImage = transform.GetChild(1).GetChild(0).GetChild(1).GetComponent<Image>();
        // イメージのスプライトをセットする
        rarityImage.sprite = raritySprite;
    }
}
