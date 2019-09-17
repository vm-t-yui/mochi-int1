using UnityEngine;
using TMPro;

/// <summary>
/// レコード表示コントロールクラス
/// </summary>
public class RecordController : MonoBehaviour
{
    // 各レコードの数値テキスト
    [SerializeField]
    TMP_Text playCountText    = default,
             highScoreText    = default,
             totalScoreText   = default,
             rescuedCountText = default,
             punchedCountText = default;

    /// <summary>
    /// 起動処理
    /// </summary>
    void OnEnable()
    {
        // プレイデータのインスタンスを取得
        PlayData playData = GameDataManager.Inst.PlayData;

        // 各レコードの数値をUIに反映
        playCountText.text = playData.PlayCount.ToString("N0");
        highScoreText.text = playData.HighScore.ToString("N0");
        totalScoreText.text = playData.TotalScore.ToString("N0");
        rescuedCountText.text = playData.TotalRescueCount.ToString("N0");
        punchedCountText.text = playData.PunchCount.ToString("N0");
    }
}
