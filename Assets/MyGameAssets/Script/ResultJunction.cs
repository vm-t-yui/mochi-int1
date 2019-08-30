using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// リザルトの分岐クラス
/// </summary>
public class ResultJunction : MonoBehaviour
{
    // リザルトの種類
    enum ResultKind
    {
        Good,       // 良
        But,        // 悪
        Lenght,     // enumの長さ
    }

    public bool IsJunction { get; private set; } = false;               // 分岐フラグ

    [SerializeField]
    GameObject[] resultObject = new GameObject[(int)ResultKind.Lenght]; // それぞれのリザルトのオブジェクト

    [SerializeField]
    GameObject[] brokenRabbit = default;                                // 骨折したうさぎたち

    /// <summary>
    /// 起動処理
    /// </summary>
    void OnEnable()
    {
        // 骨折うさぎを非表示
        brokenRabbit[0].SetActive(false);
        brokenRabbit[1].SetActive(false);

        // うさぎを殴った回数が３回以上なら分岐させる
        if (GameDataManager.Inst.PlayData.PunchCount >= 3)
        {
            // 良い時
            GoodResult();
            IsJunction = true;
        }
        else
        {
            // 悪い時
            ButResult();
            IsJunction = false;
        }
    }

    /// <summary>
    /// 良いリザルト用
    /// </summary>
    void GoodResult()
    {
        // オブジェクトを表示
        resultObject[(int)ResultKind.Good].SetActive(true);
        resultObject[(int)ResultKind.But].SetActive(false);

        // うさぎが殴られた回数に応じて骨折うさぎを表示
        for(int i = 0; i < GameDataManager.Inst.PlayData.PunchCount; i++)
        {
            brokenRabbit[i].SetActive(true);
        }
    }

    /// <summary>
    /// 悪いリザルト用
    /// </summary>
    void ButResult()
    {
        // オブジェクト表示
        resultObject[(int)ResultKind.Good].SetActive(false);
        resultObject[(int)ResultKind.But].SetActive(true);
    }
}
