using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// ゲージのオンオフを切り替える
/// </summary>
public class GaugeActivateSwitcher : MonoBehaviour
{
    // ウサギの耐えるゲージクラス
    [SerializeField]
    RabbitEndureGauge rabbitEndureGauge = default;

    // スポナークラス
    [SerializeField]
    TowerObjectSpawner towerObjectSpawner = default;

    // タイマークラス
    [SerializeField]
    Timer timer = default;

    // 前フレームの一番下のオブジェクトの名前
    string prevBottomObjectName = null;

    // ゲーム開始からの経過時間
    static public float GameProgressCount { get; private set; } = 0;

    /// <summary>
    /// 更新
    /// </summary>
    void Update()
    {
        // ゲームが開始されたらカウントを開始する
        GameProgressCount += Time.deltaTime;

        // 一番下のオブジェクトを取得
        Transform bottomObject = towerObjectSpawner.StackedObjects.FirstOrDefault();
        // 取得した要素がnullであれば、そのまま関数を抜ける
        if (bottomObject == null) { return; }

        // 現在と前フレームの一番下のオブジェクトを比較して、変更があれば落下処理を行う
        if (bottomObject.name != prevBottomObjectName)
        {
            // 一番下のオブジェクトがモチならば
            if (bottomObject.tag == TagName.Mochi)
            {
                // ゲージのスクリプトをオフにする
                rabbitEndureGauge.enabled = false;
            }
            // 一番下のオブジェクトがウサギならば
            else if (bottomObject.tag == TagName.Rabbit)
            {
                // ゲージのスクリプトをオンにする
                rabbitEndureGauge.enabled = true;
            }
        }

        // 現在の一番下のオブジェクトの名前を前フレームとして登録
        prevBottomObjectName = bottomObject.name;
    }
}
