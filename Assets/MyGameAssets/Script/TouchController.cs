using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using VMUnityLib;

/// <summary>
/// 画面入力クラス
/// </summary>
public class TouchController : SingletonMonoBehaviour<TouchController>
{
    /// <summary>
    /// 入力の種類
    /// </summary>
    public enum Touch
    {
        Tap,            // タップ
        DraggingStart,  // ドラック開始
        Dragging,       // ドラック中
        DraggingEnd,    // ドラック後
    }

    [SerializeField]
    TouchEffectPlayer touchEffect = default;        // タッチエフェクト
    [SerializeField]
    UnityEvent onTap = new UnityEvent();            // タップの関数リスト
    [SerializeField]
    UnityEvent onDraggingStart = new UnityEvent();  // ドラック開始の関数リスト
    [SerializeField]
    UnityEvent onDragging = new UnityEvent();       // ドラック中の関数リスト
    [SerializeField]
    UnityEvent onDraggingEnd = new UnityEvent();    // ドラック後の関数リスト

    int dragNum = 0;
    bool isTap = false;
    bool isRescue = false;

    /// <summary>
    /// 起動処理
    /// </summary>
    void OnEnable()
    {
        // Input.Touchesの初期化
        IT_Gesture.onMultiTapE += OnTap;
        IT_Gesture.onDraggingStartE += OnDraggingStart;
        IT_Gesture.onDraggingE += OnDragging;
        IT_Gesture.onDraggingEndE += OnDraggingEnd;

        AddEvent((int)Touch.Tap, PlayerActionCaller.Inst.OnPunch);
    }

    /// <summary>
    /// 停止処理
    /// </summary>
    void OnDisable()
    {
        // Input.Touchesの後処理
        IT_Gesture.onMultiTapE -= OnTap;
        IT_Gesture.onDraggingStartE -= OnDraggingStart;
        IT_Gesture.onDraggingE -= OnDragging;
        IT_Gesture.onDraggingEndE -= OnDraggingEnd;
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    void Update()
    {
        // 何も入力されていなかったら初期化
        if (Input.touchCount == 0)
        {
            Init();
        }
    }

    /// <summary>
    /// タップ開始
    /// </summary>
    void OnTap(Tap tap)
    {
        // NOTE:２本の指で同時にタップすると2回呼ばれてしまうので１回になるよう、isTapで制御
        if (!isTap)
        {
            Debug.Log("タップ開始");
            onTap.Invoke();

            isTap = true;
        }
    }

    /// <summary>
    /// ドラック開始
    /// </summary>
    void OnDraggingStart(DragInfo dragInfo)
    {
        Debug.Log("長押し開始");
        onDraggingStart.Invoke();
    }

    /// <summary>
    /// ドラック中
    /// </summary>
    void OnDragging(DragInfo dragInfo)
    {
        Debug.Log("長押し中");
        onDragging.Invoke();

        // うさぎ救助
        OnRescue();
    }

    /// <summary>
    /// ドラック終了
    /// </summary>
    void OnDraggingEnd(DragInfo dragInfo)
    {
        Debug.Log("長押し終了");
        onDraggingEnd.Invoke();
    }

    /// <summary>
    /// うさぎ救助
    /// </summary>
    /// HACK:複数の指で連続タップするとどうしてもドラック処理に1フレームだけ入り込んでしまう時があり、
    ///      2フレーム以上ドラックをし続けていればうさぎ救助を始めるようにしたら、一応動くようにはなりました。
    ///      ただ場合によっては2フレーム以上になる可能性も０ではないし、コード的にも汚いので今後修正していく予定です。
    void OnRescue()
    {
        dragNum++;

        if (dragNum >= 2 && !isRescue)
        {
           PlayerActionCaller.Inst.OnRescue();
           isRescue = true;
        }
    }

    /// <summary>
    /// 初期化
    /// </summary>
    void Init()
    {
        dragNum = 0;
        isRescue = false;
        isTap = false;
    }

    /// <summary>
    /// UnityEvent追加
    /// </summary>
    /// <param name="num">追加するUnityEventの種類</param>
    /// <param name="action">追加する関数</param>
    public void AddEvent(int num, UnityAction action) 
    {
        switch(num)
        {
            case (int)Touch.Tap: onTap.AddListener(() => action()); break;
            case (int)Touch.DraggingStart: onDraggingStart.AddListener(() => action()); break;
            case (int)Touch.Dragging: onDragging.AddListener(() => action()); break;
            case (int)Touch.DraggingEnd: onDraggingEnd.AddListener(() => action()); break;
        }
    }
}
