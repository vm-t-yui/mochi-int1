using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// スコアカウントクラス
/// </summary>
public class ScoreCounter : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI text = default;     // スコアテキスト

    /// <summary>
    /// 更新処理
    /// </summary>
    void Update()
    {
        // 現在のスコアをテキストに反映
        text.text = ScoreManager.Inst.NowBreakNum.ToString();
    }
}
