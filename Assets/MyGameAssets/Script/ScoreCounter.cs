using System.Collections;
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

            yield return new WaitForSeconds(0.1f);
        }
    }
}
