using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// フィーバータイム管理クラス
/// </summary>
public class FeverTimeController : MonoBehaviour
{
    [SerializeField]
    MainCameraAnimator mainCameraAnim = default;            // メインカメラアニメーター

    [SerializeField]
    float feverTimeCount = 0;                               // フィーバータイムの継続時間

    public bool IsFever { get; private set; } = default;    // フィーバーフラグ

    /// <summary>
    /// 起動処理
    /// </summary>
    void OnEnable()
    {
        // フィーバータイム開始
        IsFever = true;
		mainCameraAnim.AnimStart((int)MainCameraAnimator.AnimKind.FeverIn);
	}

    /// <summary>
    /// 更新処理
    /// </summary>
    void Update()
    {
        // カウントダウン
        feverTimeCount -= Time.deltaTime;

        // カウントダウンが終わったらスクリプトを停止して終了
        if (feverTimeCount < 0)
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
	}
}
