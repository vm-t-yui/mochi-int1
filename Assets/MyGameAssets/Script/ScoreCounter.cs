using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VMUnityLib;
using TMPro;

/// <summary>
/// スコアクラス
/// </summary>
public class ScoreCounter : SingletonMonoBehaviour<ScoreCounter>
{
    // 対象のオブジェクトのenum
    public enum TargetObject
    {
        Mochi,
        Rabbit,
        Length,
    }

    // [SerializeField]
    // MochiSample mochi = default;                                                         // もちクラスのサンプル

    [SerializeField]
    TextMeshProUGUI[] countText = default;                                                  // もちカウント用テキスト

    int[] getNum = new int[(int)TargetObject.Length];                                       // 壊した数の合計
    public int[] DisplayGetNum { get; private set; } = new int[(int)TargetObject.Length];   // 壊した数の合計(表示用)

    public bool[] IsCountUp { get; private set; } = new bool[(int)TargetObject.Length];                                                                  // カウントフラグ

    /// <summary>
    /// 開始処理
    /// </summary>
    void Start()
    {
        for (int i = 0; i < (int)TargetObject.Length; i++)
        {
            getNum[i] = 60;
            DisplayGetNum[i] = 0;
            IsCountUp[i] = false;
        }
    }

    /// <summary>
    /// スコアリセット
    /// </summary>
    public void Reset()
    {
        for (int i = 0; i < (int)TargetObject.Length; i++)
        {
            getNum[i] = 0;
            DisplayGetNum[i] = 0;
            IsCountUp[i] = false;
        }
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    void Update()
    {
        // 壊した数をカウント
        // getNum = mochi.getNum;
        // DisplayGetNum = getNum;
        for (int i = 0; i < (int)TargetObject.Length; i++)
        {
            countText[i].text = getNum[i].ToString();
        }
    }

    /// <summary>
    /// スコアのカウントアップ
    /// </summary>
    public int ScoreCountUp(int targetNum)
    {
        // カウントアップのコルーチン開始
        StartCoroutine(CountUp(targetNum));

        return DisplayGetNum[targetNum];
    }

    /// <summary>
    /// スコアのカウントアップのコルーチン
    /// </summary>
    /// <returns></returns>
    IEnumerator CountUp(int targetNum)
    {
        // カウントアップが終了するまで
        while (getNum[targetNum] > DisplayGetNum[targetNum])
        {
            // カウント
            DisplayGetNum[targetNum]++;

            yield return new WaitForSeconds(0.2f);
        }

        // カウント終了
        IsCountUp[targetNum] = true;
    }
}
