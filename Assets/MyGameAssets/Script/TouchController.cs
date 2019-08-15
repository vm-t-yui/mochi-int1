using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// タッチクラス
/// </summary>
public class TouchController : MonoBehaviour
{
    // 入力の種類
    enum InputKind
    {
        Touch,      // タッチ
        Swipe,      // スワイプ
        Lenght      // enumの長さ
    }

    Touch touch = default;                                  // 入力のクラス

    [SerializeField]
    PlayerController player = default;                      // プレイヤークラス

    [SerializeField]
    Timer timer = default;                                  // タイマークラス

    [SerializeField]
    int maxJudgeCount = 5;                                  // ジャッジ処理の最大カウント

    int nowJudgeCount = 0;                                  // ジャッジ処理の経過カウント

    bool isInputJudge = false;                              // タッチかスワイプかを判断する開始フラグ
    bool[] isInput = new bool[(int)InputKind.Lenght];       // 判断中のどちらの入力が入ったかを入れるフラグ
    bool isTouch = false;                                   // タッチフラグ
    bool isSwipe = false;                                   // スワイプフラグ

    /// <summary>
    /// 更新処理
    /// </summary>
    void Update()
    {
        // 入力を受けたら入力情報を取得し、入力処理のジャッジに移行
        if (Input.touchCount == 1 && timer.IsStart && !isInputJudge)
        {
            touch = Input.GetTouch(0);

            // 入力直後ならジャッジに入る
            if (touch.phase == TouchPhase.Began)
            {
                // 入力処理のジャッジ開始
                StartCoroutine(InputJudge());
            }
        }
    }

    /// <summary>
    /// 入力がタッチなのかスワイプなのか判断
    /// </summary>
    IEnumerator InputJudge()
    {
        // ジャッジの経過時間が最大を超えるまで
        while (nowJudgeCount < maxJudgeCount)
        {
            // ジャッジの経過時間をカウント
            nowJudgeCount++;

            // タッチ(タッチの許可がされてあればタッチを取得)
            if (touch.phase == TouchPhase.Began)
            {
                isInput[(int)InputKind.Touch] = true;
            }
            // スワイプ(スワイプの許可がされてあればタッチを取得)
            if (touch.deltaPosition.magnitude > 2f)
            {
                isInput[(int)InputKind.Touch] = false;
                isInput[(int)InputKind.Swipe] = true;
            }
            if (touch.phase == TouchPhase.Ended)
            {
                nowJudgeCount = maxJudgeCount;
            }

            yield return new WaitForSeconds(0.001f);
        }

        // 超えたらフラグを出力
        if (isInput[(int)InputKind.Touch])
        {
            isTouch = true;
            isSwipe = false;
        }
        else
        {
            isTouch = false;
            isSwipe = true;
        }

        // 各フラグのリセット
        FlagReset();
    }

    /// <summary>
    /// 各フラグのリセット
    /// </summary>
    void FlagReset()
    {
        isInputJudge = false;
        isInput[(int)InputKind.Touch] = false;
        isInput[(int)InputKind.Swipe] = false;
        nowJudgeCount = 0;
    }

    /// <summary>
    /// タッチフラグのゲット＋リセット関数
    /// </summary>
    /// <returns>タッチフラグの値</returns>
    public bool GetIsTouch()
    {
        bool returnflg = isTouch;
        isTouch = false;

        return returnflg;
    }

    /// <summary>
    /// スワイプフラグのゲット＋リセット関数
    /// </summary>
    /// <returns>スワイプフラグの値</returns>
    public bool GetIsSwipe()
    {
        bool returnflg = isSwipe;
        isSwipe = false;

        return returnflg;
    }
}