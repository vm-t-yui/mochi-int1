using UnityEngine;
using UnityEngine.Events;
using System.Linq;

/// <summary>
/// プレイヤーのアクションのコールクラス
/// </summary>
public class PlayerActionCaller : MonoBehaviour
{
    [SerializeField]
    Timer timer = default;                      // タイマークラス
    [SerializeField]
    TouchController touch = default;            // タッチクラス
    [SerializeField]
    TowerObjectSpawner towerObjectSpawner = default;    // タワーオブジェクト生成クラス

    [SerializeField]
    UnityEvent punch = new UnityEvent();        // パンチの関数リスト
    [SerializeField]
    UnityEvent rescue = new UnityEvent();       // 救助の関数リスト
    [SerializeField]
    UnityEvent specialArts = new UnityEvent();  // 大技の関数リスト

    bool isEnd = false;                         // 処理終了フラグ

    /// <summary>
    /// 起動処理
    /// </summary>
    void OnEnable()
    {
        // フラグリセット
        isEnd = false;
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    void Update()
    {
        // タイムアップになったら
        if (timer.IsTimeup)
        {
            if (!isEnd)
            {
                // 大技開始
                specialArts.Invoke();
                isEnd = true;
            }
        }
        // まだ時間が余っていたら
        else if(timer.IsStart)
        {
            // スワイプされたらうさぎ救助開始
            if (touch.GetIsSwipe() || Input.GetKeyDown(KeyCode.S))
            {
                rescue.Invoke();
            }
            // タッチされたらパンチ開始
            else if (touch.GetIsTouch() || Input.GetKeyDown(KeyCode.A))
            {
                punch.Invoke();
            }
        }
    }
}