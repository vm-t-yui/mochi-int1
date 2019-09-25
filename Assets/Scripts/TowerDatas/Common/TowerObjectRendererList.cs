using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// タワーオブジェクト全てのレンダラーを管理
/// </summary>
public class TowerObjectRendererList : MonoBehaviour
{
    // モチのスポーンポール
    [SerializeField]
    Transform mochiSpawnPool = default;

    // ウサギのスポーンプール
    [SerializeField]
    Transform rabbitSpawnPool = default;

    // オブジェクトの制御クラスを管理するDictionary
    Dictionary<string, SkinnedMeshRenderer[]> meshRenderers = new Dictionary<string, SkinnedMeshRenderer[]>();
    public IReadOnlyDictionary<string, SkinnedMeshRenderer[]> MeshRenderers { get { return meshRenderers; } }

    /// <summary>
    /// 開始
    /// </summary>
    void Start()
    {
        // モチの全てのレンダラーを取得
        foreach (Transform mochi in mochiSpawnPool)
        {
            // モチにアタッチしているrレンダラーを取得する
            SkinnedMeshRenderer[] meshRenderer = mochi.GetChild(0).GetComponentsInChildren<SkinnedMeshRenderer>();
            // 取得したレンダラーをオブジェクト名とセットで追加
            meshRenderers.Add(mochi.gameObject.name, meshRenderer);
        }

        // ウサギの全てのレンダラーを取得
        foreach (Transform rabbit in rabbitSpawnPool)
        {
            // ウサギにアタッチしているレンダラーを取得する
            SkinnedMeshRenderer[] meshRenderer = rabbit.GetChild(0).GetComponentsInChildren<SkinnedMeshRenderer>();
            // 取得したレンダラーをオブジェクト名とセットで追加
            meshRenderers.Add(rabbit.gameObject.name, meshRenderer);
        }
    }
}
