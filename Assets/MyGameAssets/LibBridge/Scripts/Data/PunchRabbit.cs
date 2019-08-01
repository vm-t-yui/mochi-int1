using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchRabbit
{
    [SerializeField]
    int punchCount = 0;    // リザルトの分岐フラグ

    /// <summary>
    /// 各データのプロパティ
    /// </summary>
    public int PunchCount { get { return punchCount; } set { punchCount = value; } }
}
