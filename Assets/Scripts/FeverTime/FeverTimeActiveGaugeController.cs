using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// フィーバータイムゲージの制御クラス
/// </summary>
public class FeverTimeActiveGaugeController : MonoBehaviour
{
    // ゲージ
    [SerializeField]
    Slider activeGauge = default;

    // ゲージのトランスフォーム
    [SerializeField]
    RectTransform gaugeTransform = default;

    // フィーバータイムコントローラー
    [SerializeField]
    FeverTimeController feverTimeController = default;

    // プレイヤー
    [SerializeField]
    Transform playerTransform = default;

    // ゲージの表示位置オフセット
    [SerializeField]
    Vector3 gaugePosOffset = Vector3.zero;

    // カメラ
    [SerializeField]
    Camera targetCamera = default;

    // ゲージの最大量
    [SerializeField]
    float gaugeAmountMax = 0;
    public float GaugeAmountMax { get { return gaugeAmountMax; } }

    // ゲージの増分
    [SerializeField]
    float gaugeIncrement = 0;

    // ゲージの現在の量
    public float GaugeCurrentAmount { get; private set; } = 0;

    /// <summary>
    /// 更新
    /// </summary>
    void Update()
    {
        // ゲージの表示位置を更新
        UpdateGaugePos();

        // ゲージの現在と最大の量からスライダーの値を計算
        activeGauge.value = (1.0f * (GaugeCurrentAmount / gaugeAmountMax));

        // ゲージ減算量を計算
        float gaugeDecrement = gaugeAmountMax / feverTimeController.MaxFeverTimeCount;
        // ゲージを減らしていく
        GaugeCurrentAmount -= gaugeDecrement * Time.deltaTime;

        // ゲージが０になったら、終了
        if ((int)GaugeCurrentAmount < 0)
        {
            enabled = false;
        }
    }

    /// <summary>
    /// ゲージを加算する
    /// </summary>
    public void AddGauge()
    {
        // ゲージを加算
        GaugeCurrentAmount += gaugeIncrement;
        
        // スライダーの値を更新
        // （ゲージの現在と最大の量からスライダーの値を計算）
        activeGauge.value = (1.0f * (GaugeCurrentAmount / gaugeAmountMax));
    }

    /// <summary>
    /// ゲージの表示位置を更新
    /// </summary>
    public void UpdateGaugePos()
    {
        // プレイヤーにオフセット値を加えたものをスクリーン座標に変換
        Vector3 playerScreenPos = targetCamera.WorldToScreenPoint(playerTransform.position + gaugePosOffset);
        //変換したものをゲージの表示位置にする
        gaugeTransform.position = playerScreenPos;
    }

    /// <summary>
    /// ゲージをリセットする
    /// </summary>
    public void ResetGauge()
    {
        // ゲージを０にする
        GaugeCurrentAmount = 0;
        // スライダーを０で初期化
        activeGauge.value = 0;
    }

    /// <summary>
    /// 終了
    /// </summary>
    void OnDisable()
    {
        // ゲージをリセットする
        ResetGauge();
    }
}
