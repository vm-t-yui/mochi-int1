using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ギャラリーの表示クラス
/// </summary>
public class GalleryActivator : MonoBehaviour
{
    [SerializeField]
    Timer timer = default;              // タイマークラス

    [SerializeField]
    GameObject[] gallerys = default;    // ギャラリー達

    /// <summary>
    /// 更新処理
    /// </summary>
    void Update()
    {
        // タイムアップ時に非表示にする
        if (timer.IsTimeup)
        {
            SetActive(false);
        }
        // それ以外は表示
        else
        {
            SetActive(true);
        }
    }

    /// <summary>
    /// 表示関数
    /// </summary>
    /// <param name="flg">trueなら表示、falseなら非表示</param>
    void SetActive(bool flg)
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