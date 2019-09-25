using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// フィーバータイムゲージの制御クラス
/// </summary>
public class FeverTimeActiveGaugeController : MonoBehaviour
{
    // フィーバーゲージ
    [SerializeField]
    Slider activeGauge = default;

    // タイマーのゲージ
    [SerializeField]
    Slider timerGauge = default;

    // フィーバータイムコントローラー
    [SerializeField]
    FeverTimeController feverTimeController = default;

    // プレイヤー
    [SerializeField]
    Transform playerTransform = default;

    // ゲージの最大量
    [SerializeField]
    float gaugeAmountMax = 0;
    public float GaugeAmountMax { get { return gaugeAmountMax; } }

    // ゲージの増分
    [SerializeField]
    float gaugeIncrement = 0;

    // ウサギをパンチしたときの減少値
    [SerializeField]
    int rabbitPunchDecreaseNum = 0;

    // ゲージの現在の量
    public float GaugeCurrentAmount { get; private set; } = 0;

    /// <summary>
    /// 起動処理
    /// </summary>
    void OnEnable()
    {
        // タイマーゲージに表示を切り替えるためフィーバーゲージ非表示に
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 更新
    /// </summary>
    void Update()
    {
        // フィーバータイムの残り時間をタイマーゲージにセット
        // NOTE: ゲージの現在と最大の量からスライダーの値を計算し、それを引くことによって残り時間を出しています。
        timerGauge.value = 1 - (1.0f * (GaugeCurrentAmount / gaugeAmountMax));

        // ゲージ減算量を計算
        float gaugeDecrement = gaugeAmountMax / feverTimeController.MaxFeverTimeCount;
        // ゲージを減らしていく
        GaugeCurrentAmount -= gaugeDecrement * Time.deltaTime;

        // ゲージが０になったら、フィーバーゲージを出してフィーバータイム終了
        if (feverTimeController.CurrentFeverTimeCount < 0)
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(true);
            }

            enabled = false;
        }

        // ゲージの現在の量が０以下になったら０を代入
        if (GaugeCurrentAmount < 0)
        {
            GaugeCurrentAmount = 0;
        }
    }

    /// <summary>
    /// ゲージを加算する
    /// </summary>
    public void AddGauge()
    {
        // フィーバー中は加算を行わない
        if (!feverTimeController.IsFever)
        {
            // ゲージを加算
            GaugeCurrentAmount += gaugeIncrement;

            // スライダーの値を更新
            // （ゲージの現在と最大の量からスライダーの値を計算）
            activeGauge.value = (1.0f * (GaugeCurrentAmount / gaugeAmountMax));
        }
    }

    /// <summary>
    /// ゲージの減少
    /// </summary>
    /// <param name="num">減少値</param>
    public void DecreaseGauge()
    {
        GaugeCurrentAmount -= rabbitPunchDecreaseNum;

        // スライダーの値を更新
        // （ゲージの現在と最大の量からスライダーの値を計算）
        activeGauge.value = (1.0f * (GaugeCurrentAmount / gaugeAmountMax));
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
