using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// スコアカウントアップクラス
/// </summary>
public class ScoreCountUpper : MonoBehaviour
{
    public float NowCount { get; private set; } = 0;    // 現在のカウント

    public bool IsStart { get; private set; } = false;  // 開始フラグ
    public bool IsEnd { get; private set; } = false;    // 終了フラグ

    [SerializeField]
    UIResult uIResult = default;                        // リザルトのUI

    [SerializeField]
    float waitTime = 5.0f;                              // カウントにかかるの時間

    [SerializeField]
    TextMeshProUGUI text = default;                     // テキスト

    [SerializeField]
    GameObject highScoreText = default;                 // ハイスコアテキスト

    [SerializeField]
    ResultPlayerAnimator playerAnim = default;          // プレイヤーのアニメーション

    /// <summary>
    /// 起動処理
    /// </summary>
    void OnEnable()
    {
        NowCount = 0;
        IsEnd = false;
        IsStart = true;
        highScoreText.SetActive(false);
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    void Update()
    {
        // カウント開始されたらスコアをカウントアップ
        if (IsStart && !IsEnd)
        {
            NowCount += (ScoreManager.Inst.NowBreakNum * (Time.deltaTime / waitTime));

            // タッチされたらスキップ
            if(Input.touchCount > 0)
            {
                NowCount = ScoreManager.Inst.NowBreakNum;
            }

            // カウントし終わったら
            if (ScoreManager.Inst.NowBreakNum <= NowCount)
            {
                // カウントダウン終了
                IsEnd = true;
                playerAnim.AnimStart((int)ResultPlayerAnimator.AnimKind.ScoreResult);

                // ハイスコアを超えたらハイスコアの文字表示
                if (GameDataManager.Inst.PlayData.HighScore < ScoreManager.Inst.NowBreakNum)
                {
                    highScoreText.SetActive(true);
                }
                for (int i = 0; i < GameDataManager.Inst.PlayData.IsDrawSkinNewIcon.Length; i++)
                {
                    if (!GameDataManager.Inst.PlayData.IsDrawSkinNewIcon[i])
                    {
                        GameDataManager.Inst.PlayData.IsNewReleasedSkin = true;
                        GameDataManager.Inst.PlayData.IsDrawSkinNewIcon[i] = true;
                    }
                }
                // スコアリセット
                ScoreManager.Inst.Reset();
            }
        }

        // テキストにセット
        text.text = ((int)NowCount).ToString();
    }
}