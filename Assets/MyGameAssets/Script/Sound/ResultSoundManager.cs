using UnityEngine;
using VMUnityLib;

/// <summary>
/// リザルトのサウンド管理クラス
/// </summary>
public class ResultSoundManager : MonoBehaviour
{
    [SerializeField]
    BgmPlayer    bgmPlayer    = default;    // BGM再生クラス

    [SerializeField]
    SePlayer     sePlayer     = default;    // SE再生クラス

    [SerializeField]
    ScoreCountUpper scoreCountUpper = default;    // スコアカウントクラス

    const float  WaitTimeBgm  = 1.8f;       // BGMを再生するまでの待ち時間

    bool         isDramRoll   = false;      // ドラムロール用フラグ
    bool         isPlayed     = false;      // 再生完了フラグ

    /// <summary>
    /// 起動処理
    /// </summary>
    void OnEnable()
    {
        isDramRoll = false;
        isPlayed = false;
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    void Update()
    {
        // ドラムロール音を再生
        if (!isDramRoll)
        {
            sePlayer.PlaySe(SeID.DramRoll);

            isDramRoll = true;
        }

        // スコアのカウントアップが終わったら
        if (scoreCountUpper.IsEnd && !isPlayed)
        {
            // 一度すべての効果音を停止
            sePlayer.StopSeAll();
            // ドラムロール終了音を再生
            sePlayer.PlaySe(SeID.RollFinish);

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
        if (GameDataManager.Inst.PlayData.PunchCount > 3)
        {
            bgmPlayer.PlayResultBgm(BgmID.Bad);
        }
        // グッドスコア時
        if (GameDataManager.Inst.PlayData.LastScore >= ScoreManager.GoodScore)
        {
            bgmPlayer.PlayResultBgm(BgmID.GoodScore);
        }
        // ロースコア時
        else
        {
            bgmPlayer.PlayResultBgm(BgmID.LowScore);
        }
    }

    /// <summary>
    /// 終了処理
    /// </summary>
    void OnDisable()
    {
        bgmPlayer.StopBgm();
        sePlayer.StopSeAll();
    }
}
