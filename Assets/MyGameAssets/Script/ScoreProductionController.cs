﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// スコアカウント時の演出管理クラス
/// </summary>
public class ScoreProductionController : MonoBehaviour
{
    [SerializeField]
    ScoreCountUpper countUp = default;  // スコアカウントアップクラス

    [SerializeField]
    GameObject[] scoreMochi = default;  // カウントアップ時にスコアに応じて増減するもちのオブジェクト

    /// <summary>
    /// 起動処理
    /// </summary>
    void OnEnable()
    {
        // 最初は全て非表示
        foreach (var item in scoreMochi)
        {
            item.SetActive(false);
        }
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    void Update()
    {
        // カウント中のスコアに応じてそれぞれのオブジェクトを表示
        if(countUp.NowCount > ScoreManager.LowScore)
        {
            scoreMochi[(int)ScoreManager.Score.Low].SetActive(true);
        }
        if(countUp.NowCount > ScoreManager.NormalScore)
        {
            scoreMochi[(int)ScoreManager.Score.Normal].SetActive(true);
        }
        if(countUp.NowCount > ScoreManager.GoodScore)
        {
            scoreMochi[(int)ScoreManager.Score.Good].SetActive(true);
        }
    }
}