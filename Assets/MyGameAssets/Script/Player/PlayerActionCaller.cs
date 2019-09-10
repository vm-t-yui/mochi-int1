using UnityEngine;
using UnityEngine.Events;
using VMUnityLib;

/// <summary>
/// プレイヤーのアクションのコールクラス
/// </summary>
public class PlayerActionCaller : MonoBehaviour
{
    [SerializeField]
    Timer timer = default;                              // タイマークラス
    [SerializeField]
    TowerObjectSpawner towerObjectSpawner = default;    // タワーオブジェクト生成クラス

    [SerializeField]
    UnityEvent punch = new UnityEvent();                // パンチの関数リスト
    [SerializeField]
    UnityEvent rescue = new UnityEvent();               // うさぎ救助の関数リスト
    [SerializeField]
    UnityEvent specialArts = new UnityEvent();          // 大技の関数リスト

    bool isEnd = false;                                 // 処理終了フラグ

    /// <summary>
    /// 起動処理
    /// </summary>
    void OnEnable()
    {
        // フラグリセット
        isEnd = false;

        // パンチ処理をタップイベントに追加
        TouchController.Inst.AddEvent((int)TouchController.Touch.Tap, OnPunch);
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

            // パンチ処理をタップイベントから削除して終了
            // NOTE:タイトルとリザルトはこのスクリプトが存在しないため追加したままだとエラーが出るのでその前に削除
            TouchController.Inst.RemoveEvent((int)TouchController.Touch.Tap, OnPunch);
            isEnd = true;
        }

        // うさぎ救助のできる状態なら救助開始
        // NOTE:TouchControllerにこの処理を書いてしまうと、こちらもタイトルとリザルトでエラーが出るので
        //      タイミングだけをTouchControllerで検知して、それを元にうさぎ救助処理を開始するようにしました。
        if (TouchController.Inst.StartRescue())
        {
            OnRescue();
        }
    }

    /// <summary>
    /// うさぎ救助
    /// </summary>
    public void OnRescue()
    {
        if (timer.IsStart && !timer.IsTimeup)
        {
            rescue.Invoke();
        }
    }

    /// <summary>
    /// パンチ
    /// </summary>
    public void OnPunch()
    {
        if (timer.IsStart && !timer.IsTimeup)
        {
            punch.Invoke();
        }
    }
}