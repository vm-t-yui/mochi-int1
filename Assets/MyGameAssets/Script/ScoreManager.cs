﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using VMUnityLib;

public class ScoreManager : MonoBehaviour
{
    // 対象のオブジェクトのenum
    public enum TargetObject
    {
        Mochi,     // もち
        Rabbit,    // うさぎ
        Length,    // enumの長さ
    }

    // [SerializeField]
    // MochiSample mochi = default;                     // もちクラスのサンプル

    [SerializeField]
    TextMeshProUGUI countText = default;                // もちカウント用テキスト

    [SerializeField]
    ScoreCounter counter = default;                     // もちカウント用テキスト

    int getNum = 60;                                    // 壊した数の合計
    
    public int DisplayGetNum { get; private set; } = 0; // 壊した数の合計(表示用)

    /// <summary>
    /// 起動処理
    /// </summary>
    void OnEnable()
    {
        if (counter == null)
        {
            // 初期化
            Reset();
        }
        else
        {
            // データから最終スコアを持ってくる
            getNum = GameDataManager.Inst.PlayData.LastScore;

            // カウントアップ開始
            counter.ScoreCountUp(getNum);
        }
    }

    /// <summary>
    /// 終了処理
    /// </summary>
    void OnDisable()
    {
        // スコアをデータに入れセーブ
        GameDataManager.Inst.PlayData.LastScore = getNum;
        JsonDataSaver.Save(GameDataManager.Inst.PlayData);
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    void Update()
    {
        if (counter == null)
        {
            // 壊した数をカウント
            // getNum = mochi.getNum;
            // DisplayGetNum = getNum;
            countText.text = getNum.ToString();
        }
        else
        {
            // カウントアップ中のテキスト表示
            countText.text = counter.NowScore.ToString();
        }
    }

    /// <summary>
    /// スコアリセット
    /// </summary>
    public void Reset()
    {
        // 各値リセット
        for (int i = 0; i < (int)TargetObject.Length; i++)
        {
            getNum = 60;
            DisplayGetNum = 0;
        }
    }
}
