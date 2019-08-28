using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathologicalGames;

/// <summary>
/// タワーオブジェクトの
/// </summary>
public class TowerObjectParticlePlayer : MonoBehaviour
{
    // パーティクルのスポーンプール
    [SerializeField]
    SpawnPool particleSpawnPool = default;

    // 再生中のパーティクル
    List<Transform> playingParticles = new List<Transform>();

    /// <summary>
    /// パーティクルの再生
    /// </summary>
    /// <param name="name">パーティクルの名前</param>
    /// <param name="position">再生位置</param>
    /// <param name="rotation">再生角度</param>
    public void Play(string name,Vector3 position,Quaternion rotation)
    {
        // パーティクルをスポーン
        Transform playingParticle = particleSpawnPool.Spawn(name, position, rotation);
        // パーティクルをリストに追加
        playingParticles.Add(playingParticle);
    }

    /// <summary>
    /// 更新
    /// </summary>
    void Update()
    {
        foreach(Transform playingParticle in playingParticles)
        {
            // 再生が終了したら
            if (!playingParticle.gameObject.activeSelf)
            {
                // デスポーン
                particleSpawnPool.Despawn(playingParticle);
                // リストから削除
                playingParticles.Remove(playingParticle);
            }
        }
    }
}
