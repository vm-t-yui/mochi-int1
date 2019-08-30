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
    Camera    viewCamera = default;                                             // エフェクトを映すカメラ
    
    [SerializeField]
    RawImage  rawImage   = default;                                             // RenderTextureを反映させるやつ

    [SerializeField][Range(0.1f, 1f)]
    float textureScale = 1;                                                     // 生成するRenderTextureのサイズの倍率

    RenderTexture renderTexture = default;                                      // RenderTextureのインスタンス

    List<ParticleSystem> endWatchTapEffectList = new List<ParticleSystem>();    // タップエフェクトの終了検知リスト

    ParticleSystem swipeEffect;                                                 // スワイプエフェクトのインスタンス

    const string TapEffectID   = "TapEffect";                                   // タップエフェクトのID
    const string SwipeEffectID = "SwipeEffect";                                 // スワイプエフェクトのID

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
    /// 更新処理.
    /// </summary>
    void Update()
    {
#if UNITY_EDITOR
        // マウスの位置からタップ位置を取得
        Vector3 touchPos = viewCamera.ScreenToWorldPoint(Input.mousePosition * textureScale + Vector3.forward * EffectZ);

        // クリックされた時
        if (Input.GetMouseButtonDown(0))
        {
            // タップ位置でエフェクト再生
            PlayEffect(touchPos);
        }
        // 押し続けている間
        if (Input.GetMouseButton(0))
        {
            // スワイプエフェクトの位置をタップ位置に固定
            swipeEffect.transform.position = touchPos;
        }
        // ボタンが離された時
        if (Input.GetMouseButtonUp(0))
        {
            // スワイプエフェクトを停止
            swipeEffect.Stop();

            // スワイプエフェクトをデスポーン
            effectPool.Despawn(swipeEffect.transform);
        }
#else
        // タッチカウントが0以上の時
        if (Input.touchCount > 0)
        {
            // タップ位置取得
            Vector3 touchPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position * textureScale) + Vector3.forward * EffectZ;

            // タップされた時
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                // タップ位置でエフェクト再生
                PlayEffect(touchPos);
            }
            // スワイプ時
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                // スワイプエフェクトの位置をタップ位置に固定
                swipeEffect.transform.position = touchPos;
            }
            // 指が離された時
            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                // スワイプエフェクトを停止
                swipeEffect.Stop();

                // スワイプエフェクトをデスポーン
                effectPool.Despawn(swipeEffect.transform);
            }
        }
        // タッチしていない時
        else
        {
            // 再生中なら消す
            if (swipeEffect.isPlaying)
            {
                swipeEffect.Stop();
            }
        }
#endif
    }

    /// <summary>
    /// エフェクト再生.
    /// </summary>
    void PlayEffect(Vector3 pos)
    {
        // 各エフェクトをスポーン
        Transform tapEffectTrans = effectPool.Spawn(TapEffectID);
        Transform swipeEffectTrans = effectPool.Spawn(SwipeEffectID);

        // 各エフェクトのコンポーネントを取得
        ParticleSystem tapEffect = tapEffectTrans.GetComponent<ParticleSystem>();
        swipeEffect = swipeEffectTrans.GetComponent<ParticleSystem>();

        // 指定された位置に各エフェクトを配置
        tapEffectTrans.position = pos;
        swipeEffectTrans.position = pos;

        // 再生
        tapEffect.Play();
        swipeEffect.Play();

        // 終了検知リストに追加
        endWatchTapEffectList.Add(tapEffect);
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
