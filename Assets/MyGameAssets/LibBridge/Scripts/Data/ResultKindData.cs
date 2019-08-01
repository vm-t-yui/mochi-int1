using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultKindData
{
    [SerializeField]
    bool isJunction = false;    // リザルトの分岐フラグ

    /// <summary>
    /// 各データのプロパティ
    /// </summary>
    public bool IsJunction { get { return isJunction; } set { isJunction = value; } }
}
