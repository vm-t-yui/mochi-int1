using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// エフェクト再生クラス
/// </summary>
public class EffectPlayer : MonoBehaviour
{
    enum EffectKind
    {
        RightPunch,     // 右パンチ
        LeftPunch,      // 左パンチ
        Chage,          // チャージ
        SpecialArts,    // 最後の大技
    }

    [SerializeField]
    GameObject[] effects = default;       // エフェクトのオブジェクト

    /// <summary>
    /// エフェクト再生
    /// </summary>
    /// <param name="kind"></param>
    public void PlayEffect(int kind)
    {
        // 番号に応じたエフェクトを再生
        switch (kind)
        {
            case (int)EffectKind.RightPunch:
                effects[(int)EffectKind.RightPunch].SetActive(true);
                effects[(int)EffectKind.LeftPunch].SetActive(false);
                break;
            case (int)EffectKind.LeftPunch:
                effects[(int)EffectKind.RightPunch].SetActive(false);
                effects[(int)EffectKind.LeftPunch].SetActive(true);
                break;
            case (int)EffectKind.Chage:
                effects[(int)EffectKind.Chage].SetActive(true);
                break;
            case (int)EffectKind.SpecialArts:
                effects[(int)EffectKind.SpecialArts].SetActive(true);
                break;
        }
    }

    /// <summary>
    /// エフェクト停止
    /// </summary>
    /// <param name="kind"></param>
    public void StopEffect(int kind)
    {
        // 番号に応じたエフェクトを停止
        switch (kind)
        {
            case (int)EffectKind.RightPunch: effects[(int)EffectKind.RightPunch].SetActive(false); break;
            case (int)EffectKind.LeftPunch: effects[(int)EffectKind.LeftPunch].SetActive(false); break;
            case (int)EffectKind.Chage: effects[(int)EffectKind.Chage].SetActive(false); break;
            case (int)EffectKind.SpecialArts: effects[(int)EffectKind.SpecialArts].SetActive(false); break;
        }
    }
}
