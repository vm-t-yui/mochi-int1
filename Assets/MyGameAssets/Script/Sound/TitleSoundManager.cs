using UnityEngine;
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
    /// 起動処理
    /// </summary>
    void Awake()
    {
        // 各ボタンにSE再生処理を追加
        // 決定音再生ボタン
        foreach (var item in selectSeButtons)
        {
            item.onClick.AddListener(() => SePlayer.Inst.PlaySe(SeID.Select));
        }
        // ウィンドウオープン音再生ボタン
        foreach (var item in windowOpenSeButtons)
        {
            item.onClick.AddListener(() => SePlayer.Inst.PlaySe(SeID.WindowOpen));
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

        BgmPlayer.Inst.StopBgm();
    }
}
