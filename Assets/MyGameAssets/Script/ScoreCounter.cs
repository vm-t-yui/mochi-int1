﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using VMUnityLib;

/// <summary>
/// スコアクラス
/// </summary>
public class ScoreCounter : MonoBehaviour
{
    public int NowScore { get; private set; } = 0;
    public bool IsEnd { get; private set; } = false;

    /// <summary>
    /// スコアのカウントアップ
    /// </summary>
    public void ScoreCountUp(int maxScore)
    {
        // カウントアップのコルーチン開始
        StartCoroutine(CountUp(maxScore));
    }

    /// <summary>
    /// スコアのカウントアップのコルーチン
    /// </summary>
    /// <returns></returns>
    IEnumerator CountUp(int maxScore)
    {
        // カウントアップが終了するまで
        while (maxScore > NowScore)
        {
            NowScore++;

            yield return new WaitForSeconds(0.05f);
        }

        // カウントダウン終了
        IsEnd = true;
    }

    /// <summary>
    /// リセット
    /// </summary>
    public void Reset()
    {
        NowScore = 0;
        IsEnd = false;
    }
}
