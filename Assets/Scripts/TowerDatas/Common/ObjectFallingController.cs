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

    // 落下時のLerpの割合
    [SerializeField]
    float fallingLerpRate = 0;

    // 前フレームのオブジェクトの数
    int prevStackedObjectNum = 0;

    // 落下距離
    float fallDistance = 0;
    // 一番上のオブジェクトの落下終了位置
    Vector3 topObjectFallEndPosition = Vector3.zero;
    // 落下中かどうか
    public bool IsFalling { get; private set; } = false;

    // 前フレームの一番下のオブジェクトの名前
    string prevBottomObjectName = null;

    /// <summary>
    /// 更新
    /// </summary>
    void FixedUpdate()
    {
        // 一番下のオブジェクトを取得
        Transform bottomObject = towerObjectSpawner.StackedObjects.FirstOrDefault();
        // 取得した要素がnullであれば、そのまま関数を抜ける
        if (bottomObject == null) { return; }

        // 現在と前フレームの一番下のオブジェクトを比較して、変更があれば落下処理を行う
        if (bottomObject.name != prevBottomObjectName)
        {
            // 一番上のオブジェクトを取得
            Transform topObject = towerObjectSpawner.StackedObjects.Last();

            // オブジェクトのスポーン間隔の幅を落下移動距離として取得
            fallDistance = towerObjectSpawner.SpawnHeightInterval;

            // 親オブジェクトの移動先の位置を算出
            topObjectFallEndPosition = new Vector3(topObject.position.x,
                                                   topObject.position.y - fallDistance,
                                                   topObject.position.z);

            // 落下フラグをオンにする
            IsFalling = true;
        }

        // フラグがオンであれば落下処理を行う
        if (IsFalling) { Falling(); }

        // 現在の一番下のオブジェクトの名前を前フレームとして登録
        prevBottomObjectName = bottomObject.name;
    }

    /// <summary>
    /// 落下処理を行う
    /// </summary>
    void Falling()
    {
        foreach (Transform towerObject in towerObjectSpawner.StackedObjects)
        {
            // Lerpを利用してオブジェクトの落下移動を行う
            towerObject.position = new Vector3(0, towerObject.position.y - (fallDistance / fallingLerpRate), 0);
        }

        // 一番上のオブジェクトを取得
        Transform topObject = towerObjectSpawner.StackedObjects.Last();
        // 現在の位置と終了位置との距離を算出
        float currentPosToEndPosDist = Vector3.Magnitude(topObjectFallEndPosition - topObject.position);

        // Lerpの移動量が0.01以下になったら落下終了とみなす
        if (currentPosToEndPosDist <= 0.01f)
        {
            IsFalling = false;
        }
    }
}
