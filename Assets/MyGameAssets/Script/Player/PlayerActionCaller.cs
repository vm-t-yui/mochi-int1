using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// プレイヤーのアクションのコールクラス
/// </summary>
public class PlayerActionCaller : MonoBehaviour
{
    [SerializeField]
    Timer timer = default;                      // タイマークラス

    [SerializeField]
    TouchController touch = default;            // タッチクラス

    bool isEnd = false;                         // 処理終了フラグ

    [SerializeField]
    UnityEvent punch = new UnityEvent();        // パンチの関数リスト
    [SerializeField]
    UnityEvent rescue = new UnityEvent();       // 救助の関数リスト
    [SerializeField]
    UnityEvent specialArts = new UnityEvent();  // 大技の関数リスト

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
        // まだ時間が余っていたら
        else
        {
            // スワイプされたら
            if (touch.GetIsSwipe())
            {
                // うさぎ救助開始
                rescue.Invoke();
            }
            // タッチされたら
            else if (touch.GetIsTouch())
            {
                // パンチ開始
                punch.Invoke();
            }
        }
    }
}