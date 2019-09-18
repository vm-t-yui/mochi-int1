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
    // ウサギの名前のローカライズデータ
    [SerializeField]
    Localize rabbitNameLocalize = default;

    // レアリティのスプライト
    [SerializeField]
    Sprite raritySprite = default;

    // レアリティの星のイメージ
    Image rarityImage = default;

    /// <summary>
    /// 開始
    /// </summary>
    void Start()
    {
        // ウサギごとに名前のローカライズをセットする
        rabbitNameLocalize.SetTerm("Rabbit/" + name);

        // レアリティのイメージを取得
        rarityImage = transform.GetChild(1).GetChild(0).GetChild(1).GetComponent<Image>();
        // イメージのスプライトをセットする
        rarityImage.sprite = raritySprite;
    }
}
