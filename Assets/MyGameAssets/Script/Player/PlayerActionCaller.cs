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
    /// 開始処理
    /// </summary>
    void Awake()
    {
        TouchController.Inst.AddEvent((int)TouchController.Touch.Tap, OnPunch);
        TouchController.Inst.AddEvent((int)TouchController.Touch.Swipe, OnRescue);
    }

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
        if (timer.IsTimeup && !isEnd)
        {
            // 大技開始
            specialArts.Invoke();
            isEnd = true;
        }
    }

    /// <summary>
    /// 救助
    /// </summary>
    void OnRescue()
    {
        if (timer.IsStart && !timer.IsTimeup)
        {
            rescue.Invoke();
        }
    }

    /// <summary>
    /// パンチ
    /// </summary>
    void OnPunch()
    {
        if (timer.IsStart && !timer.IsTimeup)
        {
            punch.Invoke();
        }
    }
}