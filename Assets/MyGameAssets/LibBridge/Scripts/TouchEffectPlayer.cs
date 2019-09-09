using System.Collections.Generic;
using UnityEngine;
using PathologicalGames;
using UnityEngine.UI;

/// <summary>
/// タップ・スワイプ時のエフェクト再生クラス
/// NOTO: m.tanaka 今のところ最初にタップした指のみエフェクトが出るようになってます。
/// </summary>
public class TouchEffectPlayer : MonoBehaviour
{
    [SerializeField]
    SpawnPool effectPool = default;                                             // エフェクトのプール

    [SerializeField]
    Camera viewCamera = default;                                             // エフェクトを映すカメラ

    [SerializeField]
    RawImage rawImage = default;                                             // RenderTextureを反映させるやつ

    [SerializeField]
    [Range(0.1f, 1f)]
    float textureScale = 1;                                                     // 生成するRenderTextureのサイズの倍率

    RenderTexture renderTexture = default;                                      // RenderTextureのインスタンス

    List<ParticleSystem> endWatchTapEffectList = new List<ParticleSystem>();    // タップエフェクトの終了検知リスト

    ParticleSystem swipeEffect;                                                 // スワイプエフェクトのインスタンス

    public const string TapEffectID = "TapEffect";                                   // タップエフェクトのID
    public const string SwipeEffectID = "SwipeEffect";                                 // スワイプエフェクトのID

    [SerializeField]
    float EffectZ = 7.0f;                                                       // エフェクトのZ軸座標

    /// <summary>
    /// 起動処理.
    /// </summary>
    void Awake()
    {
        // RenderTextureを画面サイズで生成
        renderTexture = new RenderTexture((int)(Screen.width * textureScale), (int)(Screen.height * textureScale), 0);

        // カメラとRawImageにRenderTextureを設定
        viewCamera.targetTexture = renderTexture;
        rawImage.texture = renderTexture;

        // カメラとRawImageを表示
        viewCamera.gameObject.SetActive(true);
        rawImage.gameObject.SetActive(true);
    }

    /// <summary>
    /// エフェクト再生
    /// </summary>
    public void PlayTapEffect()
    {
        // idに応じたエフェクトを指定の位置にスポーン
        Transform effectTrans = effectPool.Spawn(TapEffectID);
        ParticleSystem effect = effectTrans.GetComponent<ParticleSystem>();
        effectTrans.position = viewCamera.ScreenToWorldPoint(Input.mousePosition * textureScale + Vector3.forward * EffectZ);

        // 再生した後、終了検知リストに追加
        effect.Play();
        endWatchTapEffectList.Add(effect);
    }

    /// <summary>
    /// エフェクト再生
    /// </summary>
    public void PlaySwipeEffect()
    {
        // idに応じたエフェクトを指定の位置にスポーン
        Transform effectTrans = effectPool.Spawn(SwipeEffectID);
        swipeEffect = effectTrans.GetComponent<ParticleSystem>();
        effectTrans.position = viewCamera.ScreenToWorldPoint(Input.mousePosition * textureScale + Vector3.forward * EffectZ);

        // 再生
        swipeEffect.Play();
    }

    /// <summary>
    /// スワイプのポジションを更新
    /// </summary>
    public void UpdateSwipePosition()
    {
        // スワイプエフェクトの位置をタップ位置に固定
        swipeEffect.transform.position = viewCamera.ScreenToWorldPoint(Input.mousePosition * textureScale + Vector3.forward * EffectZ);
    }

    /// <summary>
    /// エフェクト停止
    /// </summary>
    public void StopEffect()
    {
        // スワイプエフェクトを停止
        swipeEffect.Stop();

        // スワイプエフェクトをデスポーン
        effectPool.Despawn(swipeEffect.transform);
    }

    /// <summary>
    /// 再生が終了したものをすべて削除する.
    /// </summary>
    void LateUpdate()
    {
        // 再生が終了しているすべてのタップエフェクトを取得
        List<ParticleSystem> removeTapList = endWatchTapEffectList.FindAll(tap => !tap.isPlaying);

        // 取得したすべてのタップエフェクトをデスポーン
        foreach (var item in removeTapList)
        {
            effectPool.Despawn(item.transform);

            endWatchTapEffectList.Remove(item);
        }
    }
}