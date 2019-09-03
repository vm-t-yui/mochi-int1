using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class MainSpriteSetter : MonoBehaviour
{
    [SerializeField]
    SpriteAtlas atlas = default;    // スプライトアトラス

    [SerializeField]
    Image mochi = default;          // もち

    [SerializeField]
    Image timer = default;          // タイマー

    [SerializeField]
    Image[] galley = default;       // ギャラリー

    /// <summary>
    /// 起動処理.
    /// </summary>
    void Awake()
    {
        // 各スプライトをアタッチ
        mochi.sprite = atlas.GetSprite(SpriteName.NormalMochi);
        timer.sprite = atlas.GetSprite(SpriteName.Timer);

        for (int i = 0; i < galley.Length; i++)
        {
            galley[i].sprite = atlas.GetSprite(SpriteName.Galley);
        }
    }
}