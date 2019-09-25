using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 負傷したウサギたちの生成クラス
/// </summary>
public class DamageRabbitSpawer : MonoBehaviour
{
    [SerializeField]
    ResultPlayerAnimator playerAnim = default;  // プレイヤーアニメーション

    [SerializeField]
    GameObject[] brokenRabbit = default;        // 負傷したうさぎたち

    /// <summary>
    /// 起動処理
    /// </summary>
    void OnEnable()
    {
        // リザルトアニメーション開始
        playerAnim.AnimStart((int)ResultPlayerAnimator.AnimKind.Result);

        // うさぎの表示非表示
        RabbitSetActive(true);
    }

    /// <summary>
    /// うさぎの一括SetActive関数
    /// </summary>
    /// <param name="flag">trueなら表示、falseなら非表示</param>
    void RabbitSetActive(bool flag)
    {
        for (int i = 0; i < brokenRabbit.Length; i++)
        {
            // flagがfalseなら一括非表示
            if (!flag)
            {
                brokenRabbit[i].SetActive(false);
            }
            // flagがtrueならパンチされた数だけ表示
            else if(GameDataManager.Inst.PlayData.PlayCount >= i)
            {
                brokenRabbit[i].SetActive(true);
            }
        }
    }
}
