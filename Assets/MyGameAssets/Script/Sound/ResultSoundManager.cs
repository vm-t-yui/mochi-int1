using UnityEngine;
using UnityEngine.UI;
using VMUnityLib;

/// <summary>
/// リザルトのサウンド管理クラス
/// </summary>
public class ResultSoundManager : MonoBehaviour
{
    [SerializeField]
    Button[]        selectSeButtons     = default,    // 決定音を再生するボタン
                    windowOpenSeButtons = default,    // ウィンドウオープン音を再生するボタン
                    cancelSeButtons     = default,    // キャンセル音を再生するボタン
                    tapSeButtons        = default;    // タップ音を再生するボタン

    [SerializeField]
    ScoreCountUpper scoreCountUpper     = default;    // スコアカウントクラス

    const float     WaitTimeBgm  = 1.8f;              // BGMを再生するまでの待ち時間

    bool            isDramRoll   = false;             // ドラムロール用フラグ
    bool            isPlayed     = false;             // 再生完了フラグ

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
            item.onClick.AddListener(() => BgmPlayer.Inst.FadeOut(0.5f));
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
        // ドラムロール音を再生
        if (!isDramRoll)
        {
            SePlayer.Inst.PlaySe(SeID.DramRoll);

            isDramRoll = true;
        }

        // スコアのカウントアップが終わったら
        if (scoreCountUpper.IsEnd && !isPlayed)
        {
            // 一度すべての効果音を停止
            SePlayer.Inst.StopSeAll();
            // ドラムロール終了音を再生
            SePlayer.Inst.PlaySe(SeID.RollFinish);

            // 指定時間待ってBGMを再生
            Invoke("PlaySuitableBgm", WaitTimeBgm);

            isPlayed = true;
        }
    }

    /// <summary>
    /// 条件に応じたBGMを再生
    /// </summary>
    void PlaySuitableBgm()
    {
        // うさぎを3回以上殴った時
        if (GameDataManager.Inst.PlayData.PunchCount >= 3)
        {
            BgmPlayer.Inst.PlayResultBgm(BgmID.Bad);
        }
        // グッドスコア時
        else if (GameDataManager.Inst.PlayData.LastScore >= ScoreManager.NormalScore)
        {
            BgmPlayer.Inst.PlayResultBgm(BgmID.GoodScore);
        }
        // ロースコア時
        else
        {
            BgmPlayer.Inst.PlayResultBgm(BgmID.LowScore);
        }
    }

    /// <summary>
    /// 終了処理
    /// </summary>
    void OnDisable()
    {
        isDramRoll = false;
        isPlayed = false;

        SePlayer.Inst.StopSeAll();
    }
}
