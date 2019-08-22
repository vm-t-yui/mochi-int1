using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ウサギの耐えるゲージ
/// </summary>
public class RabbitEndureGauge : MonoBehaviour
{
    // 耐久時間の処理
    [SerializeField]
    RabbitEndureTimeCalculator rabbitEndureTimeCalculator = default;

    [SerializeField]
    Slider enduranceTimeGuage = default;    // 耐久時間ゲージ

    bool isCrushed = false;  // うさぎが吹っ飛んだフラグ

    /// <summary>
    /// 開始
    /// </summary>
    void OnEnable()
    {
        // ゲージを表示
        enduranceTimeGuage.gameObject.SetActive(true);
    }

    /// <summary>
    /// 吹っ飛んだフラグ受け渡し関数
    /// </summary>
    public bool GetIsCrushed()
    {
        bool flag = isCrushed;

        if (flag)
        {
            isCrushed = false;
        }

        return flag;
    }

    /// <summary>
    /// ゲージ減少処理
    /// </summary>
    void Update()
    {
        // ゲージ減少中の処理
        if (enduranceTimeGuage.value > 0)
        {
            // ゲージを減少していく
            enduranceTimeGuage.value -= 1.0f * (Time.deltaTime / rabbitEndureTimeCalculator.NowEnduranceTime);
        }
        // ゲージが０になれば
        else
        {
            // ゲージをリセットする
            enduranceTimeGuage.value = 1;
            // ウサギのクラッシュフラグをオンにする
            isCrushed = true;
        }
    }

    /// <summary>
    /// 終了
    /// </summary>
    void OnDisable()
    {
        // ゲージを非表示に設定
        enduranceTimeGuage.gameObject.SetActive(false);
        // ゲージの量をリセット
        enduranceTimeGuage.value = 1;
    }
}
