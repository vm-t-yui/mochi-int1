﻿using UnityEngine;
using UnityEngine.UI;
using VMUnityLib;

/// <summary>
/// タイトルのサウンド管理クラス
/// </summary>
public class TitleSoundManager : MonoBehaviour
{
    [SerializeField]
    Button[] selectSeButtons     = default,    // 決定音を再生するボタン
             windowOpenSeButtons = default,    // ウィンドウオープン音を再生するボタン
             cancelSeButtons     = default,    // キャンセル音を再生するボタン
             tapSeButtons        = default;    // タップ音を再生するボタン

    bool     isPlayed            = false;      // 再生完了フラグ

    /// <summary>
    /// 初回起動時処理
    /// </summary>
    void Awake()
    {
        // 各ボタンにSE再生処理を追加
        // スタート音再生ボタン
        foreach (var item in selectSeButtons)
        {
            item.onClick.AddListener(() => SePlayer.Inst.PlaySe(SeID.Start));
            item.onClick.AddListener(() => BgmPlayer.Inst.FadeOut(1f));
        }
        // セレクト音再生ボタン
        foreach (var item in windowOpenSeButtons)
        {
            item.onClick.AddListener(() => SePlayer.Inst.PlaySe(SeID.Select));
        }
        // キャンセル音再生ボタン
        foreach (var item in cancelSeButtons)
        {
            item.onClick.AddListener(() => SePlayer.Inst.PlaySe(SeID.Cancel));
        }
        // タップ音再生ボタン
        foreach (var item in tapSeButtons)
        {
            item.onClick.AddListener(() => SePlayer.Inst.PlaySe(SeID.Tap));
        }
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    void Update()
    {
        // BGMを再生
        if (!isPlayed)
        {
            BgmPlayer.Inst.PlayBgm(BgmID.Title);

            isPlayed = true;
        }
    }

    /// <summary>
    /// 終了処理
    /// </summary>
    void OnDisable()
    {
        isPlayed = false;

        SePlayer.Inst.StopSeAll();
    }
}
