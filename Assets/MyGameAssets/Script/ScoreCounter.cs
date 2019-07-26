using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VMUnityLib;
using UnityEngine.UI;

/// <summary>
/// スコアクラス
/// </summary>
public class ScoreCounter : SingletonMonoBehaviour<ScoreCounter>
{
    // [SerializeField]
    // MochiSample mochi = default;                         // もちクラスのサンプル

    int breakNum  = 60;                                     // 壊した数の合計
    public int DisplayBreakNum { get; private set; } = 60;   // 壊した数の合計(表示用)
    bool isCountUp = false;

    /// <summary>
    /// スコアリセット
    /// </summary>
    public void Reset()
    {
        breakNum = 0;
        DisplayBreakNum = 0; 
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    void Update()
    {
        if (!isCountUp)
        {
            // 壊した数をカウント
            // breakNum = mochi.breakNum;
            // DisplayBreakNum = breakNum; 
        }
    }

    /// <summary>
    /// スコアのカウントアップ
    /// </summary>
    public void ScoreCountUp()
    {
        // カウントアップ前処理
        if (!isCountUp)
        {
            isCountUp = true;
            DisplayBreakNum = 0;
        }

        // カウントアップのコルーチン開始
        StartCoroutine(CountUp());
    }

    /// <summary>
    /// スコアのカウントアップのコルーチン
    /// </summary>
    /// <returns></returns>
    IEnumerator CountUp()
    {
        // カウントアップが終了するまで
        while (breakNum > DisplayBreakNum)
        {
            // カウント
            DisplayBreakNum++;

            yield return new WaitForSeconds(0.1f);
        }

        isCountUp = false;
    }
}
