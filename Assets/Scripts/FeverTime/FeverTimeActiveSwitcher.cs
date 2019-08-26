﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// フィーバータイムの発動を制御する
/// </summary>
public class FeverTimeActiveSwitcher : MonoBehaviour
{
    //　フィーバータイム制御クラス
    [SerializeField]
    FeverTimeController feverTimeController = default;

    // フィーバータイム用ゲージの制御クラス
    [SerializeField]
    FeverTimeActiveGaugeController feverTimeActiveGaugeController = default;

    // タイマークラス
    [SerializeField]
    Timer timer = default;

    /// <summary>
    /// 更新
    /// </summary>
    void Update()
    {
        // ゲージの表示位置を更新
        feverTimeActiveGaugeController.UpdateGaugePos();

        // ゲージが溜まったら
        if (feverTimeActiveGaugeController.GaugeCurrentAmount >=
            feverTimeActiveGaugeController.GaugeAmountMax)
        {
            // フィーバータイムの制御をオンにする
            feverTimeController.enabled = true;
            // ゲージの制御をオンにする
            feverTimeActiveGaugeController.enabled = true;
        }

        // タイムアップと同時に非表示
        if (timer.IsTimeup)
        {
            feverTimeActiveGaugeController.gameObject.SetActive(false);
        }
    }
}
