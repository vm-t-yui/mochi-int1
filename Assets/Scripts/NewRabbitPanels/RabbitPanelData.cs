using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using I2.Loc;

/// <summary>
/// ウサギのパネルのデータ
/// </summary>
public class RabbitPanelData : MonoBehaviour
{
    // レアリティのスプライト
    [SerializeField]
    Sprite raritySprite = default;

    // レアリティの星のイメージ
    Image rarityImage = default;

    // ウサギの名前のローカライズデータ
    Localize rabbitNameLocalize = default;

    /// <summary>
    /// 開始
    /// </summary>
    void Start()
    {
        // ローカライズデータを取得
        rabbitNameLocalize = transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Localize>();
        // ウサギごとに名前のローカライズをセットする
        rabbitNameLocalize.SetTerm("Rabbit/" + name);

        // レアリティのイメージを取得
        rarityImage = transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<Image>();
        // イメージのスプライトをセットする
        rarityImage.sprite = raritySprite;
    }
}
