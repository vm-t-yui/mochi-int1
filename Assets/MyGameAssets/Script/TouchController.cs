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
    UnityEvent onTap = new UnityEvent();            // タップの関数リスト
    [SerializeField]
    UnityEvent onDraggingStart = new UnityEvent();  // ドラック開始の関数リスト
    [SerializeField]
    UnityEvent onDragging = new UnityEvent();       // ドラック中の関数リスト
    [SerializeField]
    UnityEvent onDraggingEnd = new UnityEvent();    // ドラック後の関数リスト

    int dragFrame = 0;                              // ドラック処理のフレーム数
    bool isTap = false;                             // タップの制御フラグ
    bool isRescue = false;                          // うさぎ救助の制御フラグ

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
        if (Input.touchCount == 0 && !Input.GetMouseButton(0))
        {
            Init();
        }
    }

    /// <summary>
    /// タップ開始
    /// </summary>
    void OnTap(Tap tap)
    {
        Debug.Log("タップ開始");

        if (!isTap)
        {
            onTap.Invoke();

            // 連続で呼ばれないようにフラグ制御
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

        // ドラックのフレーム数をカウント
        dragFrame++;
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
    /// うさぎ救助のタイミング検知
    /// </summary>
    /// HACK:複数の指で連続タップするとどうしてもドラック処理に1フレームだけ入り込んでしまう時があり、
    ///      2フレーム以上ドラックをし続けていればうさぎ救助を始めるようにしたら、一応動くようにはなりました。
    ///      ただ場合によっては2フレーム以上になる可能性も０ではないし、コード的にも汚いので今後修正していく予定です。
    public bool StartRescue()
    {
        // ドラックが２フレーム続いているならうさぎ救助開始
        if (dragFrame >= 2 && !isRescue)
        {
            // 連続で呼ばれないようにフラグ制御
            isRescue = true;

            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// 各値初期化
    /// </summary>
    void Init()
    {
        dragFrame = 0;
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
            case (int)Touch.Tap: onTap.AddListener(action); break;
            case (int)Touch.DraggingStart: onDraggingStart.AddListener(action); break;
            case (int)Touch.Dragging: onDragging.AddListener(action); break;
            case (int)Touch.DraggingEnd: onDraggingEnd.AddListener(action); break;
        }
    }

    /// <summary>
    /// UnityEvent追加
    /// </summary>
    /// <param name="num">追加するUnityEventの種類</param>
    /// <param name="action">追加する関数</param>
    public void RemoveEvent(int num, UnityAction action)
    {
        switch (num)
        {
            case (int)Touch.Tap: onTap.RemoveListener(action); break;
            case (int)Touch.DraggingStart: onDraggingStart.RemoveListener(action); break;
            case (int)Touch.Dragging: onDragging.RemoveListener(action); break;
            case (int)Touch.DraggingEnd: onDraggingEnd.RemoveListener(action); break;
        }
    }
}
