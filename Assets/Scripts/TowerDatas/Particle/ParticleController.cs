using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// パーティクルの制御
/// </summary>
public class ParticleController : MonoBehaviour
{
    // パーティクル
    [SerializeField]
    ParticleSystem particleSystem = default;

    /// <summary>
    /// 更新
    /// </summary>
    void Update()
    {
        // 再生が終了したら
        if (!particleSystem.isPlaying)
        {
            // オブジェクトをオフにする
            gameObject.SetActive(false);
        }
    }
}
