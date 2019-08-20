using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// バッタマンクラス
/// </summary>
public class BtmanController : MonoBehaviour
{
    Vector3 initPos = new Vector3(0, 15, 60);

    [SerializeField]
    Animator btmanPosAnim = default;    // バッタマンのポジションのアニメーター

    [SerializeField]
    Animator btmanAnim = default;       // バッタマンのアニメーター

    [SerializeField]
    Timer timer = default;              // タイマークラス

    [SerializeField]
    float apperTimeMin = 0;             // 出現するまでの秒数(最大)

    [SerializeField]
    float apperTimeMax = 0;             // 出現するまでの秒数(最小)

    [SerializeField]
    float fallTimeMin = 0;              // 落下するまでの秒数(最大)

    [SerializeField]
    float fallTimeMax = 0;              // 落下するまでの秒数(最小)

    float apperTime = 0;                // 出現するまでの秒数(確定)
    float fallTime = 0;                 // 落下するまでの秒数(確定)

    /// <summary>
    /// 起動処理
    /// </summary>
    void OnEnable()
    {
        // 初期位置設定
        transform.position = initPos;
        transform.localScale = Vector3.zero;

        // 出現、落下までの時間をランダムで設定
        apperTime = Random.Range(apperTimeMin, apperTimeMax);
        fallTime = Random.Range(fallTimeMin, fallTimeMax);
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    void Update()
    {
        // 出現までの時間をすぎたらバッタマン出現
        if (timer.IsStart && timer.CountTime < apperTime)
        {
            // まだアニメーターを起動していなかったら起動
            if (!btmanPosAnim.enabled)
            {
                btmanPosAnim.enabled = true;
            }

            // 落下までの時間をすぎたら落下
            if (fallTime < timer.CountTime - apperTime)
            {
                btmanPosAnim.SetTrigger("Fall");
                btmanAnim.SetTrigger("Fall");
            }
        }
    }

    /// <summary>
    /// 停止処理
    /// </summary>
    void OnDisable()
    {
        // 停止時に初期化
        btmanAnim.ResetTrigger("Fall");
        btmanPosAnim.ResetTrigger("Fall");
        btmanPosAnim.enabled = false;
    }
}
