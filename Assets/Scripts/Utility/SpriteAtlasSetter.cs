using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

/// <summary>
/// スプライトデータ
/// </summary>
[System.Serializable]
public class SpriteData
{
    // セットするアトラスのスプライト
    public string atlasSpriteName = default;

    // セットするイメージ
    public Image image = default;
}

/// <summary>
/// アトラスからスプライトをセットするクラス
/// </summary>
public class SpriteAtlasSetter : MonoBehaviour
{
    // スプライトアトラス
    [SerializeField]
    SpriteAtlas spriteAtlas = default;

    // スプライトデータのリスト
    [SerializeField]
    List<SpriteData> spriteDatas = default;

    /// <summary>
    /// 起動処理
    /// </summary>
    void Awake()
    {
        foreach(SpriteData spriteData in spriteDatas)
        {
            // アトラスのスプライトを各イメージにセットする
            spriteData.image.sprite = spriteAtlas.GetSprite(spriteData.atlasSpriteName);
        }
    }
}
