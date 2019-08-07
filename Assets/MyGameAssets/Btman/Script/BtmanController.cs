using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// バッタマンクラス
/// </summary>
public class BtmanController : MonoBehaviour
{
    float flyTime = 0;                                      // 飛べる時間

    bool isFly = true;                                      // 飛べるかどうかのフラグ

    Vector3 initPos = new Vector3(10.0f, -40.0f, 100.0f); 　// 初期位置

    [SerializeField]
    GameObject flyParticle = default;                       // 飛んでる時の煙パーティクル

    [SerializeField]
    Animator btmanAnim = default;                           // バッタマンのアニメーター

    [SerializeField]
    int minTime = 0;                                        // 飛べる時間の最小

    [SerializeField]
    int maxTime = 0;                                        // 飛べる時間の最大

    [SerializeField]
    Timer timer = default;                                  // タイマークラス

    /// <summary>
    /// 起動処理
    /// </summary>
    void OnEnable()
    {
        // 各値初期化
        transform.position = initPos;
        flyTime = Random.Range(minTime, maxTime);
        isFly = true;
        flyParticle.SetActive(true);
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    void Update()
    {
        // 飛べない状態なら落下
        if (!isFly)
        {
            transform.position += Vector3.down;
        }
        // 飛べる状態なら時間をマイナスしていきながら上昇
        else if (timer.IsStart)
        {
            transform.position += Vector3.up;
            flyTime--;

            // 時間がマイナスになるか、タイムアップになったら落下開始
            if (flyTime < 0 || timer.IsTimeup)
            {
                Fall();
            }
        }
    }

    void Fall()
    {
        isFly = false;
        flyParticle.SetActive(false);
        btmanAnim.SetTrigger("Fall");
    }
}
