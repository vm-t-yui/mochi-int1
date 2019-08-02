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

    public bool isJunction { get; private set; } = false;               // 分岐フラグ

    [SerializeField]
    GameObject[] resultObject = new GameObject[(int)ResultKind.Lenght]; // それぞれのリザルトのオブジェクト

    /// <summary>
    /// 起動処理
    /// </summary>
    void OnEnable()
    {
        // うさぎを殴った回数が３回以上なら分岐させる
        if (GameDataManager.Inst.PlayData.PunchCount >= 3)
        {
            isJunction = true;
        }
        else
        {
            isJunction = false;
        }

        // リザルトの分岐状態に応じた処理
        if (!isJunction)
        {
            // 良い時
            resultObject[(int)ResultKind.Good].SetActive(true);
            resultObject[(int)ResultKind.But].SetActive(false);
        }
        else
        {
            // 悪い時
            resultObject[(int)ResultKind.Good].SetActive(false);
            resultObject[(int)ResultKind.But].SetActive(true);
        }
    }
}
