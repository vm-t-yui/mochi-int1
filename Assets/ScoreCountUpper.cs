using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// スコアカウントアップクラス
/// </summary>
public class ScoreCountUpper : MonoBehaviour
{
    public int NowScore { get; private set; } = 0;
    public bool IsEnd { get; private set; } = false;
    bool isStart = false;

    [SerializeField]
    float waitTime = 5.0f;

    /// <summary>
    /// 起動処理
    /// </summary>
    void OnEnable()
    {
        isStart = true;
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    void Update()
    {
        // カウント開始されたらスコアをカウントアップ
        if (isStart && !IsEnd)
        {
            NowScore += (int)(ScoreManager.Inst.NowBreakNum * (Time.deltaTime / waitTime));

            if (ScoreManager.Inst.NowBreakNum <= NowScore)
            {
                // カウントダウン終了
                IsEnd = true;
            }
        }
    }

    /// <summary>
    /// 停止処理
    /// </summary>
    void OnDisable()
    {
        // リセット
        NowScore = 0;
        IsEnd = false;
        ScoreManager.Inst.Reset();
    }
}