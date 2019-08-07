using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// タワーオブジェクトの落下制御を行う
/// </summary>
public class TowerObjectFallController : MonoBehaviour
{
    // タワーオブジェクトのスポーンクラス
    [SerializeField]
    TowerObjectSpawner towerObjectSpawner = default;

    // 積みあがっているオブジェクトの親
    [SerializeField]
    Transform stackedObjectParent = default;

    // 落下時のLerpの割合
    [SerializeField]
    float fallingLerpRate = 0;

    // 前フレームのオブジェクトの数
    int prevStackedObjectNum = 0;

    // 落下距離
    float fallDistance = 0;
    // 落下終了位置
    Vector3 fallEndPosition = Vector3.zero;
    // 落下中かどうか
    bool isFalling = false;

    /// <summary>
    /// 更新
    /// </summary>
    void Update()
    {
        // 前フレームのオブジェクト数と比較して、タワーオブジェクトの数が変動したかチェック
        if (stackedObjectParent.childCount != prevStackedObjectNum)
        {
            // オブジェクトのスポーン間隔の幅を落下移動距離として取得
            fallDistance = towerObjectSpawner.SpawnHeightInterval;

            // 親オブジェクトの移動先の位置を算出
            fallEndPosition = new Vector3(stackedObjectParent.position.x,
                                          stackedObjectParent.position.y - fallDistance,
                                          stackedObjectParent.position.z);

            // 落下フラグをオンにする
            isFalling = true;
        }

        // フラグがオンであれば落下処理を行う
        if (isFalling) { Falling(); }

        // 前フレームのオブジェクト数として登録
        prevStackedObjectNum = stackedObjectParent.childCount;
    }

    /// <summary>
    /// 落下処理を行う
    /// </summary>
    void Falling()
    {
        // Lerpを利用してオブジェクトの落下移動を行う
        stackedObjectParent.position = Vector3.Lerp(stackedObjectParent.position, fallEndPosition, fallingLerpRate);

        // 現在の位置と終了位置との距離を算出
        float currentPosToEndPosDist = Vector3.Magnitude(fallEndPosition - stackedObjectParent.position);

        // Lerpの移動量が0.01以下になったら落下終了とみなす
        if (currentPosToEndPosDist <= 0.01f)
        {
            isFalling = false;
        }
    }
}
