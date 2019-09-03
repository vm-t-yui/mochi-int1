using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VMUnityLib;

/// <summary>
/// フィーバータイム管理クラス
/// </summary>
public class FeverTimeController : MonoBehaviour
{
    [SerializeField]
    MainCameraAnimator mainCameraAnim = default;          // メインカメラアニメーター
    
    [SerializeField]
    float maxFeverTimeCount = 0;                          // フィーバータイムの最大時間
    // フィーバータイムの最大値を取得
    public float MaxFeverTimeCount { get { return maxFeverTimeCount; } }

    [SerializeField]
    float ChangeBgmFadeTime = 0.8f;                       // BGM切り替え時のフェードにかかる時間
    [SerializeField]
    float ReturnBgmFadeTime = 1.2f;                       // メインBGMに戻すときのフェードにかかる時間

    public float CurrentFeverTimeCount { get; private set; } = 0;                      // フィーバータイムの継続時間
    

    public bool IsFever { get; private set; } = default;  // フィーバーフラグ

    /// <summary>
    /// 起動処理
    /// </summary>
    void OnEnable()
    {
        // フィーバータイムを最大時間で初期化
        CurrentFeverTimeCount = maxFeverTimeCount;

        // フィーバータイム開始
        IsFever = true;
		mainCameraAnim.AnimStart((int)MainCameraAnimator.AnimKind.FeverIn);

        // フィーバータイムのBGMに切り替え
        BgmPlayer.Inst.ChangeBgm(BgmID.FeverTime, ChangeBgmFadeTime);
	}

    /// <summary>
    /// 更新処理
    /// </summary>
    void Update()
    {
        // カウントダウン
        CurrentFeverTimeCount -= Time.deltaTime;

        // カウントダウンが終わったらスクリプトを停止して終了
        if (CurrentFeverTimeCount < 0)
        {
            enabled = false;
        }
    }

    /// <summary>
    /// 停止処理
    /// </summary>
    void OnDisable()
    {
        // フィーバータイム終了
        IsFever = false;
		mainCameraAnim.AnimStart((int)MainCameraAnimator.AnimKind.FeverOut);

        // 元のBGMに戻す
        BgmPlayer.Inst.ReturnBgm(ReturnBgmFadeTime);
	}
}
