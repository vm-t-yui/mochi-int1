using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// オブジェクトの落下制御を行う
/// </summary>
public class ObjectFallingController : MonoBehaviour
{
    // スポナークラス
    [SerializeField]
    TowerObjectSpawner towerObjectSpawner = default;

    // フィーバータイムコントローラー
    [SerializeField]
    FeverTimeController feverTimeController = default;

    // オブジェクトの発光エフェクト
    [SerializeField]
    GameObject objectLightEffect = default;

    // 落下時のLerpの割合
    [SerializeField]
    float fallingLerpRate = 0;

    // 落下時のLerpの割合
    [SerializeField]
    float feverTimeFallingLerpRate = 0;

    // 落下距離
    float fallDistance = 0;
    // 一番下のオブジェクト
    Transform bottomObject = null;
    // 一番下のオブジェクトの落下終了位置
    Vector3 bottomObjectFallEndPosition = Vector3.zero;
    // 落下中かどうか
    public bool IsFalling { get; private set; } = false;

    // 前フレームの一番下のオブジェクトの名前
    string prevBottomObjectName = null;

    /// <summary>
    /// 開始
    /// </summary>
    void OnEnable()
    {
        // オブジェクトのスポーン間隔の幅を落下移動距離として取得
        fallDistance = towerObjectSpawner.SpawnHeightInterval;
        // 地面への落下終了位置を算出する
        bottomObjectFallEndPosition = towerObjectSpawner.transform.position;
    }

    /// <summary>
    /// 更新
    /// </summary>
    void FixedUpdate()
    {
        // 一番下のオブジェクトを取得
        bottomObject = towerObjectSpawner.StackedObjects.FirstOrDefault();
        // 取得した要素がnullであれば、そのまま関数を抜ける
        if (bottomObject == null) { return; }

        if (prevBottomObjectName == null)
        {
            // 一番下のオブジェクトの名前を取得
            prevBottomObjectName = bottomObject.name;
        }

        // 既に落下中であれば、新たに落下処理は行わない
        if (!IsFalling)
        {
            // 現在と前フレームの一番下のオブジェクトを比較して、変更があれば落下処理を行う
            if (bottomObject.name != prevBottomObjectName)
            {
                // オブジェクトの発光をオフにする
                objectLightEffect.SetActive(false);
                // 落下フラグをオンにする
                IsFalling = true;
            }
        }

        // フラグがオンであれば落下処理を行う
        if (IsFalling)
        {
            // note : フィーバータイムとそうでないときで落下速度を変える

            // フィーバータイム中
            if (feverTimeController.IsFever)
            {
                Falling(feverTimeFallingLerpRate);
            }
            // 通常状態
            else
            {
                Falling(fallingLerpRate);
            }
        }

        // 現在の一番下のオブジェクトの名前を前フレームとして登録
        prevBottomObjectName = bottomObject.name;
    }

    /// <summary>
    /// 落下処理を行う
    /// </summary>
    /// <param name="lerpRate">落下時のlerpの割合</param>
    void Falling(float lerpRate)
    {
        foreach (Transform towerObject in towerObjectSpawner.StackedObjects)
        {
            // Lerpを利用してオブジェクトの落下移動を行う
            towerObject.position = new Vector3(towerObject.position.x, towerObject.position.y - (fallDistance * lerpRate), towerObject.position.z);
        }

        // 現在の位置と終了位置との距離を算出
        float currentPosToEndPosDist = bottomObject.position.y - bottomObjectFallEndPosition.y;

        // Lerpの移動量が0以下になったら落下終了とみなす
        if (currentPosToEndPosDist < 0)
        {
            // 落下したオブジェクトを正しい位置に調整する
            bottomObject.transform.position = bottomObjectFallEndPosition;
            // オブジェクトの発光をオンにする
            objectLightEffect.SetActive(true);
            // 落下フラグをオフにする
            IsFalling = false;
        }
    }

    /// <summary>
    /// 終了処理
    /// </summary>
    void OnDisable()
    {
        prevBottomObjectName = null;
        bottomObjectFallEndPosition = Vector3.zero;
        IsFalling = false;
        bottomObject = null;
    }
}
