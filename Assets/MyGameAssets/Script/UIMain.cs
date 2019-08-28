using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VMUnityLib;

/// <summary>
/// メインゲーム時のUIクラス
/// </summary>
public class UIMain : CmnMonoBehaviour
{
#if USE_TWEEN
    uTweenAlpha tweenAlphe;
#endif

    [SerializeField]
    Timer timer = default;              // タイマークラス

    [SerializeField]
    GameObject[] gallerys = default;    // ギャラリー達

    bool isGalleryInactive = default;   // ギャラリーの非表示フラグ


    // 処理なし。メッセージ受信エラー避け.
    public override void Start() { }
    protected override void InitSceneChange() { }
    protected override void OnSceneDeactive() { }
    protected override void OnFadeInEnd() { }

    /// <summary>
    /// 起動処理
    /// </summary>
    void OnEnable()
    {
        // ギャラリーの初期化
        GallerySetActive(true);
        isGalleryInactive = false;
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    protected override void FixedUpdate()
    {
        // タイムアップ時にギャラリーを非表示にする
        if (timer.IsTimeup && !isGalleryInactive)
        {
            GallerySetActive(false);
            isGalleryInactive = true;
        }
    }

    /// <summary>
    /// ギャラリー表示関数
    /// </summary>
    /// <param name="flg">trueなら表示、falseなら非表示</param>
    void GallerySetActive(bool flg)
    {
        for (int i = 0; i < gallerys.Length; i++)
        {
            if (flg)
            {
                gallerys[i].gameObject.SetActive(true);
            }
            else
            {
                gallerys[i].gameObject.SetActive(false);
            }
        }
    }
}

